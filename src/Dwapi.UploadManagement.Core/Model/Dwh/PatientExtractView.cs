using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("PatientExtracts")]
    public class PatientExtractView : ClientExtract
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

        [NotMapped] public int PatientPID => PatientPK;
        [NotMapped] public string PatientCccNumber => PatientID;
        [NotMapped] public int FacilityId => SiteCode;

        [NotMapped]
        public ICollection<PatientArtExtractView> PatientArtExtracts { get; set; } = new List<PatientArtExtractView>();

        [NotMapped]
        public ICollection<PatientBaselinesExtractView> PatientBaselinesExtracts { get; set; } =
            new List<PatientBaselinesExtractView>();

        [NotMapped]
        public ICollection<PatientLaboratoryExtractView> PatientLaboratoryExtracts { get; set; } =
            new List<PatientLaboratoryExtractView>();

        [NotMapped]
        public ICollection<PatientPharmacyExtractView> PatientPharmacyExtracts { get; set; } =
            new List<PatientPharmacyExtractView>();

        [NotMapped]
        public ICollection<PatientStatusExtractView> PatientStatusExtracts { get; set; } =
            new List<PatientStatusExtractView>();

        [NotMapped]
        public ICollection<PatientVisitExtractView> PatientVisitExtracts { get; set; } =
            new List<PatientVisitExtractView>();

        [NotMapped]
        public ICollection<PatientAdverseEventView> PatientAdverseEventExtracts { get; set; } =
            new List<PatientAdverseEventView>();

        [NotMapped] public bool HasArt => null != PatientArtExtracts && PatientArtExtracts.Any();
        [NotMapped] public bool HasBaseline => null != PatientBaselinesExtracts && PatientBaselinesExtracts.Any();
        [NotMapped] public bool HasLab => null != PatientLaboratoryExtracts && PatientLaboratoryExtracts.Any();
        [NotMapped] public bool HasPharmacy => null != PatientPharmacyExtracts && PatientPharmacyExtracts.Any();
        [NotMapped] public bool HasStatus => null != PatientStatusExtracts && PatientStatusExtracts.Any();
        [NotMapped] public bool HasVisit => null != PatientVisitExtracts && PatientVisitExtracts.Any();

        [NotMapped] public bool HasAdverse => null != PatientAdverseEventExtracts && PatientAdverseEventExtracts.Any();

        public Facility GetFacility()
        {
            return new Facility(SiteCode, FacilityName, Emr, Project);
        }
    }
}
