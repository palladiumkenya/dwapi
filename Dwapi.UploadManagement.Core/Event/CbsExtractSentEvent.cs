using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Event
{
    public class CbsExtractSentEvent : IDomainEvent
    {
        public List<SentItem> SentItems { get; }

        public CbsExtractSentEvent(List<Guid> sentIds,SendStatus status, string statusInfo="")
        {
            SentItems = sentIds.Select(x => new SentItem(x,status, statusInfo)).ToList();
        }
        public CbsExtractSentEvent(List<SentItem> sentItems)
        {
            SentItems = sentItems;
        }
    }
}