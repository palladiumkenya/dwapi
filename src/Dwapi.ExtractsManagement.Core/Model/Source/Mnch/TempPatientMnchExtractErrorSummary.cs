using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mnch
{
    [Table("vTempPatientMnchExtractErrorSummary")]public class TempPatientMnchExtractErrorSummary:TempMnchExtractErrorSummary,IPatientMnch
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
        public string NUPI { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        
    }
}
