using System;
using System.Collections.Generic;
using Dwapi.Contracts.Exchange;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class SourceBag<T>:ISourceBag<T>
    {
        public string JobId { get; set; }
        public EmrSetup EmrSetup { get; set; }
        public UploadMode Mode { get; set; }
        public string DwapiVersion { get; set; }
        public int SiteCode { get; set; }
        public string Facility { get; set; }
        public Guid? ManifestId { get; set; }
        public Guid? SessionId { get; set; }
        public Guid? FacilityId { get; set; }
        public string Tag { get; set; }
        public List<T> Extracts { get; set; } = new List<T>();
    }
}
