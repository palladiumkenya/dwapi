using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class TransportLog:Entity<Guid>
    {
        public string Docket { get; set; }
        public string Extract { get; set; }
        public Guid ManifestId { get; set; }
        public string JobId {get; set; }
        public DateTime JobStart {get; set; }
        public DateTime JobEnd {get; set; }
        public int SiteCode { get; set; }
        public Guid FacilityId { get; set; }
        [NotMapped]
        public bool IsManifest => Extract.IsSameAs("Manifest");

        public bool IsMainExtract => Extract.IsSameAs(GetMainByDocket(Docket));

        private string GetMainByDocket(string docket)
        {
            if (Docket == "NDWH")
                return "PatientExtract";

            return string.Empty;
        }

        public TransportLog()
        {
        }
        public TransportLog(string docket,string extract, string jobId)
        {
            Docket = docket;
            Extract = extract;
            JobId = jobId;

        }
        public TransportLog(string docket,string extract, string jobId,Guid manifestId):
            this(docket,extract,jobId)
        {
            ManifestId = manifestId;
        }

        public static TransportLog GenerateManifest(string docket, string jobId, Guid manifestId, int siteCode,
            DateTime dateTime,Guid facilityId)
        {
            var m = new TransportLog(docket, "Manifest", jobId, manifestId);
            m.JobStart = dateTime;
            m.JobEnd = DateTime.Now;
            m.SiteCode = siteCode;
            m.FacilityId = facilityId;
            return m;
        }

        public static TransportLog GenerateExtract(string docket, string extract, string jobId)
        {
            var m = new TransportLog(docket, extract, jobId);
            m.JobStart = DateTime.Now;
            return m;
        }

        public void SetManifest(TransportLog manifest)
        {
            ManifestId = manifest.ManifestId;
            FacilityId = manifest.FacilityId;
            SiteCode = manifest.SiteCode;
        }

        public void SetLatest(TransportLog transportLog)
        {
            JobId = transportLog.JobId;
            JobEnd = DateTime.Now;
        }
    }
}
