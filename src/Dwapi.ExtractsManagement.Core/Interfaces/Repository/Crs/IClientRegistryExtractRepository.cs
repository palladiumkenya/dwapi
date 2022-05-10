using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs
{
    public interface IClientRegistryExtractRepository : IRepository<ClientRegistryExtract, Guid>
    {
        bool BatchInsert(IEnumerable<ClientRegistryExtract> extracts);
        IEnumerable<ClientRegistryExtract> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
}