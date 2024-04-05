﻿using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh
{
    public interface IIITRiskScoresExtractRepository : IRepository<IITRiskScoresExtract, Guid>
    {
        bool BatchInsert(IEnumerable<IITRiskScoresExtract> extracts);
        void UpdateSendStatus(List<SentItem> sentItems);
        long UpdateDiffSendStatus();
    }
}