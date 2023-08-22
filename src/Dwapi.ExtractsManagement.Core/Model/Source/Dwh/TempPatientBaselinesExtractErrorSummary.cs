using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientBaselinesExtractErrorSummary")]
    public class TempPatientBaselinesExtractErrorSummary : TempExtractErrorSummary
    {
        
        [NotMapped]
        public override string FacilityName { get; set; }
        public int? bCD4 { get; set; }
        public DateTime? bCD4Date { get; set; }
        public int? bWAB { get; set; }
        public DateTime? bWABDate { get; set; }
        public int? bWHO { get; set; }
        public DateTime? bWHODate { get; set; }
        public int? eWAB { get; set; }
        public DateTime? eWABDate { get; set; }
        public int? eCD4 { get; set; }
        public DateTime? eCD4Date { get; set; }
        public int? eWHO { get; set; }
        public DateTime? eWHODate { get; set; }
        public int? lastWHO { get; set; }
        public DateTime? lastWHODate { get; set; }
        public int? lastCD4 { get; set; }
        public DateTime? lastCD4Date { get; set; }
        public int? lastWAB { get; set; }
        public DateTime? lastWABDate { get; set; }
        public int? m12CD4 { get; set; }
        public DateTime? m12CD4Date { get; set; }
        public int? m6CD4 { get; set; }
        public DateTime? m6CD4Date { get; set; }
        public string RecordUUID { get; set; }


        /*public override void AddHeader(Row row)
        {
            base.AddHeader(row);
            row.Append(
                ConstructCell("bCD4", CellValues.String),
                ConstructCell("bCD4Date", CellValues.String),
                ConstructCell("bWAB", CellValues.String),
                ConstructCell("bWABDate", CellValues.String),
                ConstructCell("bWHO", CellValues.String),
                ConstructCell("bWHODate", CellValues.String),
                ConstructCell("eWAB", CellValues.String),
                ConstructCell("eWABDate", CellValues.String),
                ConstructCell("eCD4", CellValues.String),
                ConstructCell("eCD4Date", CellValues.String),
                ConstructCell("eWHO", CellValues.String),
                ConstructCell("eWHODate", CellValues.String),
                ConstructCell("lastWHO", CellValues.String),
                ConstructCell("lastWHODate", CellValues.String),
                ConstructCell("lastCD4", CellValues.String),
                ConstructCell("lastCD4Date", CellValues.String),
                ConstructCell("lastWAB", CellValues.String),
                ConstructCell("lastWABDate", CellValues.String),
                ConstructCell("m12CD4", CellValues.String),
                ConstructCell("m12CD4Date", CellValues.String),
                ConstructCell("m6CD4", CellValues.String),
                ConstructCell("m6CD4Date", CellValues.String)
            );
        }

        public override void AddRow(Row row)
        {
            base.AddRow(row);
            row.Append(
                ConstructCell(GetNullNumberValue(bCD4), CellValues.Number),
                ConstructCell(GetNullDateValue(bCD4Date), CellValues.Date),
                ConstructCell(GetNullNumberValue(bWAB), CellValues.Number),
                ConstructCell(GetNullDateValue(bWABDate), CellValues.Date),
                ConstructCell(GetNullNumberValue(bWHO), CellValues.Number),
                ConstructCell(GetNullDateValue(bWHODate), CellValues.Date),
                ConstructCell(GetNullNumberValue(eWAB), CellValues.Number),
                ConstructCell(GetNullDateValue(eWABDate), CellValues.Date),
                ConstructCell(GetNullNumberValue(eCD4), CellValues.Number),
                ConstructCell(GetNullDateValue(eCD4Date), CellValues.Date),
                ConstructCell(GetNullNumberValue(eWHO), CellValues.Number),
                ConstructCell(GetNullDateValue(eWHODate), CellValues.Date),
                ConstructCell(GetNullNumberValue(lastWHO), CellValues.Number),
                ConstructCell(GetNullDateValue(lastWHODate), CellValues.Date),
                ConstructCell(GetNullNumberValue(lastCD4), CellValues.Number),
                ConstructCell(GetNullDateValue(lastCD4Date), CellValues.Date),
                ConstructCell(GetNullNumberValue(lastWAB), CellValues.Number),
                ConstructCell(GetNullDateValue(lastWABDate), CellValues.Date),
                ConstructCell(GetNullNumberValue(m12CD4), CellValues.Number),
                ConstructCell(GetNullDateValue(m12CD4Date), CellValues.Date),
                ConstructCell(GetNullNumberValue(m6CD4), CellValues.Number),
                ConstructCell(GetNullDateValue(m6CD4Date), CellValues.Date)
            );
        }*/

    }
}
