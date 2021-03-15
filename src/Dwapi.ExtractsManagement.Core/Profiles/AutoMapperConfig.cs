using System;
using AutoMapper.Configuration;
using AutoMapper.Data;
using Dwapi.ExtractsManagement.Core.Profiles.Cbs;
using Dwapi.ExtractsManagement.Core.Profiles.Hts;
using Dwapi.ExtractsManagement.Core.Profiles.Mgs;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.ExtractsManagement.Core.Profiles
{
    public  class AutoMapperConfig
    {
        public MapperConfigurationExpression BaseMaps()
        {
            var cfg = new MapperConfigurationExpression();
            cfg.AddDataReaderMapping();
            cfg.AddProfile<EmrProfiles>();
            cfg.AddProfile<TempMasterPatientIndexProfile>();
            cfg.AddProfile<TempHtsExtractProfile>();
            cfg.AddProfile<TempMetricExtractProfile>();
            return cfg;
        }
    }
}
