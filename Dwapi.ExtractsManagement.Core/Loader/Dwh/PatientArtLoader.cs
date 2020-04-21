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
    public class PatientArtLoader : IPatientArtLoader
    {
        private readonly IPatientArtExtractRepository _patientArtExtractRepository;
        private readonly ITempPatientArtExtractRepository _tempPatientArtExtractRepository;

        public PatientArtLoader(IPatientArtExtractRepository patientArtExtractRepository, ITempPatientArtExtractRepository tempPatientArtExtractRepository)
        {
            _patientArtExtractRepository = patientArtExtractRepository;
            _tempPatientArtExtractRepository = tempPatientArtExtractRepository;
        }

        public async Task<int> Load(Guid extractId, int found)
        {
            int count = 0;

            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientArtExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));


                StringBuilder query = new StringBuilder();
                query.Append($" SELECT * FROM {nameof(TempPatientArtExtract)}s s");
                query.Append($" INNER JOIN PatientExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");

                const int take = 1000;
                var eCount = await  _tempPatientArtExtractRepository.GetCount(query.ToString());
                var pageCount = _tempPatientArtExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempPatientArtExtracts =await
                        _tempPatientArtExtractRepository.GetAll(query.ToString(), page, take);

                    var batch = tempPatientArtExtracts.ToList();
                    count += batch.Count;

                    //Auto mapper
                    var extractRecords = Mapper.Map<List<TempPatientArtExtract>, List<PatientArtExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _patientArtExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PatientArtExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new ExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientArtExtract),
                            nameof(ExtractStatus.Loading),
                            found, count, 0, 0, 0)));
                }
                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientArtExtract)} not Loaded");
                return 0;
            }
        }
    }
}
