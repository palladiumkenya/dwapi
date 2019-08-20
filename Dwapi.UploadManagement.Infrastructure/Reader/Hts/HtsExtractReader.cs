using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Reader.Hts;
using Dwapi.UploadManagement.Core.Model.Cbs;
using Dwapi.UploadManagement.Core.Model.Hts;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.UploadManagement.Infrastructure.Reader.Hts
{
    public class HtsExtractReader:IHtsExtractReader
    {

        private readonly UploadContext _context;

        public HtsExtractReader(UploadContext context)
        {
            _context = context;
        }

        public IEnumerable<HtsClientsExtractView> ReadAllClients()
        {
            return _context.ClientExtracts.Where(x=>!x.IsSent).AsNoTracking();
        }

        public IEnumerable<HtsClientTestsExtractView> ReadAllClientTests()
        {
            return _context.ClientTestsExtracts.Where(x=>!x.IsSent).AsNoTracking();
        }

        public IEnumerable<HtsTestKitsExtractView> ReadAllTestKits()
        {
            return _context.TestKitsExtracts.Where(x=>!x.IsSent).AsNoTracking();
        }

        public IEnumerable<HtsClientTracingExtractView> ReadAllClientTracing()
        {
            return _context.ClientTracingExtracts.Where(x => !x.IsSent).AsNoTracking();
        }

        public IEnumerable<HtsPartnerTracingExtractView> ReadAllPartnerTracing()
        {
            return _context.PartnerTracingExtracts.Where(x => !x.IsSent).AsNoTracking();
        }

        public IEnumerable<HtsPartnerNotificationServicesExtractView> ReadAllPartnerNotificationServices()
        {
            return _context.PartnerNotificationServicesExtracts.Where(x => !x.IsSent).AsNoTracking();
        }
        public IEnumerable<HtsClientsLinkageExtractView> ReadAllClientsLinkage()
        {
            return _context.ClientLinkageExtracts.Where(x => !x.IsSent).AsNoTracking();
        }
    }
}
