using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Hts
{
    public class HtsClientsLinkageLoader : IHtsClientsLinkageLoader
    {
        private readonly IHtsClientsLinkageExtractRepository _clientsLinkageExtractRepository;
        private readonly ITempHtsClientsLinkageExtractRepository _tempHtsClientsLinkageExtractRepository;
        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HtsClientsLinkageLoader(IHtsClientsLinkageExtractRepository clientsLinkageExtractRepository, ITempHtsClientsLinkageExtractRepository tempHtsClientsLinkageExtractRepository)
        {
            _clientsLinkageExtractRepository = clientsLinkageExtractRepository;
            _tempHtsClientsLinkageExtractRepository = tempHtsClientsLinkageExtractRepository;
        }

        public async Task<int> Load()
        {
            int count = 0;

            try
            {
                /*
                   DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                 */

                const int take = 1000;
                var eCount = await  _tempHtsClientsLinkageExtractRepository.GetCleanCount();
                var pageCount = _tempHtsClientsLinkageExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempHtsClientLinkages =await
                        _tempHtsClientsLinkageExtractRepository.GetAll(a => a.ErrorType == 0, page, take);

                    var batch = tempHtsClientLinkages.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = Mapper.Map<List<TempHtsClientLinkage>, List<HtsClientLinkage>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _clientsLinkageExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(HtsClientLinkage)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    /*
                    DomainEvents.Dispatch(
                        new ExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientExtract),
                            nameof(ExtractStatus.Loading),
                            found, count, 0, 0, 0)));
                    */
                }
                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientLinkage), "Loading...", Found, 0, 0, 0, 0)));
                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(HtsClientLinkage)} not Loaded");
                throw;
            }
        }

        public Task<int> Load(Guid extractId, int found)
        {
            Found = found;
            ExtractId = extractId;
            return Load();
        }
    }
}
