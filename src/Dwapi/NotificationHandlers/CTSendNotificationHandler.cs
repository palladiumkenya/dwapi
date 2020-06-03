using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Dwapi.UploadManagement.Core.Notifications.Hts;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class CTSendNotificationHandler : IHandler<CTSendNotification>
    {
        public async void Handle(CTSendNotification domainEvent)
        {
            await Startup.DwhSendHubContext.Clients.All.SendAsync("ShowDwhSendProgress", domainEvent.Progress);

            if (domainEvent.Progress.Done)
            {
                await Startup.DwhSendHubContext.Clients.All.SendAsync("ShowDwhSendProgressDone",
                    domainEvent.Progress.Extract);
            }

        }
    }
}
