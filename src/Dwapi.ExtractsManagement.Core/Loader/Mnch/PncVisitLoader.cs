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
    public class PncVisitLoader : IPncVisitLoader
    {
        private readonly IPncVisitExtractRepository _pncVisitExtractRepository;
        private readonly ITempPncVisitExtractRepository _tempPncVisitExtractRepository;
        private readonly IMediator _mediator;

        public PncVisitLoader(IPncVisitExtractRepository pncVisitExtractRepository, ITempPncVisitExtractRepository tempPncVisitExtractRepository, IMediator mediator)
        {
            _pncVisitExtractRepository = pncVisitExtractRepository;
            _tempPncVisitExtractRepository = tempPncVisitExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            int count = 0; var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            try
            {
                DomainEvents.Dispatch(
                    new MnchExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PncVisitExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));


                StringBuilder query = new StringBuilder();
                query.Append($" SELECT s.* FROM {nameof(TempPncVisitExtract)}s s");
                query.Append($" INNER JOIN PatientMnchExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");

                const int take = 1000;
                var eCount = await  _tempPncVisitExtractRepository.GetCount(query.ToString());
                var pageCount = _tempPncVisitExtractRepository.PageCount(take, eCount);
                
                int extractssitecode = 0;

                int page = 1;
                while (page <= pageCount)
                {
                    var tempPncVisitExtracts =await
                        _tempPncVisitExtractRepository.ReadAll(query.ToString(), page, take);

                    var batch = tempPncVisitExtracts.ToList();
                    count += batch.Count;

                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempPncVisitExtract>, List<PncVisitExtract>>(batch);
                    extractssitecode = extractRecords.First().SiteCode;

                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _pncVisitExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PncVisitExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new MnchExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PncVisitExtract),
                            nameof(ExtractStatus.Loading),
                            found, count , 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("MNCH", nameof(PncVisitExtract), extractssitecode));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PncVisitExtract)} not Loaded");
                return 0;
            }
        }
    }
}
