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
/*
 5E05C83B-8146-4A8C-A459-86478C2308A5|Allergies Chronic Illness|NDWH|A6221861-0E85-11E8-BA89-0ED5F89F718B|""|dwhstage|false|AllergiesChronicIllnessExtract|11.00|A6221AA7-0E85-11E8-BA89-0ED5F89F718B
D57522E5-5F90-4762-9725-60D106FD2475|IPT|NDWH|A6221861-0E85-11E8-BA89-0ED5F89F718B|""|dwhstage|false|IptExtract|12.00|A6221AA7-0E85-11E8-BA89-0ED5F89F718B
D319EE2C-70B6-4A71-A1C5-C132FC4EF07A|Depression Screening|NDWH|A6221861-0E85-11E8-BA89-0ED5F89F718B|""|dwhstage|false|DepressionScreeningExtract|13.00|A6221AA7-0E85-11E8-BA89-0ED5F89F718B
66CC2352-FD9F-42CC-AAF5-BDD5B40FDB12|Contact Listing|NDWH|A6221861-0E85-11E8-BA89-0ED5F89F718B|""|dwhstage|false|ContactListingExtract|14.00|A6221AA7-0E85-11E8-BA89-0ED5F89F718B
7F4B7F3B-0CD6-4809-8DA7-417F3234B448|GBV Screening|NDWH|A6221861-0E85-11E8-BA89-0ED5F89F718B|""|dwhstage|false|GbvScreeningExtract|15.00|A6221AA7-0E85-11E8-BA89-0ED5F89F718B
B46AF441-83F7-4E2F-95B2-1FF6642587F6|Enhanced Adherence Counselling|NDWH|A6221861-0E85-11E8-BA89-0ED5F89F718B|""|dwhstage|false|EnhancedAdherenceCounsellingExtract|16.00|A6221AA7-0E85-11E8-BA89-0ED5F89F718B
7F343E0B-FEAB-408D-A5D6-6771B7C84461|Drug and Alcohol Screening|NDWH|A6221861-0E85-11E8-BA89-0ED5F89F718B|""|dwhstage|false|DrugAlcoholScreeningExtract|17.00|A6221AA7-0E85-11E8-BA89-0ED5F89F718B
8CD7C588-478A-4D2A-B2B4-1CCD33CEB9C5|OVC|NDWH|A6221861-0E85-11E8-BA89-0ED5F89F718B|""|dwhstage|false|OvcExtract|18.00|A6221AA7-0E85-11E8-BA89-0ED5F89F718B
F9A1E560-DD0B-40EF-A84F-0642B8410DA8|OTZ|NDWH|A6221861-0E85-11E8-BA89-0ED5F89F718B|""|dwhstage|false|OtzExtract|19.00|A6221AA7-0E85-11E8-BA89-0ED5F89F718B
 */
