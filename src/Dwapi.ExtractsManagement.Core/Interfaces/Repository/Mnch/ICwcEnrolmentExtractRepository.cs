using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch
{
    public interface ICwcEnrolmentExtractRepository: IRepository<CwcEnrolmentExtract, Guid>{bool BatchInsert(IEnumerable<CwcEnrolmentExtract> extracts); void UpdateSendStatus(List<SentItem> sentItems);long UpdateDiffSendStatus();}
}