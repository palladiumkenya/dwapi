using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface IHtsRiskScoresRepository : IRepository<HtsRiskScores, Guid>
    {
        bool BatchInsert(IEnumerable<HtsRiskScores> extracts);
        IEnumerable<HtsRiskScores> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }
}