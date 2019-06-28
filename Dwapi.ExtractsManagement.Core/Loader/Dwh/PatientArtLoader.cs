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
            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientArtExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                //load temp extracts without errors
                StringBuilder querybuilder = new StringBuilder();
                querybuilder.Append($" SELECT * FROM {nameof(TempPatientArtExtract)}s s");
                querybuilder.Append($" INNER JOIN PatientExtracts p ON ");
                querybuilder.Append($" s.PatientPK = p.PatientPK AND ");
                querybuilder.Append($" s.SiteCode = p.SiteCode ");
                //querybuilder.Append($" WHERE s.CheckError = 0");

                string query = querybuilder.ToString();

                var tempPatientArtExtracts = _tempPatientArtExtractRepository.GetFromSql(query);

                const int take = 1000;
                int skip = 0;
                var count = tempPatientArtExtracts.Count();
                while (skip < count)
                {
                    var batch = tempPatientArtExtracts.Skip(skip).Take(take).ToList();
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
                    skip = skip + take;
                    DomainEvents.Dispatch(
                        new ExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientArtExtract),
                            nameof(ExtractStatus.Loading),
                            found, skip, 0, 0, 0)));
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
