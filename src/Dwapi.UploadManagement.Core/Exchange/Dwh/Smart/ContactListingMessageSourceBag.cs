using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class ContactListingMessageSourceBag : MessageSourceBag<ContactListingExtractView>{
        public override string EndPoint => "ContactListing";
        public override string ExtractName => $"{nameof(ContactListingExtract)}";}
}
