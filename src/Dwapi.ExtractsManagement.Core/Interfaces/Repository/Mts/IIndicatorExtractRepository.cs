using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts.Dto;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts
{
    public interface IIndicatorExtractRepository : IRepository<IndicatorExtract, Guid>
    {
        IEnumerable<IndicatorExtractDto> Load();
        Boolean CheckIfStale();
        int GetMflCode();
        IndicatorExtract GetIndicatorValue(string name);
        Task<int> CountMetrics();


    }
}
