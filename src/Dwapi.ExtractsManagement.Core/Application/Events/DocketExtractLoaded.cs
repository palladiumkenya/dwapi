using System;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Diff;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
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
        private readonly IIndicatorExtractRepository _indicatorExtractRepository;


        public DocketExtractLoadedEventHandler(IDiffLogRepository repository, IIndicatorExtractRepository indicatorExtractRepository)
        {
            _repository = repository;
            _indicatorExtractRepository = indicatorExtractRepository;
        }

        public Task Handle(DocketExtractLoaded notification, CancellationToken cancellationToken)
        {
            var diffLog = _repository.GetLog(notification.Docket, notification.Extract, notification.SiteCode);
            if (null == diffLog)
            {
                _repository.InitLog(notification.Docket, notification.Extract, notification.SiteCode);
            }

            if (null != diffLog)
            {
                var siteCode = _indicatorExtractRepository.GetMflCode();
            
                // var diffLogs = _repository.GetAllDocketLogs("NDWH");

                _repository.UpdateMaxDates(notification.Docket, notification.Extract, siteCode);
                    
            }

            return Task.CompletedTask;
        }
    }
}
