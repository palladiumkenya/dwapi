using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Notifications.Mgs
{
    public class MgsSendNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public MgsSendNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
