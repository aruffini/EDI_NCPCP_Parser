using Amazon.S3;
using Amazon.S3.Model;
using EdiFabric;
using EdiFabric.Framework.Readers;
using EdiFabric.Templates.TelcoD0;

//using var client = new AmazonS3Client();

namespace EDI_NCPDP_Ingestion
{
    public class S3FileLoad
    {
        // This method parses an NCPDP file from S3 using the EdiFabric SDK.
        // It takes in the bucket name, key name, and serial key as parameters and returns
        // a list of TSB1 objects representing the parsed transactions.
        public List<TSB1> ParseS3File(string bucketName, string fileName, string serialKey)
        {
            try
            {
                SerialKey.Set(serialKey, true);
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Can't set token"))
                    throw new Exception("Your trial has expired! To continue using EdiFabric SDK you must purchase a plan from https://www.edifabric.com/pricing.html");
            }

            using var s3Client = new AmazonS3Client();
            var memStream = LoadS3FileToMemoryStreamAsync(s3Client, bucketName, fileName).Result;
            using var ncpdpReader = new NcpdpTelcoReader(memStream, "EdiFabric.Templates.Ncpdp");
            var ncpdpItems = ncpdpReader.ReadToEnd().ToList();
            var ncpdpTrans = ncpdpItems.OfType<TSB1>();
            var ncpdpList = new List<TSB1>();

            // Loop through each of the NCPDP transactions
            foreach (var transaction in ncpdpTrans)
            {
                if (transaction.HasErrors)
                {
                    var errors = transaction.ErrorContext.Flatten();
                    foreach (var err in errors)
                    {
                        Console.WriteLine($"ERROR: {err}");
                    }
                }
                else
                {
                    Console.WriteLine("File Parsed.");
                    ncpdpList.Add(transaction);
                }
            }
            return ncpdpList;
        }

        // This method loads a file from S3 into a MemoryStream to be parsed by the EdiFabric SDK.
        // The method takes in the S3 client, bucket name, and key name as parameters and
        // returns a MemoryStream containing the file data.
        private async Task<MemoryStream> LoadS3FileToMemoryStreamAsync(IAmazonS3 s3Cleint, string bucketName, string fileName)
        {
            var _memStream = new MemoryStream();
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                using var response = await s3Cleint.GetObjectAsync(request);
                using var responseStream = response.ResponseStream;
                await responseStream.CopyToAsync(_memStream);
                _memStream.Position = 0; // Reset the position to the beginning of the stream
                return _memStream;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file from S3: {ex.Message}");
                throw;
            }
        }
        
    }
}
