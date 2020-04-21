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
    public class PatientPharmacyLoader : IPatientPharmacyLoader
    {
        private readonly IPatientPharmacyExtractRepository _patientPharmacyExtractRepository;
        private readonly ITempPatientPharmacyExtractRepository _tempPatientPharmacyExtractRepository;

        public PatientPharmacyLoader(IPatientPharmacyExtractRepository patientPharmacyExtractRepository, ITempPatientPharmacyExtractRepository tempPatientPharmacyExtractRepository)
        {
            _patientPharmacyExtractRepository = patientPharmacyExtractRepository;
            _tempPatientPharmacyExtractRepository = tempPatientPharmacyExtractRepository;
        }

        public async Task<int> Load(Guid extractId, int found)
        {
            int count = 0;

            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientPharmacyExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append($" SELECT s.* FROM {nameof(TempPatientPharmacyExtract)}s s");
                queryBuilder.Append($" INNER JOIN PatientExtracts p ON ");
                queryBuilder.Append($" s.PatientPK = p.PatientPK AND ");
                queryBuilder.Append($" s.SiteCode = p.SiteCode ");

                const int take = 5000;
                var eCount = await  _tempPatientPharmacyExtractRepository.GetCount(queryBuilder.ToString());
                var pageCount = _tempPatientPharmacyExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempPatientPharmacyExtracts =await
                        _tempPatientPharmacyExtractRepository.GetAll(queryBuilder.ToString(), page, take);

                    var batch = tempPatientPharmacyExtracts.ToList();
                    count += batch.Count();

                    //Auto mapper
                    var extractRecords = Mapper.Map<List<TempPatientPharmacyExtract>, List<PatientPharmacyExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _patientPharmacyExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PatientPharmacyExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new ExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientPharmacyExtract),
                            nameof(ExtractStatus.Loading),
                            found, count, 0, 0, 0)));
                }
                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientPharmacyExtract)} not Loaded");
                return 0;
            }
        }
    }
}
