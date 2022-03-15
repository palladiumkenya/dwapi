using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Dwh;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers.Dwh
{
    public class CTExtractSentEventHandlers : IHandler<CTExtractSentEvent>
    {
        private readonly IDwhExtractSentServcie _service;

        public CTExtractSentEventHandlers()
        {
            _service = Startup.ServiceProvider.GetService<IDwhExtractSentServcie>();
        }

        public Task Handle(CTExtractSentEvent domainEvent)
        {
            if (domainEvent.SentItems.Any())
            {
                _service.UpdateSendStatus(domainEvent.SentItems.First().ExtractType, domainEvent.SentItems);
            }
            return Task.CompletedTask;
        }
    }
}
