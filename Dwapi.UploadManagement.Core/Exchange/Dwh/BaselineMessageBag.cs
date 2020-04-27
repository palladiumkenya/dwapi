using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class BaselineMessageBag
    {
        public string EndPoint => "PatientBaselines";
        public BaselineMessage Message { get; set; } = new BaselineMessage();

        public BaselineMessageBag()
        {
        }

        public BaselineMessageBag(BaselineMessage message)
        {
            Message = message;
        }
        public static BaselineMessageBag Create(PatientExtractView patient)
        {
            var message = new BaselineMessage(patient);
            return new BaselineMessageBag(message);
        }
    }
}