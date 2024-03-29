using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Hts
{
    [Obsolete]
    public class HTSClientLinkageLoader : IHTSClientLinkageLoader
    {
        private readonly IHTSClientLinkageExtractRepository _patientExtractRepository;
        private readonly ITempHTSClientLinkageExtractRepository _tempPatientExtractRepository;
        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HTSClientLinkageLoader(IHTSClientLinkageExtractRepository patientExtractRepository, ITempHTSClientLinkageExtractRepository tempPatientExtractRepository)
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
                //var tempPatientExtracts = _tempPatientExtractRepository.GetAll().Where(a=>a.CheckError == false).ToList();
                var tempPatientExtracts = _tempPatientExtractRepository.GetAll().Where(a => a.ErrorType == 0).ToList();

                //Auto mapper
                var extractRecords = mapper.Map<List<TempHTSClientLinkageExtract>, List<HTSClientLinkageExtract>>(tempPatientExtracts);

                //Batch Insert
                _patientExtractRepository.BatchInsert(extractRecords);
                Log.Debug("saved batch");

                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HTSClientLinkageExtract), "Loading...", Found, 0, 0, 0, 0)));
                return Task.FromResult(tempPatientExtracts.Count);

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(HTSClientLinkageExtract)} not Loaded");
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
