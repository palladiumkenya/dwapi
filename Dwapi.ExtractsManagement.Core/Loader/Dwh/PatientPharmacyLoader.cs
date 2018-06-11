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
    public class PatientPharmacyLoader : IPatientPharmacyLoader
    {
        private readonly IPatientPharmacyExtractRepository _patientPharmacyExtractRepository;
        private readonly ITempPatientPharmacyExtractRepository _tempPatientPharmacyExtractRepository;

        public PatientPharmacyLoader(IPatientPharmacyExtractRepository patientPharmacyExtractRepository, ITempPatientPharmacyExtractRepository tempPatientPharmacyExtractRepository)
        {
            _patientPharmacyExtractRepository = patientPharmacyExtractRepository;
            _tempPatientPharmacyExtractRepository = tempPatientPharmacyExtractRepository;
        }

        public async Task<int> Load(int found)
        {
            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(new DwhProgress(
                        nameof(PatientPharmacyExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                //load temp extracts without errors
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append($" SELECT * FROM {nameof(TempPatientPharmacyExtract)}s s");
                queryBuilder.Append($" INNER JOIN PatientExtracts p ON ");
                queryBuilder.Append($" s.PatientPK = p.PatientPK AND ");
                queryBuilder.Append($" s.SiteCode = p.SiteCode ");
                queryBuilder.Append($" WHERE s.CheckError = 0");

                string query = queryBuilder.ToString();
                var tempPatientPharmacyExtracts = await _tempPatientPharmacyExtractRepository.GetFromSql(query);

                //Auto mapper
                var extractRecords = Mapper.Map<List<TempPatientPharmacyExtract>, List<PatientPharmacyExtract>>(tempPatientPharmacyExtracts);

                //Batch Insert
                _patientPharmacyExtractRepository.BatchInsert(extractRecords);
                Log.Debug("saved batch");


                return tempPatientPharmacyExtracts.Count;

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientPharmacyExtract)} not Loaded");
                return 0;
            }
        }
    }
}
