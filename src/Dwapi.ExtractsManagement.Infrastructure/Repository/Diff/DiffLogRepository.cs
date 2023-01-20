using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
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

        public DiffLog GetLog(string docket, string extract, int siteCode)
        {
            return Get(x =>
                x.Docket.ToLower() == docket.ToLower()
                && x.Extract.ToLower() == extract.ToLower() && x.SiteCode==siteCode);
        }
        
        public List<DiffLog> GetAllDocketLogs(string docket)
        {
            return GetAll(x =>
                x.Docket.ToLower() == docket.ToLower()).ToList();
            // return logs;
        }
        
        public DiffLog GetIfHasBeenSentBeforeLog(string docket, string extract, int siteCode)
        {
            return Get(x =>
                x.Docket.ToLower() == docket.ToLower() &&
                x.LastSent == null && x.Extract.ToLower() == extract.ToLower() && x.SiteCode==siteCode);
        }
        
        public DiffLog GetIfChangesHasBeenLoadedAlreadyLog(string docket, string extract, int siteCode)
        {
            return Get(x =>
                x.Docket.ToLower() == docket.ToLower() && x.Extract.ToLower() == extract.ToLower() &&
                x.ExtractsSent == false && x.ChangesLoaded==true && x.SiteCode==siteCode);
        }
        
        public DiffLog GetIfLoadedAllLog(string docket, string extract, int siteCode)
        {
            return Get(x =>
                x.Docket.ToLower() == docket.ToLower() && x.Extract.ToLower() == extract.ToLower() &&
                x.ExtractsSent == false && x.ChangesLoaded==false && x.SiteCode==siteCode);
        }

        public DiffLog InitLog(string docket, string extract, int siteCode)
        {
            var sql =
                $"SELECT MAX({nameof(PatientExtract.Date_Created)}) {nameof(PatientExtract.Date_Created)},MAX({nameof(PatientExtract.Date_Last_Modified)}) {nameof(PatientExtract.Date_Last_Modified)} FROM {extract}s";

            var extractDates = Context.Database.GetDbConnection().QuerySingle(sql);

            var diffLog = Get(x =>
                x.Docket.ToLower() == docket.ToLower() &&
                x.Extract.ToLower() == extract.ToLower() &&
                x.SiteCode == siteCode);

            if (null == diffLog)
            {
                diffLog = DiffLog.Create(docket, extract, siteCode,extractDates.Date_Created,extractDates.Date_Last_Modified);
                // Create(diffLog);
                SaveChanges();
                Context.Database.GetDbConnection().BulkMerge(diffLog);
            }

            return diffLog;
        }
        
        public void UpdateExtractsSentStatus(string docket, string extract, bool status)
        {
            var diffLog = GetAll(x =>
                x.Docket.ToLower() == docket.ToLower()).ToList();

            if (null != diffLog)
            {
                foreach (var log in diffLog)
                {
                    log.ChangesLoaded = status;
                    log.ExtractsSent = false;
                    SaveChanges();
                    Context.Database.GetDbConnection().BulkMerge(diffLog);
                }
            }
        }

        public void SaveLog(DiffLog diffLog)
        {
            Context.Database.GetDbConnection().BulkMerge(diffLog);
            
        }
        
       
        public DiffLog GenerateDiff(string docket, string extract, int siteCode)
        {
            var sql =
                $"SELECT MAX({nameof(PatientExtract.Date_Created)}) {nameof(PatientExtract.Date_Created)},MAX({nameof(PatientExtract.Date_Last_Modified)}) {nameof(PatientExtract.Date_Last_Modified)} FROM {extract}s";

            var extractDates = Context.Database.GetDbConnection().QuerySingle(sql);
          
            var diffLog = DiffLog.Create(docket, extract, siteCode, extractDates.Date_Created, extractDates.Date_Last_Modified);
            
            if (null != extractDates)
            {

                diffLog.MaxCreated = Extentions.CastDateTime(extractDates.Date_Created);
                diffLog.MaxModified = Extentions.CastDateTime(extractDates.Date_Last_Modified);

            }

            return diffLog;
        }

        
        public DiffLog UpdateMaxDates(string docket, string extract, int siteCode)
        {
            var diffLog = Get(x =>
                x.Docket.ToLower() == docket.ToLower() &&
                x.Extract.ToLower() == extract.ToLower());
                
            var sql =
                $"SELECT MAX({nameof(PatientExtract.Date_Created)}) {nameof(PatientExtract.Date_Created)},MAX({nameof(PatientExtract.Date_Last_Modified)}) {nameof(PatientExtract.Date_Last_Modified)} FROM {extract}s";

            var extractDates = Context.Database.GetDbConnection().QuerySingle(sql);

            if (null != extractDates.Date_Created)
            {
                diffLog.MaxCreated = Extentions.CastDateTime(extractDates.Date_Created);
             }   
            
            if (null != extractDates.Date_Last_Modified)
            {
                diffLog.MaxModified = Extentions.CastDateTime(extractDates.Date_Last_Modified);
            }    
            SaveChanges();
            Context.Database.GetDbConnection().BulkMerge(diffLog);
           

            return diffLog;
        }
        
    }
}
