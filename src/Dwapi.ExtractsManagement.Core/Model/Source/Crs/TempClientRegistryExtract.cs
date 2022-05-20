using System;
using Dwapi.Contracts.Crs;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Crs
{
    public class TempClientRegistryExtract : TempExtract,IClientRegistry
    {
       
        public string CCCNumber { get; set; } 
        public string NationalId { get; set; }
        public string Passport { get; set; }
        public string HudumaNumber { get; set; }
        public string BirthCertificateNumber { get; set; }
        public string AlienIdNo { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string PatientClinicNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string HighestLevelOfEducation { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternativePhoneNumber { get; set; }
        public string SpousePhoneNumber { get; set; }
        public string NameOfNextOfKin { get; set; }
        public string NextOfKinRelationship { get; set; }
        public string NextOfKinTelNo { get; set; }
        public string County { get; set; }
        public string SubCounty { get; set; }
        public string Ward { get; set; }
        public string Location { get; set; }
        public string Village { get; set; }
        public string Landmark { get; set; }
        public string FacilityName { get; set; }
        public string MFLCode { get; set; }
        public DateTime? DateOfInitiation { get; set; }
        public string TreatmentOutcome { get; set; }
        public DateTime? DateOfLastEncounter { get; set; }
        public DateTime? DateOfLastViralLoad { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string LastRegimen { get; set; }
        public string LastRegimenLine { get; set; }
        public string CurrentOnART { get; set; }
        public DateTime? DateOfHIVdiagnosis { get; set; }
        public string LastViralLoadResult { get; set; }
    }
}
