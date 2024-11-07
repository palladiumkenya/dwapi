using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
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
    public class PatientExtractRepository : BaseRepository<PatientExtract, Guid>,IPatientExtractRepository
    {
        public PatientExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<PatientExtract> extracts)
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
                throw;
            }
        }

        public void UpdateSendStatus(List<SentItem> sentItems)
        {
            var mpi = GetAll(x => sentItems.Select(i => i.Id).Contains(x.Id))
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
        }

        public int GetSent(Guid domainEventPatientExtractId)
        {
            var count = DbSet.AsNoTracking().Where(x => x.Status == "Sent").Select(x => x.Id).Count();
            return count;
        }

        public long UpdateDiffSendStatus()
        {
            int totalUpdated;

            using (var cn = GetNewConnection())
            {

                var sql = $@"
                UPDATE
                    {nameof(ExtractsContext.PatientExtracts)}
                SET
                    {nameof(PatientExtract.Status)}=@Status,{nameof(PatientExtract.StatusDate)}=@StatusDate
                ";

                totalUpdated = cn.Execute(sql, new {Status = nameof(SendStatus.Sent), StatusDate = DateTime.Now});
            }

            return totalUpdated;
        }
        
        public int GetSiteCode()
        {
            int sitecode  = Get(x => x.SiteCode!=null).SiteCode;

            // using (var cn = GetNewConnection())
            // {
            //
            //     var sql = $@"
            //             select SiteCode from {nameof(ExtractsContext.PatientExtracts)}
            //         {nameof(ExtractsContext.PatientExtracts)}                
            //     ";
            //
            //     totalUpdated = cn.Execute(sql, new {Status = nameof(SendStatus.Sent), StatusDate = DateTime.Now});
            // }

            return sitecode;
        }
        
        
        public string GetFacilityName()
        {
            string name  = Get(x => x.FacilityName!=null).FacilityName;
            
            return name;
        }
        
    }
}
