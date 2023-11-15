using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Source.Mts;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Mts.TempExtracts
{
    public class TempIndicatorExtractRepository: BaseRepository<TempIndicatorExtract, Guid>, ITempIndicatorExtractRepository
    {
        private readonly IPatientExtractRepository _repository;

        public TempIndicatorExtractRepository(ExtractsContext context,IPatientExtractRepository repository) : base(context)
        {
            _repository = repository;

        }

        public async Task Clear()
        {
            var cn = GetConnection();

            var truncates = new List<string>
            {
                nameof(ExtractsContext.TempIndicatorExtracts),
                nameof(ExtractsContext.IndicatorExtracts)
            };

            int sitecode = _repository.GetSiteCode();
            var truncateCommands = truncates.Select(x => GetSqlCommand(cn, $"DELETE FROM {x} where SiteCode={sitecode} or SiteCode=0;"));

            foreach (var truncateCommand in truncateCommands)
            {
                await truncateCommand;
            }
            CloseConnection(cn);
        }

        private Task<int> GetSqlCommand(IDbConnection cn, string sql)
        {
            if (cn is SqliteConnection)
            {
                return cn.ExecuteAsync(sql.Replace("TRUNCATE TABLE", "DELETE FROM"));
            }
            return cn.ExecuteAsync(sql);
        }
    }
}
