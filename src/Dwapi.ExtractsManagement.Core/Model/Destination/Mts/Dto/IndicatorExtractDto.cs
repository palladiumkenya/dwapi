using System;
using Humanizer;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Mts.Dto
{
    public class IndicatorExtractDto
    {
        public Guid Id { get; set; }
        public string Indicator { get; set; }
        public string IndicatorValue { get; set; }
        public DateTime? IndicatorDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int Rank => GetRank();
        public string TimeAgo => GetTimeAgo();

        private int GetRank()
        {
            if (Indicator.Contains("EMR_ETL_Refresh"))
                return 99;

            if (Indicator.StartsWith("H"))
                return 1;

            if (Indicator.StartsWith("R"))
                return 5;
            if (Indicator.StartsWith("LAST"))
                return 98;
            return 3;
        }
        private string GetTimeAgo()
        {
            return DateCreated.Humanize(false);
        }
    }
}
