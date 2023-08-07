using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Dwapi.Contracts.Ct;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("PatientExtracts")]
    public class PatientExtractView : ClientExtract,IPatient
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
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }
        public string Pkv { get; set; }
        public string Occupation { get; set; }
        public string NUPI { get; set; }


        [NotMapped] public int PatientPID => PatientPK;
        [NotMapped] public string PatientCccNumber => PatientID;
        [NotMapped] public int FacilityId => SiteCode;

        [JsonIgnore]
        [NotMapped]
        public ICollection<PatientArtExtractView> PatientArtExtracts { get; set; } = new List<PatientArtExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<PatientBaselinesExtractView> PatientBaselinesExtracts { get; set; } =
            new List<PatientBaselinesExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<PatientLaboratoryExtractView> PatientLaboratoryExtracts { get; set; } =
            new List<PatientLaboratoryExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<PatientPharmacyExtractView> PatientPharmacyExtracts { get; set; } =
            new List<PatientPharmacyExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<PatientStatusExtractView> PatientStatusExtracts { get; set; } =
            new List<PatientStatusExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<PatientVisitExtractView> PatientVisitExtracts { get; set; } =
            new List<PatientVisitExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<PatientAdverseEventView> PatientAdverseEventExtracts { get; set; } =
            new List<PatientAdverseEventView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<AllergiesChronicIllnessExtractView> AllergiesChronicIllnessExtracts { get; set; } =
            new List<AllergiesChronicIllnessExtractView>();
        [JsonIgnore]
        [NotMapped] public ICollection<IptExtractView> IptExtracts { get; set; } = new List<IptExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<DepressionScreeningExtractView> DepressionScreeningExtracts { get; set; } =
            new List<DepressionScreeningExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<ContactListingExtractView> ContactListingExtracts { get; set; } =
            new List<ContactListingExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<GbvScreeningExtractView> GbvScreeningExtracts { get; set; } =
            new List<GbvScreeningExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<EnhancedAdherenceCounsellingExtractView> EnhancedAdherenceCounsellingExtracts { get; set; } =
            new List<EnhancedAdherenceCounsellingExtractView>();
        [JsonIgnore]
        [NotMapped]
        public ICollection<DrugAlcoholScreeningExtractView> DrugAlcoholScreeningExtracts { get; set; } =
            new List<DrugAlcoholScreeningExtractView>();

        [JsonIgnore]
        [NotMapped] public ICollection<OvcExtractView> OvcExtracts { get; set; } = new List<OvcExtractView>();
        [JsonIgnore]
        [NotMapped] public ICollection<OtzExtractView> OtzExtracts { get; set; } = new List<OtzExtractView>();
        [JsonIgnore]
        [NotMapped] public ICollection<CovidExtractView> CovidExtracts { get; set; } = new List<CovidExtractView>();
        [JsonIgnore]
        [NotMapped] public ICollection<DefaulterTracingExtractView> DefaulterTracingExtracts { get; set; } = new List<DefaulterTracingExtractView>();
        [NotMapped] public ICollection<CervicalCancerScreeningExtractView> CervicalCancerScreeningExtracts { get; set; } = new List<CervicalCancerScreeningExtractView>();

        [JsonIgnore]
        [NotMapped] public bool HasArt => null != PatientArtExtracts && PatientArtExtracts.Any();
        [JsonIgnore]
        [NotMapped] public bool HasBaseline => null != PatientBaselinesExtracts && PatientBaselinesExtracts.Any();
        [JsonIgnore]
        [NotMapped] public bool HasLab => null != PatientLaboratoryExtracts && PatientLaboratoryExtracts.Any();
        [JsonIgnore]
        [NotMapped] public bool HasPharmacy => null != PatientPharmacyExtracts && PatientPharmacyExtracts.Any();
        [JsonIgnore]
        [NotMapped] public bool HasStatus => null != PatientStatusExtracts && PatientStatusExtracts.Any();
        [JsonIgnore]
        [NotMapped] public bool HasVisit => null != PatientVisitExtracts && PatientVisitExtracts.Any();
        [JsonIgnore]
        [NotMapped] public bool HasAdverse => null != PatientAdverseEventExtracts && PatientAdverseEventExtracts.Any();
        [JsonIgnore]
        [NotMapped] public bool HasCancer => null != CervicalCancerScreeningExtracts && CervicalCancerScreeningExtracts.Any();

        public Facility GetFacility()
        {
            return new Facility(SiteCode, FacilityName, Emr, Project);
        }

    }
}
