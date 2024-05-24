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
        // Task<int> ClearSendingHistory(List<Guid> extractIds);
        IEnumerable<ExtractHistory> CheckWhichWasNotSent(List<Guid> extractIds);

        ExtractHistory GetLatest(Guid extractId);
        ExtractHistory GetLatest(Guid extractId,ExtractStatus status,ExtractStatus otherStatus);
        IEnumerable<ExtractHistory> GetAllExtractStatus(Guid extractId);
        void UpdateStatus(Guid extractId, ExtractStatus status,int? stats=null,string statusInfo="", bool express = false);
        void DwhUpdateStatus(Guid extractId, ExtractStatus status, int? stats = null, string statusInfo = "");
        void Complete(Guid extractId);
        int ProcessExcluded(Guid extractId,int rejectedCount,DbExtract extract,bool checkDb=true);
        int ProcessRejected(Guid extractId,int rejectedCount,DbExtract extract,bool checkDb=true);
        int ProcessExcluded(Guid extractId,int rejectedCount,int excludedCount);
        int GetSiteCode(string tempTableName);
    }
}
