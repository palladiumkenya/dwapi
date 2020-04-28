namespace Dwapi.SharedKernel.Exchange
{
    public class SendDhwManifestResponse
    {
        public string MasterFacility { get; set; }

        public SendDhwManifestResponse()
        {
        }

        public SendDhwManifestResponse(string masterFacility)
        {
            MasterFacility = masterFacility;
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