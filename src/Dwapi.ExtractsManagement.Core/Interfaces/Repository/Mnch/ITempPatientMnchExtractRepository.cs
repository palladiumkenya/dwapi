using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch
{

        public interface ITempPatientMnchExtractRepository : IRepository<TempPatientMnchExtract, Guid>
        {
                bool BatchInsert(IEnumerable<TempPatientMnchExtract> extracts);
                Task<int> Clear();
                Task<int> GetCleanCount();
                int GetSiteCode();
        }
}

