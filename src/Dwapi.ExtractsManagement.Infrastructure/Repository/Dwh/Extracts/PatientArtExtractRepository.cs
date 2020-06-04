using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Dwh.Extracts
{
    public class PatientArtExtractRepository : BaseRepository<PatientArtExtract, Guid>, IPatientArtExtractRepository
    {
        public PatientArtExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<PatientArtExtract> extracts)
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

        public IEnumerable<PatientArtExtract> BatchGet()
        {
            var cn = GetConnectionString();
            var patientArtExtracts = new List<PatientArtExtract>();
            if (Context.Database.ProviderName.ToLower().Contains("SqlServer".ToLower()))
            {
                using (var connection = new SqlConnection(cn))
                {
                    patientArtExtracts = (List<PatientArtExtract>) connection.GetAll<PatientArtExtract>();
                }
            }

            if (Context.Database.ProviderName.ToLower().Contains("MySql".ToLower()))
            {
                using (var connection = new MySqlConnection(cn))
                {
                    patientArtExtracts = (List<PatientArtExtract>) connection.GetAll<PatientArtExtract>();
                }
            }
            return patientArtExtracts;
        }

        public void UpdateSendStatus(List<SentItem> sentItems)
        {
            var extracts = GetAll(x => sentItems.Select(i => i.Id).Contains(x.Id)).ToList();

            var mpi = extracts
                .Select(x =>
                {
                    var sentItem = sentItems.First(s => s.Id == x.Id);
                    x.Status = $"{sentItem.Status}";
                    x.StatusDate = sentItem.StatusDate;
                    return x;
                });

            var cn = GetConnection();
            cn.BulkUpdate(mpi);
            CloseConnection(cn);

            var successOnly = sentItems.Where(x => x.Status == SendStatus.Sent).Select(x => x.Id).ToList();

            var pks = extracts
                .Where(x => successOnly.Contains(x.Id))
                .Select(x => new PatientSiteCodeDto()
                    {PatientPK = x.PatientPK, SiteCode = x.SiteCode});

            UpdatePatientSendStatus(pks);
        }

        private void UpdatePatientSendStatus(IEnumerable<PatientSiteCodeDto> patientSiteCodeDtos)
        {
            var sitePks = patientSiteCodeDtos
                .GroupBy(x => x.SiteCode)
                .ToList();

            foreach (var sitePk in sitePks)
            {
                string sql = @"
                    update PatientExtracts
                    set Status=@Status,StatusDate=@StatusDate
                    where SiteCode=@SiteCode
                    and PatientPK in @PatientPK and Status is null
                ";
                using (var connection= GetNewConnection())
                {
                    connection.Execute(sql,
                        new
                        {
                            Status = "Sent", StatusDate = DateTime.Now, SiteCode = sitePk.Key, PatientPK = sitePk.Select(x=>x.PatientPK).ToArray()
                        });
                }

            }
        }
    }
}
