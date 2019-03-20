using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class VisitsMessageBag
    {
        public string EndPoint => "PatientVisits";
        public VisitsMessage Message { get; set; } = new VisitsMessage();

        public VisitsMessageBag()
        {
        }

        public VisitsMessageBag(VisitsMessage message)
        {
            Message = message;
        }
        public static VisitsMessageBag Create(PatientExtractView patient)
        {
            var message = new VisitsMessage(patient);
            return new VisitsMessageBag(message);
        }
    }
}