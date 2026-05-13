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
        public NCPDPTest()
        {
            EnvConfig.CheckSerialKey();
        }

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
            var filePath = Environment.GetEnvironmentVariable("filePath") + Environment.GetEnvironmentVariable("fileName");

            var result = ReadNCPDP.ReadFile(filePath);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }


        
    }
}
