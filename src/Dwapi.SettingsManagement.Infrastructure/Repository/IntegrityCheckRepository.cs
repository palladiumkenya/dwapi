using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure.Repository;

namespace Dwapi.SettingsManagement.Infrastructure.Repository
{
    public class IntegrityCheckRepository : BaseRepository<IntegrityCheck, Guid>, IIntegrityCheckRepository
    {
        public IntegrityCheckRepository(SettingsContext context) : base(context)
        {
        }

        public void Clear()
        {
            var sql = $@"delete from {nameof(SettingsContext.IntegrityCheckRuns)}";
            ExecCommand(sql);
        }

        public IEnumerable<IndicatorDto> LoadIndicators()
        {
            var sql = @"select Indicator,IndicatorValue from IndicatorExtracts";
            var indicators = GetConnection().Query<IndicatorDto>(sql);
            return indicators;
        }

        public IEnumerable<MetricDto> LoadMetices()
        {
            var sql = @"select * from EmrMetrics";
            var metrics = GetConnection().Query<MetricDto>(sql).ToList();
            return metrics;
        }
    }
}
