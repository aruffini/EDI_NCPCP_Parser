using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using EdiFabric;
using EdiFabric.Templates.TelcoD0;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using var client = new AmazonS3Client();

namespace EDI_NCPDP_Ingestion
{
    public class S3FileLoad
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucketName;
        private readonly string _s3Prefix;
        private readonly ILogger<S3FileLoad> _logger;
        private readonly AmazonS3Client amazonS3Client;
        private readonly object value;

        public S3FileLoad(AmazonS3Client amazonS3Client, string bucketName, string s3Prefix, object value)
        {
            this.amazonS3Client = amazonS3Client;
            _bucketName = bucketName;
            _s3Prefix = s3Prefix;
            this.value = value;
        }

        public static List<TSB1> ParseS3File(string bucketName, string s3Prefix, string serialKey)
        {
            // Check the SerialKey prior to parsing the file. If the key is invalid or expired, an exception will be thrown and the file will not be parsed.
            try
            {
                SerialKey.Set(serialKey, true);
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Can't set token"))
                    throw new Exception("Your trial has expired! To continue using EdiFabric SDK you must purchase a plan from https://www.edifabric.com/pricing.html");
            }

            var s3FileLoad = new S3FileLoad(new AmazonS3Client(), bucketName, s3Prefix, null);
            s3FileLoad.GetS3ObjectsAsync(bucketName, s3Prefix).Wait();
            // Placeholder for actual parsing logic to convert S3 objects to List<TSB1>
            return new List<TSB1>();
        }

        private async Task GetS3ObjectsAsync(string bucketName, string bucketPartition)
        {
            var request = new ListObjectsV2Request
            {
                BucketName = _bucketName,
                Prefix = _s3Prefix
            };

            var paginator = _s3.Paginators.ListObjectsV2(request);

            await foreach (var page in paginator.Responses)
            {
                foreach (var s3Object in page.S3Objects)
                {
                    _logger.LogInformation($"Found object: {s3Object.Key}");
                    
                }
            }
        }


    }
}
