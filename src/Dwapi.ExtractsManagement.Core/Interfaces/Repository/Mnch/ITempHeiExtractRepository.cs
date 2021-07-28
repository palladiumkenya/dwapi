using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch
{
    public interface ITempHeiExtractRepository : IRepository<TempHeiExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempHeiExtract> extracts);
    }
}
