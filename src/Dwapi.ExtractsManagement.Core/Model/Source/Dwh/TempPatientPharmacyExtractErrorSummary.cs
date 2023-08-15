using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientPharmacyExtractErrorSummary")]
    public class TempPatientPharmacyExtractErrorSummary : TempExtractErrorSummary
    {
        [NotMapped]
        public override string FacilityName { get; set; }


        public int? VisitID { get; set; }
        public string Drug { get; set; }
        public string Provider { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string RegimenLine { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }
        public string RecordUUID { get; set; }


        /*public override void AddHeader(Row row)
        {
            base.AddHeader(row);
            row.Append(
                ConstructCell("VisitID", CellValues.String),
                ConstructCell("Drug", CellValues.String),
                ConstructCell("Provider", CellValues.String),
                ConstructCell("DispenseDate", CellValues.String),
                ConstructCell("Duration", CellValues.String),
                ConstructCell("ExpectedReturn", CellValues.String),
                ConstructCell("TreatmentType", CellValues.String),
                ConstructCell("RegimenLine", CellValues.String),
                ConstructCell("PeriodTaken", CellValues.String),
                ConstructCell("ProphylaxisType", CellValues.String)
            );
        }

        public override void AddRow(Row row)
        {
            base.AddRow(row);
            row.Append(
                ConstructCell(GetNullNumberValue(VisitID), CellValues.Number),
                ConstructCell(Drug, CellValues.String),
                ConstructCell(Provider, CellValues.String),
                ConstructCell(GetNullDateValue(DispenseDate), CellValues.Date),
                ConstructCell(GetNullDecimalValue(Duration), CellValues.Number),
                ConstructCell(GetNullDateValue(ExpectedReturn), CellValues.Date),
                ConstructCell(TreatmentType, CellValues.String),
                ConstructCell(RegimenLine, CellValues.String),
                ConstructCell(PeriodTaken, CellValues.String),
                ConstructCell(ProphylaxisType, CellValues.String)
            );
        }*/
    }
}
