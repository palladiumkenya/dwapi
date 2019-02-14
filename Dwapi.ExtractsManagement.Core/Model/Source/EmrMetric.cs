using System;

namespace Dwapi.ExtractsManagement.Core.Model.Source
{
    public class EmrMetricSource
    {
        public string EmrName { get; set; }
        public string EmrVersion { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastMoH731RunDate { get; set; }
        public DateTime DateExtracted { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{EmrName} {EmrVersion}";
        }
    }
}