using Dwapi.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dwapi.Domain
{
    [Table("vTempPatientArtExtractErrorSummary")]
    public class TempPatientArtExtractErrorSummary : TempExtractErrorSummary
    {
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string PatientSource { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public decimal? AgeLastVisit { get; set; }
        public DateTime? PreviousARTStartDate { get; set; }
        public string PreviousARTRegimen { get; set; }
        public DateTime? StartARTAtThisFacility { get; set; }
        public DateTime? StartARTDate { get; set; }
        public string StartRegimen { get; set; }
        public string StartRegimenLine { get; set; }
        public DateTime? LastARTDate { get; set; }
        public string LastRegimen { get; set; }
        public string LastRegimenLine { get; set; }
        public DateTime? LastVisit { get; set; }
        public string ExitReason { get; set; }
        public DateTime? ExitDate { get; set; }

        //public override void AddHeader(Row row)
        //{
        //    base.AddHeader(row);
        //    row.Append(
        //        ConstructCell("PatientSource ", CellValues.String),
        //        ConstructCell("RegistrationDate ", CellValues.String),
        //        ConstructCell("AgeLastVisit ", CellValues.String),
        //        ConstructCell("PreviousARTStartDate ", CellValues.String),
        //        ConstructCell("PreviousARTRegimen ", CellValues.String),
        //        ConstructCell("StartARTAtThisFacility ", CellValues.String),
        //        ConstructCell("StartARTDate ", CellValues.String),
        //        ConstructCell("StartRegimen ", CellValues.String),
        //        ConstructCell("StartRegimenLine ", CellValues.String),
        //        ConstructCell("LastARTDate ", CellValues.String),
        //        ConstructCell("LastRegimen ", CellValues.String),
        //        ConstructCell("LastRegimenLine ", CellValues.String),
        //        ConstructCell("LastVisit ", CellValues.String),
        //        ConstructCell("ExitReason ", CellValues.String),
        //        ConstructCell("ExitDate ", CellValues.String)

        //    );
        //}

        //public override void AddRow(Row row)
        //{
        //    base.AddRow(row);
        //    row.Append(
        //        ConstructCell(PatientSource, CellValues.String),
        //        ConstructCell(GetNullDateValue(RegistrationDate), CellValues.Date),
        //        ConstructCell(GetNullDecimalValue(AgeLastVisit), CellValues.Number),
        //        ConstructCell(GetNullDateValue(PreviousARTStartDate), CellValues.Date),
        //        ConstructCell(PreviousARTRegimen, CellValues.String),
        //        ConstructCell(GetNullDateValue(StartARTAtThisFacility), CellValues.Date),
        //        ConstructCell(GetNullDateValue(StartARTDate), CellValues.Date),
        //        ConstructCell(StartRegimen, CellValues.String),
        //        ConstructCell(StartRegimenLine, CellValues.String),
        //        ConstructCell(GetNullDateValue(LastARTDate), CellValues.Date),
        //        ConstructCell(LastRegimen, CellValues.String),
        //        ConstructCell(LastRegimenLine, CellValues.String),
        //        ConstructCell(GetNullDateValue(LastVisit), CellValues.Date),
        //        ConstructCell(ExitReason, CellValues.String),
        //        ConstructCell(GetNullDateValue(ExitDate), CellValues.Date)

        //    );
        //}
    }
}
