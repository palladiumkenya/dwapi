using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Cbs;
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

        public Task Handle(CbsExtractSentEvent domainEvent)
        {
            _repository.UpdateSendStatus(domainEvent.SentItems);
            return Task.CompletedTask;
        }
    }
}
