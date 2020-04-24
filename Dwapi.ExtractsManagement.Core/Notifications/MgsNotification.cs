using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
    public class MgsNotification : IDomainEvent
    {
        public ExtractProgress Progress { get; set; }

        public MgsNotification(ExtractProgress progress)
        {
            Progress = progress;
        }
    }
}
