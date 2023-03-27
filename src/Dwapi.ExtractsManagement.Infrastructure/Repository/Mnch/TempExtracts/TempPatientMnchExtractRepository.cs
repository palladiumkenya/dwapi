using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Mnch.TempExtracts
{
    public class TempPatientMnchExtractRepository : BaseRepository<TempPatientMnchExtract, Guid>,
        ITempPatientMnchExtractRepository
    {
        public TempPatientMnchExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<TempPatientMnchExtract> extracts)
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

        public async Task<int> Clear()
        {
            var cn = GetConnection();

            var truncates = new List<string>
            {
                nameof(ExtractsContext.TempPatientMnchExtracts),
                nameof(ExtractsContext.TempMnchEnrolmentExtracts),
                nameof(ExtractsContext.TempMnchArtExtracts),
                nameof(ExtractsContext.TempAncVisitExtracts),
                nameof(ExtractsContext.TempMatVisitExtracts),
                nameof(ExtractsContext.TempPncVisitExtracts),
                nameof(ExtractsContext.TempMotherBabyPairExtracts),
                nameof(ExtractsContext.TempCwcEnrolmentExtracts),
                nameof(ExtractsContext.TempCwcVisitExtracts),
                nameof(ExtractsContext.TempHeiExtracts),
                nameof(ExtractsContext.TempMnchLabExtracts),
                nameof(ExtractsContext.TempMnchImmunizationExtracts),

                nameof(ExtractsContext.MnchEnrolmentExtracts),
                nameof(ExtractsContext.MnchArtExtracts),
                nameof(ExtractsContext.AncVisitExtracts),
                nameof(ExtractsContext.MatVisitExtracts),
                nameof(ExtractsContext.PncVisitExtracts),
                nameof(ExtractsContext.MotherBabyPairExtracts),
                nameof(ExtractsContext.CwcEnrolmentExtracts),
                nameof(ExtractsContext.CwcVisitExtracts),
                nameof(ExtractsContext.HeiExtracts),
                nameof(ExtractsContext.MnchLabExtracts),
                nameof(ExtractsContext.MnchImmunizationExtracts),

            };

            var deletes = new List<string> {nameof(ExtractsContext.PatientMnchExtracts)};

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

            return 1;
        }

        public Task<int> GetCleanCount()
        {
            return DbSet.AsNoTracking()
                .Where(a => a.ErrorType == 0)
                .Select(x => x.Id)
                .CountAsync();
        }
        public int GetSiteCode()
        {
            try
            {
                int sitecode = (int) DbSet.AsNoTracking()
                    .Where(a => a.ErrorType == 0)
                    .Select(x => x.SiteCode)
                    .ToList().FirstOrDefault();
                return sitecode;
            }
            catch (Exception e)
            {
                return 99999;
            }
        }
        
        private Task<int> GetSqlCommand(IDbConnection cn, string sql)
        {
            if (cn is SqliteConnection)
                return cn.ExecuteAsync(sql.Replace("TRUNCATE TABLE", "DELETE FROM"));

            return cn.ExecuteAsync(sql);
        }
    }
}
