using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Notifications.Crs
{
    public class CrsSendNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public CrsSendNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
