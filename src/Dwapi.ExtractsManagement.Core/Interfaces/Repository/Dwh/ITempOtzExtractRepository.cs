using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface ITempOtzExtractRepository : IRepository<TempOtzExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempOtzExtract> extracts);
    }
}