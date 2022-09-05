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
    public class CwcEnrolmentLoader : ICwcEnrolmentLoader
    {
        private readonly ICwcEnrolmentExtractRepository _cwcEnrolmentExtractRepository;
        private readonly ITempCwcEnrolmentExtractRepository _tempCwcEnrolmentExtractRepository;
        private readonly IMediator _mediator;

        public CwcEnrolmentLoader(ICwcEnrolmentExtractRepository cwcEnrolmentExtractRepository, ITempCwcEnrolmentExtractRepository tempCwcEnrolmentExtractRepository, IMediator mediator)
        {
            _cwcEnrolmentExtractRepository = cwcEnrolmentExtractRepository;
            _tempCwcEnrolmentExtractRepository = tempCwcEnrolmentExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            int count = 0; var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            try
            {
                DomainEvents.Dispatch(
                    new MnchExtractActivityNotification(extractId, new DwhProgress(
                        nameof(CwcEnrolmentExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));


                StringBuilder query = new StringBuilder();
                query.Append($" SELECT s.* FROM {nameof(TempCwcEnrolmentExtract)}s s");
                query.Append($" INNER JOIN PatientMnchExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");

                const int take = 1000;
                var eCount = await  _tempCwcEnrolmentExtractRepository.GetCount(query.ToString());
                var pageCount = _tempCwcEnrolmentExtractRepository.PageCount(take, eCount);
                
                int extractssitecode = (int) _tempCwcEnrolmentExtractRepository.GetSiteCode(query.ToString()).SiteCode;

                int page = 1;
                while (page <= pageCount)
                {
                    var tempCwcEnrolmentExtracts =await
                        _tempCwcEnrolmentExtractRepository.ReadAll(query.ToString(), page, take);

                    var batch = tempCwcEnrolmentExtracts.ToList();
                    count += batch.Count;

                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempCwcEnrolmentExtract>, List<CwcEnrolmentExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _cwcEnrolmentExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(CwcEnrolmentExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new MnchExtractActivityNotification(extractId, new DwhProgress(
                            nameof(CwcEnrolmentExtract),
                            nameof(ExtractStatus.Loading),
                            found, count , 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("MNCH", nameof(CwcEnrolmentExtract), extractssitecode));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(CwcEnrolmentExtract)} not Loaded");
                return 0;
            }
        }
    }
}
