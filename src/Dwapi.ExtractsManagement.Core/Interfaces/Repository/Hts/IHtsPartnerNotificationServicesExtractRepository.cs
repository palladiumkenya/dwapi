using System;
using System.Collections.Generic;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.SharedKernel.Interfaces;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Interfaces.Repository.Hts
{
    public interface IHtsPartnerNotificationServicesExtractRepository : IRepository<HtsPartnerNotificationServices, Guid>
    {
        bool BatchInsert(IEnumerable<HtsPartnerNotificationServices> extracts);
        IEnumerable<HtsPartnerNotificationServices> GetView();
        void UpdateSendStatus(List<SentItem> sentItems);
    }

}
