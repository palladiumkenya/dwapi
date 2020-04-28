using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mgs;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Mgs;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers.Mgs
{

    public class MgsExtractSentEventHandlers : IHandler<MgsExtractSentEvent>
    {
        private readonly IMetricMigrationExtractRepository _metricMigrationExtractRepository;

        public MgsExtractSentEventHandlers()
        {
            _metricMigrationExtractRepository = Startup.ServiceProvider.GetService<IMetricMigrationExtractRepository>();
        }

        public void Handle(MgsExtractSentEvent domainEvent)
        {
            if (domainEvent.SentItems.Any())
            {
                if (domainEvent.SentItems.First().Extract == "Migration")
                    _metricMigrationExtractRepository.UpdateSendStatus(domainEvent.SentItems);
            }
        }
    }
}
