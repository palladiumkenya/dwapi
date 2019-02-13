using System.Linq;
using System.Reflection;
using CsvHelper.Configuration;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure;
using EFCore.Seeder.Configuration;
using EFCore.Seeder.Extensions;
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

            Dockets.SeedDbSetIfEmpty($"{nameof(Dockets)}");
            CentralRegistries.SeedDbSetIfEmpty($"{nameof(CentralRegistries)}");
            EmrSystems.SeedDbSetIfEmpty($"{nameof(EmrSystems)}");
            DatabaseProtocols.SeedDbSetIfEmpty($"{nameof(DatabaseProtocols)}");
            Resources.SeedDbSetIfEmpty($"{nameof(Resources)}");
            var ex = Extracts.Where(e => e.EmrSystemId.ToString() == "a62216ee-0e85-11e8-ba89-0ed5f89f718b" ||
            e.EmrSystemId.ToString() == "a6221856-0e85-11e8-ba89-0ed5f89f718b" ||
            e.EmrSystemId.ToString() == "a6221857-0e85-11e8-ba89-0ed5f89f718b"

            );
            Extracts.RemoveRange(ex);
            Extracts.SeedFromResource($"{nameof(Extracts)}");
            SaveChanges();
        }
    }
}