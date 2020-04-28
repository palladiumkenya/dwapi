using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.NotificationHandlers
{
    public class MgsStatusNotificationHandler : IHandler<MgsStatusNotification>
    {
        private IExtractHistoryRepository _repository;

        public MgsStatusNotificationHandler()
        {
            _repository = Startup.ServiceProvider.GetService<IExtractHistoryRepository>();
        }

        public void Handle(MgsStatusNotification domainEvent)
        {
            _repository.UpdateStatus(domainEvent.ExtractId,domainEvent.Status,domainEvent.Stats,domainEvent.StatusInfo,true);
        }
    }
}
