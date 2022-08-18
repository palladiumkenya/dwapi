using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Source.Prep;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep
{
    public interface ITempPrepLabExtractRepository: IRepository<TempPrepLabExtract, Guid>{bool BatchInsert(IEnumerable<TempPrepLabExtract> extracts);}
}