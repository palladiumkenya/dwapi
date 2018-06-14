using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Services.Dwh;
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

        public void Handle(DwhExtractSentEvent domainEvent)
        {
            _service.UpdateSendStatus(domainEvent.ExtractType,domainEvent.SentItems);
        }
    }
}
