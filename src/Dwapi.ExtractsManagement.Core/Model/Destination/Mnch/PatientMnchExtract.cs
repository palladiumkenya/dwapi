using System;
using System.Collections.Generic;
using Dwapi.Contracts.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Mnch
{
    public class PatientMnchExtract : MnchClientExtract, IPatientMnch
    {
        public string FacilityName { get; set; }
        public string Pkv { get; set; }
        public string PatientMnchID { get; set; }
        public string PatientHeiID { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? FirstEnrollmentAtMnch { get; set; }
        public string Occupation { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public string PatientResidentCounty { get; set; }
        public string PatientResidentSubCounty { get; set; }
        public string PatientResidentWard { get; set; }
        public string InSchool { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public virtual ICollection<MnchEnrolmentExtract> MnchEnrolmentExtracts { get; set; } = new List<MnchEnrolmentExtract>();
        public virtual ICollection<MnchArtExtract> MnchArtExtracts { get; set; } = new List<MnchArtExtract>();
        public virtual ICollection<AncVisitExtract> AncVisitExtracts { get; set; } = new List<AncVisitExtract>();
        public virtual ICollection<MatVisitExtract> MatVisitExtracts { get; set; } = new List<MatVisitExtract>();
        public virtual ICollection<PncVisitExtract> PncVisitExtracts { get; set; } = new List<PncVisitExtract>();
        public virtual ICollection<MotherBabyPairExtract> MotherBabyPairExtracts { get; set; } = new List<MotherBabyPairExtract>();
        public virtual ICollection<CwcEnrolmentExtract> CwcEnrolmentExtracts { get; set; } = new List<CwcEnrolmentExtract>();
        public virtual ICollection<CwcVisitExtract> CwcVisitExtracts { get; set; } = new List<CwcVisitExtract>();
        public virtual ICollection<HeiExtract> HeiExtracts { get; set; } = new List<HeiExtract>();
        public virtual ICollection<MnchLabExtract> MnchLabExtracts { get; set; } = new List<MnchLabExtract>();
    }
}
