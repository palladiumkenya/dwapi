using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;

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

        public static HtsMessageBag Create(List<HtsClients> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }
        public static HtsMessageBag Create(List<HtsClientTests> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }
        public static HtsMessageBag Create(List<HtsTestKits> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }
        public static HtsMessageBag Create(List<HtsClientTracing> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }
        public static HtsMessageBag Create(List<HtsPartnerTracing> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }
        public static HtsMessageBag Create(List<HtsPartnerNotificationServices> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }
        public static HtsMessageBag Create(List<HtsClientLinkage> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }
        public static HtsMessageBag Create(List<HtsEligibilityExtract> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.Create(patientIndices));
        }

        //BoardRoom
        public static HtsMessageBag CreateEx(List<HtsClients> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.CreateEx(patientIndices));
        }
        public static HtsMessageBag CreateEx(List<HtsClientTests> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.CreateEx(patientIndices));
        }
        public static HtsMessageBag CreateEx(List<HtsTestKits> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.CreateEx(patientIndices));
        }
        public static HtsMessageBag CreateEx(List<HtsClientTracing> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.CreateEx(patientIndices));
        }
        public static HtsMessageBag CreateEx(List<HtsPartnerTracing> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.CreateEx(patientIndices));
        }
        public static HtsMessageBag CreateEx(List<HtsPartnerNotificationServices> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.CreateEx(patientIndices));
        }
        public static HtsMessageBag CreateEx(List<HtsClientLinkage> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.CreateEx(patientIndices));
        }
        public static HtsMessageBag CreateEx(List<HtsEligibilityExtract> patientIndices)
        {
            return new HtsMessageBag(HtsMessage.CreateEx(patientIndices));
        }
    }
}
