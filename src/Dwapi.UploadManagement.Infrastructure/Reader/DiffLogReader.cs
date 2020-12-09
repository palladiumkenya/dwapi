using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Model;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.UploadManagement.Infrastructure.Reader
{
    public class DiffLogReader:IDiffLogReader
    {
        private readonly UploadContext _context;

        public DiffLogReader(UploadContext context)
        {
            _context = context;
        }


        public IEnumerable<DiffLogView> ReadAll()
        {
            return _context.DiffLogs.AsNoTracking();
        }
    }
}