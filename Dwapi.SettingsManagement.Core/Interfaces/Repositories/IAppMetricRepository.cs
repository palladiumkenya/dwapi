using System;
using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.SettingsManagement.Core.Interfaces.Repositories
{
    public interface IAppMetricRepository : IRepository<AppMetric, Guid>
    {
        IEnumerable<AppMetric> LoadCurrent();
    }
}
