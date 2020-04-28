using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using MediatR;
using X.PagedList;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Queries.Handlers
{
    public class GetAppMetricHandler:IRequestHandler<GetAppMetric,IEnumerable<AppMetricDto>>
    {
        private readonly IAppMetricRepository _appMetricRepository;

        public GetAppMetricHandler(IAppMetricRepository appMetricRepository)
        {
            _appMetricRepository = appMetricRepository;
        }

        public async Task<IEnumerable<AppMetricDto>> Handle(GetAppMetric request, CancellationToken cancellationToken)
        {
            var metricsDto=new List<AppMetricDto>();

            var metrics =await  _appMetricRepository.LoadCurrent().ToListAsync(cancellationToken);

            foreach (var m in metrics)
            {
                metricsDto.Add(new AppMetricDto(m.Display, m.Action, m.LogDate, m.Rank));
            }

            return metricsDto;
        }
    }
}
