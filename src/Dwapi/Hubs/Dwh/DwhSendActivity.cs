using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Dwh
{
    public class DwhSendActivity : Hub
    {
        public async Task ShowProgress(ExtractProgress progress)
        {
            await Clients.All.SendAsync("ShowDwhSendProgress", progress);
        }
    }
}
