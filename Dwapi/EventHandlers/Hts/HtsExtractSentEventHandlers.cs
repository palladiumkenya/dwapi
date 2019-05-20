using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Cbs;
using Dwapi.UploadManagement.Core.Event.Hts;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers
{
    public class HtsExtractSentEventHandlers : IHandler<HtsExtractSentEvent>
    {
        private readonly IHTSClientExtractRepository _clientExtractRepository;
        private readonly IHTSClientLinkageExtractRepository _clientLinkageExtractRepository;
        private readonly IHTSClientPartnerExtractRepository _partnerExtractRepository;

        public HtsExtractSentEventHandlers()
        {
            _clientExtractRepository = Startup.ServiceProvider.GetService<IHTSClientExtractRepository>();
            _clientLinkageExtractRepository = Startup.ServiceProvider.GetService<IHTSClientLinkageExtractRepository>();
            _partnerExtractRepository = Startup.ServiceProvider.GetService<IHTSClientPartnerExtractRepository>();
        }

        public void Handle(HtsExtractSentEvent domainEvent)
        {
            if (domainEvent.SentItems.First().Extract == "HTSClientExtract")
                _clientExtractRepository.UpdateSendStatus(domainEvent.SentItems);
            if (domainEvent.SentItems.First().Extract == "HTSClientLinkageExtract")
                _clientLinkageExtractRepository.UpdateSendStatus(domainEvent.SentItems);
            if (domainEvent.SentItems.First().Extract == "HTSClientPartnerExtract")
                _partnerExtractRepository.UpdateSendStatus(domainEvent.SentItems);
        }
    }
}
