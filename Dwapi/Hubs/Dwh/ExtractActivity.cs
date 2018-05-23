using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Dwapi.Hubs.Dwh
{
    public class ExtractActivity : Hub
    {
        public async Task ShowProgress(string extract,string status, int count)
        {
            await Clients.All.SendAsync("ShowProgress", extract, status, count);
        }
    }
}
