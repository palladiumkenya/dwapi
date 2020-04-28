using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mgs
{
    [Table("vTempMetricMigrationExtractError")]
    public class TempMetricMigrationExtractError
    {
        public string Dataset { get; set; }
        public string Metric { get; set; }
        public string MetricValue { get; set; }
        public DateTime? CreateDate { get; set; }
        public string FacilityName { get; set; }
        public int? SiteCode { get; set; }
        public int? MetricId { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool CheckError { get; set; }
        public DateTime DateExtracted { get; set; }
        public Guid Id { get; set; }

        [NotMapped]
        public virtual ICollection<TempMetricMigrationExtractErrorSummary> TempMetricMigrationExtractErrorSummaries { get; set; } =new List<TempMetricMigrationExtractErrorSummary>();
    }
}
