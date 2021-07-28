using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Event.Mnch
{
    public class MnchExtractSentEvent : IDomainEvent
    {
        public List<SentItem> SentItems { get; }

        public MnchExtractSentEvent(List<Guid> sentIds,SendStatus status,string extract, string statusInfo="")
        {
            SentItems = sentIds.Select(x => new SentItem(x,status, statusInfo,extract)).ToList();
        }

        public MnchExtractSentEvent(List<SentItem> sentItems)
        {

            SentItems = sentItems;
        }
    }
}
