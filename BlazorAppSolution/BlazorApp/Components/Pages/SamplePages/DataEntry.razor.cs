using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OOPsReview;

namespace BlazorApp.Components.Pages.SamplePages
{
    //we need to make this a partial class of DataEntry
    //even though this is in a separate physical file it will
    //  be treated as one complete class with the form file
    public partial class DataEntry
    {
        private string feedback = "";
        private Dictionary<string, string> errormsgs = new();
        //could be using a List<string> as well as this Dictionary
        //private List<string> errormsgs = new ();

        private string employmentTitle = "";
        private DateTime StartDate;
        private double empYears = 0;
        private SupervisoryLevel empLevel;

        //injected services into your application
        //the declaration needes to be coded as a property, typically an auto-implemented property
        //WARNING: you may get a exception on the declaration (red line), if so place a using
        //  clause at the top of this file (using Microsoft.JSInterop;)
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        protected override void OnInitialized()
        {
            StartDate = DateTime.Today;
            base.OnInitialized();
        }

        private Exception GetInnerException(Exception ex)
        {
            //drill down into your Exception until there are no more inner exceptions
            //at this point you have the "real" error
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

        private void OnCollect()
        {
            feedback = "inside collect";
        }

        //this method is being done as an async task as it has to wait for the user
        //  to respond
        //this async task will need a service called JSRunTime
        //you will need to inject a service into my code
        private async Task OnClear()
        {
            feedback = "";

            //issue a prompt dialogue to the user to obtain confirmation of the action
            //create your message for the dialogue box into a generic object
            object[] messageline = new object[] {"Claering will lose all unsaved data." +
                " Are you sure you want to clear the form?" };
            if (await JSRuntime.InvokeAsync<bool>("confirm", messageline))
            {
                employmentTitle = "";
                StartDate = DateTime.Today;
                empYears = 0;
                empLevel = SupervisoryLevel.Entry;
            }
        }
    }
}
