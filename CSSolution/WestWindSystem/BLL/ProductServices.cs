using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
#endregion

namespace WestWindSystem.BLL
{
    public class ProductServices
    {
        #region setup of the context connection variable and class constructor

        private readonly WestWindContext _context;

        internal ProductServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Queries
        public List<Product> Product_GetByCategoryID(int categoryid)
        {
            IEnumerable<Product> info = _context.Products
                                                .Where(p => p.CategoryID == categoryid)
                                                .OrderBy(p => p.ProductName);
            return info.ToList();
        }

        public Product Product_GetByID(int productid)
        {
            //the .FirstOrDefault indicates that only a single record will be return at most
            //  by the query.
            //if the .FirstOrDefault is NOT on the query, it will return a collection of 0, 1 or more record
            //  AND therefore would need to use IEnumerable<T>
            Product info = _context.Products
                                   .Where(p => p.ProductID == productid)
                                   .FirstOrDefault();

            //alterive queries
            //the predicate can go directly into FirstOrDefault
            //Product info = _context.Products
            //                      .FirstOrDefault(p => p.ProductID == productid);

            //the .Find will look for a record having a key value of the parameter
            //Product info = _context.Products
            //                      .Find(productid);

            return info;
        }
        #endregion

        #region Maintainance Service: Add, Update and Delete

        //Adding a record to your database MAY require you to verify that the data does not already exist within the DB
        //verify that the incoming data is valid (do not trust the front end to do this work
        //you PK may not be created by the DB, it is supplied by the user
        //does the supplied PK already exist on the DB?
        //if so do not add record
        //these actions are referred to as business logic (business rules), hence the BLL
        //comment checcks
        //  was data actually sent to the services
        //  primary key tests (dependent on PK being IDENTITY or not)

        //custom checks

        //these are specific checks for your data beyond the primitive checks that may have been done on the form (presence, range, datatype)
        //Example of a custom check

        //  in this demo we will not allow a product
        //  from the same supplier
        //  with the same name 
        //  for the same unit size

        //if the situation occus them the data is not valid and an exception should occur

        public int Product_Add(Product item)
        {
            //was data actually passed for processing
            if (item == null)
            {
                throw new ArgumentNullException("You must supply the product information");
            }

            //example of a custom business rule
            bool exists = false;

            //.Any(predicate)
            // returns a true or false (not the data) depending on the success of the
            //      predicate on the collection existing
            //this is different then the .Where(predicate) that returns actual rows of data
            exists = _context.Products
                            .Any(p => p.SupplierID == item.SupplierID
                                   && p.ProductName.Equals(item.ProductName)
                                   && p.QuantityPerUnit.Equals(item.QuantityPerUnit));
            if(exists)
            {
                throw new ArgumentException($"Product {item.ProductName} from" +
                    $" {item.Supplier.CompanyName} of size {item.QuantityPerUnit} already " +
                    $" exists on file.");
            }

            //after all business rules have been passed, you can assume that the 
            //  data is good to place on the database

            //there is a two step processs to complete the process against the database
            // a) staging
            // B) commit to database

            //Staging (EntityFramework for our database processing)
            //  staging is the act of placing your data into local memory
            //      for the eventual processing on the database
            // a) DbSet: Products
            // b) know the action: Add
            // c) indicate the data involved: item

            //IMPORTANT: the data is in LOCAL MEMORY
            //           the data is NOT!!!! yet been sent to the database
            //THEREFORE: at this time, there is NO!!! IDENITY primary key value
            //              on this instance (except for the default of the datatype)
            //UNLESS: you have placed a valued in the NON_IDENTITY key fields

            _context.Products.Add(item);

            //Commit
            //  this sends ALL the local Staged data to the database for processing

            //ANY validation in the entity model defintion will be applied at this point
            //    prior ato the data being sent to the database
            //the validation annotation that exists in your entity model defintions
            //      were place there during your reversing engineering
            //IF any of that validation does not pass, your data does not go to the database
            //      and an exception is thrown

            _context.SaveChanges();

            //After the successful commit to the databases, your new product id
            //      will be inserted into your local instance of the data
            //NOW you have access to your NEW primary key
            //optionally: you could return the value to your front end
            return item.ProductID;


        }

        public int Product_Update(Product item)
        {
            //was data actually passed for processing
            if (item == null)
            {
                throw new ArgumentNullException("You must supply the product information");
            }

            //example of a custom business rule
            bool exists = false;

            //does the product still exist on the database
            //the product could have been physically deleted while
            //  the user was doing some processing with the record in question

            exists = _context.Products
                             .Any(p => p.ProductID == item.ProductID);
            if (!exists)
            {
                throw new ArgumentException($"Product {item.ProductName} from" +
                    $" {item.Supplier.CompanyName} of size {item.QuantityPerUnit} does " +
                    $" not exists onfile.");
            }

            //.Any(predicate)
            // returns a true or false (not the data) depending on the success of the
            //      predicate on the collection existing
            //this is different then the .Where(predicate) that returns actual rows of data
            //on the update one can change to record with the same supplier,
            //  same name and same qty per unit with the same productid
            //  HOWEVER, one need to check all other products to see if they already
            //  have the same supplier,name and qty per unit
            //  THERFORE the 4th condition which checks other products
            exists = _context.Products
                            .Any(p => p.SupplierID == item.SupplierID
                                   && p.ProductName.Equals(item.ProductName)
                                   && p.QuantityPerUnit.Equals(item.QuantityPerUnit)
                                   && p.ProductID != item.ProductID);
            if (exists)
            {
                throw new ArgumentException($"Product {item.ProductName} from" +
                    $" {item.Supplier.CompanyName} of size {item.QuantityPerUnit} already " +
                    $" exists on a different product.");
            }

            //SPECIAL!!!!!!! change productid so no record is affected
            //comment out after your test
            //item.ProductID = 9999;

            //Staging
            EntityEntry<Product> updating = _context.Entry(item);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //Commit
            //After the successful commit to the database
            //the resulting valid from the database is the "number of rows affected"
            int rowsaffected = _context.SaveChanges();

            //SPECIAL!!!! return a 0 rows affected to generate event message
            //comment out after your test
            //rowsaffected = 0;

            return rowsaffected;



        }

