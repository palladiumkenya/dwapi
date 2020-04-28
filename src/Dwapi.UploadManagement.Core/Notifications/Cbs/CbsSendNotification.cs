using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Notifications.Cbs
{
    public class CbsSendNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public CbsSendNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
