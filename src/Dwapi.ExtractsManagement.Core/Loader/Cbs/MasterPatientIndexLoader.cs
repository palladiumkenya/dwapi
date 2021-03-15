using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Cbs
{
    public class MasterPatientIndexLoader : IMasterPatientIndexLoader
    {
        private readonly IMasterPatientIndexRepository _patientExtractRepository;
        private readonly ITempMasterPatientIndexRepository _tempPatientExtractRepository;
        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public MasterPatientIndexLoader(IMasterPatientIndexRepository patientExtractRepository, ITempMasterPatientIndexRepository tempPatientExtractRepository)
        {
            _patientExtractRepository = patientExtractRepository;
            _tempPatientExtractRepository = tempPatientExtractRepository;
        }

        public Task<int> Load(bool diffSupport)
        {
            var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;
            try
            {
                //load temp extracts without errors
                var tempPatientExtracts = _tempPatientExtractRepository.GetAll().Where(a=>a.CheckError == false).ToList();

                //Auto mapper
                var extractRecords = mapper.Map<List<TempMasterPatientIndex>, List<MasterPatientIndex>>(tempPatientExtracts);

                //Batch Insert
                // TODO PLEASE PAGE DIS
                _patientExtractRepository.BatchInsert(extractRecords);
                Log.Debug("saved batch");

                DomainEvents.Dispatch(new CbsNotification(new ExtractProgress(nameof(MasterPatientIndex), "Loading...", Found, 0, 0, 0, 0)));
                return Task.FromResult(tempPatientExtracts.Count);

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(MasterPatientIndex)} not Loaded");
                throw;
            }
        }

        public Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            Found = found;
            ExtractId = extractId;
            return Load(diffSupport);
        }
    }
}
