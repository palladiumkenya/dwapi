using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientStatusExtractErrorSummary")]
    public class TempPatientStatusExtractErrorSummary: TempExtractErrorSummary
    {

        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public string PatientUUID { get; set; }


        /*public override void AddHeader(Row row)
        {
            base.AddHeader(row);
            row.Append(
                ConstructCell("ExitDescription", CellValues.String),
                ConstructCell("ExitDate", CellValues.String),
                ConstructCell("ExitReason", CellValues.String)
            );
        }
   
        public override void AddRow(Row row)
        {
            base.AddRow(row);
            row.Append(
                ConstructCell(ExitDescription, CellValues.String),
                ConstructCell(GetNullDateValue(ExitDate), CellValues.Date),
                ConstructCell(ExitReason, CellValues.String)
            );
        }*/

    }
}
