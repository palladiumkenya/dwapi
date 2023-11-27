using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Prep;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class LoadPrepFromEmrHandler : IRequestHandler<LoadPrepFromEmrCommand, bool>
    {
        private IMediator _mediator;

        public LoadPrepFromEmrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(LoadPrepFromEmrCommand request, CancellationToken cancellationToken)
        {
            var extractIds = request.Extracts.Select(x => x.Extract.Id).ToList();

            await _mediator.Send(new ClearAllPrepExtracts(extractIds), cancellationToken);


            var patientProfile = request.Extracts.FirstOrDefault(x => x.Extract.IsPriority);
            //Generate extract patient command
            if (patientProfile != null)
            {
                var extractPatient = new ExtractPatientPrep()
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

        private async Task<bool> ExtractAll(LoadPrepFromEmrCommand request, CancellationToken cancellationToken)
        {
            var ts1 = new List<Task<bool>>();
            var ts2 = new List<Task<bool>>();
            var ts3 = new List<Task<bool>>();
            var ts4 = new List<Task<bool>>();

            // ExtractPrepAdverseEventExtract
            var patientArtProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PrepAdverseEventExtract");
            if (null != patientArtProfile)
            {
                var patientArtCommand = new ExtractPrepAdverseEvent()
                {
                    Extract = patientArtProfile?.Extract,
                    DatabaseProtocol = patientArtProfile?.DatabaseProtocol
                };
                ts1.Add(_mediator.Send(patientArtCommand, cancellationToken));
            }

            // ExtractPrepArtExtract
            var patientBaselinesProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "PrepBehaviourRiskExtract");
            if (null != patientBaselinesProfile)
            {
                var patientBaselinesCommand = new ExtractPrepBehaviourRisk()
                {
                    Extract = patientBaselinesProfile?.Extract,
                    DatabaseProtocol = patientBaselinesProfile?.DatabaseProtocol
                };
                ts1.Add( _mediator.Send(patientBaselinesCommand, cancellationToken));
            }

            // ExtractAncVisitExtract
            var patientLaboratoryProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PrepCareTerminationExtract");
            if (null != patientLaboratoryProfile)
            {
                var patientLaboratoryCommand = new ExtractPrepCareTermination()
                {
                    Extract = patientLaboratoryProfile?.Extract,
                    DatabaseProtocol = patientLaboratoryProfile?.DatabaseProtocol
                };
                ts2.Add( _mediator.Send(patientLaboratoryCommand, cancellationToken));
            }

            // ExtractPrepVisitExtract
            var allergiesChronicIllnessProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "PrepVisitExtract");
            if (null != allergiesChronicIllnessProfile)
            {
                var allergiesChronicIllnessCommand = new ExtractPrepVisit()
                {
                    Extract = allergiesChronicIllnessProfile?.Extract,
                    DatabaseProtocol = allergiesChronicIllnessProfile?.DatabaseProtocol
                };
                ts2.Add(_mediator.Send(allergiesChronicIllnessCommand, cancellationToken));
            }

            // ExtractPrepPharmacyExtract
            var contactListingProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PrepPharmacyExtract");
            if (null != contactListingProfile)
            {
                var contactListingCommand = new ExtractPrepPharmacy()
                {
                    Extract = contactListingProfile?.Extract,
                    DatabaseProtocol = contactListingProfile?.DatabaseProtocol
                };
                ts3.Add(_mediator.Send(contactListingCommand, cancellationToken));
            }

            // ExtractPrepLabExtract
            var depressionScreeningProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "PrepLabExtract");
            if (null != depressionScreeningProfile)
            {
                var depressionScreeningCommand = new ExtractPrepLab()
                {
                    Extract = depressionScreeningProfile?.Extract,
                    DatabaseProtocol = depressionScreeningProfile?.DatabaseProtocol
                };
                ts4.Add( _mediator.Send(depressionScreeningCommand, cancellationToken));
            }
            
            // ExtractPrepMonthlyRefillExtract
            var prepMonthlyRefillProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "PrepMonthlyRefillExtract");
            if (null != prepMonthlyRefillProfile)
            {
                var prepMonthlyRefillCommand = new ExtractPrepMonthlyRefill()
                {
                    Extract = prepMonthlyRefillProfile?.Extract,
                    DatabaseProtocol = prepMonthlyRefillProfile?.DatabaseProtocol
                };
                ts4.Add( _mediator.Send(prepMonthlyRefillCommand, cancellationToken));
            }



            var result1 = await Task.WhenAll(ts1);
            var result2 = await Task.WhenAll(ts2);
            var result3 = await Task.WhenAll(ts3);
            var result4 = await Task.WhenAll(ts4);

            var result = new List<bool>();

            result.AddRange(result1);
            result.AddRange(result2);
            result.AddRange(result3);
            result.AddRange(result4);

            return result.All(x => x);
        }
    }
}
