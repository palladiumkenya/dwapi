using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Utilities;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators;
using Dwapi.ExtractsManagement.Core.Interfaces.Validators.Prep;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Prep;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers.Prep
{
    public class ExtractPatientPrepHandler :IRequestHandler<ExtractPatientPrep,bool>
    {
        private readonly IPatientPrepSourceExtractor _patientSourceExtractor;
        private readonly IPrepExtractValidator _extractValidator;
        private readonly IPatientPrepLoader _patientLoader;
        private readonly ITempPatientPrepExtractRepository _tempPatientPrepExtractRepository;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractPatientPrepHandler(IPatientPrepSourceExtractor patientSourceExtractor, IPrepExtractValidator extractValidator, IPatientPrepLoader patientLoader, ITempPatientPrepExtractRepository tempPatientPrepExtractRepository, IExtractHistoryRepository extractHistoryRepository)
        {
            _patientSourceExtractor = patientSourceExtractor;
            _extractValidator = extractValidator;
            _patientLoader = patientLoader;
            _tempPatientPrepExtractRepository = tempPatientPrepExtractRepository;
            _extractHistoryRepository = extractHistoryRepository;
        }

        public async Task<bool> Handle(ExtractPatientPrep request, CancellationToken cancellationToken)
        {

            //Extract
            int found = await _patientSourceExtractor.Extract(request.Extract, request.DatabaseProtocol);

            //TODO Check for duplicate patients with SITE CODE
            var patientKeys = _tempPatientPrepExtractRepository.GetAll().Select(k => k.PatientPK);
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
                throw new DuplicatePatientPrepException($"Duplicate patient(s) with PatientPK(s) {readDuplicates} found");
            }

            //Validate
            await _extractValidator.Validate(request.Extract.Id, found, nameof(PatientPrepExtract), $"{nameof(TempPatientPrepExtract)}s");

            //Load
            int loaded = await _patientLoader.Load(request.Extract.Id, found, request.DatabaseProtocol.SupportsDifferential);

            int rejected =
                _extractHistoryRepository.ProcessRejected(request.Extract.Id, found - loaded, request.Extract);


           // _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected,0);
           _extractHistoryRepository.ProcessExcluded(request.Extract.Id, rejected,request.Extract);

            //notify loaded
            DomainEvents.Dispatch(
                new PrepExtractActivityNotification(request.Extract.Id, new DwhProgress(
                    nameof(PatientPrepExtract),
                    nameof(ExtractStatus.Loaded),
                    found, loaded, rejected, loaded, 0)));

            return true;
        }
    }

    public class DuplicatePatientPrepException : Exception
    {
        public DuplicatePatientPrepException(string msg) : base(msg) { }
    }
}
