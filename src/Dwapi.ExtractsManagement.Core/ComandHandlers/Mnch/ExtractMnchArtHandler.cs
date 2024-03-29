﻿using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Mnch
{
    public class ExtractMnchArtHandler :IRequestHandler<ExtractMnchArt,bool>
    {
        private readonly IMnchArtSourceExtractor _mnchArtSourceExtractor;
        private readonly IMnchExtractValidator _extractValidator;
        private readonly IMnchArtLoader _mnchArtLoader;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractMnchArtHandler(IMnchArtSourceExtractor mnchArtSourceExtractor, IMnchExtractValidator extractValidator, IMnchArtLoader mnchArtLoader, IExtractHistoryRepository extractHistoryRepository)
        {
            _mnchArtSourceExtractor = mnchArtSourceExtractor;
            _extractValidator = extractValidator;
            _mnchArtLoader = mnchArtLoader;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractMnchArt request, CancellationToken cancellationToken)
        {
            //Extract
            int found = await _mnchArtSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(MnchArtExtract), $"{nameof(TempMnchArtExtract)}s");

            //Load
            int loaded = await _mnchArtLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new MnchExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(MnchArtExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
