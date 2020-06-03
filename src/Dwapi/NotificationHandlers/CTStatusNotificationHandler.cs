using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.NotificationHandlers
{
    public class CTStatusNotificationHandler : IHandler<CTStatusNotification>
    {
        private IExtractHistoryRepository _repository;

        public CTStatusNotificationHandler()
        {
            _repository = Startup.ServiceProvider.GetService<IExtractHistoryRepository>();
        }

        public void Handle(CTStatusNotification domainEvent)
        {
            _repository.UpdateStatus(domainEvent.ExtractId, domainEvent.Status,domainEvent.Stats,domainEvent.StatusInfo,true);
        }
    }
}
