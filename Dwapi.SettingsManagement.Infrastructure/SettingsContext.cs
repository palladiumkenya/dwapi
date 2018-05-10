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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CentralRegistry>().ToTable(nameof(CentralRegistry).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<EmrSystem>().ToTable(nameof(EmrSystem).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<DatabaseProtocol>().ToTable(nameof(DatabaseProtocol).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<RestProtocol>().ToTable(nameof(RestProtocol).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<Docket>().ToTable(nameof(Docket).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<Extract>().ToTable(nameof(Extract).ToLower())
                .HasKey(e => e.Id);
        }

        public DbSet<CentralRegistry> CentralRegistries { get; set; }

        public DbSet<EmrSystem> EmrSystems { get; set; }
        public DbSet<DatabaseProtocol> DatabaseProtocols { get; set; }
        public DbSet<RestProtocol> RestProtocols { get; set; }

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

            CentralRegistries.SeedDbSetIfEmpty($"{nameof(CentralRegistries)}");
            EmrSystems.SeedDbSetIfEmpty($"{nameof(EmrSystems)}");
            DatabaseProtocols.SeedDbSetIfEmpty($"{nameof(DatabaseProtocols)}");
            Dockets.SeedDbSetIfEmpty($"{nameof(Dockets)}");
            Extracts.SeedDbSetIfEmpty($"{nameof(Extracts)}");

            SaveChanges();
        }


    }
}