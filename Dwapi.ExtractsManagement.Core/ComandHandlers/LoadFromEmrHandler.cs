using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Services;
using MediatR;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.SharedKernel.Enum;
using Dwapi.Domain;
using Hangfire;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Extractors;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class LoadFromEmrCommandHandler : IRequestHandler<LoadFromEmrCommand, LoadFromEmrResponse>
    {
        private readonly IBackgroundJobInit _backgroundJob;
        private readonly ExtractorAdapter _extractorAdapter;

        public LoadFromEmrCommandHandler(IBackgroundJobInit backgroundJobInit, ExtractorAdapter extractorAdapter)
        {
            _backgroundJob = backgroundJobInit ?? throw new ArgumentNullException(nameof(backgroundJobInit));
            _extractorAdapter = extractorAdapter ?? throw new ArgumentNullException(nameof(extractorAdapter));
        }

        public async Task<LoadFromEmrResponse> Handle(LoadFromEmrCommand request, CancellationToken cancellationToken)
        {
            var extracts = request.Extracts.ToHashSet()
                .OrderBy(e => e.Rank).ToHashSet();

            foreach(var extract in extracts)
            {
                var extractor = _extractorAdapter.GetExtractor(extract.ExtractType);
                _backgroundJob.EnqueueJob(() => extractor.Extract(extract, request.DatabaseProtocol));
            }

            return new LoadFromEmrResponse();
        }

        public async Task EnqueueJobsPatientFirst(IOrderedEnumerable<DwhExtract> orderedExtracts)
        {
            if (orderedExtracts == null)
                throw new ArgumentNullException(nameof(orderedExtracts));

            if (orderedExtracts.FirstOrDefault().ExtractType != ExtractType.Patient)
                throw new InvalidOperationException();

            var patientExtract = orderedExtracts.FirstOrDefault();
        }
    }
}
