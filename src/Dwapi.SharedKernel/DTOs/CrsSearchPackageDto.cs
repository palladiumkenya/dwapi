using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.DTOs
{
    public class CrsSearchPackageDto
    {

        public CrsSearchPackageDto(Registry destination, CrsSearch crsSearch)
        {
            Destination = destination;
            CrsSearch = crsSearch;
        }

        public CrsSearchPackageDto()
        {
        }

        public Registry Destination { get; set; }
        public CrsSearch  CrsSearch { get; set; }
        public string Endpoint { get; set; }

        public bool IsValid()
        {
            ValidationContext context = new ValidationContext(this);
            List<ValidationResult> results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results, true);
        }

        public string GetUrl(string endPoint = "")
        {
            Endpoint = string.IsNullOrWhiteSpace(endPoint) ? string.Empty : endPoint.HasToStartWith("/");
            var url = $"{Destination.Url}{Endpoint}";
            return url;
        }

    }

    public class CrsSearch
    {
        public CrsSearch(string firstName, string middleName, string lastName, DateTime dateOfBirth, string sex, string county, string phoneNumber, string nationalId, string nextOfKinRelationship,
            string cccNumber, string passport, string hudumaNumber, string birthCertificateNumber, string alienIdNo, string drivingLicenseNumber, string patientClinicNumber, string maritalStatus, 
            string occupation, string highestLevelOfEducation, string alternativePhoneNumber, string spousePhoneNumber, string nameOfNextOfKin, 
            string nextOfKinTelNo, string subCounty, string ward, string location, string village, string landmark, string facilityName, 
            string mflCode, DateTime dateOfInitiation, string treatmentOutcome, DateTime dateOfLastEncounter, DateTime dateOfLastViralLoad, 
            DateTime nextAppointmentDate, int patientPK, string siteCode, int facilityID, string emr, string project, string lastRegimen, 
            string lastRegimenLine)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Sex = sex;
            County = county;
            PhoneNumber = phoneNumber;
            NationalId = nationalId;
            NextOfKinRelationship = nextOfKinRelationship;
            CCCNumber = cccNumber;
            Passport = passport;
            HudumaNumber = hudumaNumber;
            BirthCertificateNumber = birthCertificateNumber;
            AlienIdNo = alienIdNo;
            DrivingLicenseNumber = drivingLicenseNumber;
            PatientClinicNumber = patientClinicNumber;

            MaritalStatus = maritalStatus;
            Occupation = occupation;
            HighestLevelOfEducation = highestLevelOfEducation;
            AlternativePhoneNumber = alternativePhoneNumber;
            SpousePhoneNumber = spousePhoneNumber;
            NameOfNextOfKin = nameOfNextOfKin;
            NextOfKinTelNo = nextOfKinTelNo;
            SubCounty = subCounty;
            Ward = ward;
            Location = location;
            Village = village;
            Landmark = landmark;
            FacilityName = facilityName;
            MFLCode = mflCode;
            DateOfInitiation = dateOfInitiation;
            TreatmentOutcome = treatmentOutcome;
            DateOfLastEncounter = dateOfLastEncounter;
            DateOfLastViralLoad = dateOfLastViralLoad;
            NextAppointmentDate = nextAppointmentDate;
            PatientPK = patientPK;
            SiteCode = siteCode;
            FacilityID = facilityID;
            Emr = emr;
            Project = project;
            LastRegimen = lastRegimen;
            LastRegimenLine = lastRegimenLine;
        }

        public CrsSearch()
        {
        }

        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Sex { get; set; }
        public string County { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string NextOfKinRelationship { get; set; }

        public string CCCNumber { get; set; }
        public string Passport { get; set; }
        public string HudumaNumber { get; set; }
        public string BirthCertificateNumber { get; set; }
        public string AlienIdNo { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string PatientClinicNumber { get; set; }
        
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string HighestLevelOfEducation { get; set; }
        public string AlternativePhoneNumber { get; set; }
        public string SpousePhoneNumber { get; set; }
        public string NameOfNextOfKin { get; set; }
        public string NextOfKinTelNo { get; set; }
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
        public int? PatientPK { get; set; }
        public string SiteCode { get; set; }
        public int? FacilityID { get; set; }
        public string Emr { get; set;}
        public string Project { get; set;}
        public string LastRegimen { get; set; }
        public string LastRegimenLine { get; set; }
    }
}