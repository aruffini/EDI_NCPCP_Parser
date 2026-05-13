namespace EDI_NCPDP_Ingestion
{
    public class Program
    {
        
        public static void Main()
        {
            var validKey = EnvConfig.CheckSerialKey();
            var fullFilePath = EnvConfig.GetFilePath();

            // Only execute if there is a valid serial key
            if (validKey)
            {
                // Read and Pase the File
                var ncpdpFiles = ReadNCPDP.ReadFile(fullFilePath);
                    
                // Save the file to the DB
                SaveNCPDP.ProcessClaim(ncpdpFiles);
            }
        }

    }
}
