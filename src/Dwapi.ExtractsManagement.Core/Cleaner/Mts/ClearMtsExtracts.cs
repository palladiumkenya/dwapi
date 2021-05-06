using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Cleaner.Mts
{
    public class ClearMtsExtracts : IClearMtsExtracts
    {
        private readonly ITempIndicatorExtractRepository _tempMetricMigrationExtractRepository;
        private readonly IExtractHistoryRepository _historyRepository;
        private readonly IValidatorRepository _validatorRepository;

        public ClearMtsExtracts(ITempIndicatorExtractRepository tempMetricMigrationExtractRepository, IExtractHistoryRepository historyRepository, IValidatorRepository validatorRepository)
        {
            _tempMetricMigrationExtractRepository = tempMetricMigrationExtractRepository;
            _historyRepository = historyRepository;
            _validatorRepository = validatorRepository;
        }

        public async Task Clear(List<Guid> extractIds)
        {
            Log.Debug($"Executing ClearMetricExtracts command...");

            await _historyRepository.ClearHistory(extractIds);

            // DomainEvents.Dispatch(new MgsNotification(new ExtractProgress(nameof(IndicatorExtract), "clearing...")));

            foreach (var extractId in extractIds)
            {
                // DomainEvents.Dispatch(new MgsStatusNotification(extractId, ExtractStatus.Clearing));
            }

            _validatorRepository.ClearByDocket("MTS");
            await _tempMetricMigrationExtractRepository.Clear();

            foreach (var extractId in extractIds)
            {
                // DomainEvents.Dispatch(new MgsStatusNotification(extractId, ExtractStatus.Cleared));
            }
        }
    }
}
