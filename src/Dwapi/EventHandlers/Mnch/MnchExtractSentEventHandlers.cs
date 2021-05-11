using System.Linq;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Mnch;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers.Mnch
{
    public class MnchExtractSentEventHandlers : IHandler<MnchExtractSentEvent>
    {
        private readonly IDwhExtractSentServcie _service;

        public MnchExtractSentEventHandlers()
        {
            _service = Startup.ServiceProvider.GetService<IDwhExtractSentServcie>();
        }

        public void Handle(MnchExtractSentEvent domainEvent)
        {
            if (domainEvent.SentItems.Any())
            {
                _service.UpdateSendStatus(domainEvent.SentItems.First().ExtractType, domainEvent.SentItems);
            }
        }
    }
}
