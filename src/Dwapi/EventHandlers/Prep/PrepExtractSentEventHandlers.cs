using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Prep;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers.Prep
{
    public class PrepExtractSentEventHandlers : IHandler<PrepExtractSentEvent>
    {
        private readonly IPrepExtractSentServcie _service;

        public PrepExtractSentEventHandlers()
        {
            _service = Startup.ServiceProvider.GetService<IPrepExtractSentServcie>();
        }

        public Task Handle(PrepExtractSentEvent domainEvent)
        {
            if (domainEvent.SentItems.Any())
            {
                _service.UpdateSendStatus(domainEvent.SentItems.First().ExtractType, domainEvent.SentItems);
            }
            return Task.CompletedTask;
        }
    }
}
