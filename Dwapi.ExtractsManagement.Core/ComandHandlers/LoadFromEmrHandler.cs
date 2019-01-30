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
            Task<bool> t1 = null, t2 = null, t3 = null, t4 = null, t5 = null, t6 = null, t7 = null;
            // ExtractPatientART
            var patientArtProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientArtExtract");
            if (null != patientArtProfile)
            {
                var patientArtCommand = new ExtractPatientArt()
                {
                    Extract = patientArtProfile?.Extract,
                    DatabaseProtocol = patientArtProfile?.DatabaseProtocol
                };
                t1 = _mediator.Send(patientArtCommand, cancellationToken);
            }

            // ExtractPatientBaselines
            var patientBaselinesProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientBaselineExtract");
            if (null != patientBaselinesProfile)
            {
                var patientBaselinesCommand = new ExtractPatientBaselines()
                {
                    Extract = patientBaselinesProfile?.Extract,
                    DatabaseProtocol = patientBaselinesProfile?.DatabaseProtocol
                };
                t2 = _mediator.Send(patientBaselinesCommand, cancellationToken);
            }

            // ExtractPatientLaboratory
            var patientLaboratoryProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientLabExtract");
            if (null != patientLaboratoryProfile)
            {
                var patientLaboratoryCommand = new ExtractPatientLaboratory()
                {
                    Extract = patientLaboratoryProfile?.Extract,
                    DatabaseProtocol = patientLaboratoryProfile?.DatabaseProtocol
                };
                t3 = _mediator.Send(patientLaboratoryCommand, cancellationToken);
            }

            // ExtractPatientPharmacy
            var patientPharmacyProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientPharmacyExtract");
            if (null != patientPharmacyProfile)
            {
                var patientPharmacyCommand = new ExtractPatientPharmacy()
                {
                    Extract = patientPharmacyProfile?.Extract,
                    DatabaseProtocol = patientPharmacyProfile?.DatabaseProtocol
                };
                t4 = _mediator.Send(patientPharmacyCommand, cancellationToken);
            }

            // ExtractPatientStatus
            var patientStatusProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientStatusExtract");
            if (null != patientStatusProfile)
            {
                var patientStatusCommand = new ExtractPatientStatus()
                {
                    Extract = patientStatusProfile?.Extract,
                    DatabaseProtocol = patientStatusProfile?.DatabaseProtocol
                };
                t5 = _mediator.Send(patientStatusCommand, cancellationToken);
            }

            // ExtractPatientVisit
            var patientVisitProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientVisitExtract");
            if (null != patientVisitProfile)
            {
                var patientVisitCommand = new ExtractPatientVisit()
                {
                    Extract = patientVisitProfile?.Extract,
                    DatabaseProtocol = patientVisitProfile?.DatabaseProtocol
                };
                t6 = _mediator.Send(patientVisitCommand, cancellationToken);
            }

            // ExtractPatientAdverseEvent
            var patientAdverseEventProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientAdverseEventExtract");
            if (null != patientAdverseEventProfile)
            {
                var patientAdverseEventCommand = new ExtractPatientAdverseEvent()
                {
                    Extract = patientAdverseEventProfile?.Extract,
                    DatabaseProtocol = patientAdverseEventProfile?.DatabaseProtocol
                };
                t7 = _mediator.Send(patientAdverseEventCommand, cancellationToken);
            }

            // await all tasks
            var ts = new List<Task<bool>> { t1, t2, t3, t4, t5, t6, t7};
            var result = await Task.WhenAll(ts);

            return result.All(x=>x);
        }
    }
}