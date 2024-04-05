﻿using System;
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
    public class PrepCareTerminationLoader : IPrepCareTerminationLoader
    {
        private readonly IPrepCareTerminationExtractRepository _prepCareTerminationExtractRepository;
        private readonly ITempPrepCareTerminationExtractRepository _tempPrepCareTerminationExtractRepository;
        private readonly IMediator _mediator;

        public PrepCareTerminationLoader(IPrepCareTerminationExtractRepository prepCareTerminationExtractRepository, ITempPrepCareTerminationExtractRepository tempPrepCareTerminationExtractRepository, IMediator mediator)
        {
            _prepCareTerminationExtractRepository = prepCareTerminationExtractRepository;
            _tempPrepCareTerminationExtractRepository = tempPrepCareTerminationExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            int count = 0; var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            try
            {
                DomainEvents.Dispatch(
                    new PrepExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PrepCareTerminationExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));


                StringBuilder query = new StringBuilder();
                query.Append($" SELECT s.* FROM {nameof(TempPrepCareTerminationExtract)}s s");
                // query.Append($" INNER JOIN PatientPrepExtracts p ON ");
                // query.Append($" s.PatientPK = p.PatientPK AND ");
                // query.Append($" s.SiteCode = p.SiteCode ");

                const int take = 1000;
                var eCount = await  _tempPrepCareTerminationExtractRepository.GetCount(query.ToString());
                var pageCount = _tempPrepCareTerminationExtractRepository.PageCount(take, eCount);

                int extractssitecode = 0;

                int page = 1;
                while (page <= pageCount)
                {
                    var tempPrepCareTerminationExtracts =await
                        _tempPrepCareTerminationExtractRepository.ReadAll(query.ToString(), page, take);

                    var batch = tempPrepCareTerminationExtracts.ToList();
                    count += batch.Count;

                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempPrepCareTerminationExtract>, List<PrepCareTerminationExtract>>(batch);
                    extractssitecode = extractRecords.First().SiteCode;

                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _prepCareTerminationExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PrepCareTerminationExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new PrepExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PrepCareTerminationExtract),
                            nameof(ExtractStatus.Loading),
                            found, count , 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("MNCH", nameof(PrepCareTerminationExtract), extractssitecode));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PrepCareTerminationExtract)} not Loaded");
                return 0;
            }
        }
    }
}
