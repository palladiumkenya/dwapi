using System;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Dwapi.NotificationHandlers
{
    public class HtsExtractActivityNotificationHandler : IHandler<HtsExtractActivityNotification>
    {
        private IExtractHistoryRepository _extractHistoryRepository;

        public async void Handle(HtsExtractActivityNotification domainEvent)
        {
            _extractHistoryRepository = Startup.ServiceProvider.GetService<IExtractHistoryRepository>();

            SaveExtractHistory(domainEvent);
            await Startup.HtsHubContext.Clients.All.SendAsync("ShowHtsProgress", domainEvent);
        }

        private void SaveExtractHistory(HtsExtractActivityNotification domainEvent)
        {
            int count = 0;
            var status = (ExtractStatus)Enum.Parse(typeof(ExtractStatus), domainEvent.Progress.Status);

            switch (status)
            {
                case ExtractStatus.Found:
                    count = domainEvent.Progress.Found;
                    break;

                case ExtractStatus.Loaded:
                    count = domainEvent.Progress.Loaded;
                    break;
                case ExtractStatus.Sending:
                    count = domainEvent.Progress.Sent;
                    break;
                case ExtractStatus.Sent:
                    count = domainEvent.Progress.Sent;
                    break;
            }

            if (count == 0)
            {
                _extractHistoryRepository.DwhUpdateStatus(domainEvent.ExtractId, status);
            }
            else
            {
                _extractHistoryRepository.DwhUpdateStatus(domainEvent.ExtractId, status, count);
            }
        }
    }
}
