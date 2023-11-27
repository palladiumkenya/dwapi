using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep
{
    public interface IPrepMonthlyRefillExtractRepository: IRepository<PrepMonthlyRefillExtract, Guid>{bool BatchInsert(IEnumerable<PrepMonthlyRefillExtract> extracts); void UpdateSendStatus(List<SentItem> sentItems);long UpdateDiffSendStatus();}
}