using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;

namespace Dwapi.UploadManagement.Core.Exchange.Prep
{
    public class PrepMessageBag
    {
        public List<PrepMessage> Messages { get; set; } = new List<PrepMessage>();

        public PrepMessageBag()
        {
        }
        public PrepMessageBag(List<PrepMessage> messages)
        {
            Messages = messages;
        }
        public static PrepMessageBag Create(List<PatientPrepExtract> extracts)
        {
            return new PrepMessageBag(PrepMessage.Create(extracts));
        }
        public static PrepMessageBag Create(List<PrepAdverseEventExtract> extracts)
        {
            return new PrepMessageBag(PrepMessage.Create(extracts));
        }
        public static PrepMessageBag Create(List<PrepBehaviourRiskExtract> extracts)
        {
            return new PrepMessageBag(PrepMessage.Create(extracts));
        }
        public static PrepMessageBag Create(List<PrepCareTerminationExtract> extracts)
        {
            return new PrepMessageBag(PrepMessage.Create(extracts));
        }
        public static PrepMessageBag Create(List<PrepLabExtract> extracts)
        {
            return new PrepMessageBag(PrepMessage.Create(extracts));
        }
        public static PrepMessageBag Create(List<PrepPharmacyExtract> extracts)
        {
            return new PrepMessageBag(PrepMessage.Create(extracts));
        }
        public static PrepMessageBag Create(List<PrepVisitExtract> extracts)
        {
            return new PrepMessageBag(PrepMessage.Create(extracts));
        }
    }
}
