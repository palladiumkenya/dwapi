using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.TempExtracts
{
    public class TempPatientExtractRepository : BaseRepository<TempPatientExtract, Guid>, ITempPatientExtractRepository
    {
        public TempPatientExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<TempPatientExtract> extracts)
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

        public async Task<int>Clear()
        {
            var cn = GetConnection();

            var truncates = new List<string>
            {
                nameof(ExtractsContext.TempPatientExtracts),
                nameof(ExtractsContext.TempPatientArtExtracts),
                nameof(ExtractsContext.PatientArtExtracts),
                nameof(ExtractsContext.TempPatientBaselinesExtracts),
                nameof(ExtractsContext.PatientBaselinesExtracts),
                nameof(ExtractsContext.TempPatientStatusExtracts),
                nameof(ExtractsContext.PatientStatusExtracts),
                nameof(ExtractsContext.TempPatientLaboratoryExtracts),
                nameof(ExtractsContext.PatientLaboratoryExtracts),
                nameof(ExtractsContext.TempPatientPharmacyExtracts),
                nameof(ExtractsContext.PatientPharmacyExtracts),
                nameof(ExtractsContext.TempPatientVisitExtracts),
                nameof(ExtractsContext.PatientVisitExtracts),
                nameof(ExtractsContext.TempPatientAdverseEventExtracts),
                nameof(ExtractsContext.PatientAdverseEventExtracts),
                nameof(ExtractsContext.TempAllergiesChronicIllnessExtracts),
                nameof(ExtractsContext.AllergiesChronicIllnessExtracts),
                nameof(ExtractsContext.TempContactListingExtracts),
                nameof(ExtractsContext.ContactListingExtracts),
                nameof(ExtractsContext.TempDepressionScreeningExtracts),
                nameof(ExtractsContext.DepressionScreeningExtracts),
                nameof(ExtractsContext.TempDrugAlcoholScreeningExtracts),
                nameof(ExtractsContext.DrugAlcoholScreeningExtracts),
                nameof(ExtractsContext.TempEnhancedAdherenceCounsellingExtracts),
                nameof(ExtractsContext.EnhancedAdherenceCounsellingExtracts),
                nameof(ExtractsContext.TempGbvScreeningExtracts),
                nameof(ExtractsContext.GbvScreeningExtracts),
                nameof(ExtractsContext.TempIptExtracts),
                nameof(ExtractsContext.IptExtracts),
                nameof(ExtractsContext.TempOtzExtracts),
                nameof(ExtractsContext.OtzExtracts),
                nameof(ExtractsContext.TempOvcExtracts),
                nameof(ExtractsContext.OvcExtracts),

                nameof(ExtractsContext.TempCovidExtracts),
                nameof(ExtractsContext.CovidExtracts),
                nameof(ExtractsContext.TempDefaulterTracingExtracts),
                nameof(ExtractsContext.DefaulterTracingExtracts),
                nameof(ExtractsContext.TempCervicalCancerScreeningExtracts),
                nameof(ExtractsContext.CervicalCancerScreeningExtracts),
                nameof(ExtractsContext.TempIITRiskScoresExtracts),
                nameof(ExtractsContext.IITRiskScoresExtracts)

            };

            var deletes = new List<string> { nameof(ExtractsContext.PatientExtracts) };

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
            return (int) DbSet.AsNoTracking()
                .Where(a => a.ErrorType == 0)
                .Select(x => x.SiteCode)
                .ToList().FirstOrDefault();
            // return DbSet.AsNoTracking()
            //     .Where(a => a.ErrorType == 0)
            //     .Select(x => x.SiteCode)
            //     .ToList();
            
        }

        private Task<int> GetSqlCommand(IDbConnection cn, string sql)
        {
            if (cn is SqliteConnection)
                return cn.ExecuteAsync(sql.Replace("TRUNCATE TABLE", "DELETE FROM"));

            return cn.ExecuteAsync(sql);
        }
    }
}
