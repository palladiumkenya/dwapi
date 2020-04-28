using System;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Mgs
{
    public class MetricMigrationExtract : MetricExtract
    {
        public string Dataset { get; set; }
        public string Metric { get; set; }
        public string MetricValue { get; set; }
        public DateTime? CreateDate { get; set; }

        public override string ToString()
        {
            return $"{Dataset} | {Metric} | {MetricValue}";
        }
    }
}
