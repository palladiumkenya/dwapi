using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.TempExtracts
{
    public class TempHtsClientsExtractRepository : BaseRepository<TempHtsClients, Guid>, ITempHtsClientsExtractRepository
    {
        public TempHtsClientsExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<TempHtsClients> extracts)
        {
            var cn = GetConnectionString();
            try
            {
                if (Context.Database.ProviderName.ToLower().Contains("SqlServer".ToLower()))
                {
                    using (var connection = new SqlConnection(cn))
                    {
                        connection.BulkInsert(extracts);
                        return true;
                    }
                }

                if (Context.Database.ProviderName.ToLower().Contains("MySql".ToLower()))
                {
                    using (var connection = new MySqlConnection(cn))
                    {
                        connection.BulkInsert(extracts);
                        return true;
                    }
                }

                if (Context.Database.IsSqlite())
                {
                    using (var connection = new SqliteConnection(cn))
                    {
                        connection.BulkInsert(extracts);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Log.Error(e, "Failed batch insert");
                return false;
            }
        }

        public async Task Clear()
        {
            var cn = GetConnection();

            var truncates = new List<string>
            {
                nameof(ExtractsContext.TempHtsClientTestsExtracts),
                nameof(ExtractsContext.TempHtsTestKitsExtracts),
                nameof(ExtractsContext.TempHtsClientsLinkageExtracts),
                nameof(ExtractsContext.TempHtsPartnerTracingExtracts),
                nameof(ExtractsContext.TempHtsClientTracingExtracts),
                nameof(ExtractsContext.TempHtsPartnerNotificationServicesExtracts),
                nameof(ExtractsContext.TempHtsClientsExtracts),
                nameof(ExtractsContext.TempHtsEligibilityExtracts),

                nameof(ExtractsContext.HtsClientTestsExtracts),
                nameof(ExtractsContext.HtsTestKitsExtracts),
                nameof(ExtractsContext.HtsClientsLinkageExtracts),
                nameof(ExtractsContext.HtsPartnerTracingExtracts),
                nameof(ExtractsContext.HtsClientTracingExtracts),
                nameof(ExtractsContext.HtsPartnerNotificationServicesExtracts),
                nameof(ExtractsContext.HtsEligibilityExtracts),

            };

            var deletes = new List<string> {nameof(ExtractsContext.HtsClientsExtracts)};

            var truncateCommands = truncates.Select(x => GetSqlCommand(cn, $"TRUNCATE TABLE {x};"));

            foreach (var truncateCommand in truncateCommands)
            {
                await truncateCommand;
            }

            var deleteCommands = deletes.Select(d => GetSqlCommand(cn, $"DELETE FROM {d};"));

            foreach (var deleteCommand in deleteCommands)
            {
                await deleteCommand;
            }

            CloseConnection(cn);
        }

        public Task<int> GetCleanCount()
        {
            return DbSet.AsNoTracking()
                .Where(a => a.ErrorType == 0)
                .Select(x => x.Id)
                .CountAsync();
        }
        private Task<int> GetSqlCommand(IDbConnection cn, string sql)
        {
            if (cn is SqliteConnection)
                return cn.ExecuteAsync(sql.Replace("TRUNCATE TABLE", "DELETE FROM"));

            return cn.ExecuteAsync(sql);
        }

    }
}
