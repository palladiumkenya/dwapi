﻿using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Cleaner.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
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
    public class ExtractHtsClientsHandler : IRequestHandler<ExtractHtsClients, bool>
    {
        private readonly IHtsClientsSourceExtractor _patientSourceExtractor;
        private readonly IHtsExtractValidator _extractValidator;
        private readonly IHtsClientsLoader _patientLoader;
        private readonly IClearHtsExtracts _clearDwhExtracts;
        private readonly ITempHtsClientsExtractRepository _tempPatientExtractRepository;
        private readonly IExtractHistoryRepository _extractHistoryRepository;
        private readonly IDwhExtractSourceReader _reader;

        public ExtractHtsClientsHandler(IHtsClientsSourceExtractor patientSourceExtractor, IHtsExtractValidator extractValidator, IHtsClientsLoader patientLoader, IClearHtsExtracts clearDwhExtracts, ITempHtsClientsExtractRepository tempPatientExtractRepository, IExtractHistoryRepository extractHistoryRepository,IDwhExtractSourceReader reader)
        {
            _patientSourceExtractor = patientSourceExtractor;
            _extractValidator = extractValidator;
            _patientLoader = patientLoader;
            _clearDwhExtracts = clearDwhExtracts;
            _tempPatientExtractRepository = tempPatientExtractRepository;
            _extractHistoryRepository = extractHistoryRepository;
            _reader = reader;

        }

        public async Task<bool> Handle(ExtractHtsClients request, CancellationToken cancellationToken)
        {
            if (request.DatabaseProtocol.DatabaseTypeName.ToLower().Contains("MySql".ToLower()))
            {
                _reader.ChangeSQLmode(request.DatabaseProtocol);
            }


            //Extract
            int found = await _patientSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);


            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, "HtsClientsExtracts", "TempHtsClientsExtracts");

            //Load
            int loaded = await _patientLoader.Load(request.Extract.Id, found, false);
             
            int rejected = 
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new HtsExtractActivityNotification(request.Extract.Id, new ExtractProgress(
                    nameof(HtsClients) + "Extracts",
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
