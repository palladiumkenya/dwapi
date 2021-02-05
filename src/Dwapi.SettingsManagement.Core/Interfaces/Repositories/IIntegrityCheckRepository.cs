using System;
using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.SettingsManagement.Core.Interfaces.Repositories
{
    public interface IIntegrityCheckRepository : IRepository<IntegrityCheck, Guid>
    {
        void Clear();
        IEnumerable<IndicatorDto> LoadIndicators();
        IEnumerable<MetricDto> LoadEmrMetrics();
        IEnumerable<IntegrityCheck> LoadAll();
    }
}
