using System;
using Humanizer;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Mts.Dto
{
    public class IndicatorExtractDto
    {
        public Guid Id { get; set; }
        public string Indicator { get; set; }
        public string Description { get; set; }
        public string IndicatorValue { get; set; }
        public DateTime? IndicatorDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int Rank { get; set; }
        public string TimeAgo => GetTimeAgo();

        private string GetTimeAgo()
        {
            return DateCreated.Humanize(false);
        }
    }
}
