using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.Model
{
    public class DbExtract : Entity<Guid>
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Display { get; set; }
        [MaxLength(8000)]
        public string ExtractSql { get; set; }
        public decimal Rank { get; set; }
        public bool IsPriority { get; set; }
        [NotMapped]
        public string Emr { get; set; }

        [NotMapped] public string MainName => GetMainName();
        [NotMapped] public string TableName => GetName();
        [NotMapped] public string TempTableName => GetTempName();

        private string GetName()
        {
            if (Name.IsSameAs("PatientBaselineExtract"))
                return "PatientBaselinesExtract";

            if (Name.IsSameAs("PatientLabExtract"))
                return "PatientLaboratoryExtract";

            if (Name.IsSameAs("HTSClientExtract"))
                return "HtsClientsExtract";

            if (Name.IsSameAs("HTSClientLinkageExtract"))
                return "HtsClientLinkageExtract";

            if (Name.IsSameAs("HTSClientPartnerExtract"))
                return "HtsClientPartnerExtract";

            if (Name.IsSameAs("HtsClient"))
                return "HtsClientsExtract";
           
            return Name;
        }

        private string GetMainName()
        { 

            if (Name.StartsWith("Hts"))
                return "TempHtsClientsExtracts";

            return "TempPatientExtracts";
        }

        private string GetTempName()
        {
            if (Name.IsSameAs("HtsClient"))
                return "TempHtsClientsExtract";
            if (Name.IsSameAs("HtsClientTests"))
                return "TempHtsClientTestsExtract";
            if (Name.IsSameAs("HtsTestKits"))
                return "TempHtsTestKitsExtract";
            if (Name.IsSameAs("HtsClientTracing"))
                return "TempHtsClientTracingExtract";
            if (Name.IsSameAs("HtsPartnerTracing"))
                return "TempHtsPartnerTracingExtract";
            if (Name.IsSameAs("HtsPartnerNotificationServices"))
                return "TempHtsPartnerNotificationServicesExtract";
            if (Name.IsSameAs("HtsClientLinkage"))
                return "TempHtsClientsLinkageExtract";
            return $"Temp{TableName}";
        }


        public string GetCountSQL()
        {
            return $@"select count(*) from ({ExtractSql.ToLower()})xt".ToLower();
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
