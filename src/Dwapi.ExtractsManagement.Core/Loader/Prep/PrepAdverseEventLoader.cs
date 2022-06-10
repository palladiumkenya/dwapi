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
    public class PrepAdverseEventLoader : IPrepAdverseEventLoader
    {
        private readonly IPrepAdverseEventExtractRepository _prepAdverseEventExtractRepository;
        private readonly ITempPrepAdverseEventExtractRepository _tempPrepAdverseEventExtractRepository;
        private readonly IMediator _mediator;

        public PrepAdverseEventLoader(IPrepAdverseEventExtractRepository prepAdverseEventExtractRepository, ITempPrepAdverseEventExtractRepository tempPrepAdverseEventExtractRepository, IMediator mediator)
        {
            _prepAdverseEventExtractRepository = prepAdverseEventExtractRepository;
            _tempPrepAdverseEventExtractRepository = tempPrepAdverseEventExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            int count = 0; var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            try
            {
                DomainEvents.Dispatch(
                    new PrepExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PrepAdverseEventExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));


                StringBuilder query = new StringBuilder();
                query.Append($" SELECT s.* FROM {nameof(TempPrepAdverseEventExtract)}s s");
                query.Append($" INNER JOIN PatientPrepExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");

                const int take = 1000;
                var eCount = await  _tempPrepAdverseEventExtractRepository.GetCount(query.ToString());
                var pageCount = _tempPrepAdverseEventExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempPrepAdverseEventExtracts =await
                        _tempPrepAdverseEventExtractRepository.ReadAll(query.ToString(), page, take);

                    var batch = tempPrepAdverseEventExtracts.ToList();
                    count += batch.Count;

                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempPrepAdverseEventExtract>, List<PrepAdverseEventExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _prepAdverseEventExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PrepAdverseEventExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new PrepExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PrepAdverseEventExtract),
                            nameof(ExtractStatus.Loading),
                            found, count , 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("MNCH", nameof(PrepAdverseEventExtract)));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PrepAdverseEventExtract)} not Loaded");
                return 0;
            }
        }
    }
}
