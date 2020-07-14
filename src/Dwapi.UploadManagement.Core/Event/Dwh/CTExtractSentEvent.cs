using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.UploadManagement.Core.Event.Dwh
{
    public class CTExtractSentEvent : IDomainEvent
    {
        public List<SentItem> SentItems { get; }

        public CTExtractSentEvent(List<Guid> sentIds,SendStatus status,ExtractType extractType, string statusInfo="")
        {
            SentItems = sentIds.Select(x => new SentItem(x,status, statusInfo,extractType)).ToList();
        }
    }
}
