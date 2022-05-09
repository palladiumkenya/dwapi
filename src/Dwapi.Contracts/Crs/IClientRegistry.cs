using System;

namespace Dwapi.Contracts.Crs
{
    public interface IClientRegistry
    {
        string CCCNumber { get; set;}
        string NationalId { get; set;}
        string Passport { get; set;}
        string HudumaNumber { get; set;}
        string BirthCertificateNumber { get; set;}
        string AlienIdNo { get; set;}
        string DrivingLicenseNumber { get; set;}
        string PatientClinicNumber { get; set;}
        string FirstName { get; set;}
        string MiddleName { get; set;}
        string LastName { get; set;}
        DateTime? DateOfBirth { get; set;}
        string Sex { get; set;}
        string MaritalStatus { get; set;}
        string Occupation { get; set;}
        string HighestLevelOfEducation { get; set;}
        string PhoneNumber { get; set;}
        string AlternativePhoneNumber { get; set;}
        string SpousePhoneNumber { get; set;}
        string NameOfNextOfKin { get; set;}
        string NextOfKinRelationship { get; set;}
        string NextOfKinTelNo { get; set;}
        string County { get; set;}
        string SubCounty { get; set;}
        string Ward { get; set;}
        string Location { get; set;}
        string Village { get; set;}
        string Landmark { get; set;}
        string FacilityName { get; set;}
        string MFLCode { get; set;}
        DateTime? DateOfInitiation { get; set;}
        string TreatmentOutcome { get; set;}
        DateTime? DateOfLastEncounter { get; set;}
        DateTime? DateOfLastViralLoad { get; set;}
        DateTime? NextAppointmentDate { get; set;}

    }
}