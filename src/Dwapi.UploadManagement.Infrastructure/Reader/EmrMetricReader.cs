using System;
using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Interfaces.Reader;
using Dwapi.UploadManagement.Core.Model;
using Dwapi.UploadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Newtonsoft.Json;

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

        public Guid GetSession(string notificationName)
        {
            var metric = _context.AppMetrics.AsNoTracking().FirstOrDefault(x => x.Name == notificationName);
            if (null != metric)
            {
                var handshake = JsonConvert.DeserializeObject<HandshakeStart>(metric.LogValue);
                return handshake.Session;
            }
            return Guid.Empty;
        }
    }
}
