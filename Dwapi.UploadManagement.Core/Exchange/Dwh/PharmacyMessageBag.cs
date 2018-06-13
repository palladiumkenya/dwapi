using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class PharmacyMessageBag
    {
        public string EndPoint => "PatientPharmacy";
        public List<PharmacyMessage> Messages { get; set; } = new List<PharmacyMessage>();

        public PharmacyMessageBag()
        {
        }

        public PharmacyMessageBag(List<PharmacyMessage> messages)
        {
            Messages = messages;
        }
        public static PharmacyMessageBag Create(PatientExtractView patient)
        {
            var messages = new List<PharmacyMessage> { new PharmacyMessage(patient) };
            return new PharmacyMessageBag(messages);
        }
    }
}