using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface ITempCervicalCancerScreeningExtractRepository: IRepository<TempCervicalCancerScreeningExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempCervicalCancerScreeningExtract> extracts);
    }
}