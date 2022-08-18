using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.NotificationHandlers
{
    public class CbsStatusNotificationHandler : IHandler<CbsStatusNotification>
    {
        private IExtractHistoryRepository _repository;

        public CbsStatusNotificationHandler()
        {
            _repository = Startup.ServiceProvider.GetService<IExtractHistoryRepository>();
        }

        public Task Handle(CbsStatusNotification domainEvent)
        {
           _repository.UpdateStatus(domainEvent.ExtractId,domainEvent.Status,domainEvent.Stats,domainEvent.StatusInfo,true);
           return Task.CompletedTask;
        }
    }
}
