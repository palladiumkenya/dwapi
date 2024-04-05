using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Prep
{
    public class PrepActivity : Hub
    {
        public async Task ShowProgress(PrepExtractActivityNotification extractActivityNotification)
        {
            await Clients.All.SendAsync("ShowPrepProgress", extractActivityNotification);

            
        }
    }
}
