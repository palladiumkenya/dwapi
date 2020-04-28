using System;
using Dwapi.SharedKernel.Model;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    public abstract class TempHTSExtractErrorSummary : Entity<Guid>
    {
        public virtual string Extract { get; set; }
        public virtual string Field { get; set; }
        public virtual string Type { get; set; }
        public virtual string Summary { get; set; }
        public virtual DateTime? DateGenerated { get; set; }
        public virtual int? SiteCode { get; set; }
        public virtual int? PatientPK { get; set; }
        public virtual string HtsNumber { get; set; }

        public virtual string FacilityName { get; set; }
        public virtual Guid RecordId { get; set; }

        public override string ToString()
        {
            return $"{SiteCode}-{HtsNumber}|{Summary}";
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
