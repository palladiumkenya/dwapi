using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Hts;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers
{
    public class HtsExtractSentEventHandlers : IHandler<HtsExtractSentEvent>
    {
        private readonly IHtsClientsExtractRepository _clientExtractRepository;
        private readonly IHtsClientsLinkageExtractRepository _clientLinkageExtractRepository;
        private readonly IHtsClientTestsExtractRepository _clientTestsExtractRepository;
        private readonly IHtsClientTracingExtractRepository _clientTracingExtractRepository;
        private readonly IHtsPartnerTracingExtractRepository _partnerTracingExtractRepository;
        private readonly IHtsTestKitsExtractRepository _testKitsExtractRepository;
        private readonly IHtsPartnerNotificationServicesExtractRepository _partnerNotificationServicesExtractRepository;
        private readonly IHtsEligibilityExtractRepository _htsEligibilityExtractRepository;
        private readonly IHtsRiskScoresRepository _htsRiskScoresExtractRepository;


        public HtsExtractSentEventHandlers()
        {
            _clientExtractRepository = Startup.ServiceProvider.GetService<IHtsClientsExtractRepository>();
            _clientLinkageExtractRepository = Startup.ServiceProvider.GetService<IHtsClientsLinkageExtractRepository>();
            _clientTestsExtractRepository = Startup.ServiceProvider.GetService<IHtsClientTestsExtractRepository>();
            _clientTracingExtractRepository = Startup.ServiceProvider.GetService<IHtsClientTracingExtractRepository>();
            _partnerTracingExtractRepository = Startup.ServiceProvider.GetService<IHtsPartnerTracingExtractRepository>();
            _testKitsExtractRepository = Startup.ServiceProvider.GetService<IHtsTestKitsExtractRepository>();
            _partnerNotificationServicesExtractRepository = Startup.ServiceProvider.GetService<IHtsPartnerNotificationServicesExtractRepository>();
            _htsEligibilityExtractRepository = Startup.ServiceProvider.GetService<IHtsEligibilityExtractRepository>();
            _htsRiskScoresExtractRepository = Startup.ServiceProvider.GetService<IHtsRiskScoresRepository>();

        }

        public Task Handle(HtsExtractSentEvent domainEvent)
        {
            if (domainEvent.SentItems.Any())
            {
                if (domainEvent.SentItems.First().Extract == "HtsClientsExtracts")
                    _clientExtractRepository.UpdateSendStatus(domainEvent.SentItems);
                if (domainEvent.SentItems.First().Extract == "HtsClientsLinkageExtracts")
                    _clientLinkageExtractRepository.UpdateSendStatus(domainEvent.SentItems);
                if (domainEvent.SentItems.First().Extract == "HtsClientTestsExtracts")
                    _clientTestsExtractRepository.UpdateSendStatus(domainEvent.SentItems);
                if (domainEvent.SentItems.First().Extract == "HtsClientTracingExtracts")
                    _clientTracingExtractRepository.UpdateSendStatus(domainEvent.SentItems);
                if (domainEvent.SentItems.First().Extract == "HtsPartnerTracingExtracts")
                    _partnerTracingExtractRepository.UpdateSendStatus(domainEvent.SentItems);
                if (domainEvent.SentItems.First().Extract == "HtsTestKitsExtracts")
                    _testKitsExtractRepository.UpdateSendStatus(domainEvent.SentItems);
                if (domainEvent.SentItems.First().Extract == "HtsPartnerNotificationServicesExtracts")
                    _partnerNotificationServicesExtractRepository.UpdateSendStatus(domainEvent.SentItems);
                if (domainEvent.SentItems.First().Extract == "HtsEligibilityExtracts")
                    _htsEligibilityExtractRepository.UpdateSendStatus(domainEvent.SentItems);
                if (domainEvent.SentItems.First().Extract == "HtsRiskScoresExtracts")
                    _htsRiskScoresExtractRepository.UpdateSendStatus(domainEvent.SentItems);
            }

            return Task.CompletedTask;
        }
    }
}
