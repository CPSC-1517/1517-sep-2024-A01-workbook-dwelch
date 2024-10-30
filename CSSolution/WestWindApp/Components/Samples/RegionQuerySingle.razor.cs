using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;

using WestWindSystem.BLL;
using WestWindSystem.Entities;

namespace WestWindApp.Components.Samples
{
    public partial class RegionQuerySingle
    {
        public string feedback = "";
        public List<string> errormsgs = new List<string>();
        public int regionidarg = 0;
        public int regionselectarg = 0;

        [Inject]
        public RegionServices _regionServices { get; set; }
        public List<Region> regionList = null;
        public Region datainfo = null;

        protected override void OnInitialized()
        {
            //consume a service
            //what is need:
            //  a) inject the services
            //  b) variables to hold the retrun values of the service call
            //  c) the appropriate using statements

            regionList = _regionServices.Region_GetAll();
            base.OnInitialized();
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

        //button event
        public void GetByID()
        {
            //clear out any old messages
            feedback = "";
            errormsgs.Clear();
            datainfo = null; //remember to clear any old results

            //validate incoming values
            if (regionidarg <= 0)
            {
                //pkeys can not be negative or 0 on the database
                errormsgs.Add("Region id must be a number greater than 0.");
            }
            else
            {
                //consume a service
                datainfo = _regionServices.Region_GetByID(regionidarg);
            }
        }

        //button event
        public void GetBySelection()
        {
            //clear out any old messages
            feedback = "";
            errormsgs.Clear();
            datainfo = null; //remember to clear any old results

            //validate incoming values
            if (regionselectarg == 0)
            {
                //pkeys can not be negative or 0 on the database
                errormsgs.Add("You must select the region for your query.");
            }
            else
            {
                //consume a service
                datainfo = _regionServices.Region_GetByID(regionselectarg);
            }
        }
    }
}
