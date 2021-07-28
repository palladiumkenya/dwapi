using AutoMapper;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;
using Dwapi.ExtractsManagement.Core.Profiles.Mnch;

namespace Dwapi.ExtractsManagement.Core.Profiles
{
    public sealed class ExtractMapper
    {
        private readonly AutoMapperConfig _config;
        private static IMapper _instance = null;

        private ExtractMapper()
        {
            _config = new AutoMapperConfig();
            var cfg = _config.BaseMaps();
            cfg.AddProfile<CtExtractProfile>();
            cfg.AddProfile<MnchExtractProfile>();
            _instance = new MapperConfiguration(cfg).CreateMapper();
        }

        public static IMapper Instance
        {
            get
            {
                ExtractDiffMapper.Destroy();
                if (_instance == null)
                {
                    var extractMapper = new ExtractMapper();
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
