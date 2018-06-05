using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface IPatientExtractRepository : IRepository<PatientExtract, Guid>
    {
        bool BatchInsert(IEnumerable<PatientExtract> extracts);
    }
}