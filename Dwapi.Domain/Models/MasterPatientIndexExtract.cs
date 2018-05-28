using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.Domain
{
    public class MasterPatientIndexExtract
    {
        //[Key]
        //public Guid Id { get; set; }
        //public int PatientPK { get; set; }
        //public string PatientID { get; set; }
        //public int SiteCode { get; set; }


        public int RowId { get; set; }
        public string Serial { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FirstName_Normalized { get; set; }
        public string MiddleName_Normalized { get; set; }
        public string LastName_Normalized { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientAlternatePhoneNumber { get; set; }
        public string Gender { get; set; }

        public DateTime DOB {get; set;}
        public string MaritalStatus { get; set; }
        public string PatientSource { get; set; }
        public string PatientCounty { get; set; }
        public string PatientSubCounty { get; set; }
        public string PatientVillage { get; set; }
        public string PatientID { get; set; }
        public string NationalID {get; set;}
        public string NHIFNumber {get; set;}
        public string BirthCertificate {get; set;}
        public string CCCNumber {get; set;}
        public string TBNumber {get; set;}
        public string ContactName { get; set; }
        public string ContactRelation { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactAddress { get; set; }
        public DateTime DateConfirmedHIVPositive {get; set;}
        public DateTime StartARTDate {get; set;}
        public string StartARTRegimenCode { get; set; }
        public string StartARTRegimenDesc { get; set; }


        public MasterPatientIndexExtract(string serial, string firstname, string middlename, string lastname,
        string firstname_normalized, string middlename_normalized, string lastname_normalized,
        string patientphonenumber, string patientalternatephonenumber, string gender,
        DateTime dob, string maritalstatus, string patientsource, string patientcounty,
        string patientsubcounty, string patientvillage, string patientid, string nationalid,
        string nhifnumber , string birthcertificate, string cccnumber, string tbnumber,
        string contactname, string contactrelation, string contactphonenumber, string contactaddress,
        DateTime dateconfirmedhivpositive, DateTime startartdate, string startartregimencode,
        string startartregimendesc)
        {

            Serial = serial;
            FirstName = firstname;
            MiddleName = middlename;
            LastName = lastname;
            FirstName_Normalized = firstname_normalized;
            MiddleName_Normalized = middlename_normalized;
            LastName_Normalized = lastname_normalized;
            PatientPhoneNumber = patientphonenumber;
            PatientAlternatePhoneNumber = patientalternatephonenumber;
            Gender = gender;
            DOB = dob;
            MaritalStatus = maritalstatus;
            PatientSource = patientsource;
            PatientCounty = patientcounty;
            PatientSubCounty = patientsubcounty;
            PatientVillage = patientvillage;
            PatientID = patientid;
            NationalID = nationalid;
            NHIFNumber = nhifnumber;
            BirthCertificate = birthcertificate;
            CCCNumber = cccnumber;
            TBNumber = tbnumber;
            ContactName = contactname;
            ContactRelation = contactrelation;
            ContactPhoneNumber = contactphonenumber;
            ContactAddress = contactaddress;
            DateConfirmedHIVPositive = dateconfirmedhivpositive;
            StartARTDate = startartdate;
            StartARTRegimenCode = startartregimencode;
            StartARTRegimenDesc = startartregimendesc;

        }


    }
}
