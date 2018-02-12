using System;
using System.ComponentModel.DataAnnotations;

namespace Dwapi.SharedKernel.DTOs
{
    public class PsmartStageDTO 
    {
        public Guid  Id { get; set; }
        public string Serial { get; set; }
        public string Demographics { get; set; }
        public string Encounters { get; set; }
        public string Emr { get; set; }
        public int? FacilityCode { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime DateStaged { get; set; }
    }
}