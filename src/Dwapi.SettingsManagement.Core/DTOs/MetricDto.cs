using System;

namespace Dwapi.SettingsManagement.Core.DTOs
{
    public class MetricDto
    {
        public string EmrName { get; set; }
        public string EmrVersion { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastMoH731RunDate { get; set; }
        public DateTime DateExtracted { get; set; }
    }
}
