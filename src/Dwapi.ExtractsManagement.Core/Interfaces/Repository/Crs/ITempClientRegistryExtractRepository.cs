using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.ExtractsManagement.Core.Model.Source.Crs;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs
{
    public interface ITempClientRegistryExtractRepository : IRepository<TempClientRegistryExtract,Guid>
    {
        Task Clear();
        bool BatchInsert(IEnumerable<TempClientRegistryExtract> extracts);
    }
}
