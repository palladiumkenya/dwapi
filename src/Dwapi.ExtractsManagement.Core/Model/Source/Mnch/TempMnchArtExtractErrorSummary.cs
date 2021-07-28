using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Contracts.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Mnch
{
    [Table("vTempMnchArtExtractErrorSummary")]public class TempMnchArtExtractErrorSummary:TempMnchExtractErrorSummary,IMnchArt
    {
        public string Pkv { get; set; }
        public string PatientMnchID { get; set; }
        public string PatientHeiID { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public DateTime? StartARTDate { get; set; }
        public string StartRegimen { get; set; }
        public string StartRegimenLine { get; set; }
        public string StatusAtCCC { get; set; }
        public DateTime? LastARTDate { get; set; }
        public string LastRegimen { get; set; }
        public string LastRegimenLine { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
