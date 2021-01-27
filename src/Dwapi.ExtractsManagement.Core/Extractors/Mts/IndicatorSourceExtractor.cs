using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Hts;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Source.Mts;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Extractors.Mts
{
    public class IndicatorSourceExtractor : IIndicatorSourceExtractor
    {
        private readonly IMtsExtractSourceReader _reader;
        private readonly IMediator _mediator;
        private readonly ITempIndicatorExtractRepository _extractRepository;

        public IndicatorSourceExtractor(IMtsExtractSourceReader reader, IMediator mediator, ITempIndicatorExtractRepository extractRepository)
        {
            _reader = reader;
            _mediator = mediator;
            _extractRepository = extractRepository;
        }

        public async Task<int> Extract(DbExtract extract, DbProtocol dbProtocol)
        {
            int batch = 500;

            // DomainEvents.Dispatch(new MgsNotification(new ExtractProgress(nameof(IndicatorExtract), "extracting...")));
            //DomainEvents.Dispatch(new CbsStatusNotification(extract.Id,ExtractStatus.Loading));

            var list = new List<TempIndicatorExtract>();

            int count = 0;
            int totalCount = 0;

            using (var rdr = await _reader.ExecuteReader(dbProtocol, extract))
            {
                while (rdr.Read())
                {
                    totalCount++;
                    count++;
                    // AutoMapper profiles
                    var extractRecord = Mapper.Map<IDataRecord, TempIndicatorExtract>(rdr);
                    extractRecord.Id = LiveGuid.NewGuid();
                    list.Add(extractRecord);

                    if (count == batch)
                    {
                        // TODO: batch and save
                        _extractRepository.CreateBatch(list);

                        try
                        {
                            // DomainEvents.Dispatch(new  MgsNotification(new ExtractProgress(nameof(IndicatorExtract), "extracting...",totalCount,count,0,0,0)));
                        }
                        catch (Exception e)
                        {
                            Log.Error(e,"Notification error");

                        }
                        count = 0;
                        list =new List<TempIndicatorExtract>();
                    }

                    // TODO: Notify progress...


                }

                if (count > 0)
                {
                    _extractRepository.CreateBatch(list);
                }
                _extractRepository.CloseConnection();
            }

            try
            {

                // DomainEvents.Dispatch(new MgsNotification(new ExtractProgress(nameof(IndicatorExtract), "extracted", totalCount, 0, 0, 0, 0)));
                // DomainEvents.Dispatch(new MgsStatusNotification(extract.Id, ExtractStatus.Found, totalCount));
                // DomainEvents.Dispatch(new MgsStatusNotification(extract.Id, ExtractStatus.Loaded,totalCount));
            }
            catch (Exception e)
            {
                Log.Error(e, "Notification error");
            }

            return totalCount;
        }
    }
}
