using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface ITempPatientStatusExtractRepository : IRepository<TempPatientStatusExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempPatientStatusExtract> extracts);
    }
}