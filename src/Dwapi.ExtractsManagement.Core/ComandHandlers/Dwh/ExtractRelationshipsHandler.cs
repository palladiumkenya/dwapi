﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Dwh
{
    public class ExtractRelationshipsHandler :IRequestHandler<ExtractRelationships,bool>
    {
        private readonly IRelationshipsSourceExtractor _RelationshipsSourceExtractor;
        private readonly IExtractValidator _extractValidator;
        private readonly IRelationshipsLoader _RelationshipsLoader;
        private readonly IClearDwhExtracts _clearDwhExtracts;
        private readonly IExtractHistoryRepository _extractHistoryRepository;
        private readonly IDiffLogRepository _diffLogRepository;
        private readonly IIndicatorExtractRepository _indicatorExtractRepository;
            
        public ExtractRelationshipsHandler(IRelationshipsSourceExtractor RelationshipsSourceExtractor, IExtractValidator extractValidator, IRelationshipsLoader RelationshipsLoader, IClearDwhExtracts clearDwhExtracts, IExtractHistoryRepository extractHistoryRepository, IDiffLogRepository diffLogRepository,IIndicatorExtractRepository indicatorExtractRepository)
        {
            _RelationshipsSourceExtractor = RelationshipsSourceExtractor;
            _extractValidator = extractValidator;
            _RelationshipsLoader = RelationshipsLoader;
            _clearDwhExtracts = clearDwhExtracts;
            _extractHistoryRepository = extractHistoryRepository;
            _diffLogRepository = diffLogRepository;
            _indicatorExtractRepository = indicatorExtractRepository;
        }

        public async Task<bool> Handle(ExtractRelationships request, CancellationToken cancellationToken)
        {
            // differential loading
            // Get current site and docket dates,
            int found;
            var mflcode =   _indicatorExtractRepository.GetMflCode();

            var loadChangesOnly = request.LoadChangesOnly;
            var difflog = _diffLogRepository.GetLog("NDWH", "RelationshipsExtract", mflcode);
            var changesLoadedStatus= false;
            
            if (null == difflog)
                found  = await _RelationshipsSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);
            else 
                if (true == loadChangesOnly)
                {
                    changesLoadedStatus = true;
                    found = await _RelationshipsSourceExtractor.Extract(request.Extract,
                        request.DatabaseProtocol, difflog.MaxCreated, difflog.MaxModified, difflog.SiteCode);
                }
                else
                {
                    found  = await _RelationshipsSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

                }
            //Extract
            _diffLogRepository.UpdateExtractsSentStatus("NDWH", "RelationshipsExtract", changesLoadedStatus);

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(RelationshipsExtract), $"{nameof(TempRelationshipsExtract)}s");

            //Load
            int loaded = await _RelationshipsLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


            _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected, request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new ExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(RelationshipsExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }
}
