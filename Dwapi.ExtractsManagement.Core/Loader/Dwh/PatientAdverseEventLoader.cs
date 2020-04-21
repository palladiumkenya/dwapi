﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Dwh
{
    public class PatientAdverseEventLoader : IPatientAdverseEventLoader
    {
        private readonly IPatientAdverseEventExtractRepository _patientAdverseEventExtractRepository;
        private readonly ITempPatientAdverseEventExtractRepository _tempPatientAdverseEventExtractRepository;

        public PatientAdverseEventLoader(IPatientAdverseEventExtractRepository patientAdverseEventExtractRepository, ITempPatientAdverseEventExtractRepository tempPatientAdverseEventExtractRepository)
        {
            _patientAdverseEventExtractRepository = patientAdverseEventExtractRepository;
            _tempPatientAdverseEventExtractRepository = tempPatientAdverseEventExtractRepository;
        }

        public async Task<int> Load(Guid extractId, int found)
        {
            int count = 0;

            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(PatientAdverseEventExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));


                StringBuilder query = new StringBuilder();
                query.Append($" SELECT * FROM {nameof(TempPatientAdverseEventExtract)}s s");
                query.Append($" INNER JOIN PatientExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");

                const int take = 1000;
                var eCount = await  _tempPatientAdverseEventExtractRepository.GetCount(query.ToString());
                var pageCount = _tempPatientAdverseEventExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempPatientAdverseEventExtracts =await
                        _tempPatientAdverseEventExtractRepository.GetAll(query.ToString(), page, take);

                    var batch = tempPatientAdverseEventExtracts.ToList();
                    count += batch.Count;

                    //Auto mapper
                    var extractRecords = Mapper.Map<List<TempPatientAdverseEventExtract>, List<PatientAdverseEventExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _patientAdverseEventExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(PatientAdverseEventExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new ExtractActivityNotification(extractId, new DwhProgress(
                            nameof(PatientAdverseEventExtract),
                            nameof(ExtractStatus.Loading),
                            found, count , 0, 0, 0)));
                }
                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(PatientAdverseEventExtract)} not Loaded");
                return 0;
            }
        }
    }
}
