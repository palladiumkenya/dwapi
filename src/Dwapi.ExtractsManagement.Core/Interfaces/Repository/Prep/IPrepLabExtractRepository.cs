using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep
{
    public interface IPrepLabExtractRepository: IRepository<PrepLabExtract, Guid>{bool BatchInsert(IEnumerable<PrepLabExtract> extracts); void UpdateSendStatus(List<SentItem> sentItems);long UpdateDiffSendStatus();}
}