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

        public DwhExtractSentServcie(IPatientExtractRepository patientExtractRepository, IPatientArtExtractRepository artExtractRepository, IPatientBaselinesExtractRepository baselinesExtractRepository, IPatientLaboratoryExtractRepository patientLaboratoryExtractRepository, IPatientPharmacyExtractRepository patientPharmacyExtractRepository, IPatientStatusExtractRepository patientStatusExtractRepository, IPatientVisitExtractRepository patientVisitExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _artExtractRepository = artExtractRepository;
            _baselinesExtractRepository = baselinesExtractRepository;
            _laboratoryExtractRepository = patientLaboratoryExtractRepository;
            _pharmacyExtractRepository = patientPharmacyExtractRepository;
            _statusExtractRepository = patientStatusExtractRepository;
            _visitExtractRepository = patientVisitExtractRepository;
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
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Sent status");
            }
        }
    }
}