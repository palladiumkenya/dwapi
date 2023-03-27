using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Hts
{
    public class HtsSendActivity : Hub
    {
        public async Task ShowProgress(ExtractProgress progress)
        {
            await Clients.All.SendAsync("ShowHtsSendProgress", progress);

            await Clients.All.SendAsync("ShowHtsExportProgress", progress);
        }
    }
}
