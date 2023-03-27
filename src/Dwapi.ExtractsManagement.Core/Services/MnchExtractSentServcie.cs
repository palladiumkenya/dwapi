using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public class MnchExtractSentServcie : IMnchExtractSentServcie
    {
        private readonly IPatientMnchExtractRepository _patientMnchExtractRepository;
        private readonly IAncVisitExtractRepository _ancVisitExtractRepository;
        private readonly ICwcEnrolmentExtractRepository _cwcEnrolmentExtractRepository;
        private readonly ICwcVisitExtractRepository _cwcVisitExtractRepository;
        private readonly IHeiExtractRepository _heiExtractRepository;
        private readonly IMatVisitExtractRepository _matVisitExtractRepository;
        private readonly IMnchArtExtractRepository _mnchArtExtractRepository;
        private readonly IMnchEnrolmentExtractRepository _mnchEnrolmentExtractRepository;
        private readonly IMnchLabExtractRepository _mnchLabExtractRepository;
        private readonly IMotherBabyPairExtractRepository _motherBabyPairExtractRepository;
        private readonly IPncVisitExtractRepository _pncVisitExtractRepository;
        private readonly IMnchImmunizationExtractRepository _mnchImmunizationExtractRepository;


        public MnchExtractSentServcie(IPatientMnchExtractRepository patientMnchExtractRepository, IAncVisitExtractRepository ancVisitExtractRepository, ICwcEnrolmentExtractRepository cwcEnrolmentExtractRepository, ICwcVisitExtractRepository cwcVisitExtractRepository, IHeiExtractRepository heiExtractRepository, IMatVisitExtractRepository matVisitExtractRepository, IMnchArtExtractRepository mnchArtExtractRepository, IMnchEnrolmentExtractRepository mnchEnrolmentExtractRepository, IMnchLabExtractRepository mnchLabExtractRepository, IMotherBabyPairExtractRepository motherBabyPairExtractRepository, IPncVisitExtractRepository pncVisitExtractRepository, IMnchImmunizationExtractRepository mnchImmunizationExtractRepository)
        {
            _patientMnchExtractRepository = patientMnchExtractRepository;
            _ancVisitExtractRepository = ancVisitExtractRepository;
            _cwcEnrolmentExtractRepository = cwcEnrolmentExtractRepository;
            _cwcVisitExtractRepository = cwcVisitExtractRepository;
            _heiExtractRepository = heiExtractRepository;
            _matVisitExtractRepository = matVisitExtractRepository;
            _mnchArtExtractRepository = mnchArtExtractRepository;
            _mnchEnrolmentExtractRepository = mnchEnrolmentExtractRepository;
            _mnchLabExtractRepository = mnchLabExtractRepository;
            _motherBabyPairExtractRepository = motherBabyPairExtractRepository;
            _pncVisitExtractRepository = pncVisitExtractRepository;
            _mnchImmunizationExtractRepository = mnchImmunizationExtractRepository;
        }

        public void UpdateSendStatus(ExtractType extractType, List<SentItem> sentItems)
        {
            try
            {
                switch (extractType)
                {
                    case ExtractType.Patient:
                        _patientMnchExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.AncVisit:
                        _ancVisitExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.CwcEnrollment:
                        _cwcEnrolmentExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.CwcVisit:
                        _cwcVisitExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.Hei:
                        _heiExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.MatVisit:
                        _matVisitExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.MnchArt:
                        _mnchArtExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.MnchEnrollment:
                        _mnchEnrolmentExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.MnchLab:
                        _mnchLabExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.MotherBabyPair:
                        _motherBabyPairExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.PncVisit:
                        _pncVisitExtractRepository.UpdateSendStatus(sentItems);
                        break;
                    case ExtractType.MnchImmunization:
                        _mnchImmunizationExtractRepository.UpdateSendStatus(sentItems);
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
