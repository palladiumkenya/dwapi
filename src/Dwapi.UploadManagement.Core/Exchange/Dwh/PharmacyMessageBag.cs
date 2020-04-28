using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class PharmacyMessageBag
    {
        public string EndPoint => "PatientPharmacy";
        public PharmacyMessage Message { get; set; } = new PharmacyMessage();

        public PharmacyMessageBag()
        {
        }

        public PharmacyMessageBag(PharmacyMessage message)
        {
            Message = message;
        }
        public static PharmacyMessageBag Create(PatientExtractView patient)
        {
            var message = new PharmacyMessage(patient);
            return new PharmacyMessageBag(message);
        }
    }
}