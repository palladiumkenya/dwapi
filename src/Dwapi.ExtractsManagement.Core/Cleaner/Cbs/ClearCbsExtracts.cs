using System;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Cleaner.Cbs
{
    public class ClearCbsExtracts:IClearCbsExtracts
    {
        private readonly ITempMasterPatientIndexRepository _repository;
        private readonly IExtractHistoryRepository _historyRepository;

        public ClearCbsExtracts(ITempMasterPatientIndexRepository repository, IExtractHistoryRepository historyRepository)
        {
            _repository = repository;
            _historyRepository = historyRepository;
        }

        public async Task Clean(Guid extractId)
        {
            _historyRepository.ClearHistory(extractId);

            DomainEvents.Dispatch(new CbsNotification(new ExtractProgress(nameof(MasterPatientIndex), "clearing...")));
            DomainEvents.Dispatch(new CbsStatusNotification(extractId, ExtractStatus.Clearing));

            await _repository.Clear();


            DomainEvents.Dispatch(new CbsStatusNotification(extractId, ExtractStatus.Cleared));
        }
    }
}