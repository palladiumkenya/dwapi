using System.Threading.Tasks;
using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Mgs;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class MgsSendNotificationHandler : IHandler<MgsSendNotification>
    {
        public async Task Handle(MgsSendNotification domainEvent)
        {
            await Startup.MgsHubContext.Clients.All.SendAsync("ShowMgsSendProgress", domainEvent.Progress);

            if (domainEvent.Progress.Done)
                await Startup.MgsHubContext.Clients.All.SendAsync("ShowMgsSendProgressDone",
                    domainEvent.Progress.Extract);
        }
    }
}
