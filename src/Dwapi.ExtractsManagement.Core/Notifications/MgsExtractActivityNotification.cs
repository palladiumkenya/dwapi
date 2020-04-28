using System;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
    public  class MgsExtractActivityNotification : IDomainEvent
    {
        public Guid ExtractId { get; set; }
        public DwhProgress Progress { get; set; }

        public MgsExtractActivityNotification(Guid extractId, DwhProgress progress)
        {
            ExtractId = extractId;
            Progress = progress;
        }
    }
}
