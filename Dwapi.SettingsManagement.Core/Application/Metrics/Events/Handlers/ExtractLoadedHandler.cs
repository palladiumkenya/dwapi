using System.Threading;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using MediatR;
using Newtonsoft.Json;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Events.Handlers
{
    public class ExtractLoadedHandler:INotificationHandler<ExtractLoaded>
    {
        private readonly IAppMetricRepository _repository;

        public ExtractLoadedHandler(IAppMetricRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(ExtractLoaded notification, CancellationToken cancellationToken)
        {
            var metric = new AppMetric(notification.Version, notification.Name,
                JsonConvert.SerializeObject(notification));
            _repository.Clear(notification.Name,"NoLoaded");
            _repository.Create(metric);
            _repository.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
