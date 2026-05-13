using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Migrations.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdiFabric.Templates.TelcoD0;
using EDI_NCPDP_Ingestion;
using Moq;

namespace NCPDP_Test
{
    [TestClass]
    public class NCPDPTest
    {

        [TestMethod]
        public void TestEDI()
        {
            var dbConn = new Mock<DbConnection>().Object;

            var targetDB = new NCPDPContext();
            Assert.IsNotNull(targetDB);            
           
        }

        [TestMethod]
        public void TestFileParse()
        {
            //NCPDPTest test = new NCPDPTest();
            //var filePath = Environment.GetEnvironmentVariable("filePath") + Environment.GetEnvironmentVariable("fileName");
            var filePath = "C:\\Files\\ClaimBilling";
            var serialKey = "c417cb9dd9d54297a55c032a74c87996";

            var result = ReadNCPDP.ReadFile(filePath, serialKey);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }


        
    }
}
