using System;

namespace Dwapi.Contracts.Mnch
{
    public interface IPatientMnch
    {
        string FacilityName { get; set; }
        string Pkv { get; set; }
        string PatientMnchID { get; set; }
        string PatientHeiID { get; set; }
        string Gender { get; set; }
        DateTime? DOB { get; set; }
        DateTime? FirstEnrollmentAtMnch { get; set; }
        string Occupation { get; set; }
        string MaritalStatus { get; set; }
        string EducationLevel { get; set; }
        string PatientResidentCounty { get; set; }
        string PatientResidentSubCounty { get; set; }
        string PatientResidentWard { get; set; }
        string InSchool { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
