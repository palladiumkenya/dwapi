namespace Dwapi.SharedKernel.Model
{
    public class SitePatientProfile
    {
        public int SiteCode { get; set; }
        public string SiteName { get; set; }
        public int PatientPk { get; set; }

        public SitePatientProfile()
        {
        }

        public SitePatientProfile(int siteCode, string siteName, int patientPk)
        {
            SiteCode = siteCode;
            SiteName = siteName;
            PatientPk = patientPk;
        }
    }
}
