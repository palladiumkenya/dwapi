using System;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mgs
{
    public class TempMetricMigrationExtract : TempMetricExtract
    {
        public string Dataset { get; set; }
        public string Metric { get; set; }
        public string MetricValue { get; set; }
        public string Stage { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
