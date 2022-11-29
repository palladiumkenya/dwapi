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
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.TempExtracts;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Hts
{
    public class HtsClientsLinkageLoader : IHtsClientsLinkageLoader
    {
        private readonly IHtsClientsLinkageExtractRepository _clientsLinkageExtractRepository;
        private readonly ITempHtsClientsLinkageExtractRepository _tempHtsClientsLinkageExtractRepository;
        private readonly IMediator _mediator;

        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HtsClientsLinkageLoader(IHtsClientsLinkageExtractRepository clientsLinkageExtractRepository, ITempHtsClientsLinkageExtractRepository tempHtsClientsLinkageExtractRepository, IMediator mediator)
        {
            _clientsLinkageExtractRepository = clientsLinkageExtractRepository;
            _tempHtsClientsLinkageExtractRepository = tempHtsClientsLinkageExtractRepository;
            _mediator = mediator;

        }

        public async Task<int> Load(bool diffSupport)
        {
            var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;
            int count = 0;

            try
            {
                const int take = 1000;
                var eCount = await  _tempHtsClientsLinkageExtractRepository.GetCleanCount();
                var pageCount = _tempHtsClientsLinkageExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempHtsClientLinkages =await
                        _tempHtsClientsLinkageExtractRepository.GetAll(QueryUtil.Linkage, page, take);

                    var batch = tempHtsClientLinkages.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempHtsClientLinkage>, List<HtsClientLinkage>>(batch);
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
                }
                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientLinkage), "Loading...", Found, 0, 0, 0, 0)));
               
                // int extractssitecode = (int) _clientsLinkageExtractRepository.GetSiteCode(QueryUtil.Linkage).SiteCode;
                // await _mediator.Publish(new DocketExtractLoaded("HTS", nameof(HtsClientLinkage), extractssitecode));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(HtsClientLinkage)} not Loaded");
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
