using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.SharedKernel.Model;
using Microsoft.AspNetCore.SignalR;
using Serilog;

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
