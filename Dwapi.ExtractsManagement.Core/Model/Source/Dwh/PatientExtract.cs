using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Domain;
using Dwapi.Domain.Models;
using Dwapi.Domain.Utils;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    public class PatientExtract : Entity<Guid>
    {
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int SiteCode { get; set; }
        [Column(Order = 100)]
        public string Emr { get; set; }
        [Column(Order = 101)]
        public string Project { get; set; }
        [DoNotRead]
        [Column(Order = 102)]
        public virtual bool? Processed { get; set; }

        [DoNotRead]
        public virtual string QueueId { get; set; }
        [DoNotRead]
        public virtual string Status { get; set; }
        [DoNotRead]
        public virtual DateTime? StatusDate { get; set; }
        
        public string FacilityName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public DateTime? RegistrationATPMTCT { get; set; }
        public DateTime? RegistrationAtTBClinic { get; set; }
        public string PatientSource { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string ContactRelation { get; set; }
        public DateTime? LastVisit { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public DateTime? DateConfirmedHIVPositive { get; set; }
        public string PreviousARTExposure { get; set; }
        public DateTime? DatePreviousARTStart { get; set; }
        public string StatusAtCCC { get; set; }
        public string StatusAtPMTCT { get; set; }
        public string StatusAtTBClinic { get; set; }

        [DoNotRead]
        public virtual ICollection<PatientArtExtract> ClientPatientArtExtracts { get; set; } = new List<PatientArtExtract>();
        [DoNotRead]
        public virtual ICollection<PatientBaselinesExtract> ClientPatientBaselinesExtracts { get; set; } = new List<PatientBaselinesExtract>();
        [DoNotRead]
        public virtual ICollection<PatientLaboratoryExtract> ClientPatientLaboratoryExtracts { get; set; } = new List<PatientLaboratoryExtract>();
        [DoNotRead]
        public virtual ICollection<PatientPharmacyExtract> ClientPatientPharmacyExtracts { get; set; } = new List<PatientPharmacyExtract>();
        [DoNotRead]
        public virtual ICollection<PatientStatusExtract> ClientPatientStatusExtracts { get; set; } = new List<PatientStatusExtract>();
        [DoNotRead]
        public virtual ICollection<PatientVisitExtract> ClientPatientVisitExtracts { get; set; } = new List<PatientVisitExtract>();

        public PatientExtract()
        {
        }

        //public ClientPatientExtract(int patientPk, string patientId, int siteCode, string facilityName, string gender, DateTime? dob, DateTime? registrationDate, DateTime? registrationAtCcc, DateTime? registrationAtpmtct, DateTime? registrationAtTbClinic, string patientSource, string region, string district, string village, string contactRelation, DateTime? lastVisit, string maritalStatus, string educationLevel, DateTime? dateConfirmedHivPositive, string previousArtExposure, DateTime? previousArtStartDate, string statusAtCcc, string statusAtPmtct, string statusAtTbClinic, string emr, string project)
        //{
        //    PatientPK = patientPk;
        //    PatientID = patientId;
        //    SiteCode = siteCode;
        //    FacilityName = facilityName;
        //    Gender = gender;
        //    DOB = dob;
        //    RegistrationDate = registrationDate;
        //    RegistrationAtCCC = registrationAtCcc;
        //    RegistrationATPMTCT = registrationAtpmtct;
        //    RegistrationAtTBClinic = registrationAtTbClinic;
        //    PatientSource = patientSource;
        //    Region = region;
        //    District = district;
        //    Village = village;
        //    ContactRelation = contactRelation;
        //    LastVisit = lastVisit;
        //    MaritalStatus = maritalStatus;
        //    EducationLevel = educationLevel;
        //    DateConfirmedHIVPositive = dateConfirmedHivPositive;
        //    PreviousARTExposure = previousArtExposure;
        //    PreviousARTStartDate = previousArtStartDate;
        //    StatusAtCCC = statusAtCcc;
        //    StatusAtPMTCT = statusAtPmtct;
        //    StatusAtTBClinic = statusAtTbClinic;
        //    Emr = emr;
        //    Project = project;
        //}

        //public ClientPatientExtract(TempPatientExtract extract)
        //{
        //    PatientPK = extract.PatientPK.Value;
        //    PatientID = extract.PatientID;
        //    SiteCode = extract.SiteCode.Value;
        //    FacilityName = extract.FacilityName;
        //    Gender = extract.Gender;
        //    DOB = extract.DOB;
        //    RegistrationDate = extract.RegistrationDate;
        //    RegistrationAtCCC = extract.RegistrationAtCCC;
        //    RegistrationATPMTCT = extract.RegistrationATPMTCT;
        //    RegistrationAtTBClinic = extract.RegistrationAtTBClinic;
        //    PatientSource = extract.PatientSource;
        //    Region = extract.Region;
        //    District = extract.District;
        //    Village = extract.Village;
        //    ContactRelation = extract.ContactRelation;
        //    LastVisit = extract.LastVisit;
        //    MaritalStatus = extract.MaritalStatus;
        //    EducationLevel = extract.EducationLevel;
        //    DateConfirmedHIVPositive = extract.DateConfirmedHIVPositive;
        //    PreviousARTExposure = extract.PreviousARTExposure;
        //    PreviousARTStartDate = extract.PreviousARTStartDate;
        //    StatusAtCCC = extract.StatusAtCCC;
        //    StatusAtPMTCT = extract.StatusAtPMTCT;
        //    StatusAtTBClinic = extract.StatusAtTBClinic;
        //    Emr = extract.Emr;
        //    Project = extract.Project;
        //}



        ////public string GetAddAction(string source, bool lookup = true)
        ////{
        ////    string sql = $@"
        ////    (SELECT      
	       ////     p.*
        ////    FROM            
	       ////     {source} AS p INNER JOIN
	       ////     (SELECT Id, ROW_NUMBER() OVER (PARTITION BY PatientPK,Sitecode ORDER BY PatientPK,Sitecode) AS RW
	       ////     FROM {source}) AS i ON p.Id = i.Id
        ////    WHERE        
	       ////     (i.RW = 1))xx
        ////    WHERE xx.CheckError = 0
        ////    ";
        ////    return base.GetAddAction(sql, false);
        ////}

        //public bool HasArt()
        //{
        //    return ClientPatientArtExtracts.Count > 0;
        //}
        //public bool HasBaselines()
        //{
        //    return ClientPatientBaselinesExtracts.Count > 0;
        //}
        //public bool HasLabs()
        //{
        //    return ClientPatientLaboratoryExtracts.Count > 0;
        //}
        //public bool HasPharmacy()
        //{
        //    return ClientPatientPharmacyExtracts.Count > 0;
        //}
        //public bool HasVisits()
        //{
        //    return ClientPatientVisitExtracts.Count > 0;
        //}
        //public bool HasStatus()
        //{
        //    return ClientPatientStatusExtracts.Count > 0;
        //}

        //public bool IsComplete()
        //{
        //    return HasArt() && HasBaselines() && HasLabs() && HasPharmacy() && HasVisits() && HasStatus();
        //}


        //public void AddPatientArtExtracts(IEnumerable<ClientPatientArtExtract> extracts)
        //{
        //    foreach (var e in extracts)
        //    {
        //        e.PatientPK = PatientPK;
        //        e.SiteCode = SiteCode;
        //        ClientPatientArtExtracts.Add(e);
        //    }
        //}
        //public void AddPatientBaselinesExtracts(IEnumerable<ClientPatientBaselinesExtract> extracts)
        //{
        //    foreach (var e in extracts)
        //    {
        //        e.PatientPK = PatientPK;
        //        e.SiteCode = SiteCode;
        //        ClientPatientBaselinesExtracts.Add(e);
        //    }
        //}
        //public void AddPatientLaboratoryExtracts(IEnumerable<ClientPatientLaboratoryExtract> extracts)
        //{
        //    foreach (var e in extracts)
        //    {
        //        e.PatientPK = PatientPK;
        //        e.SiteCode = SiteCode;
        //        ClientPatientLaboratoryExtracts.Add(e);
        //    }
        //}
        //public void AddPatientPharmacyExtracts(IEnumerable<ClientPatientPharmacyExtract> extracts)
        //{
        //    foreach (var e in extracts)
        //    {
        //        e.PatientPK = PatientPK;
        //        e.SiteCode = SiteCode;
        //        ClientPatientPharmacyExtracts.Add(e);
        //    }
        //}
        //public void AddPatientStatusExtracts(IEnumerable<ClientPatientStatusExtract> extracts)
        //{
        //    foreach (var e in extracts)
        //    {
        //        e.PatientPK = PatientPK;
        //        e.SiteCode = SiteCode;
        //        ClientPatientStatusExtracts.Add(e);
        //    }
        //}
        //public void AddPatientVisitExtracts(IEnumerable<ClientPatientVisitExtract> extracts)
        //{
        //    foreach (var e in extracts)
        //    {
        //        e.PatientPK = PatientPK;
        //        e.SiteCode = SiteCode;
        //        ClientPatientVisitExtracts.Add(e);
        //    }
        //}

        //public override string ToString()
        //{
        //    return $"{PatientID}";
        //}
    }
}
