namespace Dwapi.UploadManagement.Core.Exchange.Mgs
{
    public class SendMgsResponse
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
