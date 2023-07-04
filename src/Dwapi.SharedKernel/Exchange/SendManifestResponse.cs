using System;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.Exchange
{
    public class SendManifestResponse
    {
        public string MasterFacility { get; set; }
        public ManifestResponse ManifestResponse{ get; set; }
        public Guid FacilityKey { get; set; }
        
        public SendManifestResponse(ManifestResponse manifestResponse)
        {
            ManifestResponse = manifestResponse;
        }
        public SendManifestResponse(string masterFacility)
        {
            MasterFacility = masterFacility;
        }

        public bool IsValid()
        {
            return !FacilityKey.IsNullOrEmpty();
        }
        public override string ToString()
        {
            return $"{FacilityKey}";
        }
    }
}