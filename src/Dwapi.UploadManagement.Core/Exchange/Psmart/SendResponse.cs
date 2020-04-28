namespace Dwapi.UploadManagement.Core.Exchange.Psmart
{
    public class SendResponse
    {
        public string RequestId { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(RequestId);
        }
        public override string ToString()
        {
            return $"{RequestId}";
        }
    }
}