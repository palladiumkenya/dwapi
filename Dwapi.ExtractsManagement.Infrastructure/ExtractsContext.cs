using System.Reflection;
using CsvHelper.Configuration;
using Dwapi.ExtractsManagement.Core.Model.Stage.Psmart;
using Dwapi.SharedKernel.Infrastructure;
using EFCore.Seeder.Configuration;
using EFCore.Seeder.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure
{
    public class ExtractsContext : BaseContext
    {
        public DbSet<PsmartStage> PsmartStages { get; set; }

        public ExtractsContext(DbContextOptions<ExtractsContext> options) : base(options)
        {
        }

        public override void EnsureSeeded()
        {
            /*
            var csvConfig = new CsvConfiguration
            {
                Delimiter = "|",
                SkipEmptyRecords = true,
                TrimFields = true,
                TrimHeaders = true,
                WillThrowOnMissingField = false
            };

            SeederConfiguration.ResetConfiguration(csvConfig, null, typeof(ExtractsContext).GetTypeInfo().Assembly);

            PsmartStages.SeedDbSetIfEmpty($"{nameof(PsmartStages)}");
          

            SaveChanges();
            */
        }
    }
}