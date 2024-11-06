
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using WestWindSystem.DAL;
using WestWindSystem.Entities;
using Microsoft.EntityFrameworkCore; //this is required for the .Include on the query
#endregion


namespace WestWindSystem.BLL
{
    public class ShipmentServices
    {
        #region setup of the context connection variable and class constructor

        private readonly WestWindContext _context;

        internal ShipmentServices(WestWindContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Services
        // Used to navigate on the ShipmentQuery page to the Shipper talble
        //  Technique A
        //public List<Shipment> Shipment_GetByYearAndMonth(int year, int month)
        //{
        //    // for the query:
        //    //      the context instance
        //    //      the DbSet() property name
        //    //      the filter condition (.Where)
        //    //      optionally order by (.OrderBy)
        //    IEnumerable<Shipment> info = _context.Shipments
        //                                        .Where(x => x.ShippedDate.Year == year
        //                                                 && x.ShippedDate.Month == month)
        //                                        .OrderBy(x => x.ShippedDate);
        //    return info.ToList();
        //}

        //This uses the technique (b) discussed on the ShipmentTable page
        //note there is a required using class, see Additional namespaces above.
        //uses the .Include method to add navigational instances to the return record
        //note the predicate uses the virtual navigational property of the Shipment entity
        //This will include the associated record from the Shippers table (parent) for the shipment record (child)
        public List<Shipment> Shipment_GetByYearAndMonth(int year, int month)
        {
            // for the query:
            //      the context instance
            //      the DbSet() property name
            //      the filter condition (.Where)
            //      optionally order by (.OrderBy)
            IEnumerable<Shipment> info = _context.Shipments
                                                .Include(x => x.ShipViaNavigation)
                                                .Where(x => x.ShippedDate.Year == year
                                                         && x.ShippedDate.Month == month)
                                                .OrderBy(x => x.ShippedDate);
            return info.ToList();
        }
        #endregion
    }
}
