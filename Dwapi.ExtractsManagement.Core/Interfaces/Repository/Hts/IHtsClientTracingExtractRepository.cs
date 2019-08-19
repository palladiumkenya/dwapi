using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface IHtsClientTracingExtractRepository : IRepository<HtsClientTracing, Guid>
    {
        bool BatchInsert(IEnumerable<HtsClientTracing> extracts);
        IEnumerable<HtsClientTracing> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
}
