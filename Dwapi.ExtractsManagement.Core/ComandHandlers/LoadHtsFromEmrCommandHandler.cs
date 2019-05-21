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
                var extractPatient = new ExtractHTSClient()
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
            Task<bool> linkageTask = null, partnerTask = null;
            // HTSClientLinkageExtract
            var patientArtProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "HTSClientLinkageExtract");
            if (null != patientArtProfile)
            {
                var patientArtCommand = new ExtractHTSClientLinkage()
                {
                    Extract = patientArtProfile?.Extract,
                    DatabaseProtocol = patientArtProfile?.DatabaseProtocol
                };
                linkageTask = _mediator.Send(patientArtCommand, cancellationToken);
            }

            // HTSClientPartnerExtract
            var patientBaselinesProfile = request.Extracts.FirstOrDefault(x => x.Extract.Name == "HTSClientPartnerExtract");
            if (null != patientBaselinesProfile)
            {
                var patientBaselinesCommand = new ExtractHTSClientPartner()
                {
                    Extract = patientBaselinesProfile?.Extract,
                    DatabaseProtocol = patientBaselinesProfile?.DatabaseProtocol
                };
                partnerTask = _mediator.Send(patientBaselinesCommand, cancellationToken);
            }


            // await all tasks
            var ts = new List<Task<bool>> { linkageTask, partnerTask};
            var result = await Task.WhenAll(ts);

            return result.All(x=>x);
        }
    }
}
