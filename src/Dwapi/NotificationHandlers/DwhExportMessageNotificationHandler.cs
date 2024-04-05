using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class DwhExportMessageNotificationHandler : IHandler<DwExporthMessageNotification>
    {
        public async Task Handle(DwExporthMessageNotification domainEvent)
        {
            await Startup.HubContext.Clients.All.SendAsync("ShowDwhExportMessage", domainEvent);
        }
    }
}
