using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Mnch;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class MnchExportNotificationHandler : IHandler<MnchExportNotification>
    {
        public async Task Handle(MnchExportNotification domainEvent)
        {
            await Startup.MnchHubContext.Clients.All.SendAsync("ShowMnchExportProgress", domainEvent.Progress);

            if (domainEvent.Progress.Done)
                await Startup.MnchHubContext.Clients.All.SendAsync("ShowMnchExportProgressDone",
                    domainEvent.Progress.Extract);
        }
    }
}
