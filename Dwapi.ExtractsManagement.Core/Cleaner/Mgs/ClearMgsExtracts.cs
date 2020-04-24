﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Cleaner.Mgs
{
    public class ClearMgsExtracts : IClearMgsExtracts
    {
        private readonly ITempMetricMigrationExtractRepository _tempMetricMigrationExtractRepository;
        private readonly IExtractHistoryRepository _historyRepository;
        private readonly IValidatorRepository _validatorRepository;

        public ClearMgsExtracts(ITempMetricMigrationExtractRepository tempMetricMigrationExtractRepository, IExtractHistoryRepository historyRepository, IValidatorRepository validatorRepository)
        {
            _tempMetricMigrationExtractRepository = tempMetricMigrationExtractRepository;
            _historyRepository = historyRepository;
            _validatorRepository = validatorRepository;
        }

        public async Task Clear(List<Guid> extractIds)
        {
            Log.Debug($"Executing ClearMetricExtracts command...");

            await _historyRepository.ClearHistory(extractIds);

            DomainEvents.Dispatch(new MgsNotification(new ExtractProgress(nameof(MetricMigrationExtract), "clearing...")));

            foreach (var extractId in extractIds)
            {
                DomainEvents.Dispatch(new MgsStatusNotification(extractId, ExtractStatus.Clearing));
            }

            _validatorRepository.ClearByDocket("MGS");
            await _tempMetricMigrationExtractRepository.Clear();

            foreach (var extractId in extractIds)
            {
                DomainEvents.Dispatch(new MgsStatusNotification(extractId, ExtractStatus.Cleared));
            }
        }
    }
}
