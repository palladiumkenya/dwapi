using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Notifications.Prep
{
    public class PrepSendNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public PrepSendNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
