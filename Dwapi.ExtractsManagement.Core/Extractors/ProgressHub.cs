using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dwapi.ExtractsManagement.Core.Extractors
{
    public class ProgressHub : Hub
    {
        public Task Send(LoadProgress loadProgress) { return Task.Run(()=> Console.Write("sent")); }
            //=>  Clients.All.SendAsync("", loadProgress);
        
    }

    public class LoadProgress
    {
        public string Extract { get; set; }
        public LoadStatus Status { get; set; }
    }

    public enum LoadStatus
    {
        Fetching,
        FetchingFailed,
        Loading,
        LoadingFailed,
        Completed,
        Failed
    }
}