        //Delete: cruD
        //there are two types of deletes: physical and logical
        //Whether you have a physical or logical delete is determind WHEN
        //  the system is designed (database, data requirements)

        //Logical delete
        //this happens when the recorde is deemed "unwanted" BUT CANNOT be 
        //  physically removed from the database because the records has
        //  a relationship to another records (parent/child) and the associated record
        //  CANNOT be removed

        //Example: The product record is a parent to ManitfestItems records
        //         The the manitest record is need for tracking, it does to the receiver of the product
        //so, because the other record(s) are required for the busines
        //      one CANNOT physically remove the ("parent") product record.

        //usually in this situation, the parent record (product) will have some type of field
        //  that will indicate "deleted"
        //on the product record such a field is the Discontinued field

        //Qustion: If the record will not be deleted, what happens?
        //Answer: here, you will actually do an update
        //Within the method, it is a good practice NOT to rely on the user to set
        //  the "logical delete" field to the delete status
        //Your method should set the value

        public int Product_LogicalDelete(Product item)
        {
            //was data actually passed for processing
            if (item == null)
            {
                throw new ArgumentNullException("You must supply the product information");
            }

            //example of a custom business rule
            Product exists = null;

            //does the product still exist on the database
            //the product could have been physically deleted while
            //  the user was doing some processing with the record in question

            //even though this is an update, one technique is to use the exsiting
            //  data already on the database
            //the only value that needs to be altered is the Discontinue flag
            //if other was to be altered, then the user should first do the update
            //  then do the discontinue

            //remember, FirstOrDefault will either
            //  a) return the requested record if found
            //  b) return a null
            exists = _context.Products
                             .FirstOrDefault(p => p.ProductID == item.ProductID);
            
            if (exists == null)
            {
                throw new ArgumentException($"Product {item.ProductName} from" +
                    $" {item.Supplier.CompanyName} of size {item.QuantityPerUnit} does " +
                    $" not exists onfile.");
            }

            //for the logical delete
            //  set the appropriate field to the value indicating "delete"
            //this code is not relying on the user to have set the apropriate
            //  field on the form
            exists.Discontinued = true;

            //Staging
            EntityEntry<Product> updating = _context.Entry(exists);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //Commit
            //After the successful commit to the database
            //the resulting valid from the database is the "number of rows affected"
            int rowsaffected = _context.SaveChanges();

            //SPECIAL!!!! return a 0 rows affected to generate event message
            //comment out after your test
            //rowsaffected = 0;

            return rowsaffected;



        }

        //Physical Delete
        //you physically remove the record from the database
        //IF there are no "child" records to prevent the record removal, you can remove the record
        //IF there are "children" AND the "children" are not required, you can remove the record
        //      HOWEVER, you will need to first remove any "children" before removing the parent record
        //      assuming there is no cascade delete setup on the database

        public int Product_PhysicalDelete(Product item)
        {
            //was data actually passed for processing
            if (item == null)
            {
                throw new ArgumentNullException("You must supply the product information");
            }

            //example of a custom business rule
            bool exists = false;

            //does the product still exist on the database
            //the product could have been physically deleted while
            //  the user was doing some processing with the record in question

            
            exists = _context.Products
                             .Any(p => p.ProductID == item.ProductID);

            if (!exists)
            {
                throw new ArgumentException($"Product {item.ProductName} from" +
                    $" {item.Supplier.CompanyName} of size {item.QuantityPerUnit} does " +
                    $" not exists onfile.");
            }

            //this delete assumes that there is no appropriate field on the 
            //  record to indicate a logical "delete" and thus: a physical
            //  delete will occur

            //HOWEVER!! this record could be a parent to one or more "child" records
            //One should ensure that there is no existing child record for the
            //  parent BEFORE attempting the delete


            //using the viraul navigational properties, one could check to see
            //  if any child records (collection) exists for the parent
            //if there is a cascade delete setup on your dataset and is allowed
            //  then these checks are unnecessary
            exists = _context.Products
                            .Any(p => p.ManifestItems.Count > 0);

            if (exists)
            {
                throw new ArgumentException($"Product {item.ProductName} from" +
                    $" {item.Supplier.CompanyName} of size {item.QuantityPerUnit} has " +
                    $" associated manifest records on file. Unable to remove.");
            }

            exists = _context.Products
                           .Any(p => p.OrderDetails.Count > 0);

            if (exists)
            {
                throw new ArgumentException($"Product {item.ProductName} from" +
                    $" {item.Supplier.CompanyName} of size {item.QuantityPerUnit} has " +
                    $" associated order detail records on file. Unable to remove.");
            }

            //Staging
            EntityEntry<Product> deleting = _context.Entry(item);
            deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            //Commit
            //After the successful commit to the database
            //the resulting valid from the database is the "number of rows affected"
            int rowsaffected = _context.SaveChanges();

            //SPECIAL!!!! return a 0 rows affected to generate event message
            //comment out after your test
            //rowsaffected = 0;

            return rowsaffected;



        }

        #endregion

    }
}
