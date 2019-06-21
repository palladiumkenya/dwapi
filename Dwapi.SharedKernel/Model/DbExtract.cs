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

        [NotMapped] public string TableName => GetName();
        [NotMapped] public string TempTableName => GetTempName();

        private string GetName()
        {
            if (Name.IsSameAs("PatientBaselineExtract"))
                return "PatientBaselinesExtract";

            if (Name.IsSameAs("PatientLabExtract"))
                return "PatientLaboratoryExtract";

            return Name;
        }

        private string GetTempName()
        {
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
