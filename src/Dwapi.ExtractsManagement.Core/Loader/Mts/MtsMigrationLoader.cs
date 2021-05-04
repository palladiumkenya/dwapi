using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Source.Mts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Mts
{
    public class MtsMigrationLoader : IMtsMigrationLoader
    {
        private readonly IIndicatorExtractRepository _extractRepository;
        private readonly ITempIndicatorExtractRepository _tempExtractRepository;
        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public MtsMigrationLoader(IIndicatorExtractRepository extractRepository, ITempIndicatorExtractRepository tempExtractRepository)
        {
            _extractRepository = extractRepository;
            _tempExtractRepository = tempExtractRepository;
        }

        public Task<int> Load(bool diffSupport)
        {
            var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;
            try
            {
                var tempIndicatorExtracts = _tempExtractRepository.GetAll().ToList();

                //Auto mapper
                var extractRecords = mapper.Map<List<TempIndicatorExtract>, List<IndicatorExtract>>(tempIndicatorExtracts);

                //Batch Insert
                _extractRepository.CreateBatch(extractRecords);
                Log.Debug("saved batch");

                // DomainEvents.Dispatch(new MgsNotification(new ExtractProgress(nameof(IndicatorExtract), "Loading...", Found, 0, 0, 0, 0)));
                return Task.FromResult(tempIndicatorExtracts.Count);

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(IndicatorExtract)} not Loaded");
                throw;
            }
        }

        public Task<int> Load(Guid extractId, int found,bool diffSupport)
        {
            Found = found;
            ExtractId = extractId;
            return Load(diffSupport);
        }
    }
}
