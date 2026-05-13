using EdiFabric.Core.Model.Telco;
using EdiFabric.Framework.Readers;
using EdiFabric.Templates.TelcoD0;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace EDI_NCPDP_Ingestion
{
    public class ReadNCPDP
    {
        public static List<TSB1> ReadFile(string filePath)
        {
            ////  1.  Load to a stream 
            //Stream ncpdpStream = System.IO.File.OpenRead(Directory.GetCurrentDirectory() + filePath);

            //Console.WriteLine(Directory.GetCurrentDirectory() + filePath);

            ////  2.  Read multiple messages batched up in the same transmission
            //using (var ncpdpReader = new NcpdpTelcoReader(ncpdpStream, "EdiFabric.Templates.Ncpdp"))
            //{
            //    while (ncpdpReader.Read())
            //    {
            //        //Process dispenses if no parsing errors
            //        var claim = ncpdpReader.Item as TSB1;
            //        if (claim != null && !claim.HasErrors)
            //            SaveNCPDP.ProcessClaim(claim);
            //    }
            //}

            // Load into a Stream
            using var ncpdpStream = System.IO.File.OpenRead(filePath);
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

        //private static void ProcessClaim(TSB1 claim)
        //{
        //    using (var db = new NCPDPContext())
        //    {
        //        claim.ClearCache();

        //        // Load into TransactionHeader
        //        if (claim.G1 is not null)
        //        {
        //            db.transactionHeader.Add(claim.G1);
        //            db.SaveChanges();
        //        }

        //        // Load into AM01
        //        if(claim.AM04Loop.AM01 is not null)
        //        { 
        //            db.AM01.Add(claim.AM04Loop.AM01);
        //            db.SaveChanges();
        //        }

        //        // Load into AM04
        //        if (claim.AM04Loop.AM04 is not null)
        //        {
        //            db.AM04.Add(claim.AM04Loop.AM04);
        //            db.SaveChanges();
        //        }

        //        // Load into AM02
        //        if (claim.AM07Loop[0].AM02 is not null)
        //        {
        //            db.AM02.Add(claim.AM07Loop[0].AM02);
        //            db.SaveChanges();
        //        }

        //        // Load into AM03
        //        if (claim.AM07Loop[0].AM03 is not null)
        //        {
        //            db.AM03.Add(claim.AM07Loop[0].AM03);
        //            db.SaveChanges();
        //        }

        //        // Load into AM05
        //        if (claim.AM07Loop[0].AM05 is not null)
        //        {
        //            db.AM05.Add(claim.AM07Loop[0].AM05);
        //            db.SaveChanges();
        //        }

        //        // Load into AM06
        //        if (claim.AM07Loop[0].AM06 is not null)
        //        {
        //            db.AM06.Add(claim.AM07Loop[0].AM06);
        //            db.SaveChanges();
        //        }

        //        // Load into AM07
        //        if (claim.AM07Loop[0].AM07 is not null)
        //        {
        //            db.AM07.Add(claim.AM07Loop[0].AM07);
        //            db.SaveChanges();
        //        }

        //        // Load into AM08
        //        if (claim.AM07Loop[0].AM08 is not null)
        //        {
        //            db.AM08.Add(claim.AM07Loop[0].AM08);
        //            db.SaveChanges();
        //        }

        //        // Load into AM09
        //        if (claim.AM07Loop[0].AM09 is not null)
        //        {
        //            db.AM09.Add(claim.AM07Loop[0].AM09);
        //            db.SaveChanges();
        //        }

        //        // Load into AM10
        //        if (claim.AM07Loop[0].AM10 is not null)
        //        {
        //            db.AM10.Add(claim.AM07Loop[0].AM10);
        //            db.SaveChanges();
        //        }

        //        // Load into AM11
        //        if (claim.AM07Loop[0].AM11 is not null)
        //        {
        //            db.AM11.Add(claim.AM07Loop[0].AM11);
        //            db.SaveChanges();
        //        }

        //        // Load into AM13
        //        if (claim.AM07Loop[0].AM13 is not null)
        //        {
        //            db.AM13.Add(claim.AM07Loop[0].AM13);
        //            db.SaveChanges();
        //        }

        //        // Load into AM14
        //        if (claim.AM07Loop[0].AM14 is not null)
        //        {
        //            db.AM14.Add(claim.AM07Loop[0].AM14);
        //            db.SaveChanges();
        //        }

        //        // Load into AM15
        //        if (claim.AM07Loop[0].AM15 is not null)
        //        {
        //            db.AM15.Add(claim.AM07Loop[0].AM15);
        //            db.SaveChanges();
        //        }

        //        // Load into AM16
        //        if (claim.AM07Loop[0].AM16 is not null)
        //        {
        //            db.AM16.Add(claim.AM07Loop[0].AM16);
        //            db.SaveChanges();
        //        }


        //    }


    }
}
