using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;

namespace Dwapi.UploadManagement.Core.Exchange.Cbs
{
    public class MpiMessageBag
    {
        public List<MpiMessage> Messages { get; set; } = new List<MpiMessage>();

        public MpiMessageBag()
        {
        }

        public MpiMessageBag(List<MpiMessage> messages)
        {
            Messages = messages;
        }

        public static MpiMessageBag Create(List<MasterPatientIndex> patientIndices)
        {
            return new MpiMessageBag(MpiMessage.Create(patientIndices));
        }
    }
}