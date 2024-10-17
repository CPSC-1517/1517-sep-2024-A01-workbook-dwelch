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
        private int errorCounter = 1;
        private Employment employment = null;

        //injected services into your application
        //the declaration needes to be coded as a property, typically an auto-implemented property
        //WARNING: you may get a exception on the declaration (red line), if so place a using
        //  clause at the top of this file (using Microsoft.JSInterop;)
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        //this variable is required if you are using property injection
        //  for services available from the system which are not normally included
        //  for use to your application
        [Inject]
        public IWebHostEnvironment webHostEnvironment { get; set; } //auto implemented property

        //this variable is for the Navigational services
        [Inject]
        private NavigationManager navigationManager { get; set; }

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
            feedback = ""; //clear out any old messages
            errormsgs.Clear(); //clear out all existing keys and their values from the Dictionary

            //primitive validation
            //  presence
            //  datatype/pattern
            //  range of values

            //Business Rules (aka your validation requirements)
            //title must be presence, must have at least one character
            //start date cannot be in the future
            //years cannot be less than zero

            if (string.IsNullOrWhiteSpace(employmentTitle))
            {
                //if there is a violation of the rule
                //we wish to collect the error and display to the user
                //we are using a Dictionary in this example that has two components
                //  a) a unquie value that is treated as a key
                //  b) a string which represents the value associated with the key
                errormsgs.Add("Title","Title is required");
            }

            if (StartDate >= DateTime.Today.AddDays(1))
            {
                errormsgs.Add("StartDate", "Start Date is in the future. Must be today or in the past.");
            }

            if(empYears < 0)
            {
                errormsgs.Add("Years", "Years must be 0 or greater (partial years are allowed eg 3.6)");
            }

            if (errormsgs.Count == 0)
            {
                //at this point in the collection, the data is "deemed" acceptable
                //at this point your can continue the processing of your data

                //if you are using a class to collect and hold your data
                //      you need to be concerned with how the class coding handles
                //      any errors in creating or processing code within the class
                //this normally means some type of try/catch processing
                try
                {
                    feedback = $"Entered data is {employmentTitle},{StartDate},{empYears},{empLevel}";
                    //create an instance of Employment to use to write to the data file
                    //the data file record will use the ToString() of Employment
                    //(Note: you could manually in this method create the concatenate string for
                    //      writing)
                    //Remember if an error occurs while creating your instance, the instance will
                    //  throw an exception; which should be caught by the catches below.
                    employment = new Employment(employmentTitle, empLevel, StartDate, empYears);

                    //write the class data as a string to a csv text file
                    // required:
                    // a) know the location of the file (name)
                    // b) the technique to use in writing to the file
                    //    there are several ways to write to the file
                    //      a) StreamWriter/StreamReader
                    //      b) using the System.IO.File methods
                    //           (handles the streaming of the data)

                    //WARNING: when you use the System.IO.File you MUST use the
                    //          fully qualified naming to the class method you wish
                    //          to use.
                    //         you can not get by just using a reference to the
                    //           namespace at the top of your code (using System.IO;)


                    //there are a couple of ways to refer to your file and its path
                    //  i) obtain the root path of your application using an injection
                    //      service called IWebHostEnvironment via property injection (see variables)
                    //  ii) use relative addressing starting a the top of your application

                    //in this example we will demonstrate property injection
                    //the method that will be use will return the path from the
                    //  top of your drive to the top of your application
                    //append the remainder part of the file path to this result (via concentation)

                    //WARNING: the folder slash for your path can be both a forward or back slash
                    //              on a PC BUT for an Apple machine, you must use the forward slash (/)

                    string appPathName = webHostEnvironment.ContentRootPath;
                    string csvFilePathName = $@"{appPathName}/Data/Employments.csv";

                    //write the data from the employment instance (ToString) as a line to the csv file
                    //since the string does not contain a line feed character, we will need to concatenate
                    //  the character (\n)
                    //the System.IO.File method will be AppendAllText(string)
                    // AppendAllText will
                    //   a) create the file if it does not exist
                    //   b) opens
                    //   c) writes the text
                    //   d) closes
                    string line = $"{employment.ToString()}\n";
                    System.IO.File.AppendAllText(csvFilePathName,line);


                }
                catch (ArgumentNullException ex)
                {
                    //since we are using a Dictionary, the key MUST be unique.
                    //to ensure that all Missing Data errors would appear in the Dictionary
                    //  without the program abort, one needs to make the key unique
                    //here we are add a counter to the key value to ensure it is unique
                    errormsgs.Add($"Missing Data {errorCounter}", GetInnerException(ex).Message);
                    errorCounter++;
                }
                
                catch (FormatException ex)
                {
                    errormsgs.Add($"Data Format {errorCounter}", GetInnerException(ex).Message);
                    errorCounter++;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    errormsgs.Add($"Range Data {errorCounter}", GetInnerException(ex).Message);
                    errorCounter++;
                }
                catch (ArgumentException ex)
                {
                    errormsgs.Add($"Data value {errorCounter}", GetInnerException(ex).Message);
                    errorCounter++;
                }
                catch(Exception ex)
                {
                    errormsgs.Add($"System Error {errorCounter}", GetInnerException(ex).Message);
                    errorCounter++;
                }
            }
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
                errormsgs.Clear(); //clear out all existing keys and their values from the Dictionary
                employmentTitle = "";
                StartDate = DateTime.Today;
                empYears = 0;
                empLevel = SupervisoryLevel.Entry;
            }
        }

        private void GoToReport()
        {
            //this event will use the Navigate services of the web software
            //the Navigate services must be injected into the page
            //the location you are going to is reference by the page's routing name
            navigationManager.NavigateTo("report");
        }
    }
}
