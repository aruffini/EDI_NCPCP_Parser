using EdiFabric.Core.Model.Telco;
using EdiFabric.Templates.TelcoD0;
using System.Data.Entity;

namespace EDI_NCPDP_Ingestion
{
    internal class Model
    {
        public DbSet<TransactionHeader> transactionHeader { get; set; }
        public DbSet<AM04> AM04 { get; set; }
        public DbSet<AM01> AM01 { get; set; }
        public DbSet<AM07> AM07 { get; set; }
        public DbSet<AM11> AM11 { get; set; }
        public DbSet<AM02> AM02 { get; set; }
        public DbSet<AM03> AM03 { get; set; }
        public DbSet<AM05> AM05 { get; set; }
        public DbSet<AM06> AM06 { get; set; }
        public DbSet<AM08> AM08 { get; set; }
        public DbSet<AM09> AM09 { get; set; }
        public DbSet<AM10> AM10 { get; set; }
        public DbSet<AM13> AM13 { get; set; }
        public DbSet<AM14> AM14 { get; set; }
        public DbSet<AM15> AM15 { get; set; }
        public DbSet<AM16> AM16 { get; set; }

    }
}
