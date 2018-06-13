using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers
{
    public class CbsExtractSentEventHandlers : IHandler<CbsExtractSentEvent>
    {
        private readonly IMasterPatientIndexRepository _repository;

        public CbsExtractSentEventHandlers()
        {
            _repository = Startup.ServiceProvider.GetService<IMasterPatientIndexRepository>();
        }

        public void Handle(CbsExtractSentEvent domainEvent)
        {
            _repository.UpdateSendStatus(domainEvent.SentItems);
        }
    }
}
