using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Dwh;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class DwhSendNotificationHandler : IHandler<DwhSendNotification>
    {
        public async void Handle(DwhSendNotification domainEvent)
        {
            await Startup.DwhSendHubContext.Clients.All.SendAsync("ShowDwhSendProgress", domainEvent.Progress);
        }
    }
}
