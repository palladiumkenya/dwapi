using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository
{
    public class ExtractHistoryRepository :BaseRepository<ExtractHistory,Guid>, IExtractHistoryRepository
    {
        public ExtractHistoryRepository(ExtractsContext context) : base(context)
        {
        }

        public  void ClearHistory(Guid extractId)
        {
            Task.Run(() =>ClearHistory(new List<Guid> {extractId}));
        }

        public async Task<int> ClearHistory(List<Guid> extractIds)
        {
            var count = 0;
            var histories = DbSet
                .Where(x => extractIds.Contains(x.ExtractId)).ToList();
            if (histories.Any())
            {
                DbSet.RemoveRange(histories);
                count= await SaveChangesAsync();
            }

            return count;
        }

        public ExtractHistory GetLatest(Guid extractId)
        {
            return DbSet
                .Where(x => x.ExtractId == extractId)
                .OrderByDescending(x => x.StatusDate)
                .FirstOrDefault();
        }

        public ExtractHistory GetLatest(Guid extractId, ExtractStatus status, ExtractStatus otherStatus)
        {
            return DbSet
                .Where(x => x.ExtractId == extractId && (x.Status == status || x.Status == otherStatus))
                .OrderByDescending(x => x.StatusDate)
                .FirstOrDefault();
        }

        public IEnumerable<ExtractHistory> GetAllExtractStatus(Guid extractId)
        {
            List<ExtractHistory> histories;
            var sql = $"SELECT * FROM {nameof(ExtractsContext.ExtractHistory)} Where ExtractId = @extractId";

            using (var cn = GetNewConnection())
            {
                histories = cn.Query<ExtractHistory>(sql, new {ExtractId = extractId}).ToList();
            }

            return histories;
        }

        public void UpdateStatus(Guid extractId, ExtractStatus status,int? stats, string statusInfo = "", bool express = false)
        {
            if(express)
            {
                var history = new ExtractHistory(status, stats, statusInfo, extractId);
                Create(history);
                SaveChanges();
                return;
            }

            var started= DbSet.Any(x => x.ExtractId == extractId&&x.Status==status);

            if (!started)
            {
                var history=new ExtractHistory(status, stats,statusInfo,extractId);
                Create(history);
                SaveChanges();
            }
        }

        public void DwhUpdateStatus(Guid extractId, ExtractStatus status, int? stats, string statusInfo = "")
        {
                var history = new ExtractHistory(status, stats, statusInfo, extractId);
                CreateBatch(new List<ExtractHistory> {history});
        }

        public void Complete(Guid extractId)
        {
            var history = new ExtractHistory(ExtractStatus.Idle,extractId);
            CreateBatch(new List<ExtractHistory> {history});
        }
        
        public int GetSiteCode(string tempTableName)
        {
            var sql = $@"
                    select SiteCode
                    from {tempTableName}s a LIMIT 1";

            int sitecode = ExecQuery<int>(sql);
            return sitecode;
        }

        //public int ProcessExcluded(Guid extractId,int rejectedCount,DbExtract extract)
        //{
        //    var sql = $@"
        //            select count(PatientPK)
        //            from {extract.TempTableName}s a where a.PatientPk in (select PatientPK
        //            from {extract.MainName} where ErrorType=1 and a.SiteCode=SiteCode )
        //    ";

        //    int count = ExecQuery<int>(sql);

        //    Log.Debug(sql);

        //    DwhUpdateStatus(extractId, ExtractStatus.Excluded, count);
        //  //  DwhUpdateStatus(extractId, ExtractStatus.Rejected,rejectedCount-count);
        //  DwhUpdateStatus(extractId, ExtractStatus.Rejected,rejectedCount);

        //    return count;
        //}




        public int ProcessExcluded(Guid extractId,int rejectedCount,DbExtract extract, bool checkDb=true)
        {
            int count = 0;
            if (checkDb)
            {
                var sql = $@"
                    select count(id)
                    from {extract.TempTableName}s a where CheckError=1";

                count = ExecQuery<int>(sql);
                Log.Debug(sql);
            }

            DwhUpdateStatus(extractId, ExtractStatus.Excluded, count);
            //  DwhUpdateStatus(extractId, ExtractStatus.Rejected,rejectedCount-count);
            DwhUpdateStatus(extractId, ExtractStatus.Rejected,rejectedCount);

            return count;
        }

        public int ProcessExcluded(Guid extractId, int rejectedCount, int excludedCount)
        {
            DwhUpdateStatus(extractId, ExtractStatus.Excluded, excludedCount);
            DwhUpdateStatus(extractId, ExtractStatus.Rejected,rejectedCount);

            return excludedCount;
        }



        public int ProcessRejected(Guid extractId, int rejectedCount, DbExtract extract, bool checkDb=true)
        {
            int count = 0;
            if (checkDb)
            {
                var sql = $@" select count(a.PatientPK)
                    from {extract.TempTableName}s a 
                    inner join {extract.MainName} b on a.PatientPK=b.PatientPK and a.SiteCode=b.SiteCode
                    where a.ErrorType=1";
                count = ExecQuery<int>(sql);
                Log.Debug(sql);
            }

            DwhUpdateStatus(extractId, ExtractStatus.Excluded, count);
            //  DwhUpdateStatus(extractId, ExtractStatus.Rejected,rejectedCount-count);
            DwhUpdateStatus(extractId, ExtractStatus.Rejected, rejectedCount);

            return count;
        }



    }
}
