using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs
{
    public class TempMasterPatientIndexRepository : BaseRepository<TempMasterPatientIndex,Guid> ,ITempMasterPatientIndexRepository
    {
        public TempMasterPatientIndexRepository(ExtractsContext context) : base(context)
        {
        }

        public async Task Clear()
        {
            Log.Debug($"Executing ClearExtracts command...");

            var cn = GetConnection();

            var truncates = new List<string>
            {
                nameof(ExtractsContext.MasterPatientIndices),
                nameof(ExtractsContext.TempMasterPatientIndices)
            };

            var truncateCommands = truncates.Select(x => GetSqlCommand(cn, $"TRUNCATE TABLE {x};"));

            foreach (var truncateCommand in truncateCommands)
            {
                await truncateCommand;
            }
          
            CloseConnection(cn);
        }

        public bool BatchInsert(IEnumerable<TempMasterPatientIndex> extracts)
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
            return cn.ExecuteAsync(sql);
        }
    }
}