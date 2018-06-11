using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class PatientBaselinesLoader : IPatientBaselinesLoader
    {
        private readonly IPatientBaselinesExtractRepository _patientBaselinesExtractRepository;
        private readonly ITempPatientBaselinesExtractRepository _tempPatientBaselinesExtractRepository;

        public PatientBaselinesLoader(IPatientBaselinesExtractRepository patientBaselinesExtractRepository, ITempPatientBaselinesExtractRepository tempPatientBaselinesExtractRepository)
        {
            _patientBaselinesExtractRepository = patientBaselinesExtractRepository;
            _tempPatientBaselinesExtractRepository = tempPatientBaselinesExtractRepository;
        }

        public async Task<int> Load(int found)
        {
            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(new DwhProgress(
                        nameof(PatientBaselinesExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                //load temp extracts without errors
                StringBuilder query = new StringBuilder();
                query.Append($" SELECT * FROM {nameof(TempPatientBaselinesExtract)}s s");
                query.Append($" INNER JOIN PatientExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");
                query.Append($" WHERE s.CheckError = 0");

                var tempPatientBaselinesExtracts = await _tempPatientBaselinesExtractRepository.GetFromSql(query.ToString());

                //Auto mapper
                var extractRecords = Mapper.Map<List<TempPatientBaselinesExtract>, List<PatientBaselinesExtract>>(tempPatientBaselinesExtracts);

                //Batch Insert
                _patientBaselinesExtractRepository.BatchInsert(extractRecords);
                Log.Debug("saved batch");


                return tempPatientBaselinesExtracts.Count;

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientBaselinesExtract)} not Loaded");
                return 0;
            }
        }
    }
}
