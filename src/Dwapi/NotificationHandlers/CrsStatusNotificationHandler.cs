using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.NotificationHandlers
{
    public class CrsStatusNotificationHandler : IHandler<CrsStatusNotification>
    {
        private IExtractHistoryRepository _repository;

        public CrsStatusNotificationHandler()
        {
            _repository = Startup.ServiceProvider.GetService<IExtractHistoryRepository>();
        }

        public Task Handle(CrsStatusNotification domainEvent)
        {
           _repository.UpdateStatus(domainEvent.ExtractId,domainEvent.Status,domainEvent.Stats,domainEvent.StatusInfo,true);
           return Task.CompletedTask;
        }
    }
}
