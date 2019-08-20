using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface IHtsClientsLinkageExtractRepository : IRepository<HtsClientLinkage, Guid>
    {
        bool BatchInsert(IEnumerable<HtsClientLinkage> extracts);
        IEnumerable<HtsClientLinkage> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
   
}
