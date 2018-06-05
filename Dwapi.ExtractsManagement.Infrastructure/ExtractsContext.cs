using System.Reflection;
using CsvHelper.Configuration;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Infrastructure;
using EFCore.Seeder.Configuration;
using EFCore.Seeder.Extensions;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;

namespace Dwapi.ExtractsManagement.Infrastructure
{
    public class ExtractsContext : BaseContext
    {
        public DbSet<PatientExtract> PatientExtracts { get; set; }
        public DbSet<TempPatientExtract> TempPatientExtracts { get; set; }
        public DbSet<ValidationError> ValidationError { get; set; }
        public DbSet<Validator> Validator { get; set; }
        public DbSet<ExtractHistory> ExtractHistory { get; set; }
        public DbSet<PsmartStage> PsmartStage { get; set; }
        public DbSet<TempPatientExtractError> TempPatientExtractError { get; set; }
        public DbSet<TempPatientExtractErrorSummary> TempPatientExtractErrorSummary { get; set; }

        public DbSet<MasterPatientIndex> MasterPatientIndices { get; set; }
        public DbSet<TempMasterPatientIndex> TempMasterPatientIndices { get; set; }

        public ExtractsContext(DbContextOptions<ExtractsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DapperPlusManager.Entity<TempPatientExtract>().Key(x => x.Id).Table($"{nameof(TempPatientExtracts)}");
            DapperPlusManager.Entity<PatientExtract>().Key(x => x.Id).Table($"{nameof(PatientExtracts)}");
            DapperPlusManager.Entity<MasterPatientIndex>().Key(x => x.Id).Table("MasterPatientIndices");
            DapperPlusManager.Entity<TempMasterPatientIndex>().Key(x => x.Id).Table("TempMasterPatientIndices");
        }

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

            SeederConfiguration.ResetConfiguration(csvConfig, null, typeof(ExtractsContext).GetTypeInfo().Assembly);

            Validator.SeedDbSetIfEmpty($"{nameof(Validator)}");
            SaveChanges();
        }
    }
}