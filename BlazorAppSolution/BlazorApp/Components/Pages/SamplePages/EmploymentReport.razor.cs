using System.Net.NetworkInformation;
using OOPsReview;
using CommonMethods;
using System.Diagnostics.Eventing.Reader;

namespace BlazorApp.Components.Pages.SamplePages
{
    public partial class EmploymentReport
    {
        private string feedback = "";
        private List<string> errormsgs { get; set; } = new List<string>();
        private List<Employment> employments = null;
        private Employment employment = null;

        private Exception GetInnerException(Exception ex)
        {
            //drill down into your Exception until there are no more inner exceptions
            //at this point you have the "real" error
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

        protected override void OnInitialized()
        {
            Reading();
            base.OnInitialized();
        }

        private void Reading()
        {
            feedback = "";
            errormsgs.Clear();

            //there are a couple of ways to refer to your file and its path
            //  i) obtain the root path of your application using an injection
            //      service called IWebHostEnvironment via property injection (see variables)
            //  ii) use relative addressing starting a the top of your application

            //on this page we will use relative addressing
            //This addressing of the required file will start at the top of your web application
            //syntax:   string filename = @"./foldername/.../filename.csv"
            string filepathname = @"./Data/";
            string[] filenames = new string[] { "Employments.csv", "BadEmployments.csv", "EmptyEmployments.csv" };
            string filename = @$"{filepathname}{filenames[0]}";


            //The System.IO.File method ReadAllLines() will return an array
            //  of lines as strings where each array element represents a
            //  line in the file
            Array userdata = null;

            try
            {
                if(System.IO.File.Exists(filename))
                {
                    //create an instance of the table data collection
                    employments = new List<Employment>();

                    //read then file
                    userdata = System.IO.File.ReadAllLines(filename);

                    //traverse the array (lines from the file)
                    //ensure that there is sufficient data on the line to create the required instance
                    //if not: throw an FormatException
                    //if so: create an instance of the required class definition
                    //       add the instance to the collection
                    foreach(string line in userdata)
                    {
                        employment = Employment.Parse(line);
                        employments.Add(employment);
                    }
                }
                else
                {
                    throw new Exception($"File {filenames[0]} does not exist");
                }
            }
            catch(Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).Message);
            }

        }
    }
}
