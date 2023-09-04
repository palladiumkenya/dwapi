using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Ct;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{
    [Table("vTempIITRiskScoresExtractErrorSummary")]

    public class TempIITRiskScoresExtractErrorSummary : TempExtractErrorSummary, IIITRiskScores
    {
        public string SourceSysUUID { get; set; }
        public string RiskScore  { get; set; }
       public string RiskFactors  { get; set; }
       public string RiskDescription  { get; set; }
       public DateTime? RiskEvaluationDate  { get; set; }
       public bool? Voided { get; set; }
       public DateTime? Date_Created  { get; set; }
       public DateTime? Date_Last_Modified  { get; set; }
    }
}