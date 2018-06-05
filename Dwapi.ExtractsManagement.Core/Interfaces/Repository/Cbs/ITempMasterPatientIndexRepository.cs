using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs
{
    public interface ITempMasterPatientIndexRepository : IRepository<TempMasterPatientIndex,Guid>
    {
        bool BatchInsert(IEnumerable<TempMasterPatientIndex> extracts);
    }
}