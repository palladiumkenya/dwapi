using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Event.Prep
{
    public class PrepExtractSentEvent : IDomainEvent
    {
        public List<SentItem> SentItems { get; }

        public PrepExtractSentEvent(List<Guid> sentIds,SendStatus status,string extract, string statusInfo="")
        {
            SentItems = sentIds.Select(x => new SentItem(x,status, statusInfo,extract)).ToList();
        }

        public PrepExtractSentEvent(List<SentItem> sentItems)
        {

            SentItems = sentItems;
        }
    }
}
