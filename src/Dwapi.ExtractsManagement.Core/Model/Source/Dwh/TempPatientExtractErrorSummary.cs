using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientExtractErrorSummary")]
    public class TempPatientExtractErrorSummary : TempExtractErrorSummary
    {
        public string FacilityName { get; set; }

        [NotMapped]
        public string Gender { get; set; }
        [NotMapped]
        public DateTime? DOB { get; set; }
        [NotMapped]
        public DateTime? RegistrationDate { get; set; }
        [NotMapped]
        public DateTime? RegistrationAtCCC { get; set; }
        [NotMapped]
        public string PatientSource { get; set; }
        [NotMapped]
        public string MaritalStatus { get; set; }
        [NotMapped]
        public string EducationLevel { get; set; }
        [NotMapped]
        public DateTime? DateConfirmedHIVPositive { get; set; }
        [NotMapped]
        public string PreviousARTExposure { get; set; }
        [NotMapped]
        public DateTime? PreviousARTStartDate { get; set; }
        [NotMapped]
        public DateTime? LastVisit { get; set; }

        public string Orphan { get; set; }
        public string Inschool { get; set; }
        public string PatientType { get; set; }
        public string PopulationType { get; set; }
        public string KeyPopulationType { get; set; }
        public string PatientResidentCounty { get; set; }
        public string PatientResidentSubCounty { get; set; }
        public string PatientResidentLocation { get; set; }
        public string PatientResidentSubLocation { get; set; }
        public string PatientResidentWard { get; set; }
        public string PatientResidentVillage { get; set; }
        public DateTime? TransferInDate { get; set; }
        public string NUPI { get; set; }
       

        //public override void AddHeader(Row row)
        //{
        //    base.AddHeader(row);
        //    row.Append(
        //        ConstructCell("Gender", CellValues.String),
        //        ConstructCell("DOB", CellValues.String),
        //        ConstructCell("RegistrationDate", CellValues.String),
        //        ConstructCell("RegistrationAtCCC", CellValues.String),
        //        ConstructCell("PatientSource", CellValues.String),
        //        ConstructCell("MaritalStatus", CellValues.String),
        //        ConstructCell("EducationLevel", CellValues.String),
        //        ConstructCell("DateConfirmedHIVPositive", CellValues.String),
        //        ConstructCell("PreviousARTExposure", CellValues.String),
        //        ConstructCell("PreviousARTStartDate", CellValues.String),
        //        ConstructCell("LastVisit", CellValues.String)
        //    );
        //}

        //public override void AddRow(Row row)
        //{
        //    base.AddRow(row);
        //    row.Append(
        //        ConstructCell(Gender, CellValues.String),
        //        ConstructCell(GetNullDateValue(DOB), CellValues.Date),
        //        ConstructCell(GetNullDateValue(RegistrationDate), CellValues.Date),
        //        ConstructCell(GetNullDateValue(RegistrationAtCCC), CellValues.Date),
        //        ConstructCell(PatientSource, CellValues.String),
        //        ConstructCell(MaritalStatus, CellValues.String),
        //        ConstructCell(EducationLevel, CellValues.String),
        //        ConstructCell(GetNullDateValue(DateConfirmedHIVPositive), CellValues.Date),
        //        ConstructCell(PreviousARTExposure, CellValues.String),
        //        ConstructCell(GetNullDateValue(PreviousARTStartDate), CellValues.Date),
        //        ConstructCell(GetNullDateValue(LastVisit), CellValues.Date)
        //    );
        //}

    }
}
