using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs;
using Dwapi.ExtractsManagement.Core.Model.Source.Crs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Crs
{
    public class TempClientRegistryExtractRepository : BaseRepository<TempClientRegistryExtract,Guid> ,ITempClientRegistryExtractRepository
    {
        public TempClientRegistryExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public async Task Clear()
        {
            Log.Debug($"Executing ClearExtracts command...");

            var cn = GetConnection();

            var truncates = new List<string>
            {
                nameof(ExtractsContext.ClientRegistryExtracts),
                nameof(ExtractsContext.TempClientRegistryExtracts)
            };

            var truncateCommands = truncates.Select(x => GetSqlCommand(cn, $"TRUNCATE TABLE {x};"));

            foreach (var truncateCommand in truncateCommands)
            {
                await truncateCommand;
            }

            CloseConnection(cn);
        }

        public bool BatchInsert(IEnumerable<TempClientRegistryExtract> extracts)
        {
            var cn = GetConnectionString();

            try
            {

                if (GetConnectionProvider()==DatabaseProvider.MySql)
                {
                    using (var connection = new MySqlConnection(cn))
                    {
                        connection.BulkInsert(extracts);

                    }
                }

                if (GetConnectionProvider() == DatabaseProvider.MsSql)
                {
                    using (var connection = new SqlConnection(cn))
                    {
                        connection.BulkInsert(extracts);
                    }
                }

                if (GetConnectionProvider() == DatabaseProvider.Other)
                {
                    using (var connection = new SqliteConnection(cn))
                    {
                        connection.BulkInsert(extracts);
                    }
                }

                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Log.Error(e, "Failed batch insert");
                return false;
            }
        }

        private Task<int> GetSqlCommand(IDbConnection cn, string sql)
        {
            if (cn is SqliteConnection)
            {
                return cn.ExecuteAsync(sql.Replace("TRUNCATE TABLE","DELETE FROM"));
            }
            return cn.ExecuteAsync(sql);
        }
    }
}
