using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class CbsNotificationHandler: IHandler<CbsNotification>
    {
        public async void Handle(CbsNotification domainEvent)
        {
            await Startup.CbsHubContext.Clients.All.SendAsync("ShowCbsProgress", domainEvent.Progress);
        }
    }
}
