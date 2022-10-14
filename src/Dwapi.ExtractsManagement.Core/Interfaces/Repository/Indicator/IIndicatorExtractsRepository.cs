using System;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Model.Diff;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff
{
    public interface IIndicatorExtractsRepository:IRepository<IndicatorExtract,Guid>
    {
        int GetMflCode();

    }
}
