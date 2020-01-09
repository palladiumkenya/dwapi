using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class AppMetricRepository : BaseRepository<AppMetric, Guid>, IAppMetricRepository
    {
        public AppMetricRepository(SettingsContext context) : base(context)
        {
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



            return list;
        }
    }
}
