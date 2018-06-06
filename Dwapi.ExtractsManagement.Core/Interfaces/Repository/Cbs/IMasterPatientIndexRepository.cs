using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs
{
    public interface IMasterPatientIndexRepository : IRepository<MasterPatientIndex, Guid>
    {
        bool BatchInsert(IEnumerable<MasterPatientIndex> extracts);
        IEnumerable<MasterPatientIndex> GetView();
    }
}