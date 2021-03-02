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


            var patientProfile = request.Extracts.FirstOrDefault(x => x.Extract.IsPriority);
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
            Task<bool> t1 = null,
                t2 = null,
                t3 = null,
                t4 = null,
                t5 = null,
                t6 = null,
                t7 = null,
                t8 = null,
                t9 = null,
                t10 = null,
                t11 = null,
                t12 = null,
                t13 = null,
                t14 = null,
                t15 = null,
                t16 = null;
            ;
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
            var patientBaselinesProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientBaselineExtract");
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
            var patientPharmacyProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientPharmacyExtract");
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
            var patientAdverseEventProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "PatientAdverseEventExtract");
            if (null != patientAdverseEventProfile)
            {
                var patientAdverseEventCommand = new ExtractPatientAdverseEvent()
                {
                    Extract = patientAdverseEventProfile?.Extract,
                    DatabaseProtocol = patientAdverseEventProfile?.DatabaseProtocol
                };
                t7 = _mediator.Send(patientAdverseEventCommand, cancellationToken);
            }


            // ExtractAllergiesChronicIllness
            var allergiesChronicIllnessProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "AllergiesChronicIllnessExtract");
            if (null != allergiesChronicIllnessProfile)
            {
                var allergiesChronicIllnessCommand = new ExtractAllergiesChronicIllness()
                {
                    Extract = allergiesChronicIllnessProfile?.Extract,
                    DatabaseProtocol = allergiesChronicIllnessProfile?.DatabaseProtocol
                };
                t8 = _mediator.Send(allergiesChronicIllnessCommand, cancellationToken);
            }

            // ExtractContactListing
            var contactListingProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "ContactListingExtract");
            if (null != contactListingProfile)
            {
                var contactListingCommand = new ExtractContactListing()
                {
                    Extract = contactListingProfile?.Extract,
                    DatabaseProtocol = contactListingProfile?.DatabaseProtocol
                };
                t9 = _mediator.Send(contactListingCommand, cancellationToken);
            }

            // ExtractDepressionScreening
            var depressionScreeningProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "DepressionScreeningExtract");
            if (null != depressionScreeningProfile)
            {
                var depressionScreeningCommand = new ExtractDepressionScreening()
                {
                    Extract = depressionScreeningProfile?.Extract,
                    DatabaseProtocol = depressionScreeningProfile?.DatabaseProtocol
                };
                t10 = _mediator.Send(depressionScreeningCommand, cancellationToken);
            }

            // ExtractDrugAlcoholScreening
            var drugAlcoholScreeningProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "DrugAlcoholScreeningExtract");
            if (null != drugAlcoholScreeningProfile)
            {
                var drugAlcoholScreeningCommand = new ExtractDrugAlcoholScreening()
                {
                    Extract = drugAlcoholScreeningProfile?.Extract,
                    DatabaseProtocol = drugAlcoholScreeningProfile?.DatabaseProtocol
                };
                t11 = _mediator.Send(drugAlcoholScreeningCommand, cancellationToken);
            }

            // ExtractEnhancedAdherenceCounselling
            var enhancedAdherenceCounsellingProfile =
                request.Extracts.FirstOrDefault(x => x.Extract.Name == "EnhancedAdherenceCounsellingExtract");
            if (null != enhancedAdherenceCounsellingProfile)
            {
                var enhancedAdherenceCounsellingCommand = new ExtractEnhancedAdherenceCounselling()
                {
                    Extract = enhancedAdherenceCounsellingProfile?.Extract,
                    DatabaseProtocol = enhancedAdherenceCounsellingProfile?.DatabaseProtocol
                };
                t12 = _mediator.Send(enhancedAdherenceCounsellingCommand, cancellationToken);
            }

            // ExtractGbvScreening
            var gbvScreeningProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "GbvScreeningExtract");
            if (null != gbvScreeningProfile)
            {
                var gbvScreeningCommand = new ExtractGbvScreening()
                {
                    Extract = gbvScreeningProfile?.Extract,
                    DatabaseProtocol = gbvScreeningProfile?.DatabaseProtocol
                };
                t13 = _mediator.Send(gbvScreeningCommand, cancellationToken);
            }

            // ExtractPatientVisit
            var iptProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "IptExtract");
            if (null != iptProfile)
            {
                var iptCommand = new ExtractIpt()
                {
                    Extract = iptProfile?.Extract,
                    DatabaseProtocol = iptProfile?.DatabaseProtocol
                };
                t14 = _mediator.Send(iptCommand, cancellationToken);
            }


            // ExtractOtz
            var otzProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "OtzExtract");
            if (null != otzProfile)
            {
                var otzCommand = new ExtractOtz()
                {
                    Extract = otzProfile?.Extract,
                    DatabaseProtocol = otzProfile?.DatabaseProtocol
                };
                t15 = _mediator.Send(otzCommand, cancellationToken);
            }

            // ExtractOvc
            var ovcProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "OvcExtract");
            if (null != ovcProfile)
            {
                var ovcCommand = new ExtractOvc()
                {
                    Extract = ovcProfile?.Extract,
                    DatabaseProtocol = ovcProfile?.DatabaseProtocol
                };
                t16 = _mediator.Send(ovcCommand, cancellationToken);
            }





            // await all tasks
            var ts = new List<Task<bool>> {t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16};
            var result = await Task.WhenAll(ts);

            return result.All(x => x);
        }
    }
}
