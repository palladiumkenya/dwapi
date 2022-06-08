using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Crs
{
    public class CrsActivity : Hub
    {

        public async Task ShowProgress(ExtractProgress progress)
        {
            await Clients.All.SendAsync("ShowCrsProgress", progress);
        }
    }
}
