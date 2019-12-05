using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.DTOs;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Queries.Handlers
{
    public class GetAppMetricHandler:IRequestHandler<GetAppMetric,IEnumerable<AppMetricDto>>
    {
        public Task<IEnumerable<AppMetricDto>> Handle(GetAppMetric request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
