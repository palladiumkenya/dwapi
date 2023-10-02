using System;
using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.SettingsManagement.Core.Interfaces.Repositories
{
    public interface IAppMetricRepository : IRepository<AppMetric, Guid>
    {
        void Clear(string area);
        void Clear(string area, string action);
        IEnumerable<AppMetric> LoadCurrent();
        Guid GetSession(string notificationName);
        IEnumerable<ExtractCargoDto> LoadCargo();
        IEnumerable<ExtractCargoDto> LoadDetainedCargo();
        DateTime? GetCTLastLoadedDate();

    }
}
