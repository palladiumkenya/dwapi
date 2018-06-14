using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Services
{
    public class DwhExtractSentServcie: IDwhExtractSentServcie
    {
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IPatientArtExtractRepository _artExtractRepository;

        public DwhExtractSentServcie(IPatientExtractRepository patientExtractRepository, IPatientArtExtractRepository artExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _artExtractRepository = artExtractRepository;
        }

        public void UpdateSendStatus(ExtractType extractType, List<SentItem> sentItems)
        {
            try
            {
                if (extractType == ExtractType.Patient)
                    _patientExtractRepository.UpdateSendStatus(sentItems);

                if (extractType == ExtractType.PatientArt)
                    _artExtractRepository.UpdateSendStatus(sentItems);
            }
            catch (Exception e)
            {
                Log.Error(e,"Sent status");
            }
            
        }
    }
}