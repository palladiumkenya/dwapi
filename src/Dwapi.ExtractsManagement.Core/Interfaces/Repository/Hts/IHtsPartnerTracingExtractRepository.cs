using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface IHtsPartnerTracingExtractRepository : IRepository<HtsPartnerTracing, Guid>
    {
        bool BatchInsert(IEnumerable<HtsPartnerTracing> extracts);
        IEnumerable<HtsPartnerTracing> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
}
