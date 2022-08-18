using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface IHtsEligibilityExtractRepository : IRepository<HtsEligibilityExtract, Guid>
    {
        bool BatchInsert(IEnumerable<HtsEligibilityExtract> extracts);
        IEnumerable<HtsEligibilityExtract> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
}