using System;
using Dapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Diff;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Diff
{
    public class DiffLogRepository : BaseRepository<DiffLog, Guid>, IDiffLogRepository
    {
        public DiffLogRepository(ExtractsContext context) : base(context)
        {
        }

        public DiffLog GetLog(string docket, string extract)
        {
            return Get(x =>
                x.Docket.ToLower() == docket.ToLower()
                && x.Extract.ToLower() == extract.ToLower());
        }
        public DiffLog GetIfHasBeenSentBeforeLog(string docket, string extract)
        {
            return Get(x =>
                x.Docket.ToLower() == docket.ToLower() &&
                x.LastSent == null && x.Extract.ToLower() == extract.ToLower());
        }
        
        public DiffLog GetIfChangesHasBeenLoadedAlreadyLog(string docket, string extract)
        {
            return Get(x =>
                x.Docket.ToLower() == docket.ToLower() && x.Extract.ToLower() == extract.ToLower() &&
                x.ExtractsSent == false && x.ChangesLoaded==true);
        }
        
        public DiffLog GetIfLoadedAllLog(string docket, string extract)
        {
            return Get(x =>
                x.Docket.ToLower() == docket.ToLower() && x.Extract.ToLower() == extract.ToLower() &&
                x.ExtractsSent == false && x.ChangesLoaded==false);
        }

        public DiffLog InitLog(string docket, string extract, int siteCode)
        {
            var diffLog = Get(x =>
                x.Docket.ToLower() == docket.ToLower() &&
                x.Extract.ToLower() == extract.ToLower() &&
                x.SiteCode == siteCode);

            if (null == diffLog)
            {
                diffLog = DiffLog.Create(docket, extract, siteCode);
                Create(diffLog);
                SaveChanges();
            }

            return diffLog;
        }
        
        public void UpdateExtractsSentStatus(string docket, string extract, bool status)
        {
            var diffLog = Get(x =>
                x.Docket.ToLower() == docket.ToLower() &&
                x.Extract.ToLower() == extract.ToLower() );

            if (null != diffLog)
            {
                diffLog.ChangesLoaded = status;
                diffLog.ExtractsSent = false;
                SaveChanges();
                Context.Database.GetDbConnection().BulkMerge(diffLog);
            }
        }

        public void SaveLog(DiffLog diffLog)
        {
            Context.Database.GetDbConnection().BulkMerge(diffLog);
        }

        public DiffLog GenerateDiff(string docket,string extract, int siteCode)
        {
            var diffLog = DiffLog.Create(docket, extract, siteCode);

            var sql =
                $"SELECT MAX({nameof(PatientExtract.Date_Created)}) {nameof(PatientExtract.Date_Created)},MAX({nameof(PatientExtract.Date_Last_Modified)}) {nameof(PatientExtract.Date_Last_Modified)} FROM {extract}";

            var extractDates = Context.Database.GetDbConnection().QuerySingle(sql);

            if (null != extractDates)
            {

                diffLog.MaxCreated = Extentions.CastDateTime(extractDates.Date_Created);
                diffLog.MaxModified = Extentions.CastDateTime(extractDates.Date_Last_Modified);

            }

            return diffLog;
        }
    }
}
