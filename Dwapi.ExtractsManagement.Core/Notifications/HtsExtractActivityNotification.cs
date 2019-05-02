using System;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
    public  class HtsExtractActivityNotification : IDomainEvent
    {
        public Guid ExtractId { get; set; }
        public DwhProgress Progress { get; set; }

        public HtsExtractActivityNotification(Guid extractId, DwhProgress progress)
        {
            ExtractId = extractId;
            Progress = progress;
        }
    }
}