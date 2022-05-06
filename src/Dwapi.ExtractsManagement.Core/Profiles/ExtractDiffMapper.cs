using AutoMapper;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;
using Dwapi.ExtractsManagement.Core.Profiles.Mnch;
using Dwapi.ExtractsManagement.Core.Profiles.Prep;

namespace Dwapi.ExtractsManagement.Core.Profiles
{
    public sealed class ExtractDiffMapper
    {
        private readonly AutoMapperConfig _config;
        private static IMapper _instance = null;

        private ExtractDiffMapper()
        {
            _config = new AutoMapperConfig();
            var cfg = _config.BaseMaps();
            cfg.AddProfile<DiffCtExtractProfile>();
            cfg.AddProfile<MnchExtractProfile>();//TODO :PMTCT DIFF
            cfg.AddProfile<PrepExtractProfile>();//TODO :PREP DIFF
            _instance = new MapperConfiguration(cfg).CreateMapper();
        }

        public static IMapper Instance
        {
            get
            {
                ExtractMapper.Destroy();
                if (_instance == null)
                {
                    var extractMapper = new ExtractDiffMapper();
                }
                return _instance;
            }
        }

        public static void Destroy()
        {
            _instance = null;
        }
    }
}
