using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class DwhSendNotificationHandler : IHandler<DwhSendNotification>
    {
        public async Task Handle(DwhSendNotification domainEvent)
        {
            await Startup.HubContext.Clients.All.SendAsync("ShowDwhSendProgress", domainEvent.Progress);
        }
    }
}
