using System;
using Dwapi.Contracts.Ct;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Dwh
{
    public class IITRiskScoresExtract : ClientExtract, IIITRiskScores
    {
        public string FacilityName { get; set; }
        public string SourceSysUUID { get; set; }

        public decimal? RiskScore  { get; set; }
        public string RiskFactors  { get; set; }
        public string RiskDescription  { get; set; }
        public DateTime? RiskEvaluationDate  { get; set; }
        public DateTime? Date_Created  { get; set; }
        public DateTime? Date_Last_Modified  { get; set; }
    }
}