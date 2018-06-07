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
            //Generate extract patient command
            var extractPatient = new ExtractPatient()
            {
                //todo check if extracts from all emrs are loaded
                Extract = request.Extracts.FirstOrDefault(x => x.IsPriority),
                DatabaseProtocol = request.DatabaseProtocol
            };

            var result = await _mediator.Send(extractPatient, cancellationToken);

            if (!result)
            {
                return false;
            }
            return await ExtractAll(request, cancellationToken);
        }

        private async Task<bool> ExtractAll(LoadFromEmrCommand request, CancellationToken cancellationToken)
        {
            var protocal = request.DatabaseProtocol;

            // ExtractPatientART
            var patientArtCommand = new ExtractPatientArt()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientArtExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            // ExtractPatientBaselines
            var patientBaselinesCommand = new ExtractPatientBaselines()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientBaselineExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            // ExtractPatientLaboratory
            var patientLaboratoryCommand = new ExtractPatientLaboratory()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientLaboratoryExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            // ExtractPatientPharmacy
            var patientPharmacyCommand = new ExtractPatientBaselines()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientPharmacyExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            // ExtractPatientStatus
            var patientStatusCommand = new ExtractPatientBaselines()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientStatusExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            // ExtractPatientVisit
            var patientVisitCommand = new ExtractPatientVisit()
            {
                Extract = request.Extracts.FirstOrDefault(x => x.Name == "PatientVisitExtract"),
                DatabaseProtocol = request.DatabaseProtocol
            };

            var t1 = _mediator.Send(patientStatusCommand, cancellationToken);
            var t2 = _mediator.Send(patientPharmacyCommand, cancellationToken);
            var t3 = _mediator.Send(patientLaboratoryCommand, cancellationToken);
            var t4 = _mediator.Send(patientBaselinesCommand, cancellationToken);
            var t5 = _mediator.Send(patientArtCommand, cancellationToken);
            var t6 = _mediator.Send(patientVisitCommand, cancellationToken);

            var ts = new List<Task<bool>> { t1, t2, t3, t4, t5, t6 };

            var tresult = await Task.WhenAll(ts);

            return tresult.All(x=>x);
        }
    }
}