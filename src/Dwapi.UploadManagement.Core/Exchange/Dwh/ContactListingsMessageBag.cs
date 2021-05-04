﻿using System;
using System.Collections.Generic;
using System.Linq;
 using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
 using Dwapi.SharedKernel.Enum;
 using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class ContactListingsMessageBag:IContactListingMessageBag
    {
        private int stake = 5;
        public string EndPoint => "ContactListing";
        public IMessage<ContactListingExtractView> Message { get; set; }
        public List<IMessage<ContactListingExtractView>> Messages { get; set; }
        public List<Guid> SendIds => GetIds();
        public string ExtractName => "ContactListingExtract";
        public ExtractType ExtractType => ExtractType.ContactListing;
        public string Docket  => "NDWH";
        public string DocketExtract => nameof(ContactListingExtract);

        public int GetProgress(int count, int total)
        {
            if (total == 0)
                return stake;

            var percentageStake=  ((float)count / (float)total) * stake;
            return (int) percentageStake;
        }

        public ContactListingsMessageBag()
        {
        }

        public ContactListingsMessageBag(IMessage<ContactListingExtractView> message)
        {
            Message = message;
        }

        public ContactListingsMessageBag(List<IMessage<ContactListingExtractView>> messages)
        {
            Messages = messages;
        }

        public ContactListingsMessageBag(ContactListingsMessage message)
        {
            Message = message;
        }

        public static ContactListingsMessageBag Create(PatientExtractView patient)
        {
            var message = new ContactListingsMessage(patient);
            return new ContactListingsMessageBag(message);
        }


        public IMessageBag<ContactListingExtractView> Generate(List<ContactListingExtractView> extracts)
        {
            var messages = new List<IMessage<ContactListingExtractView>>();
            foreach (var artExtractView in extracts)
            {
                var message = new ContactListingsMessage(artExtractView);
                messages.Add(message);
            }

            return new ContactListingsMessageBag(messages);
        }

        private List<Guid> GetIds()
        {
            var ids= Messages.SelectMany(x => x.SendIds).ToList();
            return ids;
        }
    }
}
