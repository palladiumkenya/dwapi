using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs
{
    public interface ITempMetricMigrationExtractRepository : IRepository<TempMetricMigrationExtract,Guid>
    {
        Task Clear();
        bool BatchInsert(IEnumerable<TempMetricMigrationExtract> extracts);
    }
}
