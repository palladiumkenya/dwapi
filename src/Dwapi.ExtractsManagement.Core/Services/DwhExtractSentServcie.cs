using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Serilog;
using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public class DwhExtractSentServcie : IDwhExtractSentServcie
    {
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IPatientArtExtractRepository _artExtractRepository;
        private readonly IPatientBaselinesExtractRepository _baselinesExtractRepository;
        private readonly IPatientLaboratoryExtractRepository _laboratoryExtractRepository;
        private readonly IPatientPharmacyExtractRepository _pharmacyExtractRepository;
        private readonly IPatientStatusExtractRepository _statusExtractRepository;
        private readonly IPatientVisitExtractRepository _visitExtractRepository;
        private readonly IPatientAdverseEventExtractRepository _adverseEventExtractRepository;

        private readonly IAllergiesChronicIllnessExtractRepository _allergiesChronicIllnessExtractRepository;
        private readonly IIptExtractRepository _iptExtractRepository;
        private readonly IDepressionScreeningExtractRepository _depressionScreeningExtractRepository;
        private readonly IContactListingExtractRepository _contactListingExtractRepository;
        private readonly IGbvScreeningExtractRepository _gbvScreeningExtractRepository;
        private readonly IEnhancedAdherenceCounsellingExtractRepository _enhancedAdherenceCounsellingExtractRepository;
        private readonly IDrugAlcoholScreeningExtractRepository _drugAlcoholScreeningExtractRepository;
        private readonly IOvcExtractRepository _ovcExtractRepository;
        private readonly IOtzExtractRepository _otzExtractRepository;


        public DwhExtractSentServcie(IPatientExtractRepository patientExtractRepository, IPatientArtExtractRepository artExtractRepository, IPatientBaselinesExtractRepository baselinesExtractRepository, IPatientLaboratoryExtractRepository laboratoryExtractRepository, IPatientPharmacyExtractRepository pharmacyExtractRepository, IPatientStatusExtractRepository statusExtractRepository, IPatientVisitExtractRepository visitExtractRepository, IPatientAdverseEventExtractRepository adverseEventExtractRepository, IAllergiesChronicIllnessExtractRepository allergiesChronicIllnessExtractRepository, IIptExtractRepository iptExtractRepository, IDepressionScreeningExtractRepository depressionScreeningExtractRepository, IContactListingExtractRepository contactListingExtractRepository, IGbvScreeningExtractRepository gbvScreeningExtractRepository, IEnhancedAdherenceCounsellingExtractRepository enhancedAdherenceCounsellingExtractRepository, IDrugAlcoholScreeningExtractRepository drugAlcoholScreeningExtractRepository, IOvcExtractRepository ovcExtractRepository, IOtzExtractRepository otzExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _artExtractRepository = artExtractRepository;
            _baselinesExtractRepository = baselinesExtractRepository;
            _laboratoryExtractRepository = laboratoryExtractRepository;
            _pharmacyExtractRepository = pharmacyExtractRepository;
            _statusExtractRepository = statusExtractRepository;
            _visitExtractRepository = visitExtractRepository;
            _adverseEventExtractRepository = adverseEventExtractRepository;

            _allergiesChronicIllnessExtractRepository = allergiesChronicIllnessExtractRepository;
            _iptExtractRepository = iptExtractRepository;
            _depressionScreeningExtractRepository = depressionScreeningExtractRepository;
            _contactListingExtractRepository = contactListingExtractRepository;
            _gbvScreeningExtractRepository = gbvScreeningExtractRepository;
            _enhancedAdherenceCounsellingExtractRepository = enhancedAdherenceCounsellingExtractRepository;
            _drugAlcoholScreeningExtractRepository = drugAlcoholScreeningExtractRepository;
            _ovcExtractRepository = ovcExtractRepository;
            _otzExtractRepository = otzExtractRepository;
        }

        public void UpdateSendStatus(ExtractType extractType, List<SentItem> sentItems)
        {
            try
            {
                switch (extractType)
                {
                    case ExtractType.Patient:
                        _patientExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PatientArt:
                        _artExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PatientBaseline:
                        _baselinesExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PatientLab:
                        _laboratoryExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PatientPharmacy:
                        _pharmacyExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PatientStatus:
                        _statusExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PatientVisit:
                        _visitExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PatientAdverseEvent:
                        _adverseEventExtractRepository.UpdateSendStatus(sentItems);
                        break;


                    case ExtractType.AllergiesChronicIllness:
                        _allergiesChronicIllnessExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.ContactListing:
                        _contactListingExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.DepressionScreening:
                        _depressionScreeningExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.DrugAlcoholScreening:
                        _drugAlcoholScreeningExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.EnhancedAdherenceCounselling:
                        _enhancedAdherenceCounsellingExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.GbvScreening:
                        _gbvScreeningExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.Ipt:
                        _iptExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.Otz:
                        _otzExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.Ovc:
                        _ovcExtractRepository.UpdateSendStatus(sentItems);
                        break;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Sent status");
            }
        }
    }
}
