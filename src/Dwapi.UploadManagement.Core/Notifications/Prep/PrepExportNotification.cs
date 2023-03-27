using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Notifications.Prep
{
    public class PrepExportNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public PrepExportNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
