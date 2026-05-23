using EdiFabric;
using System.Configuration;

namespace EDI_NCPDP_Ingestion
{
    public class Program
    {
        public static void Main() 
        {
            string _readS3 = ConfigurationManager.AppSettings["ReadS3"];
            string _local = ConfigurationManager.AppSettings["ReadLocal"];

            var fullFilePath = Config.TestFilesPath+@"\ClaimBilling"; // Needs to be moved
            var ncpdpFiles = new List<EdiFabric.Templates.TelcoD0.TSB1>();

            try
            {
                //Get the serial key from the Config class
                var serialKey = Config.TrialSerialKey;

                if (_local == "1")
                {
                    Console.WriteLine("Reading file from local path: " + fullFilePath);

                    // Read the file and get the list of TSB1 objects
                    ncpdpFiles = ReadNCPDP.ReadFile(fullFilePath, serialKey);

                    // Save the file to the DB
                    SaveNCPDP.ProcessClaim(ncpdpFiles);

                }
                else if (_readS3 == "1")
                {
                    Console.WriteLine("Reading file from S3 bucket: ");
                    
                    // Read the file from S3 and get the list of TSB1 objects
                    //sncpdpFiles = S3FileLoad.GetS3ObjectsAsync("test","test",serialKey);

                    // Save the file to the DB
                    //SaveNCPDP.ProcessClaim(ncpdpFiles);
                }

                


            }
            catch (Exception ex)
            {
                // Print the exception message to the console
                Console.WriteLine(ex.Message);
            }
        } // End of Main
    } // End of Program
} // End of Namespace
