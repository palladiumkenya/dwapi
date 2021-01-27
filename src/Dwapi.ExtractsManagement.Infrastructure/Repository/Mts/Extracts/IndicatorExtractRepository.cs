using System;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Mts.Extracts
{
    public class IndicatorExtractRepository: BaseRepository<IndicatorExtract, Guid>,IIndicatorExtractRepository
    {
        public IndicatorExtractRepository(ExtractsContext context) : base(context)
        {
        }
    }
}
