using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class VisitsMessageBag
    {
        public string EndPoint => "PatientVisits";
        public List<VisitsMessage> Messages { get; set; } = new List<VisitsMessage>();

        public VisitsMessageBag()
        {
        }

        public VisitsMessageBag(List<VisitsMessage> messages)
        {
            Messages = messages;
        }
        public static VisitsMessageBag Create(PatientExtractView patient)
        {
            var messages = new List<VisitsMessage> { new VisitsMessage(patient) };
            return new VisitsMessageBag(messages);
        }
    }
}