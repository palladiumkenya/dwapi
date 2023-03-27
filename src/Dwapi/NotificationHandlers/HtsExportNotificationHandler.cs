using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Dwapi.UploadManagement.Core.Notifications.Hts;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class HtsExportNotificationHandler : IHandler<HtsExportNotification>
    {
        public async Task Handle(HtsExportNotification domainEvent)
        {
            await Startup.HtsHubContext.Clients.All.SendAsync("ShowHtsExportProgress", domainEvent.Progress);

            if (domainEvent.Progress.Done)
                await Startup.HtsHubContext.Clients.All.SendAsync("ShowHtsExportProgressDone",
                    domainEvent.Progress.Extract);
        }
    }
}
