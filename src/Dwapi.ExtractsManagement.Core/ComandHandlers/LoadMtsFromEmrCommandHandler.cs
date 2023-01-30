using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Mgs;
using Dwapi.ExtractsManagement.Core.Commands.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class LoadMtsFromEmrCommandHandler : IRequestHandler<LoadMtsFromEmrCommand, bool>
    {
        private IMediator _mediator;

        public LoadMtsFromEmrCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(LoadMtsFromEmrCommand request, CancellationToken cancellationToken)
        {
            var extractIds = request.Extracts.Select(x => x.Id).ToList();

            await _mediator.Send(new ClearAllMtsExtracts(extractIds), cancellationToken);

            var migration = request.Extracts.FirstOrDefault(x => x.Name == nameof(IndicatorExtract));

            if (migration != null && !string.IsNullOrEmpty(migration.ExtractSql))
            {
                var extractMetric = new ExtractMts()
                {
                    // todo check if extracts from all emrs are loaded
                    Extract = migration,
                    DatabaseProtocol = request.DatabaseProtocol
                };

                var result = await _mediator.Send(extractMetric, cancellationToken);

                if (!result)
                {
                    return false;
                }
            }

            return await Task.FromResult(true);
        }
    }
}
