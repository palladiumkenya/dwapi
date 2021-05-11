using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Mnch
{
    public class MnchSendActivity : Hub
    {
        public async Task ShowProgress(ExtractProgress progress)
        {
            await Clients.All.SendAsync("ShowMnchSendProgress", progress);
        }
    }
}
