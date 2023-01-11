using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class CTExportNotificationHandler : IHandler<CTExportNotification>
    {
        public async Task Handle(CTExportNotification domainEvent)
        {
            await Startup.HubContext.Clients.All.SendAsync("ShowDwhExportProgress", domainEvent.Progress);

            if (domainEvent.Progress.Done)
            {
                await Startup.HubContext.Clients.All.SendAsync("ShowDwhExportProgressDone",
                    domainEvent.Progress.Extract);
            }

        }
    }
}
