using Dwapi.SharedKernel.Infrastructure;
using Dwapi.UploadManagement.Core.Model;
using Dwapi.UploadManagement.Core.Model.Cbs;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Dwapi.UploadManagement.Core.Model.Hts;
using Dwapi.UploadManagement.Core.Model.Mgs;
using Dwapi.UploadManagement.Core.Model.Mts;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.UploadManagement.Infrastructure.Data
{
    public class UploadContext : BaseContext
    {
        public virtual DbSet<MasterPatientIndexView> ClientMasterPatientIndices { get; set; }
        public virtual DbSet<PatientExtractView> ClientPatientExtracts { get; set; }
        public virtual DbSet<PatientArtExtractView> ClientPatientArtExtracts { get; set; }
        public virtual DbSet<PatientBaselinesExtractView> ClientPatientBaselinesExtracts { get; set; }
        public virtual DbSet<PatientLaboratoryExtractView> ClientPatientLaboratoryExtracts { get; set; }
        public virtual DbSet<PatientPharmacyExtractView> ClientPatientPharmacyExtracts { get; set; }
        public virtual DbSet<PatientStatusExtractView> ClientPatientStatusExtracts { get; set; }
        public virtual DbSet<PatientVisitExtractView> ClientPatientVisitExtracts { get; set; }
        public virtual DbSet<PatientAdverseEventView> ClientPatientAdverseEventExtracts { get; set; }

        public virtual DbSet<HtsClientsExtractView> ClientExtracts { get; set; }
        public virtual DbSet<HtsClientsLinkageExtractView> ClientLinkageExtracts { get; set; }
        public virtual DbSet<HtsPartnerTracingExtractView> PartnerTracingExtracts { get; set; }
        public virtual DbSet<HtsTestKitsExtractView> TestKitsExtracts { get; set; }
        public virtual DbSet<HtsClientTestsExtractView> ClientTestsExtracts { get; set; }
        public virtual DbSet<HtsPartnerNotificationServicesExtractView> PartnerNotificationServicesExtracts { get; set; }
        public virtual DbSet<HtsClientTracingExtractView> ClientTracingExtracts { get; set; }

        public virtual DbSet<MetricMigrationExtractView> MetricMigrationExtracts { get; set; }

        public virtual DbSet<EmrMetricView> EmrMetrics { get; set; }
        public virtual DbSet<AppMetricView> AppMetrics { get; set; }

        public virtual DbSet<DiffLogView> DiffLogs { get; set; }
        public virtual DbSet<IndicatorExtractView> IndicatorExtracts { get; set; }

        public UploadContext(DbContextOptions<UploadContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PatientExtractView>()
                .HasKey(f => new {f.SiteCode, f.PatientPK});

            modelBuilder.Entity<HtsClientsExtractView>()
                .HasKey(f => f.Id);
        }
    }
}
