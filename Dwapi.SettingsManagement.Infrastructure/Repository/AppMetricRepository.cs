using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class AppMetricRepository:BaseRepository<AppMetric,Guid>, IAppMetricRepository
    {
        public AppMetricRepository(SettingsContext context) : base(context)
        {
        }

        public IEnumerable<AppMetric> LoadCurrent()
        {
            var list = new List<AppMetric>();

            //CareTreatment

            var ctLoaded = DbSet.AsNoTracking().Where(x => x.Name == "CareTreatment" && x.LogValue.Contains("NoLoaded"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            list.Add(ctLoaded);

            var ctSent = DbSet.AsNoTracking().Where(x => x.Name == "CareTreatment" && x.LogValue.Contains("NoSent"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            list.Add(ctSent);

            //HivTestingService

            var htsLoaded = DbSet.AsNoTracking().Where(x => x.Name == "HivTestingService" && x.LogValue.Contains("NoLoaded"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            list.Add(htsLoaded);

            var htsSent = DbSet.AsNoTracking().Where(x => x.Name == "HivTestingService" && x.LogValue.Contains("NoSent"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            list.Add(htsSent);

            //MasterPatientIndex

            var mpiLoaded = DbSet.AsNoTracking().Where(x => x.Name == "MasterPatientIndex" && x.LogValue.Contains("NoLoaded"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            list.Add(mpiLoaded);

            var mpiSent = DbSet.AsNoTracking().Where(x => x.Name == "MasterPatientIndex" && x.LogValue.Contains("NoSent"))
                .OrderByDescending(x => x.LogDate).FirstOrDefault();
            list.Add(mpiSent);



            return list;
        }
    }
}
