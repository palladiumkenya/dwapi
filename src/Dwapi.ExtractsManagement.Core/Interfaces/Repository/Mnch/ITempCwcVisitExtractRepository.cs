using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch
{
    public interface ITempCwcVisitExtractRepository : IRepository<TempCwcVisitExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempCwcVisitExtract> extracts);
    }
}
