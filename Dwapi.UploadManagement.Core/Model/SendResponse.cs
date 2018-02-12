namespace Dwapi.UploadManagement.Core.Model
{
    public class SendResponse
    {
        public string Status { get; set; }

        public override string ToString()
        {
            return $"{Status}";
        }
    }
}