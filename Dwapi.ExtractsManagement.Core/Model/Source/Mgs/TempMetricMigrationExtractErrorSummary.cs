using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mgs
{
    [Table("vTempMetricMigrationExtractErrorSummary")]
    public class TempMetricMigrationExtractErrorSummary: TempMetricExtractErrorSummary
    {
        public string Dataset { get; set; }
        public string Metric { get; set; }
        public string MetricValue { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
