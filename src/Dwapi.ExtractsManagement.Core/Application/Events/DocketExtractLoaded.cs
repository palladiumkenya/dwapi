using System;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Application.Events
{
    public class DocketExtractLoaded : INotification
    {
        public string Docket { get;  }
        public string Extract { get;  }
        public int SiteCode { get;  }
        public DocketExtractLoaded(string docket, string extract, int siteCode)
        {
            Docket = docket;
            Extract = extract;
            SiteCode = siteCode;
        }
    }

    public class DocketExtractLoadedEventHandler:INotificationHandler<DocketExtractLoaded>
    {
        private readonly IDiffLogRepository _repository;

        public DocketExtractLoadedEventHandler(IDiffLogRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(DocketExtractLoaded notification, CancellationToken cancellationToken)
        {
            var generatedDates = _repository.GenerateDiff(notification.Docket, $"{notification.Extract}s", notification.SiteCode);

            var diffLog = _repository.InitLog(notification.Docket, notification.Extract, notification.SiteCode);

            if (null != diffLog)
            {
                diffLog.LogLoad(generatedDates.MaxCreated, generatedDates.MaxModified);
                _repository.SaveLog(diffLog);
            }

            return Task.CompletedTask;
        }
    }
}
