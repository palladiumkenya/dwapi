using System;
using System.Collections.Generic;
using Dwapi.SettingsManagement.Core.Interfaces;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.Services
{
    public class ExtractManagerService : IExtractManagerService
    {
        private readonly IExtractRepository _extractRepository;
        private readonly IDatabaseManager _databaseManager;
        public ExtractManagerService(IExtractRepository extractRepository, IDatabaseManager databaseManager)
        {
            _extractRepository = extractRepository;
            _databaseManager = databaseManager;
        }

        public IEnumerable<Extract> GetAllByEmr(Guid emrId, string docketId)
        {
            return _extractRepository.GetAllByEmr(emrId, docketId);
        }

        public void Save(Extract extract)
        {
            _extractRepository.CreateOrUpdate(extract);
            _extractRepository.SaveChanges();
        }


        public bool Verfiy(Extract extract, DatabaseProtocol databaseProtocol)
        {
            return _databaseManager.VerifyQuery(extract, databaseProtocol);
        }

        public string GetDatabaseError()
        {
            return _databaseManager.ConnectionError;
        }

        public Extract Update(Extract extract)
        {
            _extractRepository.Update(extract);
            _extractRepository.SaveChanges();
            return extract;
        }
    }
}