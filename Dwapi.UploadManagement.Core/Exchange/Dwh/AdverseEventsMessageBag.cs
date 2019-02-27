using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class AdverseEventsMessageBag
    {
        public string EndPoint => "PatientAdverseEvents";
        public AdverseEventsMessage Message { get; set; } = new AdverseEventsMessage();

        public AdverseEventsMessageBag()
        {
        }

        public AdverseEventsMessageBag(AdverseEventsMessage message)
        {
            Message = message;
        }

        public static AdverseEventsMessageBag Create(PatientExtractView patient)
        {
            var message = new AdverseEventsMessage(patient);
            return new AdverseEventsMessageBag(message);
        }
    }
}