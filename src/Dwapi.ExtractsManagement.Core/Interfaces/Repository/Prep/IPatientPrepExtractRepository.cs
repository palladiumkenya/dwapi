using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep
{
    public interface IPatientPrepExtractRepository : IRepository<PatientPrepExtract, Guid>
    {
        bool BatchInsert(IEnumerable<PatientPrepExtract> extracts);
        void UpdateSendStatus(List<SentItem> sentItems);
        long UpdateDiffSendStatus();
    }
}
