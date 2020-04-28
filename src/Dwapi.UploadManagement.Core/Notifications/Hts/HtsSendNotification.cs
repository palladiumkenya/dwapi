using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Notifications.Hts
{
    public class HtsSendNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public HtsSendNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
