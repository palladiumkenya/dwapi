using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Dwh;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers.Dwh
{
    public class DwhExtractSentEventHandlers : IHandler<DwhExtractSentEvent>
    {
        private readonly IDwhExtractSentServcie _service;

        public DwhExtractSentEventHandlers()
        {
            _service = Startup.ServiceProvider.GetService<IDwhExtractSentServcie>();
        }

        public Task Handle(DwhExtractSentEvent domainEvent)
        {
            _service.UpdateSendStatus(domainEvent.ExtractType, domainEvent.SentItems);
            return Task.CompletedTask;
        }
    }
}
