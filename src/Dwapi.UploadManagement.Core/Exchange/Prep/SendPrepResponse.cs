namespace Dwapi.UploadManagement.Core.Exchange.Prep
{
    public class SendPrepResponse
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
