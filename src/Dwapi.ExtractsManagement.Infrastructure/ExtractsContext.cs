using Dwapi.Contracts.Crs;
using Dwapi.ExtractsManagement.Core.Model;
using Dwapi.ExtractsManagement.Core.Model.Destination;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Prep;
using Dwapi.ExtractsManagement.Core.Model.Diff;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Crs;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Mts;
using Dwapi.ExtractsManagement.Core.Model.Source.Prep;
using Dwapi.SharedKernel.Infrastructure;
using LiveSeeder.Core;
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
       public DbSet<HtsEligibilityExtract> HtsEligibilityExtracts { get; set; }




       public DbSet<TempHtsClients> TempHtsClientsExtracts { get; set; }
       public DbSet<TempHtsClientTests> TempHtsClientTestsExtracts { get; set; }
       public DbSet<TempHtsClientTracing> TempHtsClientTracingExtracts { get; set; }
       public DbSet<TempHtsPartnerTracing> TempHtsPartnerTracingExtracts { get; set; }
       public DbSet<TempHtsTestKits> TempHtsTestKitsExtracts { get; set; }
       public DbSet<TempHtsClientLinkage> TempHtsClientsLinkageExtracts { get; set; }
       public DbSet<TempHtsPartnerNotificationServices> TempHtsPartnerNotificationServicesExtracts { get; set; }
       public DbSet<TempHtsEligibilityExtract> TempHtsEligibilityExtracts { get; set; }










       public DbSet<TempHtsClientsError> TempHtsClientsExtractsErrors { get; set; }
       public DbSet<TempHtsClientTestsError> TempHtsClientTestsExtractsErrors { get; set; }
       public DbSet<TempHtsClientTracingError> TempHtsClientTracingExtractsErrors { get; set; }
       public DbSet<TempHtsPartnerTracingError> TempHtsPartnerTracingExtractsErrors { get; set; }
       public DbSet<TempHtsTestKitsError> TempHtsTestKitsExtractsErrors { get; set; }
       public DbSet<TempHtsClientLinkageError> TempHtsClientsLinkageExtractsErrors { get; set; }
       public DbSet<TempHtsPartnerNotificationServicesError> TempHtsPartnerNotificationServicesExtractsErrors { get; set; }
       public DbSet<TempHtsEligibilityExtractError> TempHtsEligibilityExtractErrors { get; set; }
      
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
       public DbSet<TempHtsEligibilityExtractErrorSummary> TempHtsEligibilityExtractErrorSummaries { get; set; }


       public DbSet<TempMetricMigrationExtract> TempMetricMigrationExtracts { get; set; }
       public DbSet<MetricMigrationExtract> MetricMigrationExtracts { get; set; }
       public DbSet<TempMetricMigrationExtractError>  TempMetricMigrationExtractErrors { get; set; }
       public DbSet<TempMetricMigrationExtractErrorSummary> TempMetricMigrationExtractErrorSummaries { get; set; }


       public DbSet<DiffLog> DiffLogs { get; set; }
       public DbSet<TempIndicatorExtract> TempIndicatorExtracts { get; set; }
       public DbSet<IndicatorExtract> IndicatorExtracts { get; set; }






       public DbSet<TempAllergiesChronicIllnessExtract> TempAllergiesChronicIllnessExtracts { get; set; }
       public DbSet<TempIptExtract> TempIptExtracts { get; set; }
       public DbSet<TempDepressionScreeningExtract> TempDepressionScreeningExtracts { get; set; }
       public DbSet<TempContactListingExtract> TempContactListingExtracts { get; set; }
       public DbSet<TempGbvScreeningExtract> TempGbvScreeningExtracts { get; set; }
       public DbSet<TempEnhancedAdherenceCounsellingExtract> TempEnhancedAdherenceCounsellingExtracts { get; set; }
       public DbSet<TempDrugAlcoholScreeningExtract> TempDrugAlcoholScreeningExtracts { get; set; }
       public DbSet<TempOvcExtract> TempOvcExtracts { get; set; }
       public DbSet<TempOtzExtract> TempOtzExtracts { get; set; }
       public DbSet<TempCovidExtract> TempCovidExtracts { get; set; }
       public DbSet<TempDefaulterTracingExtract> TempDefaulterTracingExtracts { get; set; }
       public DbSet<TempCancerScreeningExtract> TempCancerScreeningExtracts { get; set; }
       public DbSet<TempIITRiskScoresExtract> TempIITRiskScoresExtracts { get; set; }
       public DbSet<TempArtFastTrackExtract> TempArtFastTrackExtracts { get; set; }




       public DbSet<AllergiesChronicIllnessExtract> AllergiesChronicIllnessExtracts { get; set; }
       public DbSet<IptExtract> IptExtracts { get; set; }
       public DbSet<DepressionScreeningExtract> DepressionScreeningExtracts { get; set; }
       public DbSet<ContactListingExtract> ContactListingExtracts { get; set; }
       public DbSet<GbvScreeningExtract> GbvScreeningExtracts { get; set; }
       public DbSet<EnhancedAdherenceCounsellingExtract> EnhancedAdherenceCounsellingExtracts { get; set; }
       public DbSet<DrugAlcoholScreeningExtract> DrugAlcoholScreeningExtracts { get; set; }
       public DbSet<OvcExtract> OvcExtracts { get; set; }
       public DbSet<OtzExtract> OtzExtracts { get; set; }
       public DbSet<CovidExtract> CovidExtracts { get; set; }
       public DbSet<DefaulterTracingExtract> DefaulterTracingExtracts { get; set; }
       public DbSet<CancerScreeningExtract> CancerScreeningExtracts { get; set; }
       public DbSet<IITRiskScoresExtract> IITRiskScoresExtracts { get; set; }
       public DbSet<ArtFastTrackExtract> ArtFastTrackExtracts { get; set; }






       public DbSet<TempAllergiesChronicIllnessExtractError> TempAllergiesChronicIllnessExtractError { get; set; }
       public DbSet<TempAllergiesChronicIllnessExtractErrorSummary> TempAllergiesChronicIllnessExtractErrorSummary { get; set; }
       public DbSet<TempIptExtractError> TempIptExtractError { get; set; }
       public DbSet<TempIptExtractErrorSummary> TempIptExtractErrorSummary { get; set; }
       public DbSet<TempDepressionScreeningExtractError> TempDepressionScreeningExtractError { get; set; }
       public DbSet<TempDepressionScreeningExtractErrorSummary> TempDepressionScreeningExtractErrorSummary { get; set; }
       public DbSet<TempContactListingExtractError> TempContactListingExtractError { get; set; }
        public DbSet<TempContactListingExtractErrorSummary> TempContactListingExtractErrorSummary { get; set; }
       public DbSet<TempGbvScreeningExtractError> TempGbvScreeningExtractError { get; set; }
       public DbSet<TempGbvScreeningExtractErrorSummary> TempGbvScreeningExtractErrorSummary { get; set; }
       public DbSet<TempEnhancedAdherenceCounsellingExtractError> TempEnhancedAdherenceCounsellingExtractError { get; set; }
       public DbSet<TempEnhancedAdherenceCounsellingExtractErrorSummary> TempEnhancedAdherenceCounsellingExtractErrorSummary { get; set; }
       public DbSet<TempDrugAlcoholScreeningExtractError> TempDrugAlcoholScreeningExtractError { get; set; }
       public DbSet<TempDrugAlcoholScreeningExtractErrorSummary> TempDrugAlcoholScreeningExtractErrorSummary { get; set; }
       public DbSet<TempOvcExtractError> TempOvcExtractError{ get; set; }
       public DbSet<TempOvcExtractErrorSummary> TempOvcExtractErrorSummaries{ get; set; }
       public DbSet<TempOtzExtractError> TempOtzExtractError { get; set; }
       public DbSet<TempOtzExtractErrorSummary> TempOtzExtractErrorSummary { get; set; }
       public DbSet<TempCancerScreeningExtractError> TempCancerScreeningExtractError { get; set; }
       public DbSet<TempCancerScreeningExtractErrorSummary> TempCancerScreeningExtractErrorSummary { get; set; }


       public DbSet<TempCovidExtractError> TempCovidExtractError { get; set; }
       public DbSet<TempCovidExtractErrorSummary> TempCovidExtractErrorSummary { get; set; }
       public DbSet<TempDefaulterTracingExtractError> TempDefaulterTracingExtractError { get; set; }
       public DbSet<TempDefaulterTracingExtractErrorSummary> TempDefaulterTracingExtractErrorSummary { get; set; }
       public DbSet<TempIITRiskScoresExtractError> TempIITRiskScoresExtractError { get; set; }
       public DbSet<TempIITRiskScoresExtractErrorSummary> TempIITRiskScoresExtractErrorSummary { get; set; }
       public DbSet<TempArtFastTrackExtractError> TempArtFastTrackExtractError { get; set; }
       public DbSet<TempArtFastTrackExtractErrorSummary> TempArtFastTrackExtractErrorSummary { get; set; }


       #region Mnch
       public virtual DbSet<TempPatientMnchExtract> TempPatientMnchExtracts { get; set; }
       public virtual DbSet<TempMnchEnrolmentExtract> TempMnchEnrolmentExtracts { get; set; }
       public virtual DbSet<TempMnchArtExtract> TempMnchArtExtracts { get; set; }
       public virtual DbSet<TempAncVisitExtract> TempAncVisitExtracts { get; set; }
       public virtual DbSet<TempMatVisitExtract> TempMatVisitExtracts { get; set; }
       public virtual DbSet<TempPncVisitExtract> TempPncVisitExtracts { get; set; }
       public virtual DbSet<TempMotherBabyPairExtract> TempMotherBabyPairExtracts { get; set; }
       public virtual DbSet<TempCwcEnrolmentExtract> TempCwcEnrolmentExtracts { get; set; }
       public virtual DbSet<TempCwcVisitExtract> TempCwcVisitExtracts { get; set; }
       public virtual DbSet<TempHeiExtract> TempHeiExtracts { get; set; }
       public virtual DbSet<TempMnchLabExtract> TempMnchLabExtracts { get; set; }
       public virtual DbSet<TempMnchImmunizationExtract> TempMnchImmunizationExtracts { get; set; }




       public virtual DbSet<PatientMnchExtract> PatientMnchExtracts { get; set; }
       public virtual DbSet<MnchEnrolmentExtract> MnchEnrolmentExtracts { get; set; }
       public virtual DbSet<MnchArtExtract> MnchArtExtracts { get; set; }
       public virtual DbSet<AncVisitExtract> AncVisitExtracts { get; set; }
       public virtual DbSet<MatVisitExtract> MatVisitExtracts { get; set; }
       public virtual DbSet<PncVisitExtract> PncVisitExtracts { get; set; }
       public virtual DbSet<MotherBabyPairExtract> MotherBabyPairExtracts { get; set; }
       public virtual DbSet<CwcEnrolmentExtract> CwcEnrolmentExtracts { get; set; }
       public virtual DbSet<CwcVisitExtract> CwcVisitExtracts { get; set; }
       public virtual DbSet<HeiExtract> HeiExtracts { get; set; }
       public virtual DbSet<MnchLabExtract> MnchLabExtracts { get; set; }
       public virtual DbSet<MnchImmunizationExtract> MnchImmunizationExtracts { get; set; }




       public virtual DbSet<TempPatientMnchExtractError> TempPatientMnchExtractError { get; set; }
       public virtual DbSet<TempMnchEnrolmentExtractError> TempMnchEnrolmentExtractError { get; set; }
       public virtual DbSet<TempMnchArtExtractError> TempMnchArtExtractError { get; set; }
       public virtual DbSet<TempAncVisitExtractError> TempAncVisitExtractError { get; set; }
       public virtual DbSet<TempMatVisitExtractError> TempMatVisitExtractError { get; set; }
       public virtual DbSet<TempPncVisitExtractError> TempPncVisitExtractError { get; set; }
       public virtual DbSet<TempMotherBabyPairExtractError> TempMotherBabyPairExtractError { get; set; }
       public virtual DbSet<TempCwcEnrolmentExtractError> TempCwcEnrolmentExtractError { get; set; }
       public virtual DbSet<TempCwcVisitExtractError> TempCwcVisitExtractError { get; set; }
       public virtual DbSet<TempHeiExtractError> TempHeiExtractError { get; set; }
       public virtual DbSet<TempMnchLabExtractError> TempMnchLabExtractError { get; set; }
       public virtual DbSet<TempMnchImmunizationExtractError> TempMnchImmunizationExtractError { get; set; }
      
       public virtual DbSet<TempPatientMnchExtractErrorSummary> TempPatientMnchExtractErrorSummary { get; set; }
       public virtual DbSet<TempMnchEnrolmentExtractErrorSummary> TempMnchEnrolmentExtractErrorSummary { get; set; }
       public virtual DbSet<TempMnchArtExtractErrorSummary> TempMnchArtExtractErrorSummary { get; set; }
       public virtual DbSet<TempAncVisitExtractErrorSummary> TempAncVisitExtractErrorSummary { get; set; }
       public virtual DbSet<TempMatVisitExtractErrorSummary> TempMatVisitExtractErrorSummary { get; set; }
       public virtual DbSet<TempPncVisitExtractErrorSummary> TempPncVisitExtractErrorSummary { get; set; }
       public virtual DbSet<TempMotherBabyPairExtractErrorSummary> TempMotherBabyPairExtractErrorSummary { get; set; }
       public virtual DbSet<TempCwcEnrolmentExtractErrorSummary> TempCwcEnrolmentExtractErrorSummary { get; set; }
       public virtual DbSet<TempCwcVisitExtractErrorSummary> TempCwcVisitExtractErrorSummary { get; set; }
       public virtual DbSet<TempHeiExtractErrorSummary> TempHeiExtractErrorSummary { get; set; }
       public virtual DbSet<TempMnchLabExtractErrorSummary> TempMnchLabExtractErrorSummary { get; set; }
       public virtual DbSet<TempMnchImmunizationExtractErrorSummary> TempMnchImmunizationExtractErrorSummary { get; set; }


       #endregion


       #region Prep
       public DbSet<TempPatientPrepExtract> TempPatientPrepExtracts { get; set; }
       public DbSet<TempPrepAdverseEventExtract> TempPrepAdverseEventExtracts { get; set; }
       public DbSet<TempPrepBehaviourRiskExtract> TempPrepBehaviourRiskExtracts { get; set; }
       public DbSet<TempPrepCareTerminationExtract> TempPrepCareTerminationExtracts { get; set; }
       public DbSet<TempPrepLabExtract> TempPrepLabExtracts { get; set; }
       public DbSet<TempPrepPharmacyExtract> TempPrepPharmacyExtracts { get; set; }
       public DbSet<TempPrepVisitExtract> TempPrepVisitExtracts { get; set; }
       public DbSet<TempPrepMonthlyRefillExtract> TempPrepMonthlyRefillExtracts { get; set; }


       public DbSet<PatientPrepExtract> PatientPrepExtracts { get; set; }
       public DbSet<PrepAdverseEventExtract> PrepAdverseEventExtracts { get; set; }
       public DbSet<PrepBehaviourRiskExtract> PrepBehaviourRiskExtracts { get; set; }
       public DbSet<PrepCareTerminationExtract> PrepCareTerminationExtracts { get; set; }
       public DbSet<PrepLabExtract> PrepLabExtracts { get; set; }
       public DbSet<PrepPharmacyExtract> PrepPharmacyExtracts { get; set; }
       public DbSet<PrepVisitExtract> PrepVisitExtracts { get; set; }
       public DbSet<PrepMonthlyRefillExtract> PrepMonthlyRefillExtracts { get; set; }


       public DbSet<TempPatientPrepExtractError> TempPatientPrepExtractError { get; set; }
       public DbSet<TempPrepAdverseEventExtractError> TempPrepAdverseEventExtractError { get; set; }
       public DbSet<TempPrepBehaviourRiskExtractError> TempPrepBehaviourRiskExtractError { get; set; }
       public DbSet<TempPrepCareTerminationExtractError> TempPrepCareTerminationExtractError { get; set; }
       public DbSet<TempPrepLabExtractError> TempPrepLabExtractError { get; set; }
       public DbSet<TempPrepPharmacyExtractError> TempPrepPharmacyExtractError { get; set; }
       public DbSet<TempPrepVisitExtractError> TempPrepVisitExtractError { get; set; }
       public DbSet<TempPrepMonthlyRefillExtractError> TempPrepMonthlyRefillExtractError { get; set; }


       public DbSet<TempPatientPrepExtractErrorSummary> TempPatientPrepExtractErrorSummary { get; set; }
       public DbSet<TempPrepAdverseEventExtractErrorSummary> TempPrepAdverseEventExtractErrorSummary { get; set; }
       public DbSet<TempPrepBehaviourRiskExtractErrorSummary> TempPrepBehaviourRiskExtractErrorSummary { get; set; }
       public DbSet<TempPrepCareTerminationExtractErrorSummary> TempPrepCareTerminationExtractErrorSummary { get; set; }
       public DbSet<TempPrepLabExtractErrorSummary> TempPrepLabExtractErrorSummary { get; set; }
       public DbSet<TempPrepPharmacyExtractErrorSummary> TempPrepPharmacyExtractErrorSummary { get; set; }
       public DbSet<TempPrepVisitExtractErrorSummary> TempPrepVisitExtractErrorSummary { get; set; }
       public DbSet<TempPrepMonthlyRefillExtractErrorSummary> TempPrepMonthlyRefillExtractErrorSummary { get; set; }


       #endregion


       public DbSet<ClientRegistryExtract> ClientRegistryExtracts{ get; set; }
       public DbSet<TempClientRegistryExtract> TempClientRegistryExtracts { get; set; }
     
       public DbSet<TempClientRegistryExtractError> TempClientRegistryExtractError { get; set; }
       public DbSet<TempClientRegistryExtractErrorSummary> TempClientRegistryExtractErrorSummary { get; set; }
      
      
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


           //Hts relationships
           modelBuilder.Entity<HtsClients>()
               .HasKey(f => new { f.SiteCode, f.PatientPk });


           modelBuilder.Entity<HtsClients>()
               .HasMany(c => c.HtsClientTestss)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new { f.SiteCode, f.PatientPk});


           modelBuilder.Entity<HtsClients>()
               .HasMany(c => c.HtsClientTracings)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new { f.SiteCode, f.PatientPk });


           modelBuilder.Entity<HtsClients>()
               .HasMany(c => c.HtsTestKitss)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new { f.SiteCode, f.PatientPk });


           modelBuilder.Entity<HtsClients>()
               .HasMany(c => c.HtsPartnerNotificationServicess)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new { f.SiteCode, f.PatientPk });


           modelBuilder.Entity<HtsClients>()
               .HasMany(c => c.HtsClientLinkages)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new { f.SiteCode, f.PatientPk });


           modelBuilder.Entity<HtsClients>()
               .HasMany(c => c.HtsPartnerTracings)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new { f.SiteCode, f.PatientPk });
          
           modelBuilder.Entity<HtsClients>()
               .HasMany(c => c.HtsEligibilityExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new { f.SiteCode, f.PatientPk });
          
        


           //            modelBuilder.Entity<HTSClientExtract>()
           //                .HasKey(f => new {f.SiteCode, f.PatientPk,f.EncounterId});




           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.AllergiesChronicIllnessExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.ContactListingExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.DepressionScreeningExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.DrugAlcoholScreeningExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.EnhancedAdherenceCounsellingExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.GbvScreeningExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.IptExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.OtzExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.OvcExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.CovidExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.DefaulterTracingExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.CancerScreeningExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.IITRiskScoresExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           modelBuilder.Entity<PatientExtract>()
               .HasMany(c => c.ArtFastTrackExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientMnchExtract>()
               .HasKey(f => new {f.SiteCode, f.PatientPK});




           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.AncVisitExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.CwcEnrolmentExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.CwcVisitExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.HeiExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.MatVisitExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.MnchArtExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.MnchEnrolmentExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.MnchLabExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.MotherBabyPairExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.PncVisitExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
          
           modelBuilder.Entity<PatientMnchExtract>()
               .HasMany(c => c.MnchImmunizationExtracts)
               .WithOne()
               .IsRequired()
               .HasForeignKey(f => new {f.SiteCode, f.PatientPK});





           //
           // modelBuilder.Entity<PatientPrepExtract>()
           //     .HasKey(f => new {f.SiteCode, f.PatientPK});
           //
           //
           // modelBuilder.Entity<PatientPrepExtract>()
           //     .HasMany(c => c.PrepAdverseEventExtracts)
           //     .WithOne()
           //     .IsRequired()
           //     .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           //
           //
           // modelBuilder.Entity<PatientPrepExtract>()
           //     .HasMany(c => c.PrepBehaviourRiskExtracts)
           //     .WithOne()
           //     .IsRequired()
           //     .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           //
           //
           // modelBuilder.Entity<PatientPrepExtract>()
           //     .HasMany(c => c.PrepCareTerminationExtracts)
           //     .WithOne()
           //     .IsRequired()
           //     .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           //
           //
           // modelBuilder.Entity<PatientPrepExtract>()
           //     .HasMany(c => c.PrepLabExtracts)
           //     .WithOne()
           //     .IsRequired()
           //     .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           //
           //
           // modelBuilder.Entity<PatientPrepExtract>()
           //     .HasMany(c => c.PrepPharmacyExtracts)
           //     .WithOne()
           //     .IsRequired()
           //     .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           //
           //
           // modelBuilder.Entity<PatientPrepExtract>()
           //     .HasMany(c => c.PrepVisitExtracts)
           //     .WithOne()
           //     .IsRequired()
           //     .HasForeignKey(f => new {f.SiteCode, f.PatientPK});
           // modelBuilder.Entity<PatientPrepExtract>()
           //     .HasMany(c => c.PrepMonthlyRefillExtracts)
           //     .WithOne()
           //     .IsRequired()
           //     .HasForeignKey(f => new {f.SiteCode, f.PatientPK});


           DapperPlusManager.Entity<TempPatientExtract>().Key(x => x.Id).Table($"{nameof(TempPatientExtracts)}");
           DapperPlusManager.Entity<TempPatientArtExtract>().Key(x => x.Id).Table($"{nameof(TempPatientArtExtracts)}");
           DapperPlusManager.Entity<TempPatientBaselinesExtract>().Key(x => x.Id)
               .Table($"{nameof(TempPatientBaselinesExtracts)}");
           DapperPlusManager.Entity<TempPatientLaboratoryExtract>().Key(x => x.Id)
               .Table($"{nameof(TempPatientLaboratoryExtracts)}");
           DapperPlusManager.Entity<TempPatientPharmacyExtract>().Key(x => x.Id)
               .Table($"{nameof(TempPatientPharmacyExtracts)}").BatchTimeout(3600);;
           DapperPlusManager.Entity<TempPatientStatusExtract>().Key(x => x.Id)
               .Table($"{nameof(TempPatientStatusExtracts)}");
           DapperPlusManager.Entity<TempPatientVisitExtract>().Key(x => x.Id)
               .Table($"{nameof(TempPatientVisitExtracts)}").BatchTimeout(3600);;
           DapperPlusManager.Entity<TempPatientAdverseEventExtract>().Key(x => x.Id)
               .Table($"{nameof(TempPatientAdverseEventExtracts)}");
           DapperPlusManager.Entity<PatientArtExtract>().Key(x => x.Id).Table($"{nameof(PatientArtExtracts)}");
           DapperPlusManager.Entity<PatientBaselinesExtract>().Key(x => x.Id)
               .Table($"{nameof(PatientBaselinesExtracts)}");
           DapperPlusManager.Entity<PatientLaboratoryExtract>().Key(x => x.Id)
               .Table($"{nameof(PatientLaboratoryExtracts)}");
           DapperPlusManager.Entity<PatientPharmacyExtract>().Key(x => x.Id)
               .Table($"{nameof(PatientPharmacyExtracts)}").BatchTimeout(3600);
           DapperPlusManager.Entity<PatientStatusExtract>().Key(x => x.Id).Table($"{nameof(PatientStatusExtracts)}");
           DapperPlusManager.Entity<PatientVisitExtract>().Key(x => x.Id).Table($"{nameof(PatientVisitExtracts)}").BatchTimeout(3600);
           DapperPlusManager.Entity<PatientAdverseEventExtract>().Key(x => x.Id)
               .Table($"{nameof(PatientAdverseEventExtracts)}");
           DapperPlusManager.Entity<PatientExtract>().Key(x => x.Id).Table($"{nameof(PatientExtracts)}");
           DapperPlusManager.Entity<MasterPatientIndex>().Key(x => x.Id).Table($"{nameof(MasterPatientIndices)}").BatchTimeout(3600);
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
           DapperPlusManager.Entity<HtsEligibilityExtract>().Key(x => x.Id).Table($"{nameof(HtsEligibilityExtracts)}");


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
           DapperPlusManager.Entity<TempHtsEligibilityExtract>().Key(x => x.Id).Table($"{nameof(TempHtsEligibilityExtracts)}");


           DapperPlusManager.Entity<MetricMigrationExtract>().Key(x => x.Id).Table($"{nameof(MetricMigrationExtracts)}");
           DapperPlusManager.Entity<TempMetricMigrationExtract>().Key(x => x.Id).Table($"{nameof(TempMetricMigrationExtracts)}");
           DapperPlusManager.Entity<Validator>().Key(x => x.Id).Table($"{nameof(Validator)}");
           DapperPlusManager.Entity<DiffLog>().Key(x => x.Id).Table($"{nameof(DiffLogs)}");
           DapperPlusManager.Entity<ExtractHistory>().Key(x => x.Id).Table($"{nameof(ExtractHistory)}");
           DapperPlusManager.Entity<IndicatorExtract>().Key(x => x.Id).Table($"{nameof(IndicatorExtracts)}");
           DapperPlusManager.Entity<TempIndicatorExtract>().Key(x => x.Id).Table($"{nameof(TempIndicatorExtracts)}");
           // DapperPlusManager.MapperFactory = mapper => mapper.BatchTimeout(6000);


           DapperPlusManager.Entity<TempAllergiesChronicIllnessExtract>().Key(x => x.Id).Table($"{nameof(TempAllergiesChronicIllnessExtracts)}");
           DapperPlusManager.Entity<TempIptExtract>().Key(x => x.Id).Table($"{nameof(TempIptExtracts)}");
           DapperPlusManager.Entity<TempDepressionScreeningExtract>().Key(x => x.Id).Table($"{nameof(TempDepressionScreeningExtracts)}");
           DapperPlusManager.Entity<TempContactListingExtract>().Key(x => x.Id).Table($"{nameof(TempContactListingExtracts)}");
           DapperPlusManager.Entity<TempGbvScreeningExtract>().Key(x => x.Id).Table($"{nameof(TempGbvScreeningExtracts)}");
           DapperPlusManager.Entity<TempEnhancedAdherenceCounsellingExtract>().Key(x => x.Id).Table($"{nameof(TempEnhancedAdherenceCounsellingExtracts)}");
           DapperPlusManager.Entity<TempDrugAlcoholScreeningExtract>().Key(x => x.Id).Table($"{nameof(TempDrugAlcoholScreeningExtracts)}");
           DapperPlusManager.Entity<TempOvcExtract>().Key(x => x.Id).Table($"{nameof(TempOvcExtracts)}");
           DapperPlusManager.Entity<TempOtzExtract>().Key(x => x.Id).Table($"{nameof(TempOtzExtracts)}");
           DapperPlusManager.Entity<TempCovidExtract>().Key(x => x.Id).Table($"{nameof(TempCovidExtracts)}");
           DapperPlusManager.Entity<TempDefaulterTracingExtract>().Key(x => x.Id).Table($"{nameof(TempDefaulterTracingExtracts)}");
           DapperPlusManager.Entity<TempCancerScreeningExtract>().Key(x => x.Id).Table($"{nameof(TempCancerScreeningExtracts)}");
           DapperPlusManager.Entity<TempIITRiskScoresExtract>().Key(x => x.Id).Table($"{nameof(TempIITRiskScoresExtracts)}");
           DapperPlusManager.Entity<TempArtFastTrackExtract>().Key(x => x.Id).Table($"{nameof(TempArtFastTrackExtracts)}");




           DapperPlusManager.Entity<AllergiesChronicIllnessExtract>().Key(x => x.Id).Table($"{nameof(AllergiesChronicIllnessExtracts)}");
           DapperPlusManager.Entity<IptExtract>().Key(x => x.Id).Table($"{nameof(IptExtracts)}");
           DapperPlusManager.Entity<DepressionScreeningExtract>().Key(x => x.Id).Table($"{nameof(DepressionScreeningExtracts)}");
           DapperPlusManager.Entity<ContactListingExtract>().Key(x => x.Id).Table($"{nameof(ContactListingExtracts)}");
           DapperPlusManager.Entity<GbvScreeningExtract>().Key(x => x.Id).Table($"{nameof(GbvScreeningExtracts)}");
           DapperPlusManager.Entity<EnhancedAdherenceCounsellingExtract>().Key(x => x.Id).Table($"{nameof(EnhancedAdherenceCounsellingExtracts)}");
           DapperPlusManager.Entity<DrugAlcoholScreeningExtract>().Key(x => x.Id).Table($"{nameof(DrugAlcoholScreeningExtracts)}");
           DapperPlusManager.Entity<OvcExtract>().Key(x => x.Id).Table($"{nameof(OvcExtracts)}");
           DapperPlusManager.Entity<OtzExtract>().Key(x => x.Id).Table($"{nameof(OtzExtracts)}");
           DapperPlusManager.Entity<CovidExtract>().Key(x => x.Id).Table($"{nameof(CovidExtracts)}");
           DapperPlusManager.Entity<DefaulterTracingExtract>().Key(x => x.Id).Table($"{nameof(DefaulterTracingExtracts)}");
           DapperPlusManager.Entity<CancerScreeningExtract>().Key(x => x.Id).Table($"{nameof(CancerScreeningExtracts)}");
           DapperPlusManager.Entity<IITRiskScoresExtract>().Key(x => x.Id).Table($"{nameof(IITRiskScoresExtracts)}");
           DapperPlusManager.Entity<ArtFastTrackExtract>().Key(x => x.Id).Table($"{nameof(ArtFastTrackExtracts)}");




           DapperPlusManager.Entity<TempPatientMnchExtract>().Key(x => x.Id).Table($"{nameof(TempPatientMnchExtracts)}");
           DapperPlusManager.Entity<TempMnchEnrolmentExtract>().Key(x => x.Id).Table($"{nameof(TempMnchEnrolmentExtracts)}");
           DapperPlusManager.Entity<TempMnchArtExtract>().Key(x => x.Id).Table($"{nameof(TempMnchArtExtracts)}");
           DapperPlusManager.Entity<TempAncVisitExtract>().Key(x => x.Id).Table($"{nameof(TempAncVisitExtracts)}");
           DapperPlusManager.Entity<TempMatVisitExtract>().Key(x => x.Id).Table($"{nameof(TempMatVisitExtracts)}");
           DapperPlusManager.Entity<TempPncVisitExtract>().Key(x => x.Id).Table($"{nameof(TempPncVisitExtracts)}");
           DapperPlusManager.Entity<TempMotherBabyPairExtract>().Key(x => x.Id).Table($"{nameof(TempMotherBabyPairExtracts)}");
           DapperPlusManager.Entity<TempCwcEnrolmentExtract>().Key(x => x.Id).Table($"{nameof(TempCwcEnrolmentExtracts)}");
           DapperPlusManager.Entity<TempCwcVisitExtract>().Key(x => x.Id).Table($"{nameof(TempCwcVisitExtracts)}");
           DapperPlusManager.Entity<TempHeiExtract>().Key(x => x.Id).Table($"{nameof(TempHeiExtracts)}");
           DapperPlusManager.Entity<TempMnchLabExtract>().Key(x => x.Id).Table($"{nameof(TempMnchLabExtracts)}");
           DapperPlusManager.Entity<TempMnchImmunizationExtract>().Key(x => x.Id).Table($"{nameof(TempMnchImmunizationExtracts)}");




           DapperPlusManager.Entity<PatientMnchExtract>().Key(x => x.Id).Table($"{nameof(PatientMnchExtracts)}");
           DapperPlusManager.Entity<MnchEnrolmentExtract>().Key(x => x.Id).Table($"{nameof(MnchEnrolmentExtracts)}");
           DapperPlusManager.Entity<MnchArtExtract>().Key(x => x.Id).Table($"{nameof(MnchArtExtracts)}");
           DapperPlusManager.Entity<AncVisitExtract>().Key(x => x.Id).Table($"{nameof(AncVisitExtracts)}");
           DapperPlusManager.Entity<MatVisitExtract>().Key(x => x.Id).Table($"{nameof(MatVisitExtracts)}");
           DapperPlusManager.Entity<PncVisitExtract>().Key(x => x.Id).Table($"{nameof(PncVisitExtracts)}");
           DapperPlusManager.Entity<MotherBabyPairExtract>().Key(x => x.Id).Table($"{nameof(MotherBabyPairExtracts)}");
           DapperPlusManager.Entity<CwcEnrolmentExtract>().Key(x => x.Id).Table($"{nameof(CwcEnrolmentExtracts)}");
           DapperPlusManager.Entity<CwcVisitExtract>().Key(x => x.Id).Table($"{nameof(CwcVisitExtracts)}");
           DapperPlusManager.Entity<HeiExtract>().Key(x => x.Id).Table($"{nameof(HeiExtracts)}");
           DapperPlusManager.Entity<MnchLabExtract>().Key(x => x.Id).Table($"{nameof(MnchLabExtracts)}");
           DapperPlusManager.Entity<MnchImmunizationExtract>().Key(x => x.Id).Table($"{nameof(MnchImmunizationExtracts)}");




           DapperPlusManager.Entity<TempPatientPrepExtract>().Key(x => x.Id).Table($"{nameof(TempPatientPrepExtracts)}");
           DapperPlusManager.Entity<TempPrepBehaviourRiskExtract>().Key(x => x.Id).Table($"{nameof(TempPrepBehaviourRiskExtracts)}");
           DapperPlusManager.Entity<TempPrepVisitExtract>().Key(x => x.Id).Table($"{nameof(TempPrepVisitExtracts)}");
           DapperPlusManager.Entity<TempPrepLabExtract>().Key(x => x.Id).Table($"{nameof(TempPrepLabExtracts)}");
           DapperPlusManager.Entity<TempPrepPharmacyExtract>().Key(x => x.Id).Table($"{nameof(TempPrepPharmacyExtracts)}");
           DapperPlusManager.Entity<TempPrepAdverseEventExtract>().Key(x => x.Id).Table($"{nameof(TempPrepAdverseEventExtracts)}");
           DapperPlusManager.Entity<TempPrepCareTerminationExtract>().Key(x => x.Id).Table($"{nameof(TempPrepCareTerminationExtracts)}");
           DapperPlusManager.Entity<TempPrepMonthlyRefillExtract>().Key(x => x.Id).Table($"{nameof(TempPrepMonthlyRefillExtracts)}");


           DapperPlusManager.Entity<PatientPrepExtract>().Key(x => x.Id).Table($"{nameof(PatientPrepExtracts)}");
           DapperPlusManager.Entity<PrepBehaviourRiskExtract>().Key(x => x.Id).Table($"{nameof(PrepBehaviourRiskExtracts)}");
           DapperPlusManager.Entity<PrepVisitExtract>().Key(x => x.Id).Table($"{nameof(PrepVisitExtracts)}");
           DapperPlusManager.Entity<PrepLabExtract>().Key(x => x.Id).Table($"{nameof(PrepLabExtracts)}");
           DapperPlusManager.Entity<PrepPharmacyExtract>().Key(x => x.Id).Table($"{nameof(PrepPharmacyExtracts)}");
           DapperPlusManager.Entity<PrepAdverseEventExtract>().Key(x => x.Id).Table($"{nameof(PrepAdverseEventExtracts)}");
           DapperPlusManager.Entity<PrepCareTerminationExtract>().Key(x => x.Id).Table($"{nameof(PrepCareTerminationExtracts)}");
           DapperPlusManager.Entity<PrepMonthlyRefillExtract>().Key(x => x.Id).Table($"{nameof(PrepMonthlyRefillExtracts)}");
          
           DapperPlusManager.Entity<ClientRegistryExtract>().Key(x => x.Id).Table($"{nameof(ClientRegistryExtracts)}");
           DapperPlusManager.Entity<TempClientRegistryExtract>().Key(x => x.Id).Table($"{nameof(TempClientRegistryExtracts)}");
         
       }


       public override void EnsureSeeded()
       {
           this.SeedClear<Validator>().Wait();
           this.SeedAdd<Validator>(typeof(ExtractsContext).Assembly, "|").Wait();
       }
   }
}
















