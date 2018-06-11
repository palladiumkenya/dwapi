using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders;
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
    public class PatientLaboratoryLoader : IPatientLaboratoryLoader
    {
        private readonly IPatientLaboratoryExtractRepository _patientLaboratoryExtractRepository;
        private readonly ITempPatientLaboratoryExtractRepository _tempPatientLaboratoryExtractRepository;

        public PatientLaboratoryLoader(IPatientLaboratoryExtractRepository patientLaboratoryExtractRepository, ITempPatientLaboratoryExtractRepository tempPatientLaboratoryExtractRepository)
        {
            _patientLaboratoryExtractRepository = patientLaboratoryExtractRepository;
            _tempPatientLaboratoryExtractRepository = tempPatientLaboratoryExtractRepository;
        }

        public async Task<int> Load(Guid extractId, int found)
        {
            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientLaboratoryExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                //load temp extracts without errors
                StringBuilder query = new StringBuilder();
                query.Append($" SELECT * FROM {nameof(TempPatientLaboratoryExtract)}s s");
                query.Append($" INNER JOIN PatientExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");
                query.Append($" WHERE s.CheckError = 0");

                var tempPatientLaboratoryExtracts = await _tempPatientLaboratoryExtractRepository.GetFromSql(query.ToString());

                //Auto mapper
                var extractRecords = Mapper.Map<List<TempPatientLaboratoryExtract>, List<PatientLaboratoryExtract>>(tempPatientLaboratoryExtracts);

                //Batch Insert
                _patientLaboratoryExtractRepository.BatchInsert(extractRecords);
                Log.Debug("saved batch");


                return tempPatientLaboratoryExtracts.Count;

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientLaboratoryExtract)} not Loaded");
                return 0;
            }
        }
    }
}
