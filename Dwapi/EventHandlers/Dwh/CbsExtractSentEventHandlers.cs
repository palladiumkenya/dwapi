using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Dwh;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers.Dwh
{
    public class DwhExtractSentEventHandlers : IHandler<DwhExtractSentEvent>
    {
        private readonly IMasterPatientIndexRepository _repository;

        public DwhExtractSentEventHandlers()
        {
            _repository = Startup.ServiceProvider.GetService<IMasterPatientIndexRepository>();
        }

        public void Handle(DwhExtractSentEvent domainEvent)
        {
            _repository.UpdateSendStatus(domainEvent.SentItems);
        }
    }
}
