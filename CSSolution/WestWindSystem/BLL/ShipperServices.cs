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
    public class ShipperServices
    {
        #region setup of the context connection variable and class constructor

        private readonly WestWindContext _context;

        internal ShipperServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Services
        public List<Shipper> Shipper_GetAll()
        {
            // for the query:
            //      the context instance
            //      the DbSet() property name
            //      optionally the filter condition (.Where)
            //      optionally order by (.OrderBy)
            IEnumerable<Shipper> info = _context.Shippers
                                                
                                                .OrderBy(x => x.CompanyName);
            return info.ToList();
        }
        #endregion

    }
}
