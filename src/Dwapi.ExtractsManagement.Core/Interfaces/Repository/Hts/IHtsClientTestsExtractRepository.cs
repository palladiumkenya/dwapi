using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface IHtsClientTestsExtractRepository : IRepository<HtsClientTests, Guid>
    {
        bool BatchInsert(IEnumerable<HtsClientTests> extracts);
        IEnumerable<HtsClientTests> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
  
}
