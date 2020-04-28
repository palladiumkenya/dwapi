using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Model;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.UploadManagement.Infrastructure.Reader
{
    public class EmrMetricReader:IEmrMetricReader
    {
        private readonly UploadContext _context;

        public EmrMetricReader(UploadContext context)
        {
            _context = context;
        }

        public IEnumerable<EmrMetricView> ReadAll()
        {
            return _context.EmrMetrics.AsNoTracking();
        }

        public IEnumerable<AppMetricView> ReadAppAll()
        {
            return _context.AppMetrics.AsNoTracking();
        }
    }
}
