using System;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Exchange
{
    public class ManifestResponse
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string ManifestId { get; set; }
        public string SessionId { get; set; }
        public string JobId { get; set; }
        public Guid FacilityId { get; set; }
        public override string ToString()
        {
            return $"{Code}-{Name} M:{ManifestId}|J:{JobId}|F:[{FacilityId}]";
        }
    }
}
