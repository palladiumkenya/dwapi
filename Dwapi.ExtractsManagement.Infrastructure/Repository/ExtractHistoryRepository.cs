using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository
{
    public class ExtractHistoryRepository :BaseRepository<ExtractHistory,Guid>, IExtractHistoryRepository
    {
        public ExtractHistoryRepository(ExtractsContext context) : base(context)
        {
        }

        public void ClearHistory(Guid extractId)
        {
            var histories = DbSet
                .Where(x => x.ExtractId == extractId).ToList();
            if (histories.Count > 0)
            {
                DbSet.RemoveRange(histories);
                SaveChanges();
            }
        }

        public ExtractHistory GetLatest(Guid extractId)
        {
            return DbSet
                .Where(x => x.ExtractId == extractId)
                .OrderByDescending(x => x.StatusDate)
                .FirstOrDefault();
        }

        public void UpdateStatus(Guid extractId, ExtractStatus status,int? stats, string statusInfo = "")
        {
            var started= DbSet.Any(x => x.ExtractId == extractId&&x.Status==status);

            if (!started)
            {
                var history=new ExtractHistory(status, stats,statusInfo,extractId);
                Create(history);
                SaveChanges();
            }

        }
    }
}