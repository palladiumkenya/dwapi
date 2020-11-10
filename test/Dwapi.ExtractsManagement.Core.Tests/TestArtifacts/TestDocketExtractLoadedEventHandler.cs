using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Application.Events;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Tests.TestArtifacts
{
    public class TestDocketExtractLoadedEventHandler : INotificationHandler<DocketExtractLoaded>
    {
        public Task Handle(DocketExtractLoaded notification, CancellationToken cancellationToken)
        {
            Log.Debug(new string('*',40));
            Log.Debug($"{notification.Docket}-{notification.Extract} LOADED");
            Log.Debug(new string('*',40));
            Log.Debug(new string('+',40));
            return Task.CompletedTask;
        }
    }
}
