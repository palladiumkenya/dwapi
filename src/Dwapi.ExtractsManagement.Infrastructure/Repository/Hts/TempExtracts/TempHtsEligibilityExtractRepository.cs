using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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
    public class TempHtsEligibilityExtractRepository: BaseRepository<TempHtsEligibilityExtract, Guid>, ITempHtsEligibilityExtractRepository
    {
        public TempHtsEligibilityExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public bool BatchInsert(IEnumerable<TempHtsEligibilityExtract> extracts)
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

        public Task<int> GetCleanCount()
        {
            int count = 0;
            using (var cn=GetNewConnection())
            {
                var query = QueryUtil.EligibilityCount;
                count = cn.ExecuteScalar<int>(query);
            }
            return Task.FromResult(count);
        }
    }
}