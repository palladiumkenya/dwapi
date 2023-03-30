using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Hts
{
    public class HtsClientsLoader : IHtsClientsLoader
    {
        private readonly IHtsClientsExtractRepository _htsClientsExtractRepository;
        private readonly ITempHtsClientsExtractRepository _tempHtsClientsExtractRepository;
        private readonly IMediator _mediator;

        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HtsClientsLoader(IHtsClientsExtractRepository htsClientsExtractRepository, ITempHtsClientsExtractRepository tempHtsClientsExtractRepository,IMediator mediator)
        {
            _htsClientsExtractRepository = htsClientsExtractRepository;
            _tempHtsClientsExtractRepository = tempHtsClientsExtractRepository;
            _mediator = mediator;

        }

        public async Task<int> Load(bool diffSupport)
        {
            var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;
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
                int extractssitecode = 0;

                while (page <= pageCount)
                {
                    var tempHtsClients =await
                        _tempHtsClientsExtractRepository.GetAll(a => a.ErrorType == 0, page, take);

                    var batch = tempHtsClients.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempHtsClients>, List<HtsClients>>(batch);
                    extractssitecode = extractRecords.First().SiteCode;

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
                
                _mediator.Publish(new DocketExtractLoaded("HTS", "HtsClientsExtract", extractssitecode));

                return count;

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(HtsClients)} not Loaded");
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
