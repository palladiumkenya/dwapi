using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientExtractErrorSummary")]
    public class TempPatientExtractErrorSummary : TempExtractErrorSummary
    {
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public string PatientSource { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public DateTime? DateConfirmedHIVPositive { get; set; }
        public string PreviousARTExposure { get; set; }
        public DateTime? PreviousARTStartDate { get; set; }
        public DateTime? LastVisit { get; set; }

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
