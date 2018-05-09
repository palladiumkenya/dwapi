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
        // ----------------------------------------------------------------------------------------
        //public DbSet<ClientPatientBaselinesExtract> ClientPatientBaselinesExtract { get; set; }
        //public DbSet<ClientPatientExtract> ClientPatientExtract { get; set; }
        //public DbSet<ClientPatientLaboratoryExtract> ClientPatientLaboratoryExtract { get; set; }
        //public DbSet<ClientPatientStatusExtract> ClientPatientStatusExtract { get; set; }
        //public DbSet<ClientPatientVisitExtract> ClientPatientVisitExtract { get; set; }
        //public DbSet<ClientPatientArtExtract> ClientPatientArtExtract { get; set; }
        //public DbSet<ClientPatientPharmacyExtract> ClientPatientPharmacyExtract { get; set; }
        //public DbSet<TempPatientArtExtract> TempPatientArtExtract { get; set; }
        //public DbSet<TempPatientBaselinesExtract> TempPatientBaselinesExtract { get; set; }
        //public DbSet<TempPatientExtract> TempPatientExtract { get; set; }
        //public DbSet<TempPatientLaboratoryExtract> TempPatientLaboratoryExtract { get; set; }
        //public DbSet<TempPatientPharmacyExtract> TempPatientPharmacyExtract { get; set; }
        //public DbSet<TempPatientStatusExtract> TempPatientStatusExtract { get; set; }
        //public DbSet<TempPatientVisitExtract> TempPatientVisitExtract { get; set; }
        //public DbSet<ValidationError> ValidationError { get; set; }
        //public DbSet<Validator> Validator { get; set; }

        // ------------------------------------------------------------------------------------

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

        public ExtractsContext(DbContextOptions<ExtractsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable(nameof(Project).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<PsmartStage>().ToTable(nameof(PsmartStage).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<ExtractHistory>().ToTable(nameof(ExtractHistory).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<ExtractSetting>().ToTable(nameof(ExtractSetting).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<Extract>().ToTable(nameof(Extract).ToLower())
                .HasKey(e => e.Id);

            //-------------DWH EXTRACTS

            modelBuilder.Entity<PatientExtract>().ToTable(nameof(PatientExtract).ToLower())
                .HasKey(e => new { e.PatientPK, e.SiteCode });

            modelBuilder.Entity<PatientArtExtract>().ToTable(nameof(PatientArtExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<PatientBaselinesExtract>().ToTable(nameof(PatientBaselinesExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<PatientLaboratoryExtract>().ToTable(nameof(PatientLaboratoryExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<PatientPharmacyExtract>().ToTable(nameof(PatientPharmacyExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<PatientStatusExtract>().ToTable(nameof(PatientStatusExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<PatientVisitExtract>().ToTable(nameof(PatientVisitExtract).ToLower())
                .HasKey(e => e.Id);


            // ----------------TEMP----

            modelBuilder.Entity<TempPatientExtract>().ToTable(nameof(TempPatientExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<TempPatientArtExtract>().ToTable(nameof(TempPatientArtExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<TempPatientBaselinesExtract>().ToTable(nameof(TempPatientBaselinesExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<TempPatientPharmacyExtract>().ToTable(nameof(TempPatientPharmacyExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<TempPatientLaboratoryExtract>().ToTable(nameof(TempPatientLaboratoryExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<TempPatientStatusExtract>().ToTable(nameof(TempPatientStatusExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<TempPatientVisitExtract>().ToTable(nameof(TempPatientVisitExtract).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<Validator>().ToTable(nameof(Validator).ToLower())
                .HasKey(e => e.Id);

            modelBuilder.Entity<ValidationError>().ToTable(nameof(ValidationError).ToLower())
                .HasKey(e => e.Id);
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