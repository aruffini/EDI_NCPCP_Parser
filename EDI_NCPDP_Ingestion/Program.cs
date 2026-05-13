using EdiFabric.Core.Model.Edi;
using EdiFabric.Core.Model.Edi.ErrorContexts;
using EdiFabric.Framework.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotNetEnv;

namespace EDI_NCPDP_Ingestion
{
    public class Program
    {
        
        public static void Main()
        {
            var validKey = EnvConfig.CheckSerialKey();
            //string? fullFilePath = Environment.GetEnvironmentVariable("filePath") + Environment.GetEnvironmentVariable("fileName");
            var fullFilePath = EnvConfig.GetFilePath();

            // Only execute if there is a valid serial key
            if (validKey)
            {
                // Read and Pase the File
                //var ncpdpFiles = ReadNCPDP.ReadFile(Config.TestFilesPath + @"\ClaimBilling");
                var ncpdpFiles = ReadNCPDP.ReadFile(fullFilePath);
                    
                // Save the file to the DB
                SaveNCPDP.ProcessClaim(ncpdpFiles);
            }
        }

        //public static bool CheckSerialKey()
        //{   
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(Config.TrialSerialKey))
        //        {
        //            Console.WriteLine("Missing or Invalid SerialKey.");
        //            validKey = false;
        //        }

        //        Console.WriteLine("SerialKey Found.");
        //        EdiFabric.SerialKey.Set(Config.TrialSerialKey, true);
        //        validKey = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.StartsWith("Can't set token"))
        //        {
        //            throw new Exception("Your trial has expired! To continue using EdiFabric SDK you must purchase a plan from https://www.edifabric.com/pricing.html");
        //        }
        //    }
            
        //    return validKey;

        //}


    }
}
