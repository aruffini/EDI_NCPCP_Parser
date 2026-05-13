using EdiFabric;

namespace EDI_NCPDP_Ingestion
{
    public class Program
    {
        
        public static void Main()
        {
            var fullFilePath = Config.TestFilesPath+@"\ClaimBilling";

            try
            {
                //Get the serial key from the Config class
                var serialKey = Config.TrialSerialKey;

                // Read the file and get the list of TSB1 objects
                var ncpdpFiles = ReadNCPDP.ReadFile(fullFilePath, serialKey);

                // Save the file to the DB
                SaveNCPDP.ProcessClaim(ncpdpFiles);
            }
            catch (Exception ex)
            {
                // Print the exception message to the console
                Console.WriteLine(ex.Message);
            }
        } // End of Main
    } // End of Program
} // End of Namespace
