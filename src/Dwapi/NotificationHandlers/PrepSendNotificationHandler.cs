using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Prep;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class PrepSendNotificationHandler : IHandler<PrepSendNotification>
    {
        public async Task Handle(PrepSendNotification domainEvent)
        {
            await Startup.PrepHubContext.Clients.All.SendAsync("ShowPrepSendProgress", domainEvent.Progress);

            if (domainEvent.Progress.Done)
                await Startup.PrepHubContext.Clients.All.SendAsync("ShowPrepSendProgressDone",
                    domainEvent.Progress.Extract);
        }
    }
}
