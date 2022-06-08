using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class CrsNotificationHandler: IHandler<CrsNotification>
    {
        public async Task Handle(CrsNotification domainEvent)
        {
            await Startup.CrsHubContext.Clients.All.SendAsync("ShowCrsProgress", domainEvent.Progress);
        }
    }
}
