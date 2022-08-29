using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PatientMnchLoader : IPatientMnchLoader
    {
        private readonly IPatientMnchExtractRepository _patientMnchExtractRepository;
        private readonly ITempPatientMnchExtractRepository _tempPatientMnchExtractRepository;
        private readonly IMediator _mediator;

        public PatientMnchLoader(IPatientMnchExtractRepository patientMnchExtractRepository, ITempPatientMnchExtractRepository tempPatientMnchExtractRepository, IMediator mediator)
        {
            _patientMnchExtractRepository = patientMnchExtractRepository;
            _tempPatientMnchExtractRepository = tempPatientMnchExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            int count = 0; var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            try
            {
                DomainEvents.Dispatch(
                    new MnchExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientMnchExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                const int take = 500;
                var eCount = await _tempPatientMnchExtractRepository.GetCleanCount();
                var pageCount = _tempPatientMnchExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempPatientMnchExtracts = await
                        _tempPatientMnchExtractRepository.GetAll(a => a.ErrorType == 0, page, take);

                    var batch = tempPatientMnchExtracts.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempPatientMnchExtract>, List<PatientMnchExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _patientMnchExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PatientMnchExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new MnchExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientMnchExtract),
                            nameof(ExtractStatus.Loading),
                            found, count, 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("MNCH", nameof(PatientMnchExtract), 10639));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientMnchExtract)} not Loaded");
                throw;
            }
        }
    }
}
