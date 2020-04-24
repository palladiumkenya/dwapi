using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;

namespace Dwapi.UploadManagement.Core.Exchange.Mgs
{
    public class MgsMessageBag
    {
        public List<MgsMessage> Messages { get; set; } = new List<MgsMessage>();

        public MgsMessageBag()
        {
        }

        public MgsMessageBag(List<MgsMessage> messages)
        {
            Messages = messages;
        }

        public static MgsMessageBag Create(List<MetricMigrationExtract> migrations)
        {
            return new MgsMessageBag(MgsMessage.Create(migrations));
        }
    }
}
