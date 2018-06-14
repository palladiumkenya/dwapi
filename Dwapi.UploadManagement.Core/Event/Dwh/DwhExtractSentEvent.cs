using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Event.Dwh
{
    public class DwhExtractSentEvent : IDomainEvent
    {
        public List<SentItem> SentItems { get; }

        public DwhExtractSentEvent(List<Guid> sentIds,SendStatus status, string statusInfo="")
        {
            SentItems = sentIds.Select(x => new SentItem(x,status, statusInfo)).ToList();
        }
        public DwhExtractSentEvent(List<SentItem> sentItems)
        {

            SentItems = sentItems;
        }
    }
}