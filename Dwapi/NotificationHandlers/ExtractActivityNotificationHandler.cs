using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.Hubs.Dwh;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.NotificationHandlers
{
    public class ExtractActivityNotificationHandler : INotificationHandler<ExtractActivityNotification>
    {
        private readonly IHubContext<ExtractActivity> _hubContext;

        public ExtractActivityNotificationHandler()
        {
            _hubContext = Startup.ServiceProvider.GetService<IHubContext<ExtractActivity>>();
        }

        public async Task Handle(ExtractActivityNotification notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("ShowProgress", notification.Progress);
        }
    }
}
