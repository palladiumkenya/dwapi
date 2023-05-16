using Dwapi.ExtractsManagement.Core.Loader.Prep;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.SharedKernel.Infrastructure;
using Dwapi.UploadManagement.Core.Model;
using Dwapi.UploadManagement.Core.Model.Cbs;
using Dwapi.UploadManagement.Core.Model.Crs;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Dwapi.UploadManagement.Core.Model.Hts;
using Dwapi.UploadManagement.Core.Model.Mgs;
using Dwapi.UploadManagement.Core.Model.Mnch;
using Dwapi.UploadManagement.Core.Model.Mts;
using Dwapi.UploadManagement.Core.Model.Prep;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.UploadManagement.Infrastructure.Data
{
    public class UploadContext : BaseContext
    {
        public virtual DbSet<ClientRegistryExtractView> ClientClientRegistryExtracts { get; set; }
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
        public virtual DbSet<HtsEligibilityExtractView> HtsEligibilityExtract { get; set; }

        public virtual DbSet<MetricMigrationExtractView> MetricMigrationExtracts { get; set; }

        public virtual DbSet<EmrMetricView> EmrMetrics { get; set; }
        public virtual DbSet<AppMetricView> AppMetrics { get; set; }

        public virtual DbSet<DiffLogView> DiffLogs { get; set; }
        public virtual DbSet<IndicatorExtractView> IndicatorExtracts { get; set; }

        public virtual DbSet<AllergiesChronicIllnessExtractView> ClientAllergiesChronicIllnessExtracts { get; set; }
        public virtual DbSet<IptExtractView> ClientIptExtracts { get; set; }
        public virtual DbSet<DepressionScreeningExtractView> ClientDepressionScreeningExtracts { get; set; }
        public virtual DbSet<ContactListingExtractView> ClientContactListingExtracts { get; set; }
        public virtual DbSet<GbvScreeningExtractView> ClientGbvScreeningExtracts { get; set; }
        public virtual DbSet<EnhancedAdherenceCounsellingExtractView> ClientEnhancedAdherenceCounsellingExtracts { get; set; }
        public virtual DbSet<DrugAlcoholScreeningExtractView> ClientDrugAlcoholScreeningExtracts { get; set; }
        public virtual DbSet<OvcExtractView> ClientOvcExtracts { get; set; }
        public virtual DbSet<OtzExtractView> ClientOtzExtracts { get; set; }

        public virtual DbSet<CovidExtractView> ClientCovidExtracts { get; set; }
        public virtual DbSet<DefaulterTracingExtractView> ClientDefaulterTracingExtracts { get; set; }
        public virtual DbSet<CervicalCancerScreeningExtractView> ClientCervicalCancerScreeningExtracts { get; set; }

        public virtual DbSet<PatientMnchExtractView> ClientPatientMnchExtracts { get; set; }
        public virtual DbSet<MnchEnrolmentExtractView> ClientMnchEnrolmentExtracts { get; set; }
        public virtual DbSet<MnchArtExtractView> ClientMnchArtExtracts { get; set; }
        public virtual DbSet<AncVisitExtractView> ClientAncVisitExtracts { get; set; }
        public virtual DbSet<MatVisitExtractView> ClientMatVisitExtracts { get; set; }
        public virtual DbSet<PncVisitExtractView> ClientPncVisitExtracts { get; set; }
        public virtual DbSet<MotherBabyPairExtractView> ClientMotherBabyPairExtracts { get; set; }
        public virtual DbSet<CwcEnrolmentExtractView> ClientCwcEnrolmentExtracts { get; set; }
        public virtual DbSet<CwcVisitExtractView> ClientCwcVisitExtracts { get; set; }
        public virtual DbSet<HeiExtractView> ClientHeiExtracts { get; set; }
        public virtual DbSet<MnchLabExtractView> ClientMnchLabExtracts { get; set; }
        public virtual DbSet<TransportLogView> TransportLogs { get; set; }


        public virtual DbSet<PatientPrepExtractView> ClientPatientPrepExtracts { get; set; }
        public virtual DbSet<PrepAdverseEventExtractView> ClientPrepAdverseEventExtracts   { get; set; }
        public virtual DbSet<PrepBehaviourRiskExtractView> ClientPrepBehaviourRiskExtracts  { get; set; }
        public virtual DbSet<PrepCareTerminationExtractView> ClientPrepCareTerminationExtracts   { get; set; }
        public virtual DbSet<PrepLabExtractView> ClientPrepLabExtracts   { get; set; }
        public virtual DbSet<PrepPharmacyExtractView> ClientPrepPharmacyExtracts   { get; set; }
        public virtual DbSet<PrepVisitExtractView> ClientPrepVisitExtracts   { get; set; }
        
        public virtual DbSet<ClientRegistryExtractView> ClientRegistryExtracts   { get; set; }

        public UploadContext(DbContextOptions<UploadContext> options) : base(options)
        {
            this.Database.SetCommandTimeout(0);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<ClientRegistryExtractView>()
                .HasKey(f => new {f.SiteCode, f.PatientPK});
            
            modelBuilder.Entity<PatientExtractView>()
                .HasKey(f => new {f.SiteCode, f.PatientPK});

            modelBuilder.Entity<HtsClientsExtractView>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<PatientMnchExtractView>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<PatientPrepExtractView>()
                .HasKey(f => f.Id);
        }
    }
}
