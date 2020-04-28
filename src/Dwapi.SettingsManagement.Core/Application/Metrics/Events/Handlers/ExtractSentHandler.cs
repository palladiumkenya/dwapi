using System.Threading;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using MediatR;
using Newtonsoft.Json;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Events.Handlers
{
    public class ExtractSentHandler : INotificationHandler<ExtractSent>
    {
        private readonly IAppMetricRepository _repository;

        public ExtractSentHandler(IAppMetricRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(ExtractSent notification, CancellationToken cancellationToken)
        {
            var metric = new AppMetric(notification.Version, notification.Name,
                JsonConvert.SerializeObject(notification));
            _repository.Clear(notification.Name,"NoSent");
            _repository.Create(metric);
            _repository.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
