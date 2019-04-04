using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    [Table("vTempHTSClientExtractErrorSummary")]
    public class TempHTSClientExtractErrorSummary: TempHTSExtractErrorSummary
    {
        public DateTime? VisitDate { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string KeyPop { get; set; }
        public string TestedBefore { get; set; }
        public int? MonthsLastTested { get; set; }
        public string ClientTestedAs { get; set; }
        public string StrategyHTS { get; set; }
        public string TestKitName1 { get; set; }
        public string TestKitLotNumber1 { get; set; }
        public DateTime? TestKitExpiryDate1 { get; set; }
        public string TestResultsHTS1 { get; set; }
        public string TestKitName2 { get; set; }
        public string TestKitLotNumber2 { get; set; }
        public string TestKitExpiryDate2 { get; set; }
        public string TestResultsHTS2 { get; set; }
        public string FinalResultHTS { get; set; }
        public string FinalResultsGiven { get; set; }
        public string TBScreeningHTS { get; set; }
        public string ClientSelfTested { get; set; }

        /*public override void AddHeader(Row row)
        {
            base.AddHeader(row);
            row.Append(
                ConstructCell("PatientSource ", CellValues.String),
                ConstructCell("RegistrationDate ", CellValues.String),
                ConstructCell("AgeLastVisit ", CellValues.String),
                ConstructCell("PreviousARTStartDate ", CellValues.String),
                ConstructCell("PreviousARTRegimen ", CellValues.String),
                ConstructCell("StartARTAtThisFacility ", CellValues.String),
                ConstructCell("StartARTDate ", CellValues.String),
                ConstructCell("StartRegimen ", CellValues.String),
                ConstructCell("StartRegimenLine ", CellValues.String),
                ConstructCell("LastARTDate ", CellValues.String),
                ConstructCell("LastRegimen ", CellValues.String),
                ConstructCell("LastRegimenLine ", CellValues.String),
                ConstructCell("LastVisit ", CellValues.String),
                ConstructCell("ExitReason ", CellValues.String),
                ConstructCell("ExitDate ", CellValues.String)

            );
        }

        public override void AddRow(Row row)
        {
            base.AddRow(row);
            row.Append(
                ConstructCell(PatientSource, CellValues.String),
                ConstructCell(GetNullDateValue(RegistrationDate), CellValues.Date),
                ConstructCell(GetNullDecimalValue(AgeLastVisit), CellValues.Number),
                ConstructCell(GetNullDateValue(PreviousARTStartDate), CellValues.Date),
                ConstructCell(PreviousARTRegimen, CellValues.String),
                ConstructCell(GetNullDateValue(StartARTAtThisFacility), CellValues.Date),
                ConstructCell(GetNullDateValue(StartARTDate), CellValues.Date),
                ConstructCell(StartRegimen, CellValues.String),
                ConstructCell(StartRegimenLine, CellValues.String),
                ConstructCell(GetNullDateValue(LastARTDate), CellValues.Date),
                ConstructCell(LastRegimen, CellValues.String),
                ConstructCell(LastRegimenLine, CellValues.String),
                ConstructCell(GetNullDateValue(LastVisit), CellValues.Date),
                ConstructCell(ExitReason, CellValues.String),
                ConstructCell(GetNullDateValue(ExitDate), CellValues.Date)

            );
        }*/
    }
}
