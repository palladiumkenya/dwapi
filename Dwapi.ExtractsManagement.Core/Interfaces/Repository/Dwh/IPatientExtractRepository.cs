using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface IPatientExtractRepository : IRepository<PatientExtract, Guid>
    {
        bool BatchInsert(IEnumerable<PatientExtract> extracts);
        void UpdateSendStatus(List<SentItem> sentItems);
    }
}