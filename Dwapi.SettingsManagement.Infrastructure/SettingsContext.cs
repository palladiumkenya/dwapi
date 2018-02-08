using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.SettingsManagement.Infrastructure
{
    public class SettingsContext:BaseContext
    {
        public DbSet<EmrSystem> EmrSystems { get; set; }
        public DbSet<DatabaseProtocol> DatabaseProtocols { get; set; }
        public DbSet<RestProtocol> RestProtocols { get; set; }
        public DbSet<CentralRegistry> CentralRegistries { get; set; }

        public SettingsContext(DbContextOptions<SettingsContext> options) : base(options)
        {
        }
    }
}