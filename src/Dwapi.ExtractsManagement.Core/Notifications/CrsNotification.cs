using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
    public class CrsNotification : IDomainEvent
    {
        public ExtractProgress Progress { get; set; }

        public CrsNotification(ExtractProgress progress)
        {
            Progress = progress;
        }
    }
}
