using System;

namespace Dwapi.SettingsManagement.Core.DTOs
{
    public class ExtractCargoDto
    {
        public string DocketId { get; set; }
        public string Name { get; set; }
        public int? Stats { get; set; }
        public int? SiteCode { get; set; }
    }
}
