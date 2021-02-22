using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts.Dto;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Mts.Extracts
{
    public class IndicatorExtractRepository: BaseRepository<IndicatorExtract, Guid>,IIndicatorExtractRepository
    {
        public IndicatorExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public IEnumerable<IndicatorExtractDto> Load()
        {
            var sql = $@"
                select e.*,k.Description,k.Rank
                from IndicatorExtracts e left outer join IndicatorKeys k on e.Indicator=k.Id
            ";
            return Context.Database.GetDbConnection().Query<IndicatorExtractDto>(sql).ToList();
        }
    }
}
