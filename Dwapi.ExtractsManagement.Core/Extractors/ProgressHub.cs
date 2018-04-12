using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public class ProgressHub : Hub
    {
        public Task Send(LoadProgress loadProgress)
            =>  Clients.All.SendAsync("", loadProgress);
        
    }

    public class LoadProgress
    {
        public string Extract { get; set; }
        public string Status { get; set; }
    }
}
