using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Notifications.Mnch
{
    public class MnchSendNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public MnchSendNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
