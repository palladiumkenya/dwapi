using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.Domain
{
    public class TempPatientExtract : TempExtract
    {
        public string FacilityName { get; set; }
        public string SatelliteName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public DateTime? RegistrationATPMTCT { get; set; }
        public DateTime? RegistrationAtTBClinic { get; set; }
        public string Region { get; set; }
        public string PatientSource { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string ContactRelation { get; set; }
        public DateTime? LastVisit { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public DateTime? DateConfirmedHIVPositive { get; set; }
        public string PreviousARTExposure { get; set; }
        public DateTime? PreviousARTStartDate { get; set; }
        public string StatusAtCCC { get; set; }
        public string StatusAtPMTCT { get; set; }
        public string StatusAtTBClinic { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

    }
}
