using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch
{
    public interface ITempMnchLabExtractRepository : IRepository<TempMnchLabExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempMnchLabExtract> extracts);
    }
}
