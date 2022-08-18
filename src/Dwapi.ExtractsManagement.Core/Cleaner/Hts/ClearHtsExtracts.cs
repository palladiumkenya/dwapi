using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository; 
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts; 
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Cleaner.Hts
{
    public class ClearHtsExtracts : IClearHtsExtracts
    {
        private readonly ITempHtsClientsExtractRepository _tempPatientExtractRepository;
        private readonly IExtractHistoryRepository _historyRepository;
        private readonly IValidatorRepository _validatorRepository;

        public ClearHtsExtracts(ITempHtsClientsExtractRepository tempPatientExtractRepository, IExtractHistoryRepository historyRepository, IValidatorRepository validatorRepository)
        {
            _tempPatientExtractRepository = tempPatientExtractRepository;
            _historyRepository = historyRepository;
            _validatorRepository = validatorRepository;
        }

        public async Task Clear(List<Guid> extractIds)
        {
            Log.Debug($"Executing ClearHtsExtracts command...");

            await _historyRepository.ClearHistory(extractIds);

            DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientTests)+"Extracts", "clearing...")));
            DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsTestKits) + "Extracts", "clearing...")));
            DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsPartnerNotificationServices) + "Extracts", "clearing...")));
            DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientTracing) + "Extracts", "clearing...")));
            DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsPartnerTracing) + "Extracts", "clearing...")));
            DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientLinkage) + "Extracts", "clearing...")));
            DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClients) + "Extracts", "clearing...")));
            DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsEligibilityExtract) + "Extracts", "clearing...")));


            foreach (var extractId in extractIds)
            {
                DomainEvents.Dispatch(new HtsStatusNotification(extractId, ExtractStatus.Clearing));
            }


            _validatorRepository.ClearByDocket("HTS");
            await _tempPatientExtractRepository.Clear();

            foreach (var extractId in extractIds)
            {
                DomainEvents.Dispatch(new HtsStatusNotification(extractId, ExtractStatus.Cleared));
            }

            //DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientTests), "clearing...")));
            //DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsTestKits), "clearing...")));
            //DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsPartnerNotificationServices), "clearing...")));
            //DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientTracing), "clearing...")));
            //DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsPartnerTracing), "clearing...")));
            //DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientLinkage), "clearing...")));
            //DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClients), "clearing...")));

        }
    }
}
