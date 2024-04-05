using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PatientPrepLoader : IPatientPrepLoader
    {
        private readonly IPatientPrepExtractRepository _patientPrepExtractRepository;
        private readonly ITempPatientPrepExtractRepository _tempPatientPrepExtractRepository;
        private readonly IMediator _mediator;

        public PatientPrepLoader(IPatientPrepExtractRepository patientPrepExtractRepository, ITempPatientPrepExtractRepository tempPatientPrepExtractRepository, IMediator mediator)
        {
            _patientPrepExtractRepository = patientPrepExtractRepository;
            _tempPatientPrepExtractRepository = tempPatientPrepExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            int count = 0; var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            try
            {
                DomainEvents.Dispatch(
                    new PrepExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientPrepExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                const int take = 500;
                var eCount = await _tempPatientPrepExtractRepository.GetCleanCount();
                var pageCount = _tempPatientPrepExtractRepository.PageCount(take, eCount);
                
                int extractssitecode = 0;
                
                int page = 1;
                while (page <= pageCount)
                {
                    var tempPatientPrepExtracts = await
                        _tempPatientPrepExtractRepository.GetAll(a => a.ErrorType == 0, page, take);

                    var batch = tempPatientPrepExtracts.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempPatientPrepExtract>, List<PatientPrepExtract>>(batch);
                    extractssitecode = extractRecords.First().SiteCode;

                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _patientPrepExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PatientPrepExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new PrepExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientPrepExtract),
                            nameof(ExtractStatus.Loading),
                            found, count, 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("PREP", nameof(PatientPrepExtract), extractssitecode));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientPrepExtract)} not Loaded");
                throw;
            }
        }
    }
}
