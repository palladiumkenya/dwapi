using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Notifications.Hts
{
    public class HtsExportNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public HtsExportNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
