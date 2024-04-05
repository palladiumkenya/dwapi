using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.SharedKernel.Enum;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Ct;
using Dwapi.UploadManagement.Core.Model.Dwh;
using System.Collections.Generic;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh.Smart
{
    public class ContactListingMessageSourceBag : MessageSourceBag<ContactListingExtractView>{
        public override string EndPoint => "ContactListing";
        public override string ExtractName => $"{nameof(ContactListingExtract)}";

        public  override string DocketExtract => ExtractName;
        public override ExtractType ExtractType => ExtractType.ContactListing;

        public List<ContactListingExtractView> _ContactListingExtractView { get; set; }
        public ContactListingMessageSourceBag()
        {
        }
        public ContactListingMessageSourceBag(List<ContactListingExtractView> contactListingExtractView)
        {
            _ContactListingExtractView = contactListingExtractView;
        }
    }
}
