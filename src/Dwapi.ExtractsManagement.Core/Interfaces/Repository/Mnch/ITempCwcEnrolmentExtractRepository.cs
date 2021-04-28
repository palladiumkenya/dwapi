using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch
{
    public interface ITempCwcEnrolmentExtractRepository : IRepository<TempCwcEnrolmentExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempCwcEnrolmentExtract> extracts);
    }
}
