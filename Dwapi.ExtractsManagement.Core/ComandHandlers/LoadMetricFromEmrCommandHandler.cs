using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Mgs;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class LoadMetricFromEmrCommandHandler : IRequestHandler<LoadMgsFromEmrCommand, bool>
    {
        private IMediator _mediator;

        public LoadMetricFromEmrCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(LoadMgsFromEmrCommand request, CancellationToken cancellationToken)
        {
            var extractIds = request.Extracts.Select(x => x.Extract.Id).ToList();

            await _mediator.Send(new ClearAllMetricExtracts(extractIds), cancellationToken);

            var migration = request.Extracts.FirstOrDefault(x => x.Extract.Name == "Migration");

            if (migration != null)
            {
                var extractMetric = new ExtractMetricMigration()
                {
                    //todo check if extracts from all emrs are loaded
                    Extract = migration.Extract,
                    DatabaseProtocol = migration.DatabaseProtocol
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
