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

        public IEnumerable<HTSClientExtractView> ReadAllClients()
        {
            return _context.ClientExtracts.Where(x=>!x.IsSent).AsNoTracking();
        }

        public IEnumerable<HTSClientPartnerExtractView> ReadAllPartners()
        {
            return _context.ClientPartnerExtracts.Where(x=>!x.IsSent).AsNoTracking();
        }

        public IEnumerable<HTSClientLinkageExtractView> ReadAllLinkages()
        {
            return _context.ClientLinkageExtracts.Where(x=>!x.IsSent).AsNoTracking();
        }
    }
}
