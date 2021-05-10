using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Cleaner.Mnch
{
    public class ClearMnchExtracts : IClearMnchExtracts
    {
        private readonly ITempPatientMnchExtractRepository _tempPatientExtractRepository;
        private readonly IExtractHistoryRepository _historyRepository;
        private readonly IValidatorRepository _validatorRepository;

        public ClearMnchExtracts(ITempPatientMnchExtractRepository tempPatientExtractRepository, IExtractHistoryRepository historyRepository, IValidatorRepository validatorRepository)
        {
            _tempPatientExtractRepository = tempPatientExtractRepository;
            _historyRepository = historyRepository;
            _validatorRepository = validatorRepository;
        }

        public async Task<int> Clear(List<Guid> extractIds)
        {
            Log.Debug($"Executing ClearMnchExtracts command...");

            foreach (var extractId in extractIds)
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientExtract),
                        nameof(ExtractStatus.Clearing),
                        0, 0, 0, 0, 0)));
            }

            await _historyRepository.ClearHistory(extractIds);
            _validatorRepository.ClearByDocket("MNCH");
            await _tempPatientExtractRepository.Clear();


            foreach (var extractId in extractIds)
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientExtract),
                        nameof(ExtractStatus.Cleared),
                        0, 0, 0, 0, 0)));
            }

            return 1;
        }
    }
}
