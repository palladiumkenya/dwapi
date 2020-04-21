using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Interfaces;
using Humanizer;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface ITempPatientExtractRepository : IRepository<TempPatientExtract,Guid>
    {
        bool BatchInsert(IEnumerable<TempPatientExtract> extracts);
        Task<int> Clear();
        Task<int> GetCleanCount();
    }
}
