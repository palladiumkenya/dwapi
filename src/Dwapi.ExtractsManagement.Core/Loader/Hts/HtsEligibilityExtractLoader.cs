using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Hts
{
    public class HtsEligibilityExtractLoader: IHtsEligibilityExtractLoader
    {
        private readonly IHtsEligibilityExtractRepository _htsEligibilityExtractRepository;
        private readonly ITempHtsEligibilityExtractRepository _tempHtsEligibilityExtractRepository;
        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HtsEligibilityExtractLoader(IHtsEligibilityExtractRepository htsEligibilityExtractExtractRepository, ITempHtsEligibilityExtractRepository tempHtsEligibilityExtractRepository)
        {
            _htsEligibilityExtractRepository = htsEligibilityExtractExtractRepository;
            _tempHtsEligibilityExtractRepository = tempHtsEligibilityExtractRepository;
        }

        public async Task<int> Load(bool diffSupport)
        {
            var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;
            int count = 0;
            try
            {

                const int take = 1000;
                var eCount = await  _tempHtsEligibilityExtractRepository.GetCleanCount();
                var pageCount = _tempHtsEligibilityExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempHtsEligibilityExtracts =await
                        _tempHtsEligibilityExtractRepository.GetAll(QueryUtil.Tracing, page, take);

                    var batch = tempHtsEligibilityExtracts.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempHtsEligibilityExtract>, List<HtsEligibilityExtract>>(batch);
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
                    /*
                    DomainEvents.Dispatch(
                        new ExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientExtract),
                            nameof(ExtractStatus.Loading),
                            found, count, 0, 0, 0)));
                    */
                }
                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsEligibilityExtract), "Loading...", Found, 0, 0, 0, 0)));
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
