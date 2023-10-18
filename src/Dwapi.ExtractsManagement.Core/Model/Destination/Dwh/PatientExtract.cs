using System;
using System.Collections.Generic;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{
    public class PatientExtract : ClientExtract,IPatient
    {
        public string FacilityName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public DateTime? RegistrationATPMTCT { get; set; }
        public DateTime? RegistrationAtTBClinic { get; set; }
        public string PatientSource { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string ContactRelation { get; set; }
        public DateTime? LastVisit { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public DateTime? DateConfirmedHIVPositive { get; set; }
        public string PreviousARTExposure { get; set; }
        public DateTime? DatePreviousARTStart { get; set; }
        public string StatusAtCCC { get; set; }
        public string StatusAtPMTCT { get; set; }
        public string StatusAtTBClinic { get; set; }
        public string Orphan { get; set; }
        public string Inschool { get; set; }
        public string PatientType { get; set; }
        public string PopulationType { get; set; }
        public string KeyPopulationType { get; set; }
        public string PatientResidentCounty { get; set; }
        public string PatientResidentSubCounty { get; set; }
        public string PatientResidentLocation { get; set; }
        public string PatientResidentSubLocation { get; set; }
        public string PatientResidentWard { get; set; }
        public string PatientResidentVillage { get; set; }
        public DateTime? TransferInDate { get; set; }
        public string Pkv { get; set; }
        public string Occupation { get; set; }
        public string NUPI { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
            public bool? Voided { get; set; }
        public virtual ICollection<PatientArtExtract> PatientArtExtracts { get; set; } = new List<PatientArtExtract>();
        public virtual ICollection<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; } = new List<PatientBaselinesExtract>();
        public virtual ICollection<PatientLaboratoryExtract> PatientLaboratoryExtracts { get; set; } = new List<PatientLaboratoryExtract>();
        public virtual ICollection<PatientPharmacyExtract> PatientPharmacyExtracts { get; set; } = new List<PatientPharmacyExtract>();
        public virtual ICollection<PatientStatusExtract> PatientStatusExtracts { get; set; } = new List<PatientStatusExtract>();
        public virtual ICollection<PatientVisitExtract> PatientVisitExtracts { get; set; } = new List<PatientVisitExtract>();
        public virtual ICollection<PatientAdverseEventExtract> PatientAdverseEventExtracts { get; set; } = new List<PatientAdverseEventExtract>();

        public virtual ICollection<AllergiesChronicIllnessExtract> AllergiesChronicIllnessExtracts { get; set; } = new List<AllergiesChronicIllnessExtract>();
        public virtual ICollection<ContactListingExtract> ContactListingExtracts { get; set; } = new List<ContactListingExtract>();
        public virtual ICollection<DepressionScreeningExtract> DepressionScreeningExtracts { get; set; } = new List<DepressionScreeningExtract>();
        public virtual ICollection<DrugAlcoholScreeningExtract> DrugAlcoholScreeningExtracts { get; set; } = new List<DrugAlcoholScreeningExtract>();
        public virtual ICollection<EnhancedAdherenceCounsellingExtract> EnhancedAdherenceCounsellingExtracts { get; set; } = new List<EnhancedAdherenceCounsellingExtract>();
        public virtual ICollection<GbvScreeningExtract> GbvScreeningExtracts { get; set; } = new List<GbvScreeningExtract>();
        public virtual ICollection<IptExtract> IptExtracts { get; set; } = new List<IptExtract>();
        public virtual ICollection<OtzExtract> OtzExtracts { get; set; } = new List<OtzExtract>();
        public virtual ICollection<OvcExtract> OvcExtracts { get; set; } = new List<OvcExtract>();

        public virtual ICollection<CovidExtract> CovidExtracts { get; set; } = new List<CovidExtract>();
        public virtual ICollection<DefaulterTracingExtract> DefaulterTracingExtracts { get; set; } = new List<DefaulterTracingExtract>();
        public virtual ICollection<CervicalCancerScreeningExtract> CervicalCancerScreeningExtracts { get; set; } = new List<CervicalCancerScreeningExtract>();
        public virtual ICollection<IITRiskScoresExtract> IITRiskScoresExtracts { get; set; } = new List<IITRiskScoresExtract>();
        public virtual ICollection<ArtFastTrackExtract> ArtFastTrackExtracts { get; set; } = new List<ArtFastTrackExtract>();

    }
}
