using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts.Dto;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts
{
    public interface IIndicatorExtractRepository : IRepository<IndicatorExtract, Guid>
    {
        IEnumerable<IndicatorExtractDto> Load();
        Boolean CheckIfStale();
    }
}
