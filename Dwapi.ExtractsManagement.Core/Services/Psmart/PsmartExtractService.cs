using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Services.Psmart;
using Dwapi.ExtractsManagement.Core.Interfaces.Stage.Psmart.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Stage.Source.Psmart.Reader;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Source.Psmart;
using Dwapi.ExtractsManagement.Core.Model.Stage.Psmart;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Services.Psmart
{
    public class PsmartExtractService:IPsmartExtractService
    {
        private readonly IPsmartSourceReader _psmartSourceReader;
        private readonly IPsmartStageRepository _psmartStageRepository;
        private readonly IMapper _mapper;
        private string _emr;
        private List<string> errorList=new List<string>();

        private IExtractHistoryRepository _extractHistoryRepository;

        public PsmartExtractService(IPsmartSourceReader psmartSourceReader, IPsmartStageRepository psmartStageRepository)
        {
            _psmartSourceReader = psmartSourceReader;
            _psmartStageRepository = psmartStageRepository;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMissingTypeMaps = false;
                cfg.AllowNullCollections = true;
                cfg.CreateMap<PsmartSource, PsmartStage>();
            });

            _mapper = new Mapper(config);
        }

        public void Find(DbProtocol protocol, DbExtract extract)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PsmartSource> Extract(DbProtocol protocol, DbExtract extract)
        {
            _emr = extract.Emr;
            return _psmartSourceReader.Read(protocol, extract);
        }

        public void Load(IEnumerable<PsmartSource> sources, bool clearFirst = true)
        {
            if (clearFirst)
                _psmartStageRepository.Clear(_emr);

            var stages = _mapper.Map<IEnumerable<PsmartSource>, IEnumerable<PsmartStage>>(sources);
            //map
            _psmartStageRepository.Load(stages);
            _psmartStageRepository.SaveChanges();
        }

        public void Sync(IEnumerable<DbExtractProtocolDTO> extracts)
        {
            foreach (var extract in extracts)
            {
                try
                {
                    Load(Extract(extract.DatabaseProtocol, extract.Extract));
                }
                catch (Exception e)
                {
                   errorList.Add(e.Message);
                    throw;
                }
               
            }
        }

        public string GetLoadError()
        {
            if (errorList.Any())
                return errorList.First();
            return string.Empty;
        }
    }
}