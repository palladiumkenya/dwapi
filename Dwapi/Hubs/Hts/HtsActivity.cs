using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Hts
{
    public class HtsActivity : Hub
    {

        public async Task ShowProgress(ExtractProgress progress)
        {
            await Clients.All.SendAsync("ShowHtsProgress", progress);
        }
    }
}
