using DotNetEnv;

namespace EDI_NCPDP_Ingestion
{
    public static class _EnvConfig
    {
        private static bool validKey = false;
        //private static string? serialKey = Environment.GetEnvironmentVariable("serialKey");        

        public static bool CheckSerialKey()
        {
            var serialKey = Environment.GetEnvironmentVariable("serialKey");

            try
            {
                if (string.IsNullOrWhiteSpace(serialKey))
                {
                    Console.WriteLine("Missing or Invalid SerialKey.");
                    validKey = false;
                }

                Console.WriteLine("SerialKey Found.");
                EdiFabric.SerialKey.Set(serialKey, true);
                validKey = true;

            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Can't set token"))
                {
                    throw new Exception("Your trial has expired! To continue using EdiFabric SDK you must purchase a plan from https://www.edifabric.com/pricing.html");
                }
            }

            return validKey;

        } // End CheckSerialKey

        public static string GetFilePath()
        {
            string? fullFilePath = Environment.GetEnvironmentVariable("filePath") + Environment.GetEnvironmentVariable("fileName");

            return fullFilePath;
        }
    }
}
