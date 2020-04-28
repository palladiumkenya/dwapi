using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Mgs
{
    public class MgsSendActivity : Hub
    {
        public async Task ShowProgress(ExtractProgress progress)
        {
            await Clients.All.SendAsync("ShowMgsSendProgress", progress);
        }
    }
}
