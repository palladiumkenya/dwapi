using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
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
    public class HtsClientTestsLoader : IHtsClientTestsLoader
    {
        private readonly IHtsClientTestsExtractRepository _htsClientTestsExtractRepository;
        private readonly ITempHtsClientTestsExtractRepository _tempHtsClientTestsExtractRepository;
        private readonly IMediator _mediator;

        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HtsClientTestsLoader(IHtsClientTestsExtractRepository htsClientTestsExtractRepository, ITempHtsClientTestsExtractRepository tempHtsClientTestsExtractRepository,IMediator mediator)
        {
            _htsClientTestsExtractRepository = htsClientTestsExtractRepository;
            _tempHtsClientTestsExtractRepository = tempHtsClientTestsExtractRepository;
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
                var eCount = await _tempHtsClientTestsExtractRepository.GetCleanCount();
                var pageCount = _tempHtsClientTestsExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempHtsClientTests = await
                        _tempHtsClientTestsExtractRepository.GetAll(QueryUtil.Tests, page, take);

                    var batch = tempHtsClientTests.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempHtsClientTests>, List<HtsClientTests>>(batch);
                    extractssitecode = extractRecords.First().SiteCode;

                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }

                    //Batch Insert
                    var inserted = _htsClientTestsExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(HtsClientTests)} not Loaded");
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

                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientTests), "Loading...",
                    Found, 0, 0, 0, 0)));
                
                _mediator.Publish(new DocketExtractLoaded("HTS", "HtsClientTestsExtract", extractssitecode));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(HtsClientTests)} not Loaded");
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
