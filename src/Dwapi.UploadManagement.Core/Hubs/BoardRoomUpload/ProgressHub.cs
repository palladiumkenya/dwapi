using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Dwapi.UploadManagement.Core.Hubs.BoardRoomUpload
{
    public class ProgressHub : Hub
    {
        public async Task SendProgress(double progress)
        {
            await Clients.All.SendAsync("ReceiveProgress", progress);
        }
    }
}


