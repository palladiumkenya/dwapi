namespace Dwapi.SharedKernel.Exchange
{
    public class SendDhwManifestResponse
    {
        public string MasterFacility { get; set; }
        public ManifestResponse ManifestResponse{ get; set; }
        public SendDhwManifestResponse()
        {
        }

        public SendDhwManifestResponse(string masterFacility)
        {
            MasterFacility = masterFacility;
        }

        public SendDhwManifestResponse(ManifestResponse manifestResponse)
        {
            ManifestResponse = manifestResponse;
        }


        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(MasterFacility);
        }
        public override string ToString()
        {
            return $"{MasterFacility}";
        }
    }
}
