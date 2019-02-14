using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination;
using Dwapi.ExtractsManagement.Core.Model.Source;

namespace Dwapi.ExtractsManagement.Core.Profiles
{
    public class EmrProfiles : Profile
    {
        public EmrProfiles()
        {
            CreateMap<EmrMetricSource, EmrMetric>();
        }
    }
}