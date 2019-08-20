using System.Reflection;
using CsvHelper.Configuration;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Destination;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;
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
        public DbSet<PatientArtExtract> PatientArtExtracts { get; set; }
        public DbSet<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }
        public DbSet<PatientLaboratoryExtract> PatientLaboratoryExtracts { get; set; }
        public DbSet<PatientPharmacyExtract> PatientPharmacyExtracts { get; set; }
        public DbSet<PatientStatusExtract> PatientStatusExtracts { get; set; }
        public DbSet<PatientVisitExtract> PatientVisitExtracts { get; set; }
        public DbSet<PatientAdverseEventExtract> PatientAdverseEventExtracts { get; set; }

        public DbSet<TempPatientExtract> TempPatientExtracts { get; set; }
        public DbSet<TempPatientArtExtract> TempPatientArtExtracts { get; set; }
        public DbSet<TempPatientBaselinesExtract> TempPatientBaselinesExtracts { get; set; }
        public DbSet<TempPatientLaboratoryExtract> TempPatientLaboratoryExtracts { get; set; }
        public DbSet<TempPatientPharmacyExtract> TempPatientPharmacyExtracts { get; set; }
        public DbSet<TempPatientStatusExtract> TempPatientStatusExtracts { get; set; }
        public DbSet<TempPatientVisitExtract> TempPatientVisitExtracts { get; set; }
        public DbSet<TempPatientAdverseEventExtract> TempPatientAdverseEventExtracts { get; set; }

        public DbSet<ValidationError> ValidationError { get; set; }
        public DbSet<Validator> Validator { get; set; }
        public DbSet<ExtractHistory> ExtractHistory { get; set; }
        public DbSet<PsmartStage> PsmartStage { get; set; }
        public DbSet<TempPatientExtractError> TempPatientExtractError { get; set; }
        public DbSet<TempPatientExtractErrorSummary> TempPatientExtractErrorSummary { get; set; }
        public DbSet<TempPatientArtExtractError> TempPatientArtExtractError { get; set; }
        public DbSet<TempPatientArtExtractErrorSummary> TempPatientArtExtractErrorSummary { get; set; }
        public DbSet<TempPatientBaselinesExtractError> TempPatientBaselinesExtractError { get; set; }
        public DbSet<TempPatientBaselinesExtractErrorSummary> TempPatientBaselinesExtractErrorSummary { get; set; }
        public DbSet<TempPatientLaboratoryExtractError> TmPatientLaboratoryExtractError { get; set; }
        public DbSet<TempPatientLaboratoryExtractErrorSummary> TempPatientLaboratoryExtractErrorSummary { get; set; }
        public DbSet<TempPatientPharmacyExtractError> TempPatientPharmacyExtractError { get; set; }
        public DbSet<TempPatientPharmacyExtractErrorSummary> TempPatientPharmacyExtractErrorSummary { get; set; }
        public DbSet<TempPatientStatusExtractError> TempPatientStatusExtractError { get; set; }
        public DbSet<TempPatientStatusExtractErrorSummary> TempPatientStatusExtractErrorSummary { get; set; }
        public DbSet<TempPatientVisitExtractError> TempPatientVisitExtractError { get; set; }
        public DbSet<TempPatientVisitExtractErrorSummary> TempPatientVisitExtractErrorSummary { get; set; }
        public DbSet<TempPatientAdverseEventExtractError> TempPatientAdverseEventExtractErrors { get; set; }

        public DbSet<TempPatientAdverseEventExtractErrorSummary> TempPatientAdverseEventExtractErrorSummaries
        {
            get;
            set;
        }

        public DbSet<MasterPatientIndex> MasterPatientIndices { get; set; }
        public DbSet<TempMasterPatientIndex> TempMasterPatientIndices { get; set; }
        public DbSet<EmrMetric> EmrMetrics { get; set; }

        public DbSet<TempHTSClientExtract> TempHtsClientExtracts { get; set; }
        public DbSet<TempHTSClientPartnerExtract> TempHtsClientPartnerExtracts { get; set; }
        public DbSet<TempHTSClientLinkageExtract> TempHtsClientLinkageExtracts { get; set; }

        public DbSet<HTSClientExtract> HtsClientExtracts { get; set; }
        public DbSet<HTSClientPartnerExtract> HtsClientPartnerExtracts { get; set; }
        public DbSet<HTSClientLinkageExtract> HtsClientLinkageExtracts { get; set; }

        public DbSet<TempHTSClientExtractError> TempHtsClientExtractErrors { get; set; }
        public DbSet<TempHTSClientPartnerExtractError> TempHtsClientPartnerExtractErrors { get; set; }
        public DbSet<TempHTSClientLinkageExtractError> TempHtsClientLinkageExtractErrors { get; set; }

        public DbSet<HtsClients> HtsClientsExtracts { get; set; }
        public DbSet<HtsClientTests> HtsClientTestsExtracts { get; set; }
        public DbSet<HtsClientTracing> HtsClientTracingExtracts { get; set; }
        public DbSet<HtsPartnerTracing> HtsPartnerTracingExtracts { get; set; }
        public DbSet<HtsTestKits> HtsTestKitsExtracts { get; set; }
        public DbSet<HtsClientLinkage> HtsClientsLinkageExtracts { get; set; }
        public DbSet<HtsPartnerNotificationServices> HtsPartnerNotificationServicesExtracts { get; set; }

        public DbSet<TempHtsClients> TempHtsClientsExtracts { get; set; }
        public DbSet<TempHtsClientTests> TempHtsClientTestsExtracts { get; set; }
        public DbSet<TempHtsClientTracing> TempHtsClientTracingExtracts { get; set; }
        public DbSet<TempHtsPartnerTracing> TempHtsPartnerTracingExtracts { get; set; }
        public DbSet<TempHtsTestKits> TempHtsTestKitsExtracts { get; set; }
        public DbSet<TempHtsClientLinkage> TempHtsClientsLinkageExtracts { get; set; }
        public DbSet<TempHtsPartnerNotificationServices> TempHtsPartnerNotificationServicesExtracts { get; set; }


        

        public DbSet<TempHtsClientsError> TempHtsClientsExtractsErrors { get; set; }
        public DbSet<TempHtsClientTestsError> TempHtsClientTestsExtractsErrors { get; set; }
        public DbSet<TempHtsClientTracingError> TempHtsClientTracingExtractsErrors { get; set; }
        public DbSet<TempHtsPartnerTracingError> TempHtsPartnerTracingExtractsErrors { get; set; }
        public DbSet<TempHtsTestKitsError> TempHtsTestKitsExtractsErrors { get; set; }
        public DbSet<TempHtsClientLinkageError> TempHtsClientsLinkageExtractsErrors { get; set; }
        public DbSet<TempHtsPartnerNotificationServicesError> TempHtsPartnerNotificationServicesExtractsErrors { get; set; }

        public DbSet<TempHTSClientExtractErrorSummary> TempHtsClientExtractErrorSummaries { get; set; }
        public DbSet<TempHTSClientPartnerExtractErrorSummary> TempHtsClientPartnerExtractErrorSummaries { get; set; }
        public DbSet<TempHTSClientLinkageExtractErrorSummary> TempHtsClientLinkageExtractErrorSummaries { get; set; }

        public DbSet<TempHtsClientsErrorSummary> TempHtsClientsExtractsErrorSummaries { get; set; }
        public DbSet<TempHtsClientTestsErrorSummary> TempHtsClientTestsExtractsErrorSummaries { get; set; }
        public DbSet<TempHtsClientTracingErrorSummary> TempHtsClientTracingExtractsErrorSummaries { get; set; }
        public DbSet<TempHtsPartnerTracingErrorSummary> TempHtsPartnerTracingExtractsErrorSummaries { get; set; }
        public DbSet<TempHtsTestKitsErrorSummary> TempHtsTestKitsExtractsErrorSummaries { get; set; }
        public DbSet<TempHtsClientLinkageErrorSummary> TempHtsClientsLinkageExtractsErrorSummaries { get; set; }
        public DbSet<TempHtsPartnerNotificationServicesErrorSummary> TempHtsPartnerNotificationServicesExtractsErrorSummaries { get; set; }



        public ExtractsContext(DbContextOptions<ExtractsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientExtract>()
                .HasKey(f => new {f.SiteCode, f.PatientPK});

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientArtExtracts)
                .WithOne()
                .IsRequired()
                .HasForeignKey(f => new {f.SiteCode, f.PatientPK});

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientBaselinesExtracts)
                .WithOne()
                .IsRequired()
                .HasForeignKey(f => new {f.SiteCode, f.PatientPK});

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientLaboratoryExtracts)
                .WithOne()
                .IsRequired()
                .HasForeignKey(f => new {f.SiteCode, f.PatientPK});

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientPharmacyExtracts)
                .WithOne()
                .IsRequired()
                .HasForeignKey(f => new {f.SiteCode, f.PatientPK});

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientStatusExtracts)
                .WithOne()
                .IsRequired()
                .HasForeignKey(f => new {f.SiteCode, f.PatientPK});

            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientVisitExtracts)
                .WithOne()
                .IsRequired()
                .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
            modelBuilder.Entity<PatientExtract>()
                .HasMany(c => c.PatientAdverseEventExtracts)
                .WithOne()
                .IsRequired()
                .HasForeignKey(f => new {f.SiteCode, f.PatientPK});

