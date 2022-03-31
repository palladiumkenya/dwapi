using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Prep;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Prep
{
    [Table("vTempPatientPrepExtractErrorSummary")]public class TempPatientPrepExtractErrorSummary:TempExtract,IPatientPrep
    {
        private IPatientPrep _patientPrepImplementation;
        public string FacilityName
        {
            get => _patientPrepImplementation.FacilityName;
            set => _patientPrepImplementation.FacilityName = value;
        }

        public string PrepNumber
        {
            get => _patientPrepImplementation.PrepNumber;
            set => _patientPrepImplementation.PrepNumber = value;
        }

        public string HtsNumber
        {
            get => _patientPrepImplementation.HtsNumber;
            set => _patientPrepImplementation.HtsNumber = value;
        }

        public DateTime? PrepEnrollmentDate
        {
            get => _patientPrepImplementation.PrepEnrollmentDate;
            set => _patientPrepImplementation.PrepEnrollmentDate = value;
        }

        public string Sex
        {
            get => _patientPrepImplementation.Sex;
            set => _patientPrepImplementation.Sex = value;
        }

        public DateTime? DateofBirth
        {
            get => _patientPrepImplementation.DateofBirth;
            set => _patientPrepImplementation.DateofBirth = value;
        }

        public string CountyofBirth
        {
            get => _patientPrepImplementation.CountyofBirth;
            set => _patientPrepImplementation.CountyofBirth = value;
        }

        public string County
        {
            get => _patientPrepImplementation.County;
            set => _patientPrepImplementation.County = value;
        }

        public string SubCounty
        {
            get => _patientPrepImplementation.SubCounty;
            set => _patientPrepImplementation.SubCounty = value;
        }

        public string Location
        {
            get => _patientPrepImplementation.Location;
            set => _patientPrepImplementation.Location = value;
        }

        public string LandMark
        {
            get => _patientPrepImplementation.LandMark;
            set => _patientPrepImplementation.LandMark = value;
        }

        public string Ward
        {
            get => _patientPrepImplementation.Ward;
            set => _patientPrepImplementation.Ward = value;
        }

        public string ClientType
        {
            get => _patientPrepImplementation.ClientType;
            set => _patientPrepImplementation.ClientType = value;
        }

        public string ReferralPoint
        {
            get => _patientPrepImplementation.ReferralPoint;
            set => _patientPrepImplementation.ReferralPoint = value;
        }

        public string MaritalStatus
        {
            get => _patientPrepImplementation.MaritalStatus;
            set => _patientPrepImplementation.MaritalStatus = value;
        }

        public string Inschool
        {
            get => _patientPrepImplementation.Inschool;
            set => _patientPrepImplementation.Inschool = value;
        }

        public string PopulationType
        {
            get => _patientPrepImplementation.PopulationType;
            set => _patientPrepImplementation.PopulationType = value;
        }

        public string KeyPopulationType
        {
            get => _patientPrepImplementation.KeyPopulationType;
            set => _patientPrepImplementation.KeyPopulationType = value;
        }

        public string Refferedfrom
        {
            get => _patientPrepImplementation.Refferedfrom;
            set => _patientPrepImplementation.Refferedfrom = value;
        }

        public string TransferIn
        {
            get => _patientPrepImplementation.TransferIn;
            set => _patientPrepImplementation.TransferIn = value;
        }

        public DateTime? TransferInDate
        {
            get => _patientPrepImplementation.TransferInDate;
            set => _patientPrepImplementation.TransferInDate = value;
        }

        public string TransferFromFacility
        {
            get => _patientPrepImplementation.TransferFromFacility;
            set => _patientPrepImplementation.TransferFromFacility = value;
        }

        public DateTime? DatefirstinitiatedinPrepCare
        {
            get => _patientPrepImplementation.DatefirstinitiatedinPrepCare;
            set => _patientPrepImplementation.DatefirstinitiatedinPrepCare = value;
        }

        public DateTime? DateStartedPrEPattransferringfacility
        {
            get => _patientPrepImplementation.DateStartedPrEPattransferringfacility;
            set => _patientPrepImplementation.DateStartedPrEPattransferringfacility = value;
        }

        public string ClientPreviouslyonPrep
        {
            get => _patientPrepImplementation.ClientPreviouslyonPrep;
            set => _patientPrepImplementation.ClientPreviouslyonPrep = value;
        }

        public string PrevPrepReg
        {
            get => _patientPrepImplementation.PrevPrepReg;
            set => _patientPrepImplementation.PrevPrepReg = value;
        }

        public DateTime? DateLastUsedPrev
        {
            get => _patientPrepImplementation.DateLastUsedPrev;
            set => _patientPrepImplementation.DateLastUsedPrev = value;
        }

        public DateTime? Date_Created
        {
            get => _patientPrepImplementation.Date_Created;
            set => _patientPrepImplementation.Date_Created = value;
        }

        public DateTime? Date_Last_Modified
        {
            get => _patientPrepImplementation.Date_Last_Modified;
            set => _patientPrepImplementation.Date_Last_Modified = value;
        }
    }
}
