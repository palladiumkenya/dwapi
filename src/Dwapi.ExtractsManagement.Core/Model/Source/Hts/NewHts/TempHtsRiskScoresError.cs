using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    [Table("vTempHtsRiskScoresError")]
    public class TempHtsRiskScoresError
    {
         public virtual ICollection<TempHtsRiskScoresErrorSummary> TempHtsRiskScoresErrorSummaries { get; set; } = new List<TempHtsRiskScoresErrorSummary>();
         
         public Guid Id { get; set; }
         public string SourceSysUUID { get; set; }
        public decimal? RiskScore { get; set; }
        public string RiskFactors { get; set; }
        public string Description { get; set; }
        public DateTime? EvaluationDate { get; set; }
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }

    }
}