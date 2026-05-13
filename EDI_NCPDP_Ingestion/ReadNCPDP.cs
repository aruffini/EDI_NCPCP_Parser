using EdiFabric;
using EdiFabric.Framework.Readers;
using EdiFabric.Templates.TelcoD0;

namespace EDI_NCPDP_Ingestion
{
    public class ReadNCPDP
    {
        public static List<TSB1> ReadFile(string filePath, string serialKey)
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

            // Load into a Stream
            using var ncpdpStream = File.OpenRead(filePath);
            using var ncpdpReader = new NcpdpTelcoReader(ncpdpStream, "EdiFabric.Templates.Ncpdp");

            var ncpdpItems = ncpdpReader.ReadToEnd().ToList();

            var ncpdpTrans = ncpdpItems.OfType<TSB1>();
            var ncpdpList = new List<TSB1>();

            // Loop through each of the NCPDP transactions
            foreach (var transaction in ncpdpTrans)
            {
                if (transaction.HasErrors)
                {
                    //  partially parsed
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
        
    }
}
