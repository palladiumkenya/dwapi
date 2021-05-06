using System;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Source.Mts;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts
{
    public interface ITempIndicatorExtractRepository : IRepository<TempIndicatorExtract,Guid>
    {
        Task Clear();
    }
}
