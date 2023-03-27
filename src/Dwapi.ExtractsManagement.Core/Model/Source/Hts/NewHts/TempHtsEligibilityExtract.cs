using System;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    public class TempHtsEligibilityExtract : TempHtsExtract
    {
        public string EncounterId { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string PopulationType { get; set; }
        public string KeyPopulation { get; set; }
        public string PriorityPopulation { get; set; }
        public string Department { get; set; }
        public string PatientType { get; set; }
        public string IsHealthWorker { get; set; }
        public string RelationshipWithContact { get; set; }
        public string TestedHIVBefore { get; set; }
        public string WhoPerformedTest { get; set; }
        public string ResultOfHIV { get; set; }
        public string StartedOnART { get; set; }
        public string CCCNumber { get; set; }
        public string EverHadSex { get; set; }
        public string SexuallyActive { get; set; }
        public string NewPartner { get; set; }
        public string PartnerHIVStatus { get; set; }
        public string CoupleDiscordant { get; set; }
        public string MultiplePartners { get; set; }
        public int? NumberOfPartners { get; set; }
        public string AlcoholSex { get; set; }
        public string MoneySex { get; set; }
        public string CondomBurst { get; set; }
        public string UnknownStatusPartner { get; set; }
        public string KnownStatusPartner { get; set; }
        public string Pregnant { get; set; }
        public string BreastfeedingMother { get; set; }
        public string ExperiencedGBV { get; set; }
        
        public string EverOnPrep { get; set; }
        public string CurrentlyOnPrep { get; set; }
        public string EverOnPep { get; set; }
        public string CurrentlyOnPep { get; set; }
        public string EverHadSTI { get; set; }
        public string CurrentlyHasSTI { get; set; }
        public string EverHadTB { get; set; }
        public string SharedNeedle { get; set; }
        public string NeedleStickInjuries { get; set; }
        public string TraditionalProcedures { get; set; }
        public string ChildReasonsForIneligibility { get; set; }
        public string EligibleForTest { get; set; }
        public string ReasonsForIneligibility { get; set; }
        public int? SpecificReasonForIneligibility { get; set; }
        
        public string MothersStatus { get; set; }
        public DateTime? DateTestedSelf { get; set; }
        public string ResultOfHIVSelf { get; set; }
        public DateTime? DateTestedProvider { get; set; }
        public string ScreenedTB { get; set; }
        public string Cough	{ get; set; }
        public string Fever	{ get; set; }
        public string WeightLoss { get; set; }
        public string NightSweats { get; set; }
        public string Lethargy { get; set; }
        public string TBStatus { get; set; }
        public string ReferredForTesting { get; set; }
        
        public string AssessmentOutcome { get; set; }
        public string TypeGBV	{ get; set; }
        public string ForcedSex { get; set; }	
        public string ReceivedServices { get; set; }
        public string ContactWithTBCase { get; set; }
        
        public string Disability { get; set; }
        public string DisabilityType { get; set; }
        public string HTSStrategy { get; set; }
        public string HTSEntryPoint { get; set; }
        public string HIVRiskCategory { get; set; }
        public string ReasonRefferredForTesting   { get; set; }
        public string ReasonNotReffered { get; set; }
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        
    }
}