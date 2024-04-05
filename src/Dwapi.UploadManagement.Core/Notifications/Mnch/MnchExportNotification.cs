using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Notifications.Mnch
{
    public class MnchExportNotification : IDomainEvent
    {
        public SendProgress Progress { get; set; }

        public MnchExportNotification(SendProgress progress)
        {
            Progress = progress;
        }
    }
}
