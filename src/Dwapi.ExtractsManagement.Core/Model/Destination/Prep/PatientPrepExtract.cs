using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Prep;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Prep
{
    public class PatientPrepExtract : PrepClientExtract, IPatientPrep
    {
        public string FacilityName { get; set; }
        public string PrepNumber { get; set; }
        public string HtsNumber { get; set; }
        public DateTime? PrepEnrollmentDate { get; set; }
        public string Sex { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string CountyofBirth { get; set; }
        public string County { get; set; }
        public string SubCounty { get; set; }
        public string Location { get; set; }
        public string LandMark { get; set; }
        public string Ward { get; set; }
        public string ClientType { get; set; }
        public string ReferralPoint { get; set; }
        public string MaritalStatus { get; set; }
        public string Inschool { get; set; }
        public string PopulationType { get; set; }
        public string KeyPopulationType { get; set; }
        public string Refferedfrom { get; set; }
        public string TransferIn { get; set; }
        public DateTime? TransferInDate { get; set; }
        public string TransferFromFacility { get; set; }
        public DateTime? DatefirstinitiatedinPrepCare { get; set; }
        public DateTime? DateStartedPrEPattransferringfacility { get; set; }
        public string ClientPreviouslyonPrep { get; set; }
        public string PrevPrepReg { get; set; }
        public DateTime? DateLastUsedPrev { get; set; }
        public string NUPI { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }


        public virtual ICollection<PrepBehaviourRiskExtract> PrepBehaviourRiskExtracts { get; set; } =new List<PrepBehaviourRiskExtract>();
        public virtual ICollection<PrepVisitExtract> PrepVisitExtracts { get; set; } = new List<PrepVisitExtract>();
        public virtual ICollection<PrepLabExtract> PrepLabExtracts { get; set; } = new List<PrepLabExtract>();
        public virtual ICollection<PrepPharmacyExtract> PrepPharmacyExtracts { get; set; } = new List<PrepPharmacyExtract>();
        public virtual  ICollection<PrepAdverseEventExtract> PrepAdverseEventExtracts { get; set; } =new List<PrepAdverseEventExtract>();
        public virtual  ICollection<PrepCareTerminationExtract> PrepCareTerminationExtracts { get; set; } =new List<PrepCareTerminationExtract>();
    }

}
