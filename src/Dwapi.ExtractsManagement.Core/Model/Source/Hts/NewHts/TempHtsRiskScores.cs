﻿using System;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    public class TempHtsRiskScores : TempHtsExtract
    {
        public string SourceSysUUID { get; set; }
        public decimal? RiskScore { get; set; }
        public string RiskFactors { get; set; }
        public string Description { get; set; }
        public DateTime? EvaluationDate { get; set; }
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        

    }
}