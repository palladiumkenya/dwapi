using System;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Application.Events
{
    public class DocketExtractSent : INotification
    {
        public string Docket { get;  }
        public string Extract { get;  }

        public DocketExtractSent(string docket, string extract)
        {
            Docket = docket;
            Extract = extract;
        }
    }

    public class DocketExtractSentEventHandler:INotificationHandler<DocketExtractSent>
    {
        private readonly IDiffLogRepository _repository;

        public DocketExtractSentEventHandler(IDiffLogRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(DocketExtractSent notification, CancellationToken cancellationToken)
        {
           
            var diffLog = _repository.GetAllDocketLogs(notification.Docket);
            if (null != diffLog)
            {
                foreach (var log in diffLog)
                {
                    log.LogSent();
                    _repository.SaveLog(log);
                }
            }

            return Task.CompletedTask;
        }
    }
}
