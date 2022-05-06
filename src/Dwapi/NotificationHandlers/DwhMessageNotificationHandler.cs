using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class DwhMessageNotificationHandler : IHandler<DwhMessageNotification>
    {
        public async Task Handle(DwhMessageNotification domainEvent)
        {
            await Startup.HubContext.Clients.All.SendAsync("ShowDwhSendMessage", domainEvent);
        }
    }
}
