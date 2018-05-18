using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.DTOs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public class ExtractStatusService: IExtractStatusService
    {

        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractStatusService(IExtractHistoryRepository extractHistoryRepository)
        {
            _extractHistoryRepository = extractHistoryRepository;
        }

        public ExtractHistory HasStarted(Guid extractId)
        {
            var history = _extractHistoryRepository.GetLatest(extractId);

            if (null == history)
            {
                _extractHistoryRepository.UpdateStatus(extractId, ExtractStatus.Idle);
                return _extractHistoryRepository.GetLatest(extractId);
            }

            return history;
        }

        public ExtractHistory HasStarted(Guid extractId, ExtractStatus status, ExtractStatus otherStatus)
        {
            var history = _extractHistoryRepository.GetLatest(extractId, status, otherStatus);
            return history;
        }

        public ExtractEventDTO GetStatus(Guid extractId)
        {
            var histories = _extractHistoryRepository.GetAllExtractStatus(extractId).ToList();
            return ExtractEventDTO.Generate(histories);
        }

        public void Found(DwhExtract extract, int found)
        {
            _extractHistoryRepository.ClearHistory(extract.Id);

            _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Idle);
            _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Finding);

            try
            {
                _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Found, found);
            }

            catch (Exception ex)
            {
                _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Idle, express: true);
                throw ex;
            }
        }

        public void Complete(Guid extractId)
        {
            _extractHistoryRepository.Complete(extractId);
        }
    }
}
