using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    [Table("vTempHTSClientLinkageExtractError")]
    public class TempHTSClientLinkageExtractError : TempHTSExtract
    {

        [NotMapped]
        public virtual ICollection<TempHTSClientLinkageExtractErrorSummary> TempHtsClientLinkageExtractErrorSummaries { get; set; } = new List<TempHTSClientLinkageExtractErrorSummary>();
    }
}