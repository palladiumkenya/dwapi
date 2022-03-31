using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Prep;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep
{
    public interface ITempPatientPrepExtractRepository: IRepository<TempPatientPrepExtract, Guid>{bool BatchInsert(IEnumerable<TempPatientPrepExtract> extracts);}
}
