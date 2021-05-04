using System.Threading;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using MediatR;
using Newtonsoft.Json;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Events.Handlers
{
    public class HandshakeStartHandler : INotificationHandler<HandshakeStart>
    {
        private readonly IAppMetricRepository _repository;

        public HandshakeStartHandler(IAppMetricRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(HandshakeStart notification, CancellationToken cancellationToken)
        {
            var metric = new AppMetric(notification.Version, notification.Name,
                JsonConvert.SerializeObject(notification));
            _repository.Clear(notification.Name);
            _repository.Create(metric);
            _repository.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
