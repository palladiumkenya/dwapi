using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Crs;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Event.Crs;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.EventHandlers
{
    public class CrsExtractSentEventHandlers : IHandler<CrsExtractSentEvent>
    {
        private readonly IClientRegistryExtractRepository _repository;

        public CrsExtractSentEventHandlers()
        {
            _repository = Startup.ServiceProvider.GetService<IClientRegistryExtractRepository>();
        }

        public Task Handle(CrsExtractSentEvent domainEvent)
        {
            _repository.UpdateSendStatus(domainEvent.SentItems);
            return Task.CompletedTask;
        }
    }
}
