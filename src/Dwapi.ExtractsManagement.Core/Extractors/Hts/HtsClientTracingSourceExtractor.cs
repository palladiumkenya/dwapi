using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Extractors.Hts
{
    public class HtsClientTracingSourceExtractor : IHtsClientTracingSourceExtractor
    {
        private readonly IHTSExtractSourceReader _reader;
        private readonly IMediator _mediator;
        private readonly ITempHtsClientTracingExtractRepository _extractRepository;

        public HtsClientTracingSourceExtractor(IHTSExtractSourceReader reader, IMediator mediator, ITempHtsClientTracingExtractRepository extractRepository)
        {
            _reader = reader;
            _mediator = mediator;
            _extractRepository = extractRepository;
        }

        public async Task<int> Extract(DbExtract extract, DbProtocol dbProtocol)
        {
            int batch = 500;

            DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientTracing), "extracting...")));
            //DomainEvents.Dispatch(new CbsStatusNotification(extract.Id,ExtractStatus.Loading));

            var list = new List<TempHtsClientTracing>();

            int count = 0;
            int totalCount = 0;

            using (var rdr = await _reader.ExecuteReader(dbProtocol, extract))
            {
                while (rdr.Read())
                {
                    totalCount++;
                    count++;
                    // AutoMapper profiles
                    var extractRecord = Mapper.Map<IDataRecord, TempHtsClientTracing>(rdr);
                    extractRecord.Id = LiveGuid.NewGuid();
                    list.Add(extractRecord);

                    if (count == batch)
                    {
                        // TODO: batch and save
                        _extractRepository.BatchInsert(list);

                        try
                        {
                            DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientTracing), "extracting...", totalCount, count, 0, 0, 0)));
                        }
                        catch (Exception e)
                        {
                            Log.Error(e, "Notification error");

                        }
                        count = 0;
                        list = new List<TempHtsClientTracing>();
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

                DomainEvents.Dispatch(new HtsNotification(new ExtractProgress(nameof(HtsClientTracing), "extracted", totalCount, 0, 0, 0, 0)));
                DomainEvents.Dispatch(new HtsStatusNotification(extract.Id, ExtractStatus.Found, totalCount));
                DomainEvents.Dispatch(new HtsStatusNotification(extract.Id, ExtractStatus.Loaded, totalCount));
            }
            catch (Exception e)
            {
                Log.Error(e, "Notification error");

            }

            return totalCount;
        }

        public Task<int> ReadExtract(DbExtract extract, DbProtocol dbProtocol)
        {
            throw new NotImplementedException();
        }
    }
}
