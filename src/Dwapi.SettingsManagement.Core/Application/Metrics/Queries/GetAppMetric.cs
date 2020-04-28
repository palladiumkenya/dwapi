using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.DTOs;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Queries
{
    public class GetAppMetric:IRequest<IEnumerable<AppMetricDto>>
    {

    }
}
