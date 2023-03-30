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
    public class HtsPartnerNotificationServicesLoader : IHtsPartnerNotificationServicesLoader
    {
        private readonly IHtsPartnerNotificationServicesExtractRepository _htsPartnerNotificationServicesExtractRepository;
        private readonly ITempHtsPartnerNotificationServicesExtractRepository _tempHtsPartnerNotificationServicesExtractRepository;
        private readonly IMediator _mediator;

        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HtsPartnerNotificationServicesLoader(IHtsPartnerNotificationServicesExtractRepository htsPartnerNotificationServicesExtractRepository, ITempHtsPartnerNotificationServicesExtractRepository tempHtsPartnerNotificationServicesExtractRepository,IMediator mediator)
        {
            _htsPartnerNotificationServicesExtractRepository = htsPartnerNotificationServicesExtractRepository;
            _tempHtsPartnerNotificationServicesExtractRepository = tempHtsPartnerNotificationServicesExtractRepository;
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
                var eCount = await  _tempHtsPartnerNotificationServicesExtractRepository.GetCleanCount();
                var pageCount = _tempHtsPartnerNotificationServicesExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempHtsPartnerNotifications =await
                        _tempHtsPartnerNotificationServicesExtractRepository.GetAll(QueryUtil.Pns, page, take);

                    var batch = tempHtsPartnerNotifications.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempHtsPartnerNotificationServices>, List<HtsPartnerNotificationServices>>(batch);
                    extractssitecode = extractRecords.First().SiteCode;

                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _htsPartnerNotificationServicesExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(HtsPartnerNotificationServices)} not Loaded");
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
                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsPartnerNotificationServices), "Loading...", Found, 0, 0, 0, 0)));
                
                _mediator.Publish(new DocketExtractLoaded("HTS", "HtsPartnerNotificationServicesExtract", extractssitecode));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(HtsPartnerNotificationServices)} not Loaded");
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
