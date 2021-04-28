using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch
{
    public interface ITempMnchArtExtractRepository : IRepository<TempMnchArtExtract, Guid>
    {
        bool BatchInsert(IEnumerable<TempMnchArtExtract> extracts);
    }
}
