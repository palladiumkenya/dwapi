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
using Dwapi.SharedKernel.Utility;
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

        public async Task<int> Load(Guid extractId, int found)
        {
            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientBaselinesExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                //load temp extracts without errors
                StringBuilder query = new StringBuilder();
                query.Append($" SELECT * FROM {nameof(TempPatientBaselinesExtract)}s s");
                query.Append($" INNER JOIN PatientExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");
                //query.Append($" WHERE s.CheckError = 0"); //load all the takaka

                var tempPatientBaselinesExtracts = _tempPatientBaselinesExtractRepository.GetFromSql(query.ToString());

                const int take = 1000;
                int skip = 0;
                var count = tempPatientBaselinesExtracts.Count();
                while (skip < count)
                {
                    var batch = tempPatientBaselinesExtracts.Skip(skip).Take(take).ToList();
                    //Auto mapper
                    var extractRecords = Mapper.Map<List<TempPatientBaselinesExtract>, List<PatientBaselinesExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _patientBaselinesExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PatientBaselinesExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    skip = skip + take;
                    DomainEvents.Dispatch(
                        new ExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientBaselinesExtract),
                            nameof(ExtractStatus.Loading),
                            found, skip, 0, 0, 0)));
                }
                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientBaselinesExtract)} not Loaded");
                return 0;
            }
        }
    }
}
