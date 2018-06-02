using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.Hubs.Dwh;
using Dwapi.SharedKernel.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.NotificationHandlers
{
    public class ExtractActivityNotificationHandler : IHandler<ExtractActivityNotification>
    {
       
        public async void Handle(ExtractActivityNotification domainEvent)
        {

            await Startup.HubContext.Clients.All.SendAsync("ShowProgress", domainEvent.Progress);
        }
    }
}
