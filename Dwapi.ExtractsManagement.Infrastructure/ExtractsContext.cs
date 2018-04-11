using System.Reflection;
using CsvHelper.Configuration;
using Dwapi.Domain;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.SharedKernel.Infrastructure;
using EFCore.Seeder.Configuration;
using EFCore.Seeder.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.ExtractsManagement.Infrastructure
{
    public class ExtractsContext : BaseContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<PsmartStage> PsmartStages { get; set; }
        public DbSet<ExtractHistory> ExtractHistories { get; set; }
        public DbSet<ExtractSetting> ExtractSettings { get; set; }
        public DbSet<ClientPatientBaselinesExtract> ClientPatientBaselinesExtracts { get; set; }
        public DbSet<ClientPatientExtract> ClientPatientExtracts { get; set; }
        public DbSet<ClientPatientLaboratoryExtract> ClientPatientLaboratoryExtracts { get; set; }
        public DbSet<ClientPatientStatusExtract> ClientPatientStatusExtracts { get; set; }
        public DbSet<ClientPatientVisitExtract> ClientPatientVisitExtracts { get; set; }
        public DbSet<ClientPatientArtExtract> ClientPatientArtExtracts { get; set; }
        public DbSet<ClientPatientPharmacyExtract> ClientPatientPharmacyExtracts { get; set; }
        public DbSet<TempPatientArtExtract> TempPatientArtExtracts { get; set; }
        public DbSet<TempPatientBaselinesExtract> TempPatientBaselinesExtracts { get; set; }
        public DbSet<TempPatientExtract> TempPatientExtracts { get; set; }
        public DbSet<TempPatientLaboratoryExtract> TempPatientLaboratoryExtracts { get; set; }
        public DbSet<TempPatientPharmacyExtract> TempPatientPharmacyExtracts { get; set; }
        public DbSet<TempPatientStatusExtract> TempPatientStatusExtracts { get; set; }
        public DbSet<TempPatientVisitExtract> TempPatientVisitExtracts { get; set; }

        //public DbSet<TempPatientArtExtractError> TempPatientArtExtractErrors { get; set; }
        //public DbSet<TempPatientArtExtractErrorSummary> TempPatientArtExtractErrorSummaries { get; set; }
        //public DbSet<TempPatientBaselinesExtractError> TempPatientBaselinesExtractErrors { get; set; }
        //public DbSet<TempPatientBaselinesExtractErrorSummary> TempPatientBaselinesExtractErrorSummaries { get; set; }
        //public DbSet<TempPatientExtractError> TempPatientExtractErrors { get; set; }
        //public DbSet<TempPatientExtractErrorSummary> TempPatientExtractErrorSummaries { get; set; }
        //public DbSet<TempPatientLaboratoryExtractError> TempPatientLaboratoryExtractErrors { get; set; }
        //public DbSet<TempPatientLaboratoryExtractErrorSummary> TempPatientLaboratoryExtractErrorSummaries { get; set; }
        //public DbSet<TempPatientPharmacyExtractError> TempPatientPharmacyExtractErrors { get; set; }
        //public DbSet<TempPatientPharmacyExtractErrorSummary> TempPatientPharmacyExtractErrorSummaries { get; set; }
        //public DbSet<TempPatientStatusExtractError> TempPatientStatusExtractErrors { get; set; }
        //public DbSet<TempPatientStatusExtractErrorSummary> TempPatientStatusExtractErrorSummaries { get; set; }
        //public DbSet<TempPatientVisitExtractError> TempPatientVisitExtractErrors { get; set; }
        //public DbSet<TempPatientVisitExtractErrorSummary> TempPatientVisitExtractErrorSummaries { get; set; }
        
        /// <summary>
        /// //  Reference Table Only
        /// </summary>
        public DbSet<Extract> Extracts { get; set; }

        public ExtractsContext(DbContextOptions<ExtractsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
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