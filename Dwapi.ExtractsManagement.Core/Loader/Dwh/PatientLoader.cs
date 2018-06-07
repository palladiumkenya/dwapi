using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Dwh
{
    public class PatientLoader : IPatientLoader
    {
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly ITempPatientExtractRepository _tempPatientExtractRepository;

        public PatientLoader(IPatientExtractRepository patientExtractRepository, ITempPatientExtractRepository tempPatientExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _tempPatientExtractRepository = tempPatientExtractRepository;
        }

        public async Task<int> Load(int found)
        {
            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(new DwhProgress(
                        nameof(PatientExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                //load temp extracts without errors
                var tempPatientExtracts = _tempPatientExtractRepository.GetAll().Where(a=>a.CheckError == false).ToList();

                //Auto mapper
                var extractRecords = Mapper.Map<List<TempPatientExtract>, List<PatientExtract>>(tempPatientExtracts);

                //Batch Insert
                _patientExtractRepository.BatchInsert(extractRecords);
                Log.Debug("saved batch");


                return tempPatientExtracts.Count;

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientExtract)} not Loaded");
                return 0;
            }
        }
    }
}
