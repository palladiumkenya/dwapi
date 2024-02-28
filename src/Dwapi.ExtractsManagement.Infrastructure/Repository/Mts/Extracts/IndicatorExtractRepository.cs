using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        public bool CheckIfStale()
        {
            var sql = $@"
                select e.*,k.Description,k.Rank
                from IndicatorExtracts e left outer join IndicatorKeys k on e.Indicator=k.Id where e.IndicatorValue='MISALIGNED'
            ";
            var result = Context.Database.GetDbConnection().Query(sql).ToList();

            if (result.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetMflCode()
        {
            
            try
            {
                var sql = $" select Indicator,IndicatorValue from IndicatorExtracts where Indicator='MFL_CODE' order by DateCreated desc";

                var query = Context.Database.GetDbConnection().Query(sql);
                var result = query.FirstOrDefault();
                var code = Int32.Parse(result.IndicatorValue.Substring(0, 5));
                return code;

            }
            catch (Exception e)
            {
                return 0;
            }
        }
        
        public IndicatorExtract GetIndicatorValue(string name)
        {
            return Get(x => x.Indicator.ToLower() == name.ToLower());
        }
        
        public Task<int> CountMetrics()
        {
            return GetCount();
        }
        
    }
}
