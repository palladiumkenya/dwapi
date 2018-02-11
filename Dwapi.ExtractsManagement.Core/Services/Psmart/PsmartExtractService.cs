using System.Collections.Generic;
using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Services.Psmart;
using Dwapi.ExtractsManagement.Core.Interfaces.Source.Psmart.Reader;
using Dwapi.ExtractsManagement.Core.Interfaces.Stage.Psmart.Repository;
using Dwapi.ExtractsManagement.Core.Source.Psmart;
using Dwapi.ExtractsManagement.Core.Stage.Psmart;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Services.Psmart
{
    public class PsmartExtractService:IPsmartExtractService
    {
        private readonly IPsmartSourceReader _psmartSourceReader;
        private readonly IPsmartStageRepository _psmartStageRepository;
        private readonly IMapper _mapper;

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

        public IEnumerable<PsmartSource> Extract(DbProtocol protocol, DbExtract extract)
        {
            return _psmartSourceReader.Read(protocol, extract);
        }

        public void Load(IEnumerable<PsmartSource> sources, bool clearFirst = true)
        {
            if (clearFirst)
                _psmartStageRepository.Clear();

            var stages = _mapper.Map<IEnumerable<PsmartSource>, IEnumerable<PsmartStage>>(sources);
            //map
            _psmartStageRepository.Load(stages);
            _psmartStageRepository.SaveChanges();
        }
    }
}