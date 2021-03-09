using AutoMapper;
using Dwapi.ExtractsManagement.Core.Profiles.Dwh;

namespace Dwapi.UploadManagement.Core.Profiles
{
    public sealed class UploadMapper
    {
        private readonly AutoMapperConfig _config;
        private static IMapper _instance = null;

        private UploadMapper()
        {
            _config = new AutoMapperConfig();
            var cfg = _config.BaseMaps();
            _instance = new MapperConfiguration(cfg).CreateMapper();
        }

        public static IMapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    var extractMapper = new UploadMapper();
                }
                return _instance;
            }
        }
    }
}
