using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class LabMessageBag
    {
        public string EndPoint => "PatientLabs";
        public LabMessage Message{ get; set; } = new LabMessage();

        public LabMessageBag()
        {
        }

        public LabMessageBag(LabMessage message)
        {
            Message = message;
        }
        public static LabMessageBag Create(PatientExtractView patient)
        {
            var message = new LabMessage(patient);
            return new LabMessageBag(message);
        }
    }
}