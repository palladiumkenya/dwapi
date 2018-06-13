using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class BaselineMessageBag
    {
        public string EndPoint => "PatientBaselines";
        public List<BaselineMessage> Messages { get; set; } = new List<BaselineMessage>();

        public BaselineMessageBag()
        {
        }

        public BaselineMessageBag(List<BaselineMessage> messages)
        {
            Messages = messages;
        }
        public static BaselineMessageBag Create(PatientExtractView patient)
        {
            var messages = new List<BaselineMessage> { new BaselineMessage(patient) };
            return new BaselineMessageBag(messages);
        }
    }
}