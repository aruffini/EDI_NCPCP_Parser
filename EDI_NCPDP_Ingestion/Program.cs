using EdiFabric;

namespace EDI_NCPDP_Ingestion
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var fullFilePath = Config.TestFilesPath+@"\ClaimBilling";

            try
            {
                //SerialKey.Set(Config.TrialSerialKey, true);
                var serialKey = Config.TrialSerialKey;

                var ncpdpFiles = ReadNCPDP.ReadFile(fullFilePath, serialKey);

                // Save the file to the DB
                SaveNCPDP.ProcessClaim(ncpdpFiles);
            }
            catch (Exception ex)
            {
                //if (ex.Message.StartsWith("Can't set token"))
                //    throw new Exception("Your trial has expired! To continue using EdiFabric SDK you must purchase a plan from https://www.edifabric.com/pricing.html");
                Console.WriteLine(ex.Message);
            }
        }

    }
}
