using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Mnch;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class MnchSendNotificationHandler : IHandler<MnchSendNotification>
    {
        public async void Handle(MnchSendNotification domainEvent)
        {
            await Startup.MnchHubContext.Clients.All.SendAsync("ShowMnchSendProgress", domainEvent.Progress);

            if (domainEvent.Progress.Done)
                await Startup.MnchHubContext.Clients.All.SendAsync("ShowMnchSendProgressDone",
                    domainEvent.Progress.Extract);
        }
    }
}
