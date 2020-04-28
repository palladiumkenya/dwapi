using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Dwh
{
    public class ExtractActivity : Hub
    {
        public async Task ShowProgress(ExtractActivityNotification extractActivityNotification)
        {
            await Clients.All.SendAsync("ShowProgress", extractActivityNotification);
        }
    }
}
