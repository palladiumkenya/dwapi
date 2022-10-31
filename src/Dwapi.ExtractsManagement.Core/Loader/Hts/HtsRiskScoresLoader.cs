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
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.TempExtracts;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Hts
{
    public class HtsRiskScoresLoader : IHtsRiskScoresLoader
    {
        private readonly IHtsRiskScoresRepository _htsRiskScoresRepository;
        private readonly ITempHtsRiskScoresRepository _tempHtsRiskScoresRepository;
        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public HtsRiskScoresLoader(IHtsRiskScoresRepository htsRiskScoresRepository, ITempHtsRiskScoresRepository tempHtsRiskScoresRepository)
        {
            _htsRiskScoresRepository = htsRiskScoresRepository;
            _tempHtsRiskScoresRepository = tempHtsRiskScoresRepository;
        }

        public async Task<int> Load(bool diffSupport)
        {
            var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;
            int count = 0;

            
            try
            {
                const int take = 1000;
                var eCount = await  _tempHtsRiskScoresRepository.GetCleanCount();
                var pageCount = _tempHtsRiskScoresRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var temphtsriskscores =await
                        _tempHtsRiskScoresRepository.GetAll(QueryUtil.RiskScores, page, take);

                    var batch = temphtsriskscores.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempHtsRiskScores>, List<HtsRiskScores>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _htsRiskScoresRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(HtsRiskScores)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                }
                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsRiskScores), "Loading...", Found, 0, 0, 0, 0)));
                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(HtsRiskScores)} not Loaded");
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
