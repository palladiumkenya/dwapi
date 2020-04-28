using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface IHtsTestKitsExtractRepository : IRepository<HtsTestKits, Guid>
    {
        bool BatchInsert(IEnumerable<HtsTestKits> extracts);
        IEnumerable<HtsTestKits> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
}
