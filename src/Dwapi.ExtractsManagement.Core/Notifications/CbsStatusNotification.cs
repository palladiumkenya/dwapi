using System;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
    public class CbsStatusNotification : IDomainEvent
    {
        public Guid ExtractId { get; }
        public ExtractStatus Status { get; }
        public int? Stats { get;  }
        public string StatusInfo { get; }

        public CbsStatusNotification(Guid extractId, ExtractStatus status, int? stats=null, string statusInfo="")
        {
            ExtractId = extractId;
            Status = status;
            Stats = stats;
            StatusInfo = statusInfo;
        }
    }
}
