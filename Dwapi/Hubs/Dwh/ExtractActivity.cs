using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Dwh
{
    public class ExtractActivity : Hub
    {
        public async Task ShowProgress(DwhProgress dwhProgress)
        {
            await Clients.All.SendAsync("ShowProgress", dwhProgress);
        }
    }
}
