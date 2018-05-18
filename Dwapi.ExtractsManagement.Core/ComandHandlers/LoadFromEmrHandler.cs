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
using System.Linq.Expressions;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class LoadFromEmrCommandHandler : IRequestHandler<LoadFromEmrCommand, LoadFromEmrResponse>
    {
        private readonly IBackgroundJobInit _backgroundJob;
        private readonly ExtractorValidatorAdapter _extractorValidatorAdapter;
        private readonly IExtractStatusService _extractStatusService;

        public LoadFromEmrCommandHandler(IBackgroundJobInit backgroundJobInit, ExtractorValidatorAdapter extractorValidatorAdapter, IExtractStatusService extractStatusService)
        {
            _backgroundJob = backgroundJobInit ?? throw new ArgumentNullException(nameof(backgroundJobInit));
            _extractorValidatorAdapter = extractorValidatorAdapter ?? throw new ArgumentNullException(nameof(extractorValidatorAdapter));
            _extractStatusService = extractStatusService;
        }

        public async Task<LoadFromEmrResponse> Handle(LoadFromEmrCommand request, CancellationToken cancellationToken)
        {
            // Rank jobs
            var extracts = request.Extracts.ToHashSet()
                .OrderBy(e => e.Rank);
            IList<Expression<Action>> methodCalls = new List<Expression<Action>>();

            // initialize the jobs
            extracts.ToList().ForEach(e =>
            {
                var extractor = _extractorValidatorAdapter.GetExtractorValidator(e.ExtractType);
                methodCalls.Add(() => extractor.ExtractAndValidateAsync(e, request.DatabaseProtocol));
                _extractStatusService.HasStarted(e.Id);
            });
            // chain jobs
            _backgroundJob.ChainJobsAfterFirst(methodCalls);
            return new LoadFromEmrResponse();
        }
        
    }
}
