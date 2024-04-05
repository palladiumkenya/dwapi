using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface
        ITempRelationshipsExtractRepository : IRepository<TempRelationshipsExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempRelationshipsExtract> extracts);
    }
}
