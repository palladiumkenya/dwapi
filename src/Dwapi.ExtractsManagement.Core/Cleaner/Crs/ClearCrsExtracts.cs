using System;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Cleaner.Crs
{
    public class ClearCrsExtracts:IClearCrsExtracts
    {
        private readonly ITempClientRegistryExtractRepository _repository;
        private readonly IExtractHistoryRepository _historyRepository;

        public ClearCrsExtracts(ITempClientRegistryExtractRepository repository, IExtractHistoryRepository historyRepository)
        {
            _repository = repository;
            _historyRepository = historyRepository;
        }

        public async Task Clean(Guid extractId)
        {
            _historyRepository.ClearHistory(extractId);

            DomainEvents.Dispatch(new CrsNotification(new ExtractProgress(nameof(ClientRegistryExtract), "clearing...")));
            DomainEvents.Dispatch(new CrsStatusNotification(extractId, ExtractStatus.Clearing));

            await _repository.Clear();


            DomainEvents.Dispatch(new CrsStatusNotification(extractId, ExtractStatus.Cleared));
        }
    }
}