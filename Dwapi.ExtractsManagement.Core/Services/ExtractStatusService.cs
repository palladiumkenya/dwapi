using System;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.DTOs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Enum;

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

            catch (Exception)
            {
                _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Idle, express: true);
                throw;
            }
        }

        public void Sync(DwhExtract extract, int count)
        {
            try
            {
                _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Loading);

                _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Loaded, count);
            }
            catch (Exception)
            {
                _extractHistoryRepository.UpdateStatus(extract.Id, ExtractStatus.Idle, express: true);
                throw;
            }
        }

        public void Complete(Guid extractId)
        {
            _extractHistoryRepository.Complete(extractId);
        }
    }
}
