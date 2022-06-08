using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Mnch;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers.Mnch
{
    public class MnchExtractSentEventHandlers : IHandler<MnchExtractSentEvent>
    {
        private readonly IMnchExtractSentServcie _service;

        public MnchExtractSentEventHandlers()
        {
            _service = Startup.ServiceProvider.GetService<IMnchExtractSentServcie>();
        }

        public Task Handle(MnchExtractSentEvent domainEvent)
        {
            if (domainEvent.SentItems.Any())
            {
                _service.UpdateSendStatus(domainEvent.SentItems.First().ExtractType, domainEvent.SentItems);
            }
            return Task.CompletedTask;
        }
    }
}
