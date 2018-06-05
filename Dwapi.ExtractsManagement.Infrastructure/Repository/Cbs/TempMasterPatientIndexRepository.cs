using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Cbs
{
    public class TempMasterPatientIndexRepository : BaseRepository<TempMasterPatientIndex,Guid> ,ITempMasterPatientIndexRepository
    {
        private SqlConnection _connection;

        public TempMasterPatientIndexRepository(ExtractsContext context) : base(context)
        {
        }

        public async Task Clear()
        {
            Log.Debug($"Executing ClearExtracts command...");
            
            var truncates = new List<string>{  "MasterPatientIndices","TempMasterPatientIndices"};

            using (_connection=new SqlConnection(GetConnectionString()))
            {
                if (_connection.State != ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }

                var command = _connection.CreateCommand();
                command.CommandTimeout = 0;

                var parallelTasks = new List<Task<int>>();

                foreach (var name in truncates)
                {
                    parallelTasks.Add(TruncateCommand(name));
                }

                await Task.WhenAll(parallelTasks);

            }
        }

        public bool BatchInsert(IEnumerable<TempMasterPatientIndex> extracts)
        {
            var cn = GetConnectionString();
            try
            {
                using (var connection = new SqlConnection(cn))
                {
                    connection.BulkInsert(extracts);
                    return true;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Log.Error(e, "Failed batch insert");
                return false;
            }
        }
        private Task<int> TruncateCommand(string extract)
        {
            var command = GetCommand(extract, "TRUNCATE TABLE");
            return command.ExecuteNonQueryAsync();
        }

        private Task<int> DeleteCommand(string extract)
        {
            var command = GetCommand(extract, "DELETE FROM");
            return command.ExecuteNonQueryAsync();
        }

        private SqlCommand GetCommand(string extract, string action)
        {
            var command = _connection.CreateCommand();
            command.CommandTimeout = 0;
            command.CommandText = $@" {action} {extract}; ";
            return command;
        }
    }
}