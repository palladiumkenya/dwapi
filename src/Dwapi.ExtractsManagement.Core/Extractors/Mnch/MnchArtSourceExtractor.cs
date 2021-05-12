using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Mnch;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Mnch;
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

namespace Dwapi.ExtractsManagement.Core.Extractors.Mnch
{
    public class MnchArtSourceExtractor : IMnchArtSourceExtractor
    {
        private readonly IMnchExtractSourceReader _reader;
        private readonly IMediator _mediator;
        private readonly ITempMnchArtExtractRepository _extractRepository;

        public MnchArtSourceExtractor(IMnchExtractSourceReader reader, IMediator mediator, ITempMnchArtExtractRepository extractRepository)
        {
            _reader = reader;
            _mediator = mediator;
            _extractRepository = extractRepository;
        }

        public async Task<int> Extract(DbExtract extract, DbProtocol dbProtocol)
        {
            var mapper = dbProtocol.SupportsDifferential ? ExtractDiffMapper.Instance : ExtractMapper.Instance;

            int batch = 500;

            var list = new List<TempMnchArtExtract>();

            int count = 0;
            int loaded = 0;
            using (var rdr = await _reader.ExecuteReader(dbProtocol, extract))
            {
                while (rdr.Read())
                {
                    count++;
                    loaded++;
                    // AutoMapper profiles
                    var extractRecord =   mapper.Map<IDataRecord, TempMnchArtExtract>(rdr);
                    extractRecord.Id = LiveGuid.NewGuid();
                    list.Add(extractRecord);

                    if (count == batch)
                    {
                        _extractRepository.BatchInsert(list);

                        count = 0;


                        DomainEvents.Dispatch(
                            new MnchExtractActivityNotification(extract.Id, new DwhProgress(
                                nameof(MnchArtExtract),
                                nameof(ExtractStatus.Finding),
                                loaded, 0, 0, 0, 0)));
                        list = new List<TempMnchArtExtract>();
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
                new MnchExtractActivityNotification(extract.Id, new DwhProgress(
                    nameof(MnchArtExtract),
                    nameof(ExtractStatus.Found),
                    loaded, 0, 0, 0, 0)));

            return loaded;
        }
    }
}
