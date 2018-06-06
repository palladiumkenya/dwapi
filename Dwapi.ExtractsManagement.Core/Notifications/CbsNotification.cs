using System;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Notifications
{
    public class CbsNotification : IDomainEvent
    {
        public ExtractProgress Progress { get; set; }

        public CbsNotification(ExtractProgress progress)
        {
            Progress = progress;
        }
    }
}
