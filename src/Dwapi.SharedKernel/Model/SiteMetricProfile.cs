namespace Dwapi.SharedKernel.Model
{
    public class SiteMetricProfile
    {
        public int SiteCode { get; set; }
        public string SiteName { get; set; }
        public int MetricId { get; set; }

        public SiteMetricProfile()
        {
        }

        public SiteMetricProfile(int siteCode, string siteName, int metricId)
        {
            SiteCode = siteCode;
            SiteName = siteName;
            MetricId = metricId;
        }
    }
}
