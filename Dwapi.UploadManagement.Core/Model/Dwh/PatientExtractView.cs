using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("PatientExtracts")]
    public class PatientExtractView : PatientExtract
    {
        [NotMapped]
        public int PatientPID => PatientPK;
        [NotMapped] public string PatientCccNumber => PatientID;
        [NotMapped] public int FacilityId =>SiteCode;

        [NotMapped]
        public IEnumerable<PatientArtExtractView> PatientArtExtracts { get; set; } = new List<PatientArtExtractView>();
        [NotMapped]
        public IEnumerable<PatientBaselinesExtractView> PatientBaselinesExtracts { get; set; }=new List<PatientBaselinesExtractView>();
        [NotMapped]
        public IEnumerable<PatientLaboratoryExtractView> PatientLaboratoryExtracts { get; set; }=new List<PatientLaboratoryExtractView>();
        [NotMapped]
        public IEnumerable<PatientPharmacyExtractView> PatientPharmacyExtracts { get; set; }=new List<PatientPharmacyExtractView>();
        [NotMapped]
        public IEnumerable<PatientStatusExtractView> PatientStatusExtracts { get; set; }=new List<PatientStatusExtractView>();
        [NotMapped]
        public IEnumerable<PatientVisitExtractView> PatientVisitExtracts { get; set; }=new List<PatientVisitExtractView>();

        [NotMapped] public bool HasArt => null!= PatientArtExtracts&& PatientArtExtracts.Any();
        [NotMapped] public bool HasBaseline => null != PatientBaselinesExtracts&& PatientBaselinesExtracts.Any();
        [NotMapped] public bool HasLab => null != PatientLaboratoryExtracts&&PatientLaboratoryExtracts.Any();
        [NotMapped] public bool HasPharmacy => null != PatientPharmacyExtracts && PatientPharmacyExtracts.Any();
        [NotMapped] public bool HasStatus => null != PatientStatusExtracts && PatientStatusExtracts.Any();
        [NotMapped] public bool HasVisit => null != PatientVisitExtracts&&PatientVisitExtracts.Any();


        public Facility GetFacility()
        {
            return new Facility(SiteCode,FacilityName,Emr,Project);
        }
    }
}