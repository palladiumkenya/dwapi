using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface IHTSClientPartnerExtractRepository : IRepository<HTSClientPartnerExtract, Guid>
    {
        bool BatchInsert(IEnumerable<HTSClientPartnerExtract> extracts);
        IEnumerable<HTSClientExtract> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
}