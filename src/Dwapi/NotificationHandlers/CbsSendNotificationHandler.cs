using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Cbs;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class CbsSendNotificationHandler : IHandler<CbsSendNotification>
    {
        public async Task Handle(CbsSendNotification domainEvent)
        {
            await Startup.CbsHubContext.Clients.All.SendAsync("ShowCbsSendProgress", domainEvent.Progress);
        }
    }
}
