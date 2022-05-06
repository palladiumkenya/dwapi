using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Prep;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep
{
    public interface ITempPrepVisitExtractRepository : IRepository<TempPrepVisitExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempPrepVisitExtract> extracts);
    }
}
