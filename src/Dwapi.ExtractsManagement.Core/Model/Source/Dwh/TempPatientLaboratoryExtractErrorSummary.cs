using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientLaboratoryExtractErrorSummary")]
    public class TempPatientLaboratoryExtractErrorSummary: TempExtractErrorSummary
    {
        public string Reason { get; set; }
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }


        /*public override void AddHeader(Row row)
        {
            base.AddHeader(row);
            row.Append(
                ConstructCell("OrderedByDate", CellValues.String),
                ConstructCell("ReportedByDate", CellValues.String),
                ConstructCell("TestName", CellValues.String),
                ConstructCell("EnrollmentTest", CellValues.String),
                ConstructCell("TestResult", CellValues.String));


        }

        public override void AddRow(Row row)
        {
            base.AddRow(row);

            row.Append(
                ConstructCell(GetNullDateValue(OrderedByDate), CellValues.Date),
                ConstructCell(GetNullDateValue(ReportedByDate), CellValues.Date),
                ConstructCell(TestName, CellValues.String),
                ConstructCell(GetNullNumberValue(EnrollmentTest), CellValues.Number),
                ConstructCell(TestResult, CellValues.String));

        }*/
    }
}
