using Dwapi.SharedKernel.Events;
using Dwapi.UploadManagement.Core.Notifications.Cbs;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.NotificationHandlers
{
    public class CbsSendNotificationHandler : IHandler<CbsSendNotification>
    {
        public async void Handle(CbsSendNotification domainEvent)
        {
            await Startup.CbsHubContext.Clients.All.SendAsync("ShowCbsSendProgress", domainEvent.Progress);
        }
    }
}
