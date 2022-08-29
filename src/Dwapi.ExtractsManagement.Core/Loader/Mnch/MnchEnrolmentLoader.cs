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
    public class MnchEnrolmentLoader : IMnchEnrolmentLoader
    {
        private readonly IMnchEnrolmentExtractRepository _mnchEnrolmentExtractRepository;
        private readonly ITempMnchEnrolmentExtractRepository _tempMnchEnrolmentExtractRepository;
        private readonly IMediator _mediator;

        public MnchEnrolmentLoader(IMnchEnrolmentExtractRepository mnchEnrolmentExtractRepository, ITempMnchEnrolmentExtractRepository tempMnchEnrolmentExtractRepository, IMediator mediator)
        {
            _mnchEnrolmentExtractRepository = mnchEnrolmentExtractRepository;
            _tempMnchEnrolmentExtractRepository = tempMnchEnrolmentExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            int count = 0; var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            try
            {
                DomainEvents.Dispatch(
                    new MnchExtractActivityNotification(extractId, new DwhProgress(
                        nameof(MnchEnrolmentExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));


                StringBuilder query = new StringBuilder();
                query.Append($" SELECT s.* FROM {nameof(TempMnchEnrolmentExtract)}s s");
                query.Append($" INNER JOIN PatientMnchExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");

                const int take = 1000;
                var eCount = await  _tempMnchEnrolmentExtractRepository.GetCount(query.ToString());
                var pageCount = _tempMnchEnrolmentExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempMnchEnrolmentExtracts =await
                        _tempMnchEnrolmentExtractRepository.ReadAll(query.ToString(), page, take);

                    var batch = tempMnchEnrolmentExtracts.ToList();
                    count += batch.Count;

                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempMnchEnrolmentExtract>, List<MnchEnrolmentExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _mnchEnrolmentExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(MnchEnrolmentExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new MnchExtractActivityNotification(extractId, new DwhProgress(
                            nameof(MnchEnrolmentExtract),
                            nameof(ExtractStatus.Loading),
                            found, count , 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("MNCH", nameof(MnchEnrolmentExtract), 10639));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(MnchEnrolmentExtract)} not Loaded");
                return 0;
            }
        }
    }
}
