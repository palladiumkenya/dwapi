using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;

namespace Dwapi.UploadManagement.Core.Exchange.Hts
{
    public class HtsMessageBag
    {
        public List<HtsMessage> Messages { get; set; } = new List<HtsMessage>();

        public HtsMessageBag()
        {
        }

        public HtsMessageBag(List<HtsMessage> messages)
        {
            Messages = messages;
        }

        public static HtsMessageBag Create(List<HTSClientExtract> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }
        public static HtsMessageBag Create(List<HTSClientLinkageExtract> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }
        public static HtsMessageBag Create(List<HTSClientPartnerExtract> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }
    }
}
