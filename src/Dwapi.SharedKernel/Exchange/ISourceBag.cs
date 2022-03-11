using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Enum;

namespace Dwapi.Contracts.Exchange
{
    public interface ISourceBag<T>
    {
        string JobId  { get; set; }
        EmrSetup EmrSetup { get; set; }
        UploadMode Mode { get; set; }
        string DwapiVersion { get; set; }
        int SiteCode { get; set; }
        string Facility { get; set; }
        Guid? ManifestId { get; set; }
        Guid? SessionId { get; set; }
        Guid? FacilityId { get; set; }
        string Tag { get; set; }
        List<T> Extracts { get; set; }
    }
}
