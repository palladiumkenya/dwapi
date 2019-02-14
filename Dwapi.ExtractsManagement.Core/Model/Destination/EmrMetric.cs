using System;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Model.Destination
{
    public class EmrMetric:Metric
    {
        public string EmrName { get; set; }
        public string EmrVersion { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastMoH731RunDate { get; set; }
        public DateTime DateExtracted { get; set; }

        public override string ToString()
        {
            return $"{EmrName} {EmrVersion}";
        }
    }
}
