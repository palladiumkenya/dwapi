using Dwapi.ExtractsManagement.Core.Model.Source;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Services
{
    public interface IEmrMetricsService:IRestClientService<EmrMetricSource>
    {
    }
}