using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.DTOs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Source;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public class PsmartExtractService:IPsmartExtractService
    {
        private readonly IPsmartSourceReader _psmartSourceReader;
        private readonly IPsmartStageRepository _psmartStageRepository;
        private readonly IMapper _mapper;
        private string _emr;
        private readonly List<string> errorList=new List<string>();

        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public PsmartExtractService(IPsmartSourceReader psmartSourceReader, IPsmartStageRepository psmartStageRepository, IExtractHistoryRepository extractHistoryRepository)
        {
            _psmartSourceReader = psmartSourceReader;
            _psmartStageRepository = psmartStageRepository;
            _extractHistoryRepository = extractHistoryRepository;

            var config = new MapperConfiguration(cfg => {
                // cfg.CreateMissingTypeMaps = false;
                cfg.AllowNullCollections = true;
                cfg.CreateMap<PsmartSource, PsmartStage>();
            });

            _mapper = new Mapper(config);
        }

        public ExtractHistory HasStarted(Guid extractId)
        {
            var history = _extractHistoryRepository.GetLatest(extractId);

            if (null == history)
            {
                _extractHistoryRepository.UpdateStatus(extractId,ExtractStatus.Idle);
                return _extractHistoryRepository.GetLatest(extractId);
            }

            return history;
        }

        public ExtractHistory HasStarted(Guid extractId, ExtractStatus status, ExtractStatus otherStatus)
        {
            var history = _extractHistoryRepository.GetLatest(extractId, status,otherStatus);
            return history;
        }

        public void Find(DbExtractProtocolDTO dbExtractProtocolDto)
        {
            var extract = dbExtractProtocolDto.Extract;
            var protocol = dbExtractProtocolDto.DatabaseProtocol;

            _extractHistoryRepository.ClearHistory(extract.Id);

            _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Idle);
            _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Finding);

            try
            {
                var found = _psmartSourceReader.Find(protocol, extract);
                _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Found, found);
            }

            catch(Exception ex)
            {
                _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Idle, express:true);
                throw ex;
            }
        }

        public void Sync(DbExtractProtocolDTO extract)
        {
            try
            {
                int count = 0;
                _extractHistoryRepository.UpdateStatus(extract.Extract.Id, ExtractStatus.Loading);

                var psmartSource = Extract(extract.DatabaseProtocol, extract.Extract).ToList();
                if (psmartSource.Any())
                {
                    count = Load(psmartSource);
                }
                _extractHistoryRepository.UpdateStatus(extract.Extract.Id, ExtractStatus.Loaded, count);
                //_extractHistoryRepository.UpdateStatus(extract.Extract.Id, ExtractStatus.Idle);
            }
            catch (Exception e)
            {
                errorList.Add(e.Message);
                _extractHistoryRepository.UpdateStatus(extract.Extract.Id, ExtractStatus.Idle, express: true);
                throw;
            }
        }

        public void Find(IEnumerable<DbExtractProtocolDTO> extracts)
        {
            foreach (var dbExtractProtocolDto in extracts)
            {
                Find(dbExtractProtocolDto);
            }
        }

        public ExtractEventDTO GetStatus(Guid extractId)
        {
            var histories = _extractHistoryRepository.GetAllExtractStatus(extractId).ToList();
            return ExtractEventDTO.Generate(histories);
        }

        public IEnumerable<PsmartSource> Extract(DbProtocol protocol, DbExtract extract)
        {
            _emr = extract.Emr;
            return _psmartSourceReader.Read(protocol, extract);
        }

        public int Load(IEnumerable<PsmartSource> sources, bool clearFirst = true)
        {
            if (clearFirst)
                _psmartStageRepository.Clear(_emr);

            var stages = _mapper.Map<IEnumerable<PsmartSource>, IEnumerable<PsmartStage>>(sources);
            _psmartStageRepository.Load(stages);

            _psmartStageRepository.SaveChanges();

            return _psmartStageRepository.Count(_emr);
        }

        public void Complete(Guid extractId)
        {
            _extractHistoryRepository.Complete(extractId);
        }

        public void Sync(IEnumerable<DbExtractProtocolDTO> extracts)
        {
            foreach (var extract in extracts)
            {
                Sync(extract);
            }
        }

        public string GetLoadError()
        {
            if (errorList.Any())
                return errorList.First();
            return string.Empty;
        }

        public void Clear()
        {
            _psmartStageRepository.Clear();
        }
    }
}