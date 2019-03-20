using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class StatusMessageBag
    {
        public string EndPoint => "PatientStatus";
        public StatusMessage Message { get; set; } = new StatusMessage();

        public StatusMessageBag()
        {
        }

        public StatusMessageBag(StatusMessage message)
        {
            Message = message;
        }
        public static StatusMessageBag Create(PatientExtractView patient)
        {
            var message = new StatusMessage(patient);
            return new StatusMessageBag(message);
        }
    }
}