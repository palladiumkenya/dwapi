using System.Reflection;
using CsvHelper.Configuration;
using Dwapi.Domain;
using Dwapi.ExtractsManagement.Core.Model;
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
        // ----------------------------------------------------------------------------------------
        //public DbSet<ClientPatientBaselinesExtract> ClientPatientBaselinesExtract { get; set; }
        public DbSet<PatientExtract> PatientExtracts { get; set; }
        //public DbSet<ClientPatientLaboratoryExtract> ClientPatientLaboratoryExtract { get; set; }
        //public DbSet<ClientPatientStatusExtract> ClientPatientStatusExtract { get; set; }
        //public DbSet<ClientPatientVisitExtract> ClientPatientVisitExtract { get; set; }
        //public DbSet<ClientPatientArtExtract> ClientPatientArtExtract { get; set; }
        //public DbSet<ClientPatientPharmacyExtract> ClientPatientPharmacyExtract { get; set; }
        //public DbSet<TempPatientArtExtract> TempPatientArtExtract { get; set; }
        //public DbSet<TempPatientBaselinesExtract> TempPatientBaselinesExtract { get; set; }
        public DbSet<TempPatientExtract> TempPatientExtracts { get; set; }
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
        public DbSet<TempPatientExtractError> TempPatientExtractErrors { get; set; }
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
            DapperPlusManager.Entity<TempPatientExtract>().Key(x => x.Id).Table($"{nameof(TempPatientExtracts)}");
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