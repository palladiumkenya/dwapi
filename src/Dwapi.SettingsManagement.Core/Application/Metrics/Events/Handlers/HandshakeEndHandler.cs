using System.Threading;
using System.Threading.Tasks;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Model;
using MediatR;
using Newtonsoft.Json;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Events.Handlers
{
    public class HandshakeEndHandler : INotificationHandler<HandshakeEnd>
    {
        private readonly IAppMetricRepository _repository;

        public HandshakeEndHandler(IAppMetricRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(HandshakeEnd notification, CancellationToken cancellationToken)
        {

            var session = _repository.GetSession(notification.EndName);
            notification.UpdateSession(session);

            var metric = new AppMetric(notification.Version, notification.Name,
                JsonConvert.SerializeObject(notification));
            _repository.Clear(notification.Name);
            _repository.Create(metric);
            _repository.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
