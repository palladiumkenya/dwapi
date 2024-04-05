using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class AppMetricRepository : BaseRepository<AppMetric, Guid>, IAppMetricRepository
    {
        public AppMetricRepository(SettingsContext context) : base(context)
        {
        }

        public void Clear(string area)
        {
            var ctLoaded = DbSet.Where(x => x.Name == area);
            if (ctLoaded.Any())
            {
                DbSet.RemoveRange(ctLoaded);
                SaveChanges();
            }
        }

        public void Clear(string area, string action)
        {
            var ctLoaded = DbSet.Where(x => x.Name == area && x.LogValue.Contains(action));
            if (ctLoaded.Any())
            {
                DbSet.RemoveRange(ctLoaded);
                SaveChanges();
            }
        }

        public IEnumerable<AppMetric> LoadCurrent()
        {
            var list = new List<AppMetric>();


            //CareTreatment

            var ctLoaded = DbSet.AsNoTracking().Where(x => x.Name == "CareTreatment" && x.LogValue.Contains("NoLoaded"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            if (null != ctLoaded)
                list.Add(ctLoaded);
            else
            {
                var ap = new AppMetric();
                ap.CreatCt("NoLoaded");
                list.Add(ap);
            }

            var ctSent = DbSet.AsNoTracking().Where(x => x.Name == "CareTreatment" && x.LogValue.Contains("NoSent"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            if (null != ctSent)
                list.Add(ctSent);
            else
            {
                var ap = new AppMetric();
                ap.CreatCt("NoSent");
                list.Add(ap);
            }

            //HivTestingService

            var htsLoaded = DbSet.AsNoTracking()
                .Where(x => x.Name == "HivTestingService" && x.LogValue.Contains("NoLoaded"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            if (null != htsLoaded)
                list.Add(htsLoaded);
            else
            {
                var ap = new AppMetric();
                ap.CreatHts("NoLoaded");
                list.Add(ap);
            }

            var htsSent = DbSet.AsNoTracking()
                .Where(x => x.Name == "HivTestingService" && x.LogValue.Contains("NoSent"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            if (null != htsSent)
                list.Add(htsSent);
            else
            {
                var ap = new AppMetric();
                ap.CreatHts("NoSent");
                list.Add(ap);
            }

            //MasterPatientIndex

            var mpiLoaded = DbSet.AsNoTracking()
                .Where(x => x.Name == "MasterPatientIndex" && x.LogValue.Contains("NoLoaded"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            if (null != mpiLoaded)
                list.Add(mpiLoaded);
            else
            {
                var ap = new AppMetric();
                ap.CreatMpi("NoLoaded");
                list.Add(ap);
            }

            var mpiSent = DbSet.AsNoTracking()
                .Where(x => x.Name == "MasterPatientIndex" && x.LogValue.Contains("NoSent"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            if (null != mpiSent)
                list.Add(mpiSent);
            else
            {
                var ap = new AppMetric();
                ap.CreatMpi("NoSent");
                list.Add(ap);
            }

            //Migration

             /*
            var mgsLoaded = DbSet.AsNoTracking()
                .Where(x => x.Name == "MigrationService" && x.LogValue.Contains("NoLoaded"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            if (null != mgsLoaded)
                list.Add(mgsLoaded);
            else
            {
                var ap = new AppMetric();
                ap.CreatMgs("NoLoaded");
                list.Add(ap);
            }

            var mgsSent = DbSet.AsNoTracking()
                .Where(x => x.Name == "MigrationService" && x.LogValue.Contains("NoSent"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            if (null != mgsSent)
                list.Add(mgsSent);
            else
            {
                var ap = new AppMetric();
                ap.CreatMgs("NoSent");
                list.Add(ap);
            }
            */

            return list;
        }

        public Guid GetSession(string notificationName)
        {
            var metric = DbSet.AsNoTracking().FirstOrDefault(x => x.Name == notificationName);
            if (null != metric)
            {
                var handshake = JsonConvert.DeserializeObject<HandshakeStart>(metric.LogValue);
                return handshake.Session;
            }
            return Guid.Empty;
        }

        public IEnumerable<ExtractCargoDto> LoadCargo()
        {
            var sql = @"
                select distinct e.DocketId,e.Name,h.Stats
                from ExtractHistory h inner join Extracts e on h.ExtractId=e.Id
                where Status=6";

            return Context.Database.GetDbConnection().Query<ExtractCargoDto>(sql).ToList();
        }

        public IEnumerable<ExtractCargoDto> LoadDetainedCargo()
        {
            var builder = new StringBuilder();
            var exlist = new List<string>();

            var list = new List<string>
            {
                "PatientArtExtracts",
                "PatientBaselinesExtracts",
                "PatientLaboratoryExtracts",
                "PatientPharmacyExtracts",
                "PatientStatusExtracts",
                "PatientVisitExtracts",
                "PatientAdverseEventExtracts"
            };

            builder.AppendLine( @"
                select 'NDWH' DocketId,'Detained' Name, p.SiteCode ,count(p.Id) Stats 
                from PatientExtracts p left outer join (
                ");

            foreach (var i in list) exlist.Add($"select distinct PatientPK,SiteCode from {i}");

            builder.AppendLine(exlist.Join(" union "));
            builder.AppendLine(@" )x on p.SiteCode=x.SiteCode and p.PatientPK=x.PatientPK
                where x.PatientPK is null
                GROUP BY p.SiteCode");
            var sql = builder.ToString();

            return Context.Database.GetDbConnection().Query<ExtractCargoDto>(sql).ToList();
        }

        public DateTime? GetCTLastLoadedDate()
        {
            //CareTreatment

            var ctLoaded = DbSet.AsNoTracking().Where(x => x.Name == "CareTreatment" && x.LogValue.Contains("NoLoaded"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            // if (null != ctLoaded)
            //     list.Add(ctLoaded);
            // else
            // {
            //     var ap = new AppMetric();
            //     ap.CreatCt("NoLoaded");
            //     list.Add(ap);
            // }
            return ctLoaded.LogDate;
        }

    }
}
