using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Hts;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Hts
{
    public interface IHtsExtractReader
    {
        IEnumerable<HtsClientsExtractView> ReadAllClients();
        IEnumerable<HtsClientTestsExtractView> ReadAllClientTests();
        IEnumerable<HtsTestKitsExtractView> ReadAllTestKits();
        IEnumerable<HtsClientTracingExtractView> ReadAllClientTracing();
        IEnumerable<HtsPartnerTracingExtractView> ReadAllPartnerTracing();
        IEnumerable<HtsPartnerNotificationServicesExtractView> ReadAllPartnerNotificationServices();
        IEnumerable<HtsClientsLinkageExtractView> ReadAllClientsLinkage();
    }
}
