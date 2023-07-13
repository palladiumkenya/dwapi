using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands.Mnch;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class LoadMnchFromEmrHandler : IRequestHandler<LoadMnchFromEmrCommand, bool>
    {
        private IMediator _mediator;

        public LoadMnchFromEmrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(LoadMnchFromEmrCommand request, CancellationToken cancellationToken)
        {
            var extractIds = request.Extracts.Select(x => x.Extract.Id).ToList();

            await _mediator.Send(new ClearAllMnchExtracts(extractIds), cancellationToken);


            var patientProfile = request.Extracts.FirstOrDefault(x => x.Extract.IsPriority);
            //Generate extract patient command
            if (patientProfile != null)
            {
                var extractPatient = new ExtractPatientMnch()
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

        private async Task<bool> ExtractAll(LoadMnchFromEmrCommand request, CancellationToken cancellationToken)
        {
            var ts1 = new List<Task<bool>>();
            var ts2 = new List<Task<bool>>();
            var ts3 = new List<Task<bool>>();
            var ts4 = new List<Task<bool>>();

            // ExtractMnchEnrolmentExtract
            var patientArtProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "MnchEnrolmentExtract");
            if (null != patientArtProfile)
            {
                var patientArtCommand = new ExtractMnchEnrolment()
                {
                    Extract = patientArtProfile?.Extract,
                    DatabaseProtocol = patientArtProfile?.DatabaseProtocol
                };
                ts1.Add(_mediator.Send(patientArtCommand, cancellationToken));
            }

            // ExtractMnchArtExtract
            var patientBaselinesProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "MnchArtExtract");
            if (null != patientBaselinesProfile)
            {
                var patientBaselinesCommand = new ExtractMnchArt()
                {
                    Extract = patientBaselinesProfile?.Extract,
                    DatabaseProtocol = patientBaselinesProfile?.DatabaseProtocol
                };
                ts1.Add( _mediator.Send(patientBaselinesCommand, cancellationToken));
            }

            // ExtractAncVisitExtract
            var patientLaboratoryProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "AncVisitExtract");
            if (null != patientLaboratoryProfile)
            {
                var patientLaboratoryCommand = new ExtractAncVisit()
                {
                    Extract = patientLaboratoryProfile?.Extract,
                    DatabaseProtocol = patientLaboratoryProfile?.DatabaseProtocol
                };
                ts1.Add( _mediator.Send(patientLaboratoryCommand, cancellationToken));
            }

            // ExtractMatVisitExtract
            var patientPharmacyProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "MatVisitExtract");
            if (null != patientPharmacyProfile)
            {
                var patientPharmacyCommand = new ExtractMatVisit()
                {
                    Extract = patientPharmacyProfile?.Extract,
                    DatabaseProtocol = patientPharmacyProfile?.DatabaseProtocol
                };
                ts2.Add(_mediator.Send(patientPharmacyCommand, cancellationToken));
            }

            // ExtractPncVisitExtract
            var patientStatusProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "PncVisitExtract");
            if (null != patientStatusProfile)
            {
                var patientStatusCommand = new ExtractPncVisit()
                {
                    Extract = patientStatusProfile?.Extract,
                    DatabaseProtocol = patientStatusProfile?.DatabaseProtocol
                };
                ts2.Add( _mediator.Send(patientStatusCommand, cancellationToken));
            }

            // ExtractMotherBabyPairExtract
            var patientVisitProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "MotherBabyPairExtract");
            if (null != patientVisitProfile)
            {
                var patientVisitCommand = new ExtractMotherBabyPair()
                {
                    Extract = patientVisitProfile?.Extract,
                    DatabaseProtocol = patientVisitProfile?.DatabaseProtocol
                };
                ts2.Add(_mediator.Send(patientVisitCommand, cancellationToken));
            }

            // ExtractCwcEnrolmentExtract
            var patientAdverseEventProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "CwcEnrolmentExtract");
            if (null != patientAdverseEventProfile)
            {
                var patientAdverseEventCommand = new ExtractCwcEnrolment()
                {
                    Extract = patientAdverseEventProfile?.Extract,
                    DatabaseProtocol = patientAdverseEventProfile?.DatabaseProtocol
                };
                ts3.Add( _mediator.Send(patientAdverseEventCommand, cancellationToken));
            }


            // ExtractCwcVisitExtract
            var allergiesChronicIllnessProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "CwcVisitExtract");
            if (null != allergiesChronicIllnessProfile)
            {
                var allergiesChronicIllnessCommand = new ExtractCwcVisit()
                {
                    Extract = allergiesChronicIllnessProfile?.Extract,
                    DatabaseProtocol = allergiesChronicIllnessProfile?.DatabaseProtocol
                };
                ts3.Add(_mediator.Send(allergiesChronicIllnessCommand, cancellationToken));
            }

            // ExtractHeiExtract
            var contactListingProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "HeiExtract");
            if (null != contactListingProfile)
            {
                var contactListingCommand = new ExtractHei()
                {
                    Extract = contactListingProfile?.Extract,
                    DatabaseProtocol = contactListingProfile?.DatabaseProtocol
                };
                ts3.Add(_mediator.Send(contactListingCommand, cancellationToken));
            }

            // ExtractMnchLabExtract
            var depressionScreeningProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "MnchLabExtract");
            if (null != depressionScreeningProfile)
            {
                var depressionScreeningCommand = new ExtractMnchLab()
                {
                    Extract = depressionScreeningProfile?.Extract,
                    DatabaseProtocol = depressionScreeningProfile?.DatabaseProtocol
                };
                ts4.Add( _mediator.Send(depressionScreeningCommand, cancellationToken));
            }
            
            // ExtractMnchImmunizationExtract
            var mnchImmunizationProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "MnchImmunizationExtract");
            if (null != mnchImmunizationProfile)
            {
                var mnchImmunizationCommand = new ExtractMnchImmunization()
                {
                    Extract = mnchImmunizationProfile?.Extract,
                    DatabaseProtocol = mnchImmunizationProfile?.DatabaseProtocol
                };
                ts4.Add( _mediator.Send(mnchImmunizationCommand, cancellationToken));
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
