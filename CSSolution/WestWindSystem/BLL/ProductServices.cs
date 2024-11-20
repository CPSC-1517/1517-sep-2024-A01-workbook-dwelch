﻿using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

        #endregion

    }
}
