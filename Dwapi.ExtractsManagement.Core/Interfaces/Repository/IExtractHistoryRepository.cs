using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository
{
    public interface IExtractHistoryRepository : IRepository<ExtractHistory,Guid>
    {
        void ClearHistory(Guid extractId);
        Task<int> ClearHistory(List<Guid> extractIds);
        ExtractHistory GetLatest(Guid extractId);
        ExtractHistory GetLatest(Guid extractId,ExtractStatus status,ExtractStatus otherStatus);
        IEnumerable<ExtractHistory> GetAllExtractStatus(Guid extractId);
        void UpdateStatus(Guid extractId, ExtractStatus status,int? stats=null,string statusInfo="", bool express = false);
        void DwhUpdateStatus(Guid extractId, ExtractStatus status, int? stats = null, string statusInfo = "");
        void Complete(Guid extractId);
        int ProcessExcluded(Guid extractId,int rejectedCount,DbExtract extract);
        int ProcessExcluded(Guid extractId,int rejectedCount,int excludedCount);
    }
}
