namespace Dwapi.UploadManagement.Core.Exchange.Hts
{
    public class SendHtsResponse
    {
        public string BatchKey { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(BatchKey);
        }
        public override string ToString()
        {
            return $"{BatchKey}";
        }
    }
}
