using System;

namespace Dwapi.SettingsManagement.Core.DTOs
{
    public class AppMetricDto
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public DateTime LastAction { get; set; }
        public int Rank { get; set; }
    }
}
