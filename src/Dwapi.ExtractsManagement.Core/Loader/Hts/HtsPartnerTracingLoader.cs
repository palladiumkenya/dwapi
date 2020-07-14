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
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.TempExtracts;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Hts
{
    public class HtsPartnerTracingLoader : IHtsPartnerTracingLoader
    {
        private readonly IHtsPartnerTracingExtractRepository _htsPartnerTracingExtractRepository;
        private readonly ITempHtsPartnerTracingExtractRepository _tempHtsPartnerTracingExtractRepository;
        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HtsPartnerTracingLoader(IHtsPartnerTracingExtractRepository htsPartnerTracingExtractRepository, ITempHtsPartnerTracingExtractRepository tempHtsPartnerTracingExtractRepository)
        {
            _htsPartnerTracingExtractRepository = htsPartnerTracingExtractRepository;
            _tempHtsPartnerTracingExtractRepository = tempHtsPartnerTracingExtractRepository;
        }

        public async Task<int> Load()
        {
            int count = 0;
            try
            {

                const int take = 1000;
                var eCount = await  _tempHtsPartnerTracingExtractRepository.GetCleanCount();
                var pageCount = _tempHtsPartnerTracingExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempHtsPartnerTracings =await
                        _tempHtsPartnerTracingExtractRepository.GetAll(QueryUtil.PartnerTracing, page, take);

                    var batch = tempHtsPartnerTracings.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = Mapper.Map<List<TempHtsPartnerTracing>, List<HtsPartnerTracing>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _htsPartnerTracingExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(HtsPartnerTracing)} not Loaded");
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
                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsPartnerTracing), "Loading...", Found, 0, 0, 0, 0)));
                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(HtsPartnerTracing)} not Loaded");
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
