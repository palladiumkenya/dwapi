using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
    public class PrepNotification : IDomainEvent
    {
        public ExtractProgress Progress { get; set; }

        public PrepNotification(ExtractProgress progress)
        {
            Progress = progress;
        }
    }
}