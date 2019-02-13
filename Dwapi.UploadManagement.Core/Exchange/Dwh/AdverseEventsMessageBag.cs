using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class AdverseEventsMessageBag
    {
        public string EndPoint => "PatientAdverseEvents";
        public List<AdverseEventsMessage> Messages { get; set; } = new List<AdverseEventsMessage>();

        public AdverseEventsMessageBag()
        {
        }

        public AdverseEventsMessageBag(List<AdverseEventsMessage> messages)
        {
            Messages = messages;
        }

        public static AdverseEventsMessageBag Create(PatientExtractView patient)
        {
            var messages = new List<AdverseEventsMessage> { new AdverseEventsMessage(patient) };
            return new AdverseEventsMessageBag(messages);
        }
    }
}