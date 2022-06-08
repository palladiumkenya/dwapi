using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Event.Crs
{
    public class CrsExtractSentEvent : IDomainEvent
    {
        public List<SentItem> SentItems { get; }

        public CrsExtractSentEvent(List<Guid> sentIds,SendStatus status, string statusInfo="")
        {
            SentItems = sentIds.Select(x => new SentItem(x,status, statusInfo)).ToList();
        }
        public CrsExtractSentEvent(List<SentItem> sentItems)
        {

            SentItems = sentItems;
        }
    }
}