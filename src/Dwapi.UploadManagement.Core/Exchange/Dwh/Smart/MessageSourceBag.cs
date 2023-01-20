using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct
{
    public  abstract class MessageSourceBag<T>:IMessageSourceBag<T> where T : ClientExtract
    {
        public virtual string JobId { get; set; }
        public virtual EmrSetup EmrSetup { get; set; }
        public virtual UploadMode Mode { get; set; }
        public virtual string DwapiVersion { get; set; }
        public virtual int SiteCode { get; set; }
        public virtual string Facility { get; set; }
        public virtual Guid? ManifestId { get; set; }
        public virtual Guid? SessionId { get; set; }
        public virtual Guid? FacilityId { get; set; }
        public virtual string Tag { get; set; }
        public virtual List<T> Extracts { get; set; } = new List<T>();
        public virtual int Stake => 100;
        [JsonIgnore]
        public virtual   string EndPoint { get; }
        [JsonIgnore]
        public virtual string ExtractName { get; }
        [JsonIgnore]
        public virtual ExtractType ExtractType { get; }
        public virtual string Docket => "NDWH";
        public virtual string DocketExtract { get; }
        [JsonIgnore]
        public virtual List<Guid> SendIds =>GetIds();
        public int MinPk => Extracts.Min(x => x.PatientPK);
        public int MaxPk => Extracts.Max(x => x.PatientPK);

        private List<Guid> GetIds()
        {
            return Extracts
                .Select(x => x.Id)
                .ToList();
        }

        public void Generate(List<T> extracts, Guid manifestId,Guid facilityId, string jobId)
        {
            Extracts = extracts;
            ManifestId = manifestId;
            FacilityId = facilityId;
            JobId = jobId;
        }

        public virtual int GetProgress(int count, int total)
        {
            if (total == 0)
                return Stake;

            var percentageStake=  ((float)count / (float)total) * Stake;
            return (int) percentageStake;
        }
    }
}
