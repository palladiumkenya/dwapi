using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface
        ITempEnhancedAdherenceCounsellingExtractRepository : IRepository<TempEnhancedAdherenceCounsellingExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempEnhancedAdherenceCounsellingExtract> extracts);
    }
}