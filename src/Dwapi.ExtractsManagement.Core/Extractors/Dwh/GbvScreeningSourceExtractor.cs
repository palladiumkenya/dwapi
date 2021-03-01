using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Extractors.Dwh
{
    public class GbvScreeningSourceExtractor : IGbvScreeningSourceExtractor
    {
        private readonly IDwhExtractSourceReader _reader;
        private readonly IMediator _mediator;
        private readonly ITempGbvScreeningExtractRepository _extractRepository;

        public GbvScreeningSourceExtractor(IDwhExtractSourceReader reader, IMediator mediator, ITempGbvScreeningExtractRepository extractRepository)
        {
            _reader = reader;
            _mediator = mediator;
            _extractRepository = extractRepository;
        }

        public async Task<int> Extract(DbExtract extract, DbProtocol dbProtocol)
        {
            if (!AppConstants.DiffSupportChecked)
            {
                AppConstants.DiffSupport = _reader.CheckDiffSupport(dbProtocol);
                AppConstants.DiffSupportChecked = true;
            }
            extract.SetupDiffSql(dbProtocol);

            int batch = 5000;

            var list = new List<TempGbvScreeningExtract>();

            int count = 0;
            int loaded = 0;
            using (var rdr = await _reader.ExecuteReader(dbProtocol, extract))
            {
                while (rdr.Read())
                {
                    count++;
                    loaded++;
                    // AutoMapper profiles
                    var extractRecord = Mapper.Map<IDataRecord, TempGbvScreeningExtract>(rdr);
                    extractRecord.Id = LiveGuid.NewGuid();
                    list.Add(extractRecord);

                    if (count == batch)
                    {
                        _extractRepository.BatchInsert(list);

                        count = 0;


                        DomainEvents.Dispatch(
                            new ExtractActivityNotification(extract.Id, new DwhProgress(
                                nameof(GbvScreeningExtract),
                                nameof(ExtractStatus.Finding),
                                loaded, 0, 0, 0, 0)));
                        list = new List<TempGbvScreeningExtract>();
                    }
                }

                if (count > 0)
                {
                    // save remaining list;
                    _extractRepository.BatchInsert(list);
                }
                _extractRepository.CloseConnection();
            }

            // TODO: Notify Completed;
            DomainEvents.Dispatch(
                new ExtractActivityNotification(extract.Id, new DwhProgress(
                    nameof(GbvScreeningExtract),
                    nameof(ExtractStatus.Found),
                    loaded, 0, 0, 0, 0)));

            return loaded;
        }
    }
}
