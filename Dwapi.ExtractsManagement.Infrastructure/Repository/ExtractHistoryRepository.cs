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

        public void UpdateStatus(Guid extractId, ExtractStatus status,int? stats, string statusInfo = "")
        {
            var started= DbSet.Any(x => x.ExtractId == extractId&&x.Status==status);

            if (!started)
            {
                var history=new ExtractHistory(status, stats,statusInfo);
                Create(history);
            }

        }
    }
}