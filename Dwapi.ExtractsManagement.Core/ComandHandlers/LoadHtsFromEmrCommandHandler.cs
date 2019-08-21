using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.ComandHandlers.Hts;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Commands.Dwh;
using Dwapi.ExtractsManagement.Core.Commands.Hts;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class LoadHtsFromEmrCommandHandler : IRequestHandler<LoadHtsFromEmrCommand, bool>
    {
        private IMediator _mediator;

        public LoadHtsFromEmrCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(LoadHtsFromEmrCommand request, CancellationToken cancellationToken)
        {
            var extractIds = request.Extracts.Select(x => x.Extract.Id).ToList();

            await _mediator.Send(new ClearAllHTSExtracts(extractIds), cancellationToken);

            var patientProfile = request.Extracts.FirstOrDefault(x=>x.Extract.IsPriority);
            //Generate extract patient command
            if (patientProfile != null)
            {
                var extractPatient = new ExtractHtsClients()
                {
                    //todo check if extracts from all emrs are loaded
                    Extract = patientProfile.Extract,
                    DatabaseProtocol = patientProfile.DatabaseProtocol
                };

                var result = await _mediator.Send(extractPatient, cancellationToken);

                if (!result)
                {
                    return false;
                }
            }
            return await ExtractAll(request, cancellationToken);

        }

        private async Task<bool> ExtractAll(LoadHtsFromEmrCommand request, CancellationToken cancellationToken)
        {
            Task<bool> ClientTestTask = null, TestKitTask = null, ClientTracingTask = null, PartnerTracingTask = null, PNSTask = null, ClientLinkageTask = null;
            // HtsClientTestExtract
            var HtsClientTestExtractProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "HtsClientTestsExtract");
            if (null != HtsClientTestExtractProfile)
            {
                var command = new ExtractHtsClientTests()
                {
                    Extract = HtsClientTestExtractProfile?.Extract,
                    DatabaseProtocol = HtsClientTestExtractProfile?.DatabaseProtocol
                };
                ClientTestTask = _mediator.Send(command, cancellationToken);
            }

            // HtsTestKitsExtract
            var HtsTestKitsExtractProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "HtsTestKitsExtract");
            if (null != HtsTestKitsExtractProfile)
            {
                var command = new ExtractHtsTestKits()
                {
                    Extract = HtsTestKitsExtractProfile?.Extract,
                    DatabaseProtocol = HtsTestKitsExtractProfile?.DatabaseProtocol
                };
                TestKitTask = _mediator.Send(command, cancellationToken);
            }

            // HtsClientTracingExtract
            var HtsClientTracingExtractProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "HtsClientTracingExtract");
            if (null != HtsClientTracingExtractProfile)
            {
                var command = new ExtractHtsClientTracing()
                {
                    Extract = HtsClientTracingExtractProfile?.Extract,
                    DatabaseProtocol = HtsClientTracingExtractProfile?.DatabaseProtocol
                };
                ClientTracingTask = _mediator.Send(command, cancellationToken);
            }

            // HtsPartnerTracingExtract
            var HtsPartnerTracingExtractProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "HtsPartnerTracingExtract");
            if (null != HtsPartnerTracingExtractProfile)
            {
                var command = new ExtractHtsPartnerTracing()
                {
                    Extract = HtsPartnerTracingExtractProfile?.Extract,
                    DatabaseProtocol = HtsPartnerTracingExtractProfile?.DatabaseProtocol
                };
                PartnerTracingTask = _mediator.Send(command, cancellationToken);
            }

            // HtsPNSExtract
            var HtsPNSExtractProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "HtsPartnerNotificationServicesExtract");
            if (null != HtsPNSExtractProfile)
            {
                var command = new ExtractHtsPartnerNotificationServices()
                {
                    Extract = HtsPNSExtractProfile?.Extract,
                    DatabaseProtocol = HtsPNSExtractProfile?.DatabaseProtocol
                };
                PNSTask = _mediator.Send(command, cancellationToken);
            }

            // HtsClientLinkageExtract
            var HtsClientLinkageExtractProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "HtsClientLinkageExtract");
            if (null != HtsClientLinkageExtractProfile)
            {
                var command = new ExtractHtsClientsLinkage()
                {
                    Extract = HtsClientLinkageExtractProfile?.Extract,
                    DatabaseProtocol = HtsClientLinkageExtractProfile?.DatabaseProtocol
                };
                ClientLinkageTask = _mediator.Send(command, cancellationToken);
            }

            // await all tasks
            var ts = new List<Task<bool>> { ClientTestTask, TestKitTask, ClientTracingTask, PartnerTracingTask, PNSTask, ClientLinkageTask };
            var result = await Task.WhenAll(ts);

            return result.All(x=>x);
        }
    }
}
