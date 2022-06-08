using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Crs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.ExtractsManagement.Core.Model.Source.Crs;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Crs
{
    public class ClientRegistryExtractLoader : IClientRegistryExtractLoader
    {
        private readonly IClientRegistryExtractRepository _clientRegistryExtractRepository;
        private readonly ITempClientRegistryExtractRepository _tempClientRegistryExtractRepository;
        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public ClientRegistryExtractLoader(IClientRegistryExtractRepository clientRegistryExtractRepository, ITempClientRegistryExtractRepository tempClientRegistryExtractRepository)
        {
            _clientRegistryExtractRepository = clientRegistryExtractRepository;
            _tempClientRegistryExtractRepository = tempClientRegistryExtractRepository;
        }

        public Task<int> Load(bool diffSupport)
        {
            var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;
            try
            {
                //load temp extracts without errors
                var tempClientRegistryExtracts = _tempClientRegistryExtractRepository.GetAll().Where(a=>a.CheckError == false).ToList();

                //Auto mapper
                var extractRecords = mapper.Map<List<TempClientRegistryExtract>, List<ClientRegistryExtract>>(tempClientRegistryExtracts);

                //Batch Insert
                // TODO PLEASE PAGE DIS
                _clientRegistryExtractRepository.BatchInsert(extractRecords);
                Log.Debug("saved batch");

                DomainEvents.Dispatch(new CrsNotification(new ExtractProgress(nameof(ClientRegistryExtract), "Loading...", Found, 0, 0, 0, 0)));
                return Task.FromResult(tempClientRegistryExtracts.Count);

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(ClientRegistryExtract)} not Loaded");
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
