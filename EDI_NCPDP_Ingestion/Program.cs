using EdiFabric;
using System.Configuration;

namespace EDI_NCPDP_Ingestion
{
    public class Program
    {
        public static void Main() 
        {
            // Get the settings for reading from S3 and local path from app.config
            string _readS3 = ConfigurationManager.AppSettings["ReadS3"];
            string _local = ConfigurationManager.AppSettings["ReadLocal"];
            string _localFileName = ConfigurationManager.AppSettings["LocalFileName"];
            string _bucketName = ConfigurationManager.AppSettings["S3BucketName"];
            string _keyName = ConfigurationManager.AppSettings["S3KeyName"];

            var fullFilePath = Config.TestFilesPath+@"\"+_localFileName; // Only used for local file reading
            var ncpdpFiles = new List<EdiFabric.Templates.TelcoD0.TSB1>();

            try
            {
                //Get the serial key from App.config
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
                    
                    var s3Loader = new S3FileLoad();
                    ncpdpFiles = s3Loader.ParseS3File(_bucketName, _keyName, serialKey);

                    // Save the file to the DB
                    SaveNCPDP.ProcessClaim(ncpdpFiles);
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
