﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
 using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Mgs;
 using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
 using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
 using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
 using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Mgs
{
    public class MetricMigrationLoader : IMetricMigrationLoader
    {
        private readonly IMetricMigrationExtractRepository _extractRepository;
        private readonly ITempMetricMigrationExtractRepository _tempExtractRepository;
        private int Found { get; set; }
        private Guid ExtractId { get; set; }

        public MetricMigrationLoader(IMetricMigrationExtractRepository extractRepository, ITempMetricMigrationExtractRepository tempExtractRepository)
        {
            _extractRepository = extractRepository;
            _tempExtractRepository = tempExtractRepository;
        }

        public Task<int> Load()
        {
            try
            {
                //load temp extracts without errors
                //var tempPatientExtracts = _tempExtractRepository.GetAll().Where(a=>a.CheckError == false).ToList();
                var tempPatientExtracts = _tempExtractRepository.GetAll().Where(a => a.ErrorType == 0).ToList();

                //Auto mapper
                var extractRecords = Mapper.Map<List<TempMetricMigrationExtract>, List<MetricMigrationExtract>>(tempPatientExtracts);

                //Batch Insert
                _extractRepository.BatchInsert(extractRecords);
                Log.Debug("saved batch");

                DomainEvents.Dispatch(new MgsNotification(new ExtractProgress(nameof(MetricMigrationExtract), "Loading...", Found, 0, 0, 0, 0)));
                return Task.FromResult(tempPatientExtracts.Count);

            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(MetricMigrationExtract)} not Loaded");
                throw;
            }
        }

        public Task<int> Load(Guid extractId, int found)
        {
            Found = found;
            ExtractId = extractId;
            return Load();
        }
    }
}
