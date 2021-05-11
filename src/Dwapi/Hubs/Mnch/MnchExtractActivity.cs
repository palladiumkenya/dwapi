using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Mnch
{
    public class MnchExtractActivity : Hub
    {
        public async Task ShowProgress(MnchExtractActivityNotification extractActivityNotification)
        {
            await Clients.All.SendAsync("ShowMnchProgress", extractActivityNotification);
        }
    }
}
