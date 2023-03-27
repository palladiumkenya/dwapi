using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Prep;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class PrepExportNotificationHandler : IHandler<PrepExportNotification>
    {
        public async Task Handle(PrepExportNotification domainEvent)
        {
            await Startup.PrepHubContext.Clients.All.SendAsync("ShowPrepExportProgress", domainEvent.Progress);


            if (domainEvent.Progress.Done)
                await Startup.PrepHubContext.Clients.All.SendAsync("ShowPrepExportProgressDone",
                    domainEvent.Progress.Extract);
        }
    }
}
