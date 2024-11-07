﻿using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.SharedKernel.Infrastructure.Repository;
using MySql.Data.MySqlClient;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.TempExtracts
{
    [Obsolete("Class is obsolete,use TempHtsClientsExtractRepository")]
    public class TempHTSClientExtractRepository : BaseRepository<TempHTSClientExtract, Guid>, ITempHTSClientExtractRepository
    {
        public TempHTSClientExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<TempHTSClientExtract> extracts)
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
                nameof(ExtractsContext.TempHtsClientLinkageExtracts),
                nameof(ExtractsContext.TempHtsClientPartnerExtracts),
                nameof(ExtractsContext.TempHtsClientExtracts),
                nameof(ExtractsContext.HtsClientLinkageExtracts),
                nameof(ExtractsContext.HtsClientPartnerExtracts),
                nameof(ExtractsContext.HtsClientExtracts)
            };

         //   var deletes = new List<string> { nameof(ExtractsContext.PatientExtracts) };

            var truncateCommands = truncates.Select(x => GetSqlCommand(cn, $"TRUNCATE TABLE {x};"));

            foreach (var truncateCommand in truncateCommands)
            {
                await truncateCommand;
            }

          /*  var deleteCommands = deletes.Select(d => GetSqlCommand(cn, $"DELETE FROM {d};"));

            foreach (var deleteCommand in deleteCommands)
            {
                await deleteCommand;
            }
            */
            CloseConnection(cn);
        }

        private Task<int> GetSqlCommand(IDbConnection cn, string sql)
        {
            return cn.ExecuteAsync(sql);
        }
    }
}
