using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    [Table("ContactListingExtracts")]
    public class ContactListingExtractView : ContactListingExtract
    {
        public PatientExtractView PatientExtractView { get; set; }
    }
}
