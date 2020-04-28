using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class ArtMessageBag
    {
        public string EndPoint => "PatientArt";
        public ArtMessage Message { get; set; } = new ArtMessage();

        public ArtMessageBag()
        {
        }

        public ArtMessageBag(ArtMessage message)
        {
            Message = message;
        }
        public static ArtMessageBag Create(PatientExtractView patient)
        {
            var message = new ArtMessage(patient);
            return new ArtMessageBag(message);
        }

       
    }
}