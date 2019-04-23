using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    [Table("vTempHTSClientExtractError")]
    public class TempHTSClientExtractError : TempHTSClientExtract
    {

        [NotMapped]
        public virtual ICollection<TempPatientArtExtractErrorSummary> TempPatientArtExtractErrorSummaries { get; set; } = new List<TempPatientArtExtractErrorSummary>();
    }
}
