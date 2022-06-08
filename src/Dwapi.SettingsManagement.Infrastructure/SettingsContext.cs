using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using LiveSeeder.Core;
using Z.Dapper.Plus;

namespace Dwapi.SettingsManagement.Infrastructure
{
    public class SettingsContext : BaseContext
    {
        public SettingsContext(DbContextOptions<SettingsContext> options) : base(options)
        {
        }
        public DbSet<CentralRegistry> CentralRegistries { get; set; }
        public DbSet<EmrSystem> EmrSystems { get; set; }
        public DbSet<DatabaseProtocol> DatabaseProtocols { get; set; }
        public DbSet<RestProtocol> RestProtocols { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Docket> Dockets { get; set; }
        public DbSet<Extract> Extracts { get; set; }
        public DbSet<AppMetric> AppMetrics { get; set; }
        public DbSet<IntegrityCheck> IntegrityChecks { get; set; }
        public DbSet<IntegrityCheckRun> IntegrityCheckRuns { get; set; }
        public DbSet<IndicatorKey> IndicatorKeys { get; set; }

        public DbSet<TransportLog> TransportLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DapperPlusManager.Entity<CentralRegistry>().Key(x => x.Id).Table($"{nameof(CentralRegistries)}");
            DapperPlusManager.Entity<EmrSystem>().Key(x => x.Id).Table($"{nameof(EmrSystems)}");
            DapperPlusManager.Entity<DatabaseProtocol>().Key(x => x.Id).Table($"{nameof(DatabaseProtocols)}");
            DapperPlusManager.Entity<RestProtocol>().Key(x => x.Id).Table($"{nameof(RestProtocols)}");
            DapperPlusManager.Entity<Resource>().Key(x => x.Id).Table($"{nameof(Resources)}");
            DapperPlusManager.Entity<Docket>().Key(x => x.Id).Table($"{nameof(Dockets)}");
            DapperPlusManager.Entity<Extract>().Key(x => x.Id).Table($"{nameof(Extracts)}");
            DapperPlusManager.Entity<AppMetric>().Key(x => x.Id).Table($"{nameof(AppMetrics)}");
            DapperPlusManager.Entity<IntegrityCheck>().Key(x => x.Id).Table($"{nameof(IntegrityChecks)}");
            DapperPlusManager.Entity<IntegrityCheckRun>().Key(x => x.Id).Table($"{nameof(IntegrityCheckRuns)}");
            DapperPlusManager.Entity<IndicatorKey>().Key(x => x.Id).Table($"{nameof(IndicatorKeys)}");
            DapperPlusManager.Entity<TransportLog>().Key(x => x.Id).Table($"{nameof(TransportLogs)}");
        }

        public override void EnsureSeeded()
        {
            var managedEmrs = new List<Guid>
            {
                new Guid("a62216ee-0e85-11e8-ba89-0ed5f89f718b"),
                new Guid("a6221856-0e85-11e8-ba89-0ed5f89f718b"),
                new Guid("a6221857-0e85-11e8-ba89-0ed5f89f718b"),
                new Guid("926f49b8-305d-11ea-978f-2e728ce88125"),
                new Guid("a6221860-0e85-11e8-ba89-0ed5f89f718b")
            };

            this.SeedMerge<Docket>(typeof(SettingsContext).Assembly, "|", "Seed", $"{nameof(Dockets)}").Wait();
            this.SeedMerge<CentralRegistry>(typeof(SettingsContext).Assembly, "|", "Seed",
                $"{nameof(CentralRegistries)}").Wait();
            this.SeedNewOnly<EmrSystem>(typeof(SettingsContext).Assembly, "|", "Seed", $"{nameof(EmrSystems)}").Wait();
            this.SeedNewOnly<DatabaseProtocol>(typeof(SettingsContext).Assembly, "|", "Seed",
                $"{nameof(DatabaseProtocols)}").Wait();

            var restEndpoints = RestProtocols.AsNoTracking().ToList();
            if (restEndpoints.All(x => x.Url.Contains("https://palladiumkenya.github.io/dwapi/stuff")))
            {
                this.SeedMerge<RestProtocol>(typeof(SettingsContext).Assembly, "|", "Seed", $"{nameof(RestProtocols)}")
                    .Wait();
                this.SeedMerge<Resource>(typeof(SettingsContext).Assembly, "|", "Seed", $"{nameof(Resources)}").Wait();
            }
            else
            {
                this.SeedNewOnly<RestProtocol>(typeof(SettingsContext).Assembly, "|", "Seed",
                    $"{nameof(RestProtocols)}").Wait();
                this.SeedNewOnly<Resource>(typeof(SettingsContext).Assembly, "|", "Seed", $"{nameof(Resources)}")
                    .Wait();
            }

            managedEmrs.ForEach(x =>
            {
                var sql = $"DELETE FROM {nameof(Extracts)} WHERE {nameof(Extract.EmrSystemId)} = '{x}'";
                Database.ExecuteSqlCommand(sql);
            });

            this.SeedNewOnly<Extract>(typeof(SettingsContext).Assembly, "|", "Seed", $"{nameof(Extracts)}").Wait();
            this.SeedMerge<IntegrityCheck>(typeof(SettingsContext).Assembly, ",", "Seed", $"{nameof(IntegrityChecks)}").Wait();
            this.SeedMerge<IndicatorKey>(typeof(SettingsContext).Assembly, ",", "Seed", $"{nameof(IndicatorKeys)}").Wait();
        }
    }
}
