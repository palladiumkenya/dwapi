using System.Collections.Generic;

namespace Dwapi.SharedKernel.Model
{
    public class Site
    {
        public int SiteCode { get; set; }
        public string SiteName { get; set; }
        public int PatientCount { get; set; }

        public override string ToString()
        {
            return $"{SiteCode}-{SiteName} | {PatientCount}";
        }
    }
}
