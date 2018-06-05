using System;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
  public  class CbsNotification : IDomainEvent
    {
        public Guid ExtractId { get; set; }
        public ExtractProgress Progress { get; set; }

        public CbsNotification(Guid extractId, ExtractProgress progress)
        {
            ExtractId = extractId;
            Progress = progress;
        }

        public CbsNotification(ExtractProgress progress)
        {
            Progress = progress;
            ExtractId = Guid.Empty;
        }
    }
}
