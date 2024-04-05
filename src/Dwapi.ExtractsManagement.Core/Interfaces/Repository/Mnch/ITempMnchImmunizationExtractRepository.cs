using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch
{
    public interface ITempMnchImmunizationExtractRepository : IRepository<TempMnchImmunizationExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempMnchImmunizationExtract> extracts);
    }
}
