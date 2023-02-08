using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Interfaces;

namespace Dwapi.SettingsManagement.Core.Services
{
    public class AutoloadService : IAutoloadService
    {
        private readonly IEmrSystemRepository _emrSystemRepository;


        public AutoloadService(IEmrSystemRepository emrSystemRepository)
        {
            _emrSystemRepository = emrSystemRepository;
        }
        
        public string refreshEMRETL(DatabaseProtocol protocol)
        {
            var refresh = _emrSystemRepository.refreshEMRETL(protocol);
            return refresh;
        }

    }
}