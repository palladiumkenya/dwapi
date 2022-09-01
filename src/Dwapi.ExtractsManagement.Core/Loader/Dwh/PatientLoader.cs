using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.ExtractsManagement.Core.Profiles;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Dwh
{
    public class PatientLoader : IPatientLoader
    {
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly ITempPatientExtractRepository _tempPatientExtractRepository;
        private readonly IMediator _mediator;

        public PatientLoader(IPatientExtractRepository patientExtractRepository, ITempPatientExtractRepository tempPatientExtractRepository, IMediator mediator)
        {
            _patientExtractRepository = patientExtractRepository;
            _tempPatientExtractRepository = tempPatientExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found, bool diffSupport)
        {
            int count = 0; var mapper = diffSupport ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));

                const int take = 500;
                var eCount = await  _tempPatientExtractRepository.GetCleanCount();
                var pageCount = _tempPatientExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempPatientExtracts =await
                        _tempPatientExtractRepository.GetAll(a => a.ErrorType == 0, page, take);

                    var batch = tempPatientExtracts.ToList();
                    count += batch.Count;
                    //Auto mapper
                    var extractRecords = mapper.Map<List<TempPatientExtract>, List<PatientExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _patientExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PatientExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new ExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientExtract),
                            nameof(ExtractStatus.Loading),
                            found, count, 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("NDWH", nameof(PatientExtract), 13812));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientExtract)} not Loaded");
                throw;
            }
        }
    }
}
