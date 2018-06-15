using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class LoadFromEmrHandler : IRequestHandler<LoadFromEmrCommand, bool>
    {
        private IMediator _mediator;

        public LoadFromEmrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(LoadFromEmrCommand request, CancellationToken cancellationToken)
        {
            var extractIds = request.Extracts.Select(x => x.Extract.Id).ToList();

            await _mediator.Send(new ClearAllExtracts(extractIds), cancellationToken);


            var patientProfile = request.Extracts.FirstOrDefault(x=>x.Extract.IsPriority);
            //Generate extract patient command
            if (patientProfile != null)
            {
                var extractPatient = new ExtractPatient()
                {
                    //todo check if extracts from all emrs are loaded
                    Extract = patientProfile.Extract,
                    DatabaseProtocol = patientProfile.DatabaseProtocol
                };

                var result = await _mediator.Send(extractPatient, cancellationToken);

                if (!result)
                {
                    return false;
                }
            }
            return await ExtractAll(request, cancellationToken);
        }

        private async Task<bool> ExtractAll(LoadFromEmrCommand request, CancellationToken cancellationToken)
        {
            // ExtractPatientART
            var patientArtProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientArtExtract");
            var patientArtCommand = new ExtractPatientArt()
            {
                Extract = patientArtProfile?.Extract,
                DatabaseProtocol = patientArtProfile?.DatabaseProtocol
            };

            // ExtractPatientBaselines
            var patientBaselinesProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientBaselineExtract");
            var patientBaselinesCommand = new ExtractPatientBaselines()
            {
                Extract = patientBaselinesProfile?.Extract,
                DatabaseProtocol = patientBaselinesProfile?.DatabaseProtocol
            };

            // ExtractPatientLaboratory
            var patientLaboratoryProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientLabExtract");
            var patientLaboratoryCommand = new ExtractPatientLaboratory()
            {
                Extract = patientLaboratoryProfile?.Extract,
                DatabaseProtocol = patientLaboratoryProfile?.DatabaseProtocol
            };

            // ExtractPatientPharmacy
            var patientPharmacyProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientPharmacyExtract");
            var patientPharmacyCommand = new ExtractPatientPharmacy()
            {
                Extract = patientPharmacyProfile?.Extract,
                DatabaseProtocol = patientPharmacyProfile?.DatabaseProtocol
            };

            // ExtractPatientStatus
            var patientStatusProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientStatusExtract");
            var patientStatusCommand = new ExtractPatientStatus()
            {
                Extract = patientStatusProfile?.Extract,
                DatabaseProtocol = patientStatusProfile?.DatabaseProtocol
            };

            // ExtractPatientVisit
            var patientVisitProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientVisitExtract");
            var patientVisitCommand = new ExtractPatientVisit()
            {
                Extract = patientVisitProfile?.Extract,
                DatabaseProtocol = patientVisitProfile?.DatabaseProtocol
            };

            var t1 = _mediator.Send(patientArtCommand, cancellationToken);
            var t2 = _mediator.Send(patientBaselinesCommand, cancellationToken);
            var t3 = _mediator.Send(patientLaboratoryCommand, cancellationToken);
            var t4 = _mediator.Send(patientPharmacyCommand, cancellationToken);
            var t5 = _mediator.Send(patientStatusCommand, cancellationToken);
            var t6 = _mediator.Send(patientVisitCommand, cancellationToken);

            var ts = new List<Task<bool>> { t1, t2, t3, t4, t5, t6 };

            var result = await Task.WhenAll(ts);

            return result.All(x=>x);
        }
    }
}