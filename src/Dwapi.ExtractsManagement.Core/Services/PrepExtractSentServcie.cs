using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public class PrepExtractSentServcie : IPrepExtractSentServcie
    {
        private readonly IPatientPrepExtractRepository _patientPrepExtractRepository;
        private readonly IPrepAdverseEventExtractRepository _prepAdverseEventExtractRepository;
        private readonly IPrepBehaviourRiskExtractRepository _prepBehaviourRiskExtractRepository;
        private readonly IPrepCareTerminationExtractRepository _prepCareTerminationExtractRepository;
        private readonly IPrepLabExtractRepository _prepLabExtractRepository;
        private readonly IPrepPharmacyExtractRepository _prepPharmacyExtractRepository;
        private readonly IPrepVisitExtractRepository _prepVisitExtractRepository;

        public PrepExtractSentServcie(IPatientPrepExtractRepository patientPrepExtractRepository, IPrepAdverseEventExtractRepository prepAdverseEventExtractRepository, IPrepBehaviourRiskExtractRepository prepBehaviourRiskExtractRepository, IPrepCareTerminationExtractRepository prepCareTerminationExtractRepository, IPrepLabExtractRepository prepLabExtractRepository, IPrepPharmacyExtractRepository prepPharmacyExtractRepository, IPrepVisitExtractRepository prepVisitExtractRepository)
        {
            _patientPrepExtractRepository = patientPrepExtractRepository;
            _prepAdverseEventExtractRepository = prepAdverseEventExtractRepository;
            _prepBehaviourRiskExtractRepository = prepBehaviourRiskExtractRepository;
            _prepCareTerminationExtractRepository = prepCareTerminationExtractRepository;
            _prepLabExtractRepository = prepLabExtractRepository;
            _prepPharmacyExtractRepository = prepPharmacyExtractRepository;
            _prepVisitExtractRepository = prepVisitExtractRepository;
        }

        public void UpdateSendStatus(ExtractType extractType, List<SentItem> sentItems)
        {
            try
            {
                switch (extractType)
                {
                    case ExtractType.Patient:
                        _patientPrepExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PrepAdverseEvent:
                        _prepAdverseEventExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PrepBehaviourRisk:
                        _prepBehaviourRiskExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PrepCareTermination:
                        _prepCareTerminationExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PrepPharmacy:
                        _prepPharmacyExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PrepLab:
                        _prepLabExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PrepVisit:
                        _prepVisitExtractRepository.UpdateSendStatus(sentItems);
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
