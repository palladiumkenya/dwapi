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

        public ExtractEventDTO GetStatus(Guid extractId)
        {
            var histories = _extractHistoryRepository.GetAllExtractStatus(extractId).ToList();
            return ExtractEventDTO.Generate(histories);
        }
    }
}
