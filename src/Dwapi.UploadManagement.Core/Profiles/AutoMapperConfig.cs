using AutoMapper;
using AutoMapper.Configuration;

namespace Dwapi.UploadManagement.Core.Profiles
{
    public  class AutoMapperConfig
    {
        public MapperConfigurationExpression BaseMaps()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.AddProfile<MasterPatientIndexProfile>();
            return cfg;
        }
    }
}
