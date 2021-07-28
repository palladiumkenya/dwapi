using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class MnchNotificationHandler: IHandler<MnchNotification>
    {
        public async void Handle(MnchNotification domainEvent)
        {
            await Startup.MnchHubContext.Clients.All.SendAsync("ShowMnchProgress", domainEvent.Progress);
        }
    }
}
