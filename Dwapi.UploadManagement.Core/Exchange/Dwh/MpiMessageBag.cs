using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Cbs
{
    public class ArtMessageBag
    {
        public List<ArtMessage> Messages { get; set; } = new List<ArtMessage>();

        public ArtMessageBag()
        {
        }

        public ArtMessageBag(List<ArtMessageBag> messages)
        {
            Messages = messages;
        }

        public static MpiMessageBag Create(List<MasterPatientIndex> patientIndices)
        {
            return new MpiMessageBag(MpiMessage.Create(patientIndices));
        }
    }
}