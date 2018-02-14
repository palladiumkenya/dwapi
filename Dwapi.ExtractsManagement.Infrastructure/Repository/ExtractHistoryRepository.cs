using System;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository
{
    public class ExtractHistoryRepository :BaseRepository<ExtractHistory,Guid>, IExtractHistoryRepository
    {
        public ExtractHistoryRepository(DbContext context) : base(context)
        {
        }

        public void Analyze(ExtractHistory extractHistory)
        {
            throw new NotImplementedException();
        }

        public void Load(Guid extractId)
        {
            throw new NotImplementedException();
        }

        public void Validate(Guid extractId)
        {
            throw new NotImplementedException();
        }

        public void Send(Guid extractId)
        {
            throw new NotImplementedException();
        }

        public void Reset(Guid extractId)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(ExtractHistory extractHistory, int stats)
        {
            var history = Get(extractHistory.Id);
                
            if (null == history)
            {
                //CreateOrUpdate(ExtractHistory.InitializeHistory());
                return;
            }

            //CreateOrUpdate(ExtractHistory.UpdateEvent(history,stats));

        }
    }
}