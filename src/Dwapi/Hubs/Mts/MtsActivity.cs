using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Mts
{
    public class MtsActivity : Hub
    {

        public async Task ShowProgress(ExtractProgress progress)
        {
            await Clients.All.SendAsync("ShowMtsProgress", progress);
        }
    }
}
