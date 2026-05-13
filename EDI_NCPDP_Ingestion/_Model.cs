using System.Data.Entity;

//namespace EDI_NCPDP_Ingestion
//{
internal class _Model
    {
        public class NCPDPContext : DbContext
        {
            public DbSet<HeaderSegment> Headers { get; set; }
            public DbSet<PatientSegment> Patients { get; set; }
            public DbSet<ClaimSegment> Claims { get; set; }
            public DbSet<PrescriberSegment> Providers { get; set; }
            public DbSet<PricingSegment> Pricings { get; set; }

        }

        public class HeaderSegment
        {
            public int HeaderSegmentID { get; set; }
            public string G11_TransactionReferenceNumber { get; set; }
            public string G12_BinNumber { get; set; }
            public string G13_VerionsReleaseNumber { get; set; }
            public string G14_TransactionCode { get; set; }
            public string G15_ProcessorControlNumber { get; set; }
            public string G16_TransactionCount { get; set; }
            public string G17_ServiceProviderIDQualifier { get; set; }
            public string G18_SreviceProviderID {  get; set; }
            public string G19_DateOfService {  get; set; }
            public string G110_SoftwareVendorCertificationID { get; set; }
        }

        public class PatientSegment
        {
            public int PatientSegmentID { get; set; }
            public int HeaderSegmentID { get; set; }
            public string AM041_CardholderID { get; set; }
            public string AM042_CardholderFirstName { get; set; }
            public string AM043_CardholderLastName { get; set; }
            public string AM044_HomePlan { get; set; }
            public string AM045_PlanID { get; set; }
            public string AM046_EligibilityClarificationCode { get; set; }
            public string AM047_GroupID { get; set; }
            public string AM048_PersonCode { get; set; }
            public string AM049_PatientRelationshipCode { get; set; }
            public string AM0410_OtherPayerBINNumber { get; set; }
            public string AM0411_OtherPayerProcessorControlNumber { get; set; }
            public string AM0412_OtherPayerCardholderID { get; set; }
            public string AM0413_OtherPayerGroupID { get; set; }
            public string AM0414_MedigapID { get; set; }
            public string AM0415_MedicaidIndicator { get; set; }
            public string AM0416_ProviderAcceptAssignmentIndicator { get; set; }
            public string AM0417_CMSPartDDefinedQualifiedFacility { get; set; }
            public string AM0418_MedicaidIDNumber { get; set; }
            public string AM0419_MedicaidAgencyNumber { get; set; }

        }

        public class ClaimSegment
        {

        }

        public class PrescriberSegment
        {

        }

        public class PricingSegment
        {

        }

    }
//}
