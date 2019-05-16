using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class HtsNotificationHandler: IHandler<HtsNotification>
    {
        public async void Handle(HtsNotification domainEvent)
        {
            await Startup.HtsHubContext.Clients.All.SendAsync("ShowHtsProgress", domainEvent.Progress);
        }
    }
}
