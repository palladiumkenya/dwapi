using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Cbs
{
    public class CrsSendActivity : Hub
    {
        public async Task ShowProgress(ExtractProgress progress)
        {
            await Clients.All.SendAsync("ShowCrsSendProgress", progress);
        }
    }
}
