using System;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
  public  class ExtractActivityNotification : IDomainEvent
    {
        public Guid ExtractId { get; set; }
        public DwhProgress Progress { get; set; }

        public ExtractActivityNotification(Guid extractId, DwhProgress progress)
        {
            ExtractId = extractId;
            Progress = progress;
        }

        public ExtractActivityNotification(DwhProgress progress)
        {
            Progress = progress;
            ExtractId = Guid.Empty;
        }
    }
}
