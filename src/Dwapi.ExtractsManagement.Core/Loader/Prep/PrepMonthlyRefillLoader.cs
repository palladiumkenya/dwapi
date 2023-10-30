using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Prep;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Prep;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Prep;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Prep
{
    public class PrepMonthlyRefillLoader : IPrepMonthlyRefillLoader
    {
        private readonly IPrepMonthlyRefillExtractRepository _prepMonthlyRefillExtractRepository;
        private readonly ITempPrepMonthlyRefillExtractRepository _tempPrepMonthlyRefillExtractRepository;
        private readonly IMediator _mediator;

        public PrepMonthlyRefillLoader(IPrepMonthlyRefillExtractRepository prepMonthlyRefillExtractRepository, ITempPrepMonthlyRefillExtractRepository tempPrepMonthlyRefillExtractRepository, IMediator mediator)
        {
            _prepMonthlyRefillExtractRepository = prepMonthlyRefillExtractRepository;
            _tempPrepMonthlyRefillExtractRepository = tempPrepMonthlyRefillExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            int count = 0; var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            try
            {
                DomainEvents.Dispatch(
                    new PrepExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PrepMonthlyRefillExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));


                StringBuilder query = new StringBuilder();
                query.Append($" SELECT s.* FROM {nameof(TempPrepMonthlyRefillExtract)}s s");
                query.Append($" INNER JOIN PatientPrepExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");

                const int take = 1000;
                var eCount = await  _tempPrepMonthlyRefillExtractRepository.GetCount(query.ToString());
                var pageCount = _tempPrepMonthlyRefillExtractRepository.PageCount(take, eCount);

                int extractssitecode = 0;

                int page = 1;
                while (page <= pageCount)
                {
                    var tempPrepMonthlyRefillExtracts =await
                        _tempPrepMonthlyRefillExtractRepository.ReadAll(query.ToString(), page, take);

                    var batch = tempPrepMonthlyRefillExtracts.ToList();
                    count += batch.Count;

                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempPrepMonthlyRefillExtract>, List<PrepMonthlyRefillExtract>>(batch);
                    extractssitecode = extractRecords.First().SiteCode;

                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _prepMonthlyRefillExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PrepMonthlyRefillExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new PrepExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PrepMonthlyRefillExtract),
                            nameof(ExtractStatus.Loading),
                            found, count , 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("MNCH", nameof(PrepMonthlyRefillExtract), extractssitecode));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PrepMonthlyRefillExtract)} not Loaded");
                return 0;
            }
        }
    }
}
