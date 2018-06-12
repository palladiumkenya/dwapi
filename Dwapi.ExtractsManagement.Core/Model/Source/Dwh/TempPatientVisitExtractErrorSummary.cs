using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    [Table("vTempPatientVisitExtractErrorSummary")]
    public class TempPatientVisitExtractErrorSummary: TempExtractErrorSummary
    {
        public DateTime? VisitDate { get; set; }
        public string Service { get; set; }
        public int? VisitId { get; set; }
        public string VisitType { get; set; }
        public int? WHOStage { get; set; }
        public string WABStage { get; set; }
        public string Pregnant { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDD { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string BP { get; set; }
        public string OI { get; set; }
        public DateTime? OIDate { get; set; }
        public string Adherence { get; set; }
        public string AdherenceCategory { get; set; }
        public DateTime? SubstitutionFirstlineRegimenDate { get; set; }
        public string SubstitutionFirstlineRegimenReason { get; set; }
        public DateTime? SubstitutionSecondlineRegimenDate { get; set; }
        public string SubstitutionSecondlineRegimenReason { get; set; }
        public DateTime? SecondlineRegimenChangeDate { get; set; }
        public string SecondlineRegimenChangeReason { get; set; }
        public string FamilyPlanningMethod { get; set; }
        public string PwP { get; set; }
        public decimal? GestationAge { get; set; }
        public DateTime? NextAppointmentDate { get; set; }

        /*public override void AddHeader(Row row)
        {
            base.AddHeader(row);
            row.Append(ConstructCell("VisitDate", CellValues.String),
                ConstructCell("Service", CellValues.String),
                ConstructCell("VisitId", CellValues.String),
                ConstructCell("VisitType", CellValues.String),
                ConstructCell("WHOStage", CellValues.String),
                ConstructCell("WABStage", CellValues.String),
                ConstructCell("Pregnant", CellValues.String),
                ConstructCell("LMP", CellValues.String),
                ConstructCell("EDD", CellValues.String),
                ConstructCell("Height", CellValues.String),
                ConstructCell("Weight", CellValues.String),
                ConstructCell("BP", CellValues.String),
                ConstructCell("OI", CellValues.String),
                ConstructCell("OIDate", CellValues.String),
                ConstructCell("Adherence", CellValues.String),
                ConstructCell("AdherenceCategory", CellValues.String),
                ConstructCell("SubstitutionFirstlineRegimenDate", CellValues.String),
                ConstructCell("SubstitutionFirstlineRegimenReason", CellValues.String),
                ConstructCell("SubstitutionSecondlineRegimenDate", CellValues.String),
                ConstructCell("SubstitutionSecondlineRegimenReason", CellValues.String),
                ConstructCell("SecondlineRegimenChangeDate", CellValues.String),
                ConstructCell("SecondlineRegimenChangeReason", CellValues.String),
                ConstructCell("FamilyPlanningMethod", CellValues.String),
                ConstructCell("PwP", CellValues.String),
                ConstructCell("GestationAge", CellValues.String),
                ConstructCell("NextAppointmentDate", CellValues.String)
            );
        }

        public override void AddRow(Row row)
        {
            base.AddRow(row);
            row.Append(
                ConstructCell(GetNullDateValue(VisitDate), CellValues.Date),
                ConstructCell(Service, CellValues.String),
                ConstructCell(GetNullNumberValue(VisitId), CellValues.Number),
                ConstructCell(VisitType, CellValues.String),
                ConstructCell(GetNullNumberValue(WHOStage), CellValues.Number),
                ConstructCell(WABStage, CellValues.String),
                ConstructCell(Pregnant, CellValues.String),
                ConstructCell(GetNullDateValue(LMP), CellValues.Date),
                ConstructCell(GetNullDateValue(EDD), CellValues.Date),
                ConstructCell(GetNullDecimalValue(Height), CellValues.Number),
                ConstructCell(GetNullDecimalValue(Weight), CellValues.Number),
                ConstructCell(BP, CellValues.String),
                ConstructCell(OI, CellValues.String),
                ConstructCell(GetNullDateValue(OIDate), CellValues.Date),
                ConstructCell(Adherence, CellValues.String),
                ConstructCell(AdherenceCategory, CellValues.String),
                ConstructCell(GetNullDateValue(SubstitutionFirstlineRegimenDate), CellValues.Date),
                ConstructCell(SubstitutionFirstlineRegimenReason, CellValues.String),
                ConstructCell(GetNullDateValue(SubstitutionSecondlineRegimenDate), CellValues.Date),
                ConstructCell(SubstitutionSecondlineRegimenReason, CellValues.String),
                ConstructCell(GetNullDateValue(SecondlineRegimenChangeDate), CellValues.Date),
                ConstructCell(SecondlineRegimenChangeReason, CellValues.String),
                ConstructCell(FamilyPlanningMethod, CellValues.String),
                ConstructCell(PwP, CellValues.String),
                ConstructCell(GetNullDecimalValue(GestationAge), CellValues.Number),
                ConstructCell(GetNullDateValue(NextAppointmentDate), CellValues.Date)
                );
        }*/
    }
}
