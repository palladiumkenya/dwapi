﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
using Dwapi.ExtractsManagement.Core.Notifications;
 using Dwapi.ExtractsManagement.Core.Profiles;
 using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Extractors.Mgs
{
    public class MetricMigrationSourceExtractor : IMetricMigrationSourceExtractor
    {
        private readonly IHTSExtractSourceReader _reader;
        private readonly IMediator _mediator;
        private readonly ITempMetricMigrationExtractRepository _extractRepository;

        public MetricMigrationSourceExtractor(IHTSExtractSourceReader reader, IMediator mediator, ITempMetricMigrationExtractRepository extractRepository)
        {
            _reader = reader;
            _mediator = mediator;
            _extractRepository = extractRepository;
        }

        public async Task<int> Extract(DbExtract extract, DbProtocol dbProtocol)
        {
            var mapper = dbProtocol.SupportsDifferential ? ExtractDiffMapper.Instance : ExtractMapper.Instance;
            int batch = 500;

            DomainEvents.Dispatch(new MgsNotification(new ExtractProgress(nameof(MetricMigrationExtract), "extracting...")));
            //DomainEvents.Dispatch(new CbsStatusNotification(extract.Id,ExtractStatus.Loading));

            var list = new List<TempMetricMigrationExtract>();

            int count = 0;
            int totalCount = 0;

            using (var rdr = await _reader.ExecuteReader(dbProtocol, extract))
            {
                while (rdr.Read())
                {
                    totalCount++;
                    count++;
                    // AutoMapper profiles
                    var extractRecord = mapper.Map<IDataRecord, TempMetricMigrationExtract>(rdr);
                    extractRecord.Id = LiveGuid.NewGuid();
                    list.Add(extractRecord);

                    if (count == batch)
                    {
                        // TODO: batch and save
                        _extractRepository.BatchInsert(list);

                        try
                        {
                            DomainEvents.Dispatch(new  MgsNotification(new ExtractProgress(nameof(MetricMigrationExtract), "extracting...",totalCount,count,0,0,0)));
                        }
                        catch (Exception e)
                        {
                            Log.Error(e,"Notification error");

                        }
                        count = 0;
                        list =new List<TempMetricMigrationExtract>();
                    }

                    // TODO: Notify progress...


                }

                if (count > 0)
                {
                    _extractRepository.BatchInsert(list);
                }
                _extractRepository.CloseConnection();
            }

            try
            {

                DomainEvents.Dispatch(new MgsNotification(new ExtractProgress(nameof(MetricMigrationExtract), "extracted", totalCount, 0, 0, 0, 0)));
                DomainEvents.Dispatch(new MgsStatusNotification(extract.Id, ExtractStatus.Found, totalCount));
                DomainEvents.Dispatch(new MgsStatusNotification(extract.Id, ExtractStatus.Loaded,totalCount));
            }
            catch (Exception e)
            {
                Log.Error(e, "Notification error");

            }

            return totalCount;
        }

        public async Task<int> Extract(DbExtract extract, DbProtocol dbProtocol, DateTime? maxCreated, DateTime? maxModified, int siteCode)
        {
                var mapper = dbProtocol.SupportsDifferential ? ExtractDiffMapper.Instance : ExtractMapper.Instance;
            int batch = 500;

            DomainEvents.Dispatch(new MgsNotification(new ExtractProgress(nameof(MetricMigrationExtract), "extracting...")));
            //DomainEvents.Dispatch(new CbsStatusNotification(extract.Id,ExtractStatus.Loading));

            var list = new List<TempMetricMigrationExtract>();

            int count = 0;
            int totalCount = 0;

            using (var rdr = await _reader.ExecuteReader(dbProtocol, extract, maxCreated, maxModified, siteCode))
            {
                while (rdr.Read())
                {
                    totalCount++;
                    count++;
                    // AutoMapper profiles
                    var extractRecord = mapper.Map<IDataRecord, TempMetricMigrationExtract>(rdr);
                    extractRecord.Id = LiveGuid.NewGuid();
                    list.Add(extractRecord);

                    if (count == batch)
                    {
                        // TODO: batch and save
                        _extractRepository.BatchInsert(list);

                        try
                        {
                            DomainEvents.Dispatch(new  MgsNotification(new ExtractProgress(nameof(MetricMigrationExtract), "extracting...",totalCount,count,0,0,0)));
                        }
                        catch (Exception e)
                        {
                            Log.Error(e,"Notification error");

                        }
                        count = 0;
                        list =new List<TempMetricMigrationExtract>();
                    }

                    // TODO: Notify progress...


                }

                if (count > 0)
                {
                    _extractRepository.BatchInsert(list);
                }
                _extractRepository.CloseConnection();
            }

            try
            {

                DomainEvents.Dispatch(new MgsNotification(new ExtractProgress(nameof(MetricMigrationExtract), "extracted", totalCount, 0, 0, 0, 0)));
                DomainEvents.Dispatch(new MgsStatusNotification(extract.Id, ExtractStatus.Found, totalCount));
                DomainEvents.Dispatch(new MgsStatusNotification(extract.Id, ExtractStatus.Loaded,totalCount));
            }
            catch (Exception e)
            {
                Log.Error(e, "Notification error");

            }

            return totalCount;
        }
    }
}
