using System;
using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Services
{
    public class EmrManagerService : IEmrManagerService
    {
        private readonly IDatabaseManager _databaseManager;
        private readonly IEmrSystemRepository _emrSystemRepository;
        private readonly IDatabaseProtocolRepository _databaseProtocolRepository;

        public EmrManagerService(IDatabaseManager databaseManager, IEmrSystemRepository emrSystemRepository,
            IDatabaseProtocolRepository databaseProtocolRepository)
        {
            _databaseManager = databaseManager;
            _emrSystemRepository = emrSystemRepository;
            _databaseProtocolRepository = databaseProtocolRepository;
        }

       
        public IEnumerable<EmrSystem> GetAllEmrs()
        {
            return _emrSystemRepository.GetAll();
        }

        public void SaveEmr(EmrSystem emrSystem)
        {
            _emrSystemRepository.CreateOrUpdate(emrSystem);
            _emrSystemRepository.SaveChanges();
        }

        public void DeleteEmr(Guid emrId)
        {
            _emrSystemRepository.Delete(emrId);
            _emrSystemRepository.SaveChanges();
        }

        public void SaveProtocol(DatabaseProtocol protocol)
        {
            _databaseProtocolRepository.CreateOrUpdate(protocol);
            _databaseProtocolRepository.SaveChanges();
        }

        public void DeleteProtocol(Guid protocolId)
        {
            _databaseProtocolRepository.Delete(protocolId);
            _databaseProtocolRepository.SaveChanges();
        }

        public bool VerifyConnection(DatabaseProtocol databaseProtocol)
        {
            return _databaseManager.VerifyConnection(databaseProtocol);
        }
    }
}