//            modelBuilder.Entity<HTSClientExtract>()
//                .HasKey(f => new {f.SiteCode, f.PatientPk,f.EncounterId});

            DapperPlusManager.Entity<TempPatientExtract>().Key(x => x.Id).Table($"{nameof(TempPatientExtracts)}");
            DapperPlusManager.Entity<TempPatientArtExtract>().Key(x => x.Id).Table($"{nameof(TempPatientArtExtracts)}");
            DapperPlusManager.Entity<TempPatientBaselinesExtract>().Key(x => x.Id)
                .Table($"{nameof(TempPatientBaselinesExtracts)}");
            DapperPlusManager.Entity<TempPatientLaboratoryExtract>().Key(x => x.Id)
                .Table($"{nameof(TempPatientLaboratoryExtracts)}");
            DapperPlusManager.Entity<TempPatientPharmacyExtract>().Key(x => x.Id)
                .Table($"{nameof(TempPatientPharmacyExtracts)}");
            DapperPlusManager.Entity<TempPatientStatusExtract>().Key(x => x.Id)
                .Table($"{nameof(TempPatientStatusExtracts)}");
            DapperPlusManager.Entity<TempPatientVisitExtract>().Key(x => x.Id)
                .Table($"{nameof(TempPatientVisitExtracts)}");
            DapperPlusManager.Entity<TempPatientAdverseEventExtract>().Key(x => x.Id)
                .Table($"{nameof(TempPatientAdverseEventExtracts)}");
            DapperPlusManager.Entity<PatientArtExtract>().Key(x => x.Id).Table($"{nameof(PatientArtExtracts)}");
            DapperPlusManager.Entity<PatientBaselinesExtract>().Key(x => x.Id)
                .Table($"{nameof(PatientBaselinesExtracts)}");
            DapperPlusManager.Entity<PatientLaboratoryExtract>().Key(x => x.Id)
                .Table($"{nameof(PatientLaboratoryExtracts)}");
            DapperPlusManager.Entity<PatientPharmacyExtract>().Key(x => x.Id)
                .Table($"{nameof(PatientPharmacyExtracts)}");
            DapperPlusManager.Entity<PatientStatusExtract>().Key(x => x.Id).Table($"{nameof(PatientStatusExtracts)}");
            DapperPlusManager.Entity<PatientVisitExtract>().Key(x => x.Id).Table($"{nameof(PatientVisitExtracts)}");
            DapperPlusManager.Entity<PatientAdverseEventExtract>().Key(x => x.Id)
                .Table($"{nameof(PatientAdverseEventExtracts)}");
            DapperPlusManager.Entity<PatientExtract>().Key(x => x.Id).Table($"{nameof(PatientExtracts)}");
            DapperPlusManager.Entity<MasterPatientIndex>().Key(x => x.Id).Table($"{nameof(MasterPatientIndices)}");
            DapperPlusManager.Entity<TempMasterPatientIndex>().Key(x => x.Id)
                .Table($"{nameof(TempMasterPatientIndices)}");
            DapperPlusManager.Entity<EmrMetric>().Key(x => x.Id).Table($"{nameof(EmrMetric)}");

            DapperPlusManager.Entity<HTSClientExtract>().Key(x => x.Id).Table($"{nameof(HtsClientExtracts)}");
            DapperPlusManager.Entity<HTSClientLinkageExtract>().Key(x => x.Id).Table($"{nameof(HtsClientLinkageExtracts)}");
            DapperPlusManager.Entity<HTSClientPartnerExtract>().Key(x => x.Id).Table($"{nameof(HtsClientPartnerExtracts)}");

            DapperPlusManager.Entity<HtsClients>().Key(x => x.Id).Table($"{nameof(HtsClientsExtracts)}");
            DapperPlusManager.Entity<HtsClientTests>().Key(x => x.Id).Table($"{nameof(HtsClientTestsExtracts)}");
            DapperPlusManager.Entity<HtsClientTracing>().Key(x => x.Id).Table($"{nameof(HtsClientTracingExtracts)}");
            DapperPlusManager.Entity<HtsPartnerNotificationServices>().Key(x => x.Id).Table($"{nameof(HtsPartnerNotificationServicesExtracts)}");
            DapperPlusManager.Entity<HtsClientLinkage>().Key(x => x.Id).Table($"{nameof(HtsClientsLinkageExtracts)}");
            DapperPlusManager.Entity<HtsTestKits>().Key(x => x.Id).Table($"{nameof(HtsTestKitsExtracts)}");
            DapperPlusManager.Entity<HtsPartnerTracing>().Key(x => x.Id).Table($"{nameof(HtsPartnerTracingExtracts)}");

            DapperPlusManager.Entity<TempHTSClientExtract>().Key(x => x.Id).Table($"{nameof(TempHtsClientExtracts)}");
            DapperPlusManager.Entity<TempHTSClientLinkageExtract>().Key(x => x.Id).Table($"{nameof(TempHtsClientLinkageExtracts)}");
            DapperPlusManager.Entity<TempHTSClientPartnerExtract>().Key(x => x.Id).Table($"{nameof(TempHtsClientPartnerExtracts)}");

            DapperPlusManager.Entity<TempHtsClients>().Key(x => x.Id).Table($"{nameof(TempHtsClientsExtracts)}");
            DapperPlusManager.Entity<TempHtsClientTests>().Key(x => x.Id).Table($"{nameof(TempHtsClientTestsExtracts)}");
            DapperPlusManager.Entity<TempHtsClientTracing>().Key(x => x.Id).Table($"{nameof(TempHtsClientTracingExtracts)}");
            DapperPlusManager.Entity<TempHtsPartnerNotificationServices>().Key(x => x.Id).Table($"{nameof(TempHtsPartnerNotificationServicesExtracts)}");
            DapperPlusManager.Entity<TempHtsClientLinkage>().Key(x => x.Id).Table($"{nameof(TempHtsClientsLinkageExtracts)}");
            DapperPlusManager.Entity<TempHtsTestKits>().Key(x => x.Id).Table($"{nameof(TempHtsTestKitsExtracts)}");
            DapperPlusManager.Entity<TempHtsPartnerTracing>().Key(x => x.Id).Table($"{nameof(TempHtsPartnerTracingExtracts)}");
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
            Validator.RemoveRange(Validator);
            Validator.SeedFromResource($"{nameof(Validator)}");
            SaveChanges();
        }
    }
}
