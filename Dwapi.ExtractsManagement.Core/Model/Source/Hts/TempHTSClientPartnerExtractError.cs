using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    [Table("vTempHTSClientPartnerExtractError")]
    public class TempHTSClientPartnerExtractError : TempHTSExtract
    {

        [NotMapped]
        public virtual ICollection<TempHTSClientPartnerExtractErrorSummary> TempHtsClientPartnerExtractErrorSummaries { get; set; } = new List<TempHTSClientPartnerExtractErrorSummary>();
    }
}