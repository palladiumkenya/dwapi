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
    public class HtsClientsLoader : IHtsClientsLoader
    {
        private readonly IHtsClientsExtractRepository _htsClientsExtractRepository;
        private readonly ITempHtsClientsExtractRepository _tempHtsClientsExtractRepository;
        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HtsClientsLoader(IHtsClientsExtractRepository htsClientsExtractRepository, ITempHtsClientsExtractRepository tempHtsClientsExtractRepository)
        {
            _htsClientsExtractRepository = htsClientsExtractRepository;
            _tempHtsClientsExtractRepository = tempHtsClientsExtractRepository;
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
                var eCount = await  _tempHtsClientsExtractRepository.GetCleanCount();
                var pageCount = _tempHtsClientsExtractRepository.PageCount(take, eCount);
                int page = 1;
                while (page <= pageCount)
                {
                    var tempHtsClients =await
                        _tempHtsClientsExtractRepository.GetAll(a => a.ErrorType == 0, page, take);

                    var batch = tempHtsClients.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = Mapper.Map<List<TempHtsClients>, List<HtsClients>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _htsClientsExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(HtsClients)} not Loaded");
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

                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClients), "Loading...", Found, 0, 0, 0, 0)));
                return count;

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(HtsClients)} not Loaded");
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
