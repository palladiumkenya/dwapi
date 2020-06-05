using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SharedKernel.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.NotificationHandlers
{
    public class CTStatusNotificationHandler : IHandler<CTStatusNotification>
    {
        private IExtractHistoryRepository _repository;
        private IPatientExtractRepository _extractRepository;

        public CTStatusNotificationHandler()
        {
            _repository = Startup.ServiceProvider.GetService<IExtractHistoryRepository>();
            _extractRepository = Startup.ServiceProvider.GetService<IPatientExtractRepository>();
        }

        public void Handle(CTStatusNotification domainEvent)
        {
            _repository.UpdateStatus(domainEvent.ExtractId, domainEvent.Status,domainEvent.Stats,domainEvent.StatusInfo,true);

            if (domainEvent.UpdatePatient)
            {
                var patientStats = _extractRepository.GetSent(domainEvent.PatientExtractId);
                _repository.UpdateStatus(domainEvent.PatientExtractId, domainEvent.Status, patientStats, "", true);
            }
        }
    }
}
