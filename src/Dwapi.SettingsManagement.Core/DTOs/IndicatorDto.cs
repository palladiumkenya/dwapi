using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SettingsManagement.Core.DTOs
{
     public class IndicatorDto
       {
        public string Indicator { get; set; }
        public string IndicatorValue { get; set; }
    }
}
