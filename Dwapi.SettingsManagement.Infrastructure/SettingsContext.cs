using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure;
using EFCore.Seeder.Configuration;
using EFCore.Seeder.Extensions;
using EFCore.Seeder.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Serilog;

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

        public override void EnsureSeeded()
        {
            var csvConfig = new CsvConfiguration
            {
                Delimiter = "|",
                SkipEmptyRecords = true,
                TrimFields = true,
                TrimHeaders = true,
                WillThrowOnMissingField = false
            };
            SeederConfiguration.ResetConfiguration(csvConfig, null, typeof(SettingsContext).GetTypeInfo().Assembly);

            if (!Dockets.Any())
                Dockets.SeedDbSetIfEmpty($"{nameof(Dockets)}");

            if (!Dockets.Any(x => x.Id =="MGS"))
                Dockets.SeedFromResource($"{nameof(Dockets)}New");

            var reg = CentralRegistries.ToList();
            if (reg.Any())
            {
                CentralRegistries.RemoveRange(reg);
                SaveChanges();
            }

            CentralRegistries.SeedDbSetIfEmpty($"{nameof(CentralRegistries)}");

            if (!EmrSystems.Any())
                EmrSystems.SeedDbSetIfEmpty($"{nameof(EmrSystems)}");

            if (!EmrSystems.Any(x => x.Id == new Guid("926f49b8-305d-11ea-978f-2e728ce88125")))
                EmrSystems.SeedFromResource($"{nameof(EmrSystems)}New");

            if (!DatabaseProtocols.Any())
                DatabaseProtocols.SeedDbSetIfEmpty($"{nameof(DatabaseProtocols)}");

            if (!DatabaseProtocols.Any(x => x.EmrSystemId == new Guid("926f49b8-305d-11ea-978f-2e728ce88125")))
                DatabaseProtocols.SeedFromResource($"{nameof(DatabaseProtocols)}New");

            var restEndpoints = RestProtocols.ToList();
            if (restEndpoints.All(x => x.Url.Contains("https://palladiumkenya.github.io/dwapi/stuff")))
            {
                RestProtocols.RemoveRange(restEndpoints);
                SaveChanges();
            }

            RestProtocols.SeedDbSetIfEmpty($"{nameof(RestProtocols)}");
            Resources.SeedDbSetIfEmpty($"{nameof(Resources)}");
            SaveChanges();

            var managedEmrs = new List<Guid>
            {
                new Guid("a62216ee-0e85-11e8-ba89-0ed5f89f718b"),
                new Guid("a6221856-0e85-11e8-ba89-0ed5f89f718b"),
                new Guid("a6221857-0e85-11e8-ba89-0ed5f89f718b"),
                new Guid("926f49b8-305d-11ea-978f-2e728ce88125")
            };
           managedEmrs.ForEach(x =>
           {
               var sql = $"DELETE FROM {nameof(Extracts)} WHERE {nameof(Extract.EmrSystemId)} = '{x}'";
               Database.ExecuteSqlCommand(sql);
           });
            var extracts=
            Extracts.SeedFromResource($"{nameof(Extracts)}");
            SaveChanges();
        }
    }
}
