using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
  public  class ExtractActivityNotification : IDomainEvent
    {
        public DwhProgress Progress { get; set; }

        public ExtractActivityNotification(DwhProgress progress)
        {
            Progress = progress;
        }
    }
}
