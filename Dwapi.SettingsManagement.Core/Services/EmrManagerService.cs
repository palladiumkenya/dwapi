using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IExtractRepository _extractRepository;
        private readonly IDatabaseProtocolRepository _databaseProtocolRepository;

        public EmrManagerService(IDatabaseManager databaseManager, IEmrSystemRepository emrSystemRepository,
            IDatabaseProtocolRepository databaseProtocolRepository, IExtractRepository extractRepository)
        {
            _databaseManager = databaseManager;
            _emrSystemRepository = emrSystemRepository;
            _databaseProtocolRepository = databaseProtocolRepository;
            _extractRepository = extractRepository;
        }


        public EmrSystem GetDefault()
        {
            return _emrSystemRepository.GetDefault();
        }

        public EmrSystem GetMiddleware()
        {
            return _emrSystemRepository.GetMiddleware();
        }

        public IEnumerable<EmrSystem> GetAllEmrs()
        {
            return _emrSystemRepository.GetAll();
        }

        public int GetEmrCount()
        {
            return _emrSystemRepository.Count();
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
            var extracts = _extractRepository.GetAllByEmr(protocol.EmrSystemId, "NDWH");
            if (!extracts.Any())
            {
                var databaseProtocol = _databaseProtocolRepository.GetAll().FirstOrDefault(p => p.EmrSystemId == protocol.EmrSystemId);
                AddDefaultExtracts(databaseProtocol);
            }
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

        public string GetConnectionError()
        {
            return _databaseManager.ConnectionError;
        }

        public void SetDefault(Guid id)
        {
            var defaultEmr = _emrSystemRepository.GetDefault();
            if (defaultEmr != null)
            {
                defaultEmr.IsDefault = false;
                _emrSystemRepository.Update(defaultEmr);
            }         

            var emr = _emrSystemRepository.Get(id);
            emr.IsDefault = true;
            _emrSystemRepository.Update(emr);

            _emrSystemRepository.SaveChanges();
        }

        public IEnumerable<DatabaseProtocol> GetByEmr(Guid emrId)
        {
            return _databaseProtocolRepository
                .GetAll()
                .Where(x=>x.EmrSystemId==emrId);
        }

        private void AddDefaultExtracts(DatabaseProtocol protocol)
        {
            var extracts = DefaultExtracts();
            var emrName = _emrSystemRepository.Get(protocol.EmrSystemId).Name;
            foreach (var extract in extracts)
            {
                extract.EmrSystemId = protocol.EmrSystemId;
                extract.DatabaseProtocolId = protocol.Id;
                extract.Emr = emrName;
                _extractRepository.CreateOrUpdate(extract);
                _extractRepository.SaveChanges();
            }
        }

        private IEnumerable<Extract> DefaultExtracts()
        {
            var dwhExtracts = _extractRepository.GetAllByEmr(GetDefault().Id, "NDWH").ToList();
            var defaultExtracts = new List<Extract>();
            
            foreach (var extract in dwhExtracts)
            {
                var defaultExtract = new Extract()
                {
                    Id = Guid.NewGuid(),
                    ExtractSql = "",
                    Name = extract.Name,
                    Emr = extract.Emr,
                    Destination = extract.Destination,
                    Display = extract.Display,
                    DocketId = extract.DocketId,
                    IsPriority = extract.IsPriority,
                    Rank = extract.Rank
                };
                defaultExtracts.Add(defaultExtract);
            }

            var cardExtract = _extractRepository.GetAllByEmr(GetDefault().Id, "PSMART").FirstOrDefault();
            if (cardExtract != null)
            {
                var defaultCardExtract = new Extract()
                {
                    Id = Guid.NewGuid(),
                    ExtractSql = "",
                    Name = cardExtract.Name,
                    Emr = cardExtract.Emr,
                    Destination = cardExtract.Destination,
                    Display = cardExtract.Display,
                    DocketId = cardExtract.DocketId,
                    IsPriority = cardExtract.IsPriority,
                    Rank = cardExtract.Rank
                };
                defaultExtracts.Add(defaultCardExtract);
            }
            var mpiExtract = _extractRepository.GetAllByEmr(GetDefault().Id, "CBS").FirstOrDefault();
            if (mpiExtract != null)
            {
                var defaultMpiExtract = new Extract()
                {
                    Id = Guid.NewGuid(),
                    ExtractSql = "",
                    Name = mpiExtract.Name,
                    Emr = mpiExtract.Emr,
                    Destination = mpiExtract.Destination,
                    Display = mpiExtract.Display,
                    DocketId = mpiExtract.DocketId,
                    IsPriority = mpiExtract.IsPriority,
                    Rank = mpiExtract.Rank
                };
                defaultExtracts.Add(defaultMpiExtract);
            }


            return defaultExtracts;
        }
    }
}