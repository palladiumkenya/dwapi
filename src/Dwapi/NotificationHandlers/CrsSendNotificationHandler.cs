using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Crs;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class CrsSendNotificationHandler : IHandler<CrsSendNotification>
    {
        public async Task Handle(CrsSendNotification domainEvent)
        {
            await Startup.CrsHubContext.Clients.All.SendAsync("ShowCrsSendProgress", domainEvent.Progress);
        }
    }
}
