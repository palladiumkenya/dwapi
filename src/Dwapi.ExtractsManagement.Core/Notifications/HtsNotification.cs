using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
    public class HtsNotification : IDomainEvent
    {
        public ExtractProgress Progress { get; set; }

        public HtsNotification(ExtractProgress progress)
        {
            Progress = progress;
        }
    }
}