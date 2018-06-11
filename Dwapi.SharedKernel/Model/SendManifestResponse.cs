using System;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.Model
{
    public class SendManifestResponse
    {
        public Guid FacilityKey { get; set; }

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