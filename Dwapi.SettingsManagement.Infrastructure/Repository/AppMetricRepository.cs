using System;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class AppMetricRepository:BaseRepository<AppMetric,Guid>, IAppMetricRepository
    {
        public AppMetricRepository(SettingsContext context) : base(context)
        {
        }
    }
}
