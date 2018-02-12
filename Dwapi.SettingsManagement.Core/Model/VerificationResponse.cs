using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.SettingsManagement.Core.Model
{
   public class VerificationResponse
    {
        public string RegistryName { get; set; }
        public bool Verified { get; set; }

        public VerificationResponse()
        {
        }

        public VerificationResponse(string registryName, bool verified)
        {
            RegistryName = registryName;
            Verified = verified;
        }

        public override string ToString()
        {
            return RegistryName;
        }
    }
}
