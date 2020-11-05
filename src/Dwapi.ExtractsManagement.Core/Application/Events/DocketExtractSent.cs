using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Dwapi.ExtractsManagement.Core.Application.Events
{
    public class DocketExtractLoaded : INotification
    {
        public string Docket { get;  }
        public string Extract { get;  }
        public DateTime? LastCreated { get;  }
        public DateTime? LastModified { get; }

        public DocketExtractLoaded(string docket, string extract, DateTime? lastCreated, DateTime? lastModified)
        {
            Docket = docket;
            Extract = extract;
            LastCreated = lastCreated;
            LastModified = lastModified;
        }
    }

    public class DocketExtractLoadedEventHanlder:INotificationHandler<DocketExtractLoaded>
    {
        public Task Handle(DocketExtractLoaded notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}