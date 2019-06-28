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
    public class PatientAdverseEventLoader : IPatientAdverseEventLoader
    {
        private readonly IPatientAdverseEventExtractRepository _patientAdverseEventExtractRepository;
        private readonly ITempPatientAdverseEventExtractRepository _tempPatientAdverseEventExtractRepository;

        public PatientAdverseEventLoader(IPatientAdverseEventExtractRepository patientAdverseEventExtractRepository, ITempPatientAdverseEventExtractRepository tempPatientAdverseEventExtractRepository)
        {
            _patientAdverseEventExtractRepository = patientAdverseEventExtractRepository;
            _tempPatientAdverseEventExtractRepository = tempPatientAdverseEventExtractRepository;
        }

        public async Task<int> Load(Guid extractId, int found)
        {
            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientAdverseEventExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                //load temp extracts without errors
                StringBuilder query = new StringBuilder();
                query.Append($" SELECT * FROM {nameof(TempPatientAdverseEventExtract)}s s");
                query.Append($" INNER JOIN PatientExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");
                //query.Append($" WHERE s.CheckError = 0");

                var tempPatientAdverseEventExtracts = _tempPatientAdverseEventExtractRepository.GetFromSql(query.ToString());

                const int take = 1000;
                int skip = 0;
                var count = tempPatientAdverseEventExtracts.Count();
                while (skip < count)
                {
                    var batch = tempPatientAdverseEventExtracts.Skip(skip).Take(take).ToList();
                    //Auto mapper
                    var extractRecords = Mapper.Map<List<TempPatientAdverseEventExtract>, List<PatientAdverseEventExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _patientAdverseEventExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PatientAdverseEventExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    skip = skip + take;
                    DomainEvents.Dispatch(
                        new ExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientAdverseEventExtract),
                            nameof(ExtractStatus.Loading),
                            found, skip , 0, 0, 0)));
                }
                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientAdverseEventExtract)} not Loaded");
                return 0;
            }
        }
    }
}
