using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class StatusMessageBag
    {
        public string EndPoint => "PatientStatus";
        public List<StatusMessage> Messages { get; set; } = new List<StatusMessage>();

        public StatusMessageBag()
        {
        }

        public StatusMessageBag(List<StatusMessage> messages)
        {
            Messages = messages;
        }
        public static StatusMessageBag Create(PatientExtractView patient)
        {
            var messages = new List<StatusMessage> { new StatusMessage(patient) };
            return new StatusMessageBag(messages);
        }
    }
}