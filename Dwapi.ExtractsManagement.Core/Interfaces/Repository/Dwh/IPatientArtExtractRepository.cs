using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface IPatientArtExtractRepository : IRepository<PatientArtExtract, Guid>
    {
        bool BatchInsert(IEnumerable<PatientArtExtract> extracts);
        IEnumerable<PatientArtExtract> BatchGet();
    }
}