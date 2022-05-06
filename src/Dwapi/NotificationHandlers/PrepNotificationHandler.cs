using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class PrepNotificationHandler: IHandler<PrepNotification>
    {
        public async Task Handle(PrepNotification domainEvent)
        {
            await Startup.PrepHubContext.Clients.All.SendAsync("ShowPrepProgress", domainEvent.Progress);
        }
    }
}
