using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Notifications.Dwh
{
    public class DwhSendNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public DwhSendNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
