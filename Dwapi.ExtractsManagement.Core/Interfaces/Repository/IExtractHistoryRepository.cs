using System;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository
{
    public interface IExtractHistoryRepository : IRepository<ExtractHistory,Guid>
    {
       
        void UpdateStatus(ExtractHistory extractHistory,int stats);
    }
}