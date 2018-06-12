using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class ArtMessageBag
    {
        public List<ArtMessage> Messages { get; set; } = new List<ArtMessage>();

        public ArtMessageBag()
        {
        }

        public ArtMessageBag(List<ArtMessage> messages)
        {
            Messages = messages;
        }
        public static ArtMessageBag Create(PatientExtractView patient, List<PatientArtExtractView> arts)
        {
            var messages = new List<ArtMessage> {new ArtMessage(patient, arts)};
            return new ArtMessageBag(messages);
        }
    }
}