using System;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Model.Diff;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Indicator
{
    public class IndicatorExtractsRepository : BaseRepository<IndicatorExtract, Guid>, IIndicatorExtractsRepository
    {
        public IndicatorExtractsRepository(ExtractsContext context) : base(context)
        {
        }

        public int GetMflCode()
        {
            // var sql = $" select Indicator,IndicatorValue from IndicatorExtracts where Indicator='MFL_CODE' ";
            //
            // var result = Context.Database.GetDbConnection().QuerySingle(sql);
            // var code = Int32.Parse(result.IndicatorValue.Substring(0, 5));
            // return code;
            
            try
            {
                var sql = $" select Indicator,IndicatorValue from IndicatorExtracts where Indicator='MFL_CODE' ";

                var result = Context.Database.GetDbConnection().QuerySingle(sql);
                var code = Int32.Parse(result.IndicatorValue.Substring(0, 5));
                return code;

            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }

}
