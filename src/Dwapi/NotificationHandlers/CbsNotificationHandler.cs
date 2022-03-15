using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class CbsNotificationHandler: IHandler<CbsNotification>
    {
        public async Task Handle(CbsNotification domainEvent)
        {
            await Startup.CbsHubContext.Clients.All.SendAsync("ShowCbsProgress", domainEvent.Progress);
        }
    }
}
