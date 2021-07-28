using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Mnch
{
    public class MatVisitLoader : IMatVisitLoader
    {
        private readonly IMatVisitExtractRepository _matVisitExtractRepository;
        private readonly ITempMatVisitExtractRepository _tempMatVisitExtractRepository;
        private readonly IMediator _mediator;

        public MatVisitLoader(IMatVisitExtractRepository matVisitExtractRepository, ITempMatVisitExtractRepository tempMatVisitExtractRepository, IMediator mediator)
        {
            _matVisitExtractRepository = matVisitExtractRepository;
            _tempMatVisitExtractRepository = tempMatVisitExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            int count = 0; var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            try
            {
                DomainEvents.Dispatch(
                    new MnchExtractActivityNotification(extractId, new DwhProgress(
                        nameof(MatVisitExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));


                StringBuilder query = new StringBuilder();
                query.Append($" SELECT s.* FROM {nameof(TempMatVisitExtract)}s s");
                query.Append($" INNER JOIN PatientMnchExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");

                const int take = 1000;
                var eCount = await  _tempMatVisitExtractRepository.GetCount(query.ToString());
                var pageCount = _tempMatVisitExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempMatVisitExtracts =await
                        _tempMatVisitExtractRepository.ReadAll(query.ToString(), page, take);

                    var batch = tempMatVisitExtracts.ToList();
                    count += batch.Count;

                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempMatVisitExtract>, List<MatVisitExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _matVisitExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(MatVisitExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new MnchExtractActivityNotification(extractId, new DwhProgress(
                            nameof(MatVisitExtract),
                            nameof(ExtractStatus.Loading),
                            found, count , 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("MNCH", nameof(MatVisitExtract)));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(MatVisitExtract)} not Loaded");
                return 0;
            }
        }
    }
}
