using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using EDI_NCPDP_Ingestion;
using EdiFabric;
//using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using System.Configuration;

//var builder = Host.CreateApplicationBuilder(args);

//builder.Services.AddSingleton<AmazonS3Client>( sp =>
//{
//    var accesskey = ConfigurationManager.AppSettings["AccessKey"];
//    var secretkey = ConfigurationManager.AppSettings["SecretAccessKey"];
//    var endpointURL = ConfigurationManager.AppSettings["EndpointUrl"];

//    var s3Config = new AmazonS3Config { RegionEndpoint = RegionEndpoint.GetBySystemName(ConfigurationManager.AppSettings["AWSRegion"])};

//    // If endpointUrl provided, point to Moto instead of real AWS
//    if (!string.IsNullOrEmpty(endpointURL))
//    {
//        s3Config.ServiceURL = endpointURL;  // http://localhost:4566 for Moto
//        s3Config.ForcePathStyle = true;     // Required for Moto compatibility
//    }

//    if (!string.IsNullOrEmpty(accesskey) && !string.IsNullOrEmpty(secretkey))
//    {
//        var credentials = new BasicAWSCredentials(accesskey, secretkey);
//        return new AmazonS3Client(credentials, s3Config);
//    }
//    else
//    {
//        // Fallback to default AWS credentials chain (IAM roles, etc.)
//        return new AmazonS3Client(s3Config);
//    }

//});

//builder.Services.AddTransient<S3FileLoad>();

namespace EDI_NCPDP_Ingestion
{
    public class Program
    {
        public static async Task Main() 
        {
            // Get the settings for reading from S3 and local path from app.config
            string _readS3 = ConfigurationManager.AppSettings["ReadS3"];
            string _local = ConfigurationManager.AppSettings["ReadLocal"];
            string _localFileName = ConfigurationManager.AppSettings["LocalFileName"];
            string _bucketName = ConfigurationManager.AppSettings["S3BucketName"];
            string _keyName = ConfigurationManager.AppSettings["S3KeyName"];
            string _localFilePath = ConfigurationManager.AppSettings["LocalFileLocation"];
            string _ediFabaricKey = ConfigurationManager.AppSettings["EDIFabricKey"];
            string _s3FilePrefix = ConfigurationManager.AppSettings["S3FilePrefix"];
            string _s3AccessKey = ConfigurationManager.AppSettings["AccessKey"];
            string _s3SecretKey = ConfigurationManager.AppSettings["SecretAccessKey"];


            // Set Environment variables for AWS
            //Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", ConfigurationManager.AppSettings["AccessKey"]);
            //Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", ConfigurationManager.AppSettings["SecretAccessKey"]);
            //Environment.SetEnvironmentVariable("AWS_REGION", ConfigurationManager.AppSettings["AWSRegion"]);
            //Environment.SetEnvironmentVariable("AWS_SECURITY_TOKEN", ConfigurationManager.AppSettings["AWSSecurityToken"]);



            //var fullFilePath = Config.TestFilesPath+@"\"+_localFileName; // Only used for local file reading
            var fullFilePath = _localFilePath + @"\" + _localFileName;
            var ncpdpFiles = new List<EdiFabric.Templates.TelcoD0.TSB1>();

            try
            {
                //Get the serial key from App.config
                var serialKey = _ediFabaricKey;

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
                    var s3Config = new AmazonS3Config
                    {
                        ServiceURL = "http://localhost:4566",//ConfigurationManager.AppSettings["EndpointUrl"],
                        ForcePathStyle = true,
                        RegionEndpoint = RegionEndpoint.USEast1
                    };

                    //var s3Client = new AmazonS3Client(RegionEndpoint.USEast1);
                    var s3Client = new AmazonS3Client(_s3AccessKey, _s3SecretKey, s3Config);
                    var intializer = new MotoInitializer(s3Client, _bucketName, _s3FilePrefix, verbose: true);

                    Console.WriteLine("Reading file from S3 bucket: ");

                    // Start Moto, create bucket, upload sample files
                    Console.WriteLine("** Starting Moto S3 mock server");
                    await intializer.InitializeAsync();
                    Console.WriteLine("** Moto initialized");

                    //Read file into a stream and then converted into a list prior to parsing
                    var s3Loader = new S3FileLoad();
                    ncpdpFiles = s3Loader.ParseS3File(s3Client, _bucketName, _keyName, serialKey);

                    // Save the file to the DB
                    //SaveNCPDP.ProcessClaim(ncpdpFiles);
                    
                    // Stop Moto
                    await intializer.CleanupAsync();
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
