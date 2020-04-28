using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class MgsNotificationHandler: IHandler<MgsNotification>
    {
        public async void Handle(MgsNotification domainEvent)
        {
            await Startup.MgsHubContext.Clients.All.SendAsync("ShowMgsProgress", domainEvent.Progress);
        }
    }
}
