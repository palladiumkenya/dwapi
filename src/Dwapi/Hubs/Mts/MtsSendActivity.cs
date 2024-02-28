using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Mgs
{
    public class MtsSendActivity : Hub
    {
        public async Task ShowProgress(ExtractProgress progress)
        {
            await Clients.All.SendAsync("ShowMtsSendProgress", progress);
        }
    }
}
