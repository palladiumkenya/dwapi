﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class RelationshipsExtractRepository : BaseRepository<RelationshipsExtract, Guid>, IRelationshipsExtractRepository
    {
        public RelationshipsExtractRepository(ExtractsContext context) : base(context)
        {
        }

        public bool BatchInsert(IEnumerable<RelationshipsExtract> extracts)
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
        public void UpdateSendStatus(List<SentItem> sentItems)
        {
            var sql = $"SELECT * FROM {nameof(ExtractsContext.RelationshipsExtracts)} Where Id IN @Ids";
            var ids = sentItems.Select(x => x.Id).ToArray();

            using (var cn = GetNewConnection())
            {

                var mpi = cn.Query<RelationshipsExtract>(sql, new { Ids = ids }).ToList()
                    .Select(x =>
                    {
                        var sentItem = sentItems.First(s => s.Id == x.Id);
                        x.Status = $"{sentItem.Status}";
                        x.StatusDate = sentItem.StatusDate;
                        return x;
                    }).ToList();


                cn.BulkUpdate(mpi);
            }
        }

        public long UpdateDiffSendStatus()
        {
            int totalUpdated;

            using (var cn = GetNewConnection())
            {

                var sql = $@"
                UPDATE
                    {nameof(ExtractsContext.PatientAdverseEventExtracts)}
                SET
                    {nameof(RelationshipsExtract.Status)}=@Status,{nameof(RelationshipsExtract.StatusDate)}=@StatusDate
                where
                    {nameof(RelationshipsExtract.PatientPK)} in (select PatientPK from {nameof(ExtractsContext.PatientExtracts)}) AND
                    {nameof(RelationshipsExtract.SiteCode)} in (select SiteCode from {nameof(ExtractsContext.PatientExtracts)})
                ";

                totalUpdated = cn.Execute(sql, new {Status = nameof(SendStatus.Sent), StatusDate = DateTime.Now});
            }

            return totalUpdated;
        }
    }
}
