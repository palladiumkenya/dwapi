using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class LabMessageBag
    {
        public string EndPoint => "PatientLabs";
        public List<LabMessage> Messages { get; set; } = new List<LabMessage>();

        public LabMessageBag()
        {
        }

        public LabMessageBag(List<LabMessage> messages)
        {
            Messages = messages;
        }
        public static LabMessageBag Create(PatientExtractView patient)
        {
            var messages = new List<LabMessage> { new LabMessage(patient) };
            return new LabMessageBag(messages);
        }
    }
}