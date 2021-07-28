using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
    public class MnchNotification : IDomainEvent
    {
        public ExtractProgress Progress { get; set; }

        public MnchNotification(ExtractProgress progress)
        {
            Progress = progress;
        }
    }
}