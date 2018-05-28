using System;
using System.ComponentModel.DataAnnotations;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    public abstract class TempExtractErrorSummary
    {
        [Key]
        public Guid Id { get; set; }
        public string Field { get; set; }
        public string Type { get; set; }
        public string Summary { get; set; }
        public DateTime? DateGenerated { get; set; }
        public int? PatientPK { get; set; }
        public string PatientID { get; set; }
        public int? FacilityId { get; set; }
        public int? SiteCode { get; set; }
        public virtual string FacilityName { get; set; }
        public Guid RecordId { get; set; }

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}|{Summary}";
        }

        //protected Cell ConstructCell(string value, CellValues dataType)
        //{
        //    return new Cell()
        //    {
        //        CellValue = new CellValue(value),
        //        DataType = new EnumValue<CellValues>(dataType)
        //    };
        //}
        //public virtual void AddHeader(Row row)
        //{
        //    row.Append(
        //        ConstructCell("SiteCode", CellValues.String),
        //        ConstructCell("PatientPK", CellValues.String),
        //        ConstructCell("PatientID", CellValues.String),
        //        ConstructCell("Type", CellValues.String),
        //        ConstructCell("Field", CellValues.String),
        //        ConstructCell("Summary", CellValues.String),
        //        ConstructCell("DateGenerated", CellValues.String),
        //        ConstructCell("RecordId", CellValues.String)
        //    );
        //}
        //public virtual void AddRow(Row row)
        //{
        //    row.Append(
        //        ConstructCell(SiteCode.ToString(), CellValues.Number),
        //        ConstructCell(PatientPK.ToString(), CellValues.Number),
        //        ConstructCell(PatientID, CellValues.String),
        //        ConstructCell(Type, CellValues.String),
        //        ConstructCell(Field, CellValues.String),
        //        ConstructCell(Summary, CellValues.String),
        //        ConstructCell(GetNullDateValue(DateGenerated), CellValues.Date),
        //        ConstructCell(RecordId.ToString(), CellValues.String)
        //    );
        //}
        protected string GetNullDateValue(DateTime? value)
        {
            return value?.ToString("dd-MMM-yyyy") ?? string.Empty;
        }
        protected string GetNullNumberValue(int? value)
        {
            return value?.ToString() ?? string.Empty;
        }
        protected string GetNullDecimalValue(decimal? value)
        {
            return value?.ToString() ?? string.Empty;
        }


    }
}
