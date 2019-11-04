using System;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class AppMetric:Entity<Guid>
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public DateTime LogDate { get; set; }=DateTime.Now;
        public string LogValue { get; set; }
        public MetricStatus Status { get; set; }

        public AppMetric()
        {
        }

        public AppMetric(string version, string name, string logValue)
        {
            Version = version;
            Name = name;
            LogValue = logValue;
        }
    }
}
