using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class MgsNotificationHandler: IHandler<MgsNotification>
    {
        public async Task Handle(MgsNotification domainEvent)
        {
            await Startup.MgsHubContext.Clients.All.SendAsync("ShowMgsProgress", domainEvent.Progress);
        }
    }
}
