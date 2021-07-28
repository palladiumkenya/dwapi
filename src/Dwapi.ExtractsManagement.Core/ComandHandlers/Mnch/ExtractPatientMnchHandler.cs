using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
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
    public class ExtractPatientMnchHandler :IRequestHandler<ExtractPatientMnch,bool>
    {
        private readonly IPatientMnchSourceExtractor _patientSourceExtractor;
        private readonly IMnchExtractValidator _extractValidator;
        private readonly IPatientMnchLoader _patientLoader;
        private readonly ITempPatientMnchExtractRepository _tempPatientMnchExtractRepository;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractPatientMnchHandler(IPatientMnchSourceExtractor patientSourceExtractor, IMnchExtractValidator extractValidator, IPatientMnchLoader patientLoader, ITempPatientMnchExtractRepository tempPatientMnchExtractRepository, IExtractHistoryRepository extractHistoryRepository)
        {
            _patientSourceExtractor = patientSourceExtractor;
            _extractValidator = extractValidator;
            _patientLoader = patientLoader;
            _tempPatientMnchExtractRepository = tempPatientMnchExtractRepository;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractPatientMnch request, CancellationToken cancellationToken)
        {

            //Extract
            int found = await _patientSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //TODO Check for duplicate patients with SITE CODE
            var patientKeys = _tempPatientMnchExtractRepository.GetAll().Select(k => k.PatientPK);
            var distinct = new HashSet<int?>();
            var duplicates = new HashSet<int?>();
            foreach (var key in patientKeys)
            {
                if (!distinct.Add(key))
                    duplicates.Add(key);
            }

            if (duplicates.Any())
            {
                var readDuplicates = string.Join(", ", duplicates.ToArray());
                throw new DuplicatePatientMnchException($"Duplicate patient(s) with PatientPK(s) {readDuplicates} found");
            }

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PatientMnchExtract), $"{nameof(TempPatientMnchExtract)}s");

            //Load
            int loaded = await _patientLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


           // _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected,0);
           _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected,request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new MnchExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PatientMnchExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }

    public class DuplicatePatientMnchException : Exception
    {
        public DuplicatePatientMnchException(string msg) : base(msg) { }
    }
}
