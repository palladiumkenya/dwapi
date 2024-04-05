using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch
{

public interface IPatientMnchExtractRepository: IRepository<PatientMnchExtract, Guid>{
    bool BatchInsert(IEnumerable<PatientMnchExtract> extracts); 
    void UpdateSendStatus(List<SentItem> sentItems);
    long UpdateDiffSendStatus();}
}
