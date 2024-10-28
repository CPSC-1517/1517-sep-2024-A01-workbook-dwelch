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
    public class BuildVersionServices
    {
        #region setup of the context connection variable and class constructor
        //any method(aka service) will probably need access to the database
        //this will be done via the context class (WestWindContext)
        //when this service class is instantiated there will be a reference call
        //  by the extension class registration method which call supply the
        //  registered content connection
        private readonly WestWindContext _context;

        internal BuildVersionServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        /************************ Services *******************************/
        //a service is a method
        //this class will be referenced by external users (aka app project)
        //therefore the class and services need to be public

        //create a service
        public BuildVersion BuildVersion_Get()
        {
            //this will use the context property BuildVersions to obtain the data
            //  from the database via the context class
            //the call will return the dataset (DbSet) from the sql table
            //data returned by the query in this fashion is returned as a set with a datatype of
            //  IEnumerable<T>
            //the will create a dataset of 0, 1 or more records, one for each record on the
            //  database table

            //go get my data
            IEnumerable<BuildVersion> info = _context.BuildVersions;

            //return one row from the data within info
            //Linq has a method that limits the number of rows from the dataset collection
            // called .FirstOrDefault()
            //this method will return the first record in the dataset collection
            //if the collection is empty, it will return the default of the datatyep
            // (in this case, it is an instance of a class, thus the default is null)
            return info.FirstOrDefault();
        }
    }
}
