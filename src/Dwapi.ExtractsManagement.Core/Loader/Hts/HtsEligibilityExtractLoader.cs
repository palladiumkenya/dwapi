using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
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
    public class HtsEligibilityExtractLoader : IHtsEligibilityExtractLoader
    {
        private readonly IHtsEligibilityExtractRepository _htsEligibilityExtractRepository;
        private readonly ITempHtsEligibilityExtractRepository _tempHtsEligibilityExtractRepository;
        private readonly IMediator _mediator;

        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HtsEligibilityExtractLoader(IHtsEligibilityExtractRepository htsEligibilityExtractRepository, ITempHtsEligibilityExtractRepository tempHtsEligibilityExtractRepository,IMediator mediator)
        {
            _htsEligibilityExtractRepository = htsEligibilityExtractRepository;
            _tempHtsEligibilityExtractRepository = tempHtsEligibilityExtractRepository;
            _mediator = mediator;

        }

        public async Task<int> Load(bool diffSupport)
        {
            var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;
            int count = 0;
            int extractssitecode = 0;

            try
            {
                const int take = 1000;
                var eCount = await  _tempHtsEligibilityExtractRepository.GetCleanCount();
                var pageCount = _tempHtsEligibilityExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempHtsEligibilityExtracts =await
                        _tempHtsEligibilityExtractRepository.GetAll(QueryUtil.Eligibility, page, take);

                    var batch = tempHtsEligibilityExtracts.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempHtsEligibilityExtract>, List<HtsEligibilityExtract>>(batch);
                    extractssitecode = extractRecords.First().SiteCode;

                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _htsEligibilityExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(HtsEligibilityExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                }
                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsEligibilityExtract), "Loading...", Found, 0, 0, 0, 0)));
                
                // _mediator.Publish(new DocketExtractLoaded("HTS", "HtsEligibilityExtract", extractssitecode));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(HtsEligibilityExtract)} not Loaded");
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
