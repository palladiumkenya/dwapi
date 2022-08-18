using System;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
    public  class PrepExtractActivityNotification : IDomainEvent
    {
        public Guid ExtractId { get; set; }
        public DwhProgress Progress { get; set; }

        public PrepExtractActivityNotification(Guid extractId, DwhProgress progress)
        {
            ExtractId = extractId;
            Progress = progress;
        }
    }
}