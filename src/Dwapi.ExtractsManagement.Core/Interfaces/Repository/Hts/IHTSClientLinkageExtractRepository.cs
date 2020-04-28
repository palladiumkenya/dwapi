using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface IHTSClientLinkageExtractRepository : IRepository<HTSClientLinkageExtract, Guid>
    {
        bool BatchInsert(IEnumerable<HTSClientLinkageExtract> extracts);
        IEnumerable<HTSClientExtract> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
}