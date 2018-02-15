namespace Dwapi.UploadManagement.Core.Model
{
    public class SendResponse
    {
        public string RequestId { get; set; }

        public override string ToString()
        {
            return $"{RequestId}";
        }
    }
}