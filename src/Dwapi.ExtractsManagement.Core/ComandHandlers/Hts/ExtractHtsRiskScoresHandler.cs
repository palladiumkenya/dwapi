﻿using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Hts
{
    public class ExtractHtsRiskScoresHandler : IRequestHandler<ExtractHtsRiskScores, bool>
    {
        private readonly IHtsRiskScoresSourceExtractor _patientSourceExtractor;
        private readonly IHtsExtractValidator _extractValidator;
        private readonly IHtsRiskScoresLoader _patientLoader;
        private readonly IClearHtsExtracts _clearDwhExtracts;
        private readonly ITempHtsRiskScoresRepository _tempPatientExtractRepository;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractHtsRiskScoresHandler(IHtsRiskScoresSourceExtractor patientSourceExtractor, IHtsExtractValidator extractValidator, IHtsRiskScoresLoader patientLoader, IClearHtsExtracts clearDwhExtracts, ITempHtsRiskScoresRepository tempPatientExtractRepository, IExtractHistoryRepository extractHistoryRepository)
        {
            _patientSourceExtractor = patientSourceExtractor;
            _extractValidator = extractValidator;
            _patientLoader = patientLoader;
            _clearDwhExtracts = clearDwhExtracts;
            _tempPatientExtractRepository = tempPatientExtractRepository;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractHtsRiskScores request, CancellationToken cancellationToken)
        {

            //Extract
            int found = await _patientSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);


            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, "HtsRiskScoresExtracts", "TempHtsRiskScoresExtracts");

            //Load
            int loaded = await _patientLoader.Load(request.Extract.Id, found, false);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new HtsExtractActivityNotification(request.Extract.Id, new ExtractProgress(
                    nameof(HtsRiskScores),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
