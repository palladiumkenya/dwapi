using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;

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

        public static MpiMessageBag Create(List<MasterPatientIndexDto> patientIndices)
        {
            return new MpiMessageBag(MpiMessage.Create(patientIndices));
        }
    }
}
