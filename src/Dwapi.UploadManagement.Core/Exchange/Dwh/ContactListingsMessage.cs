﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class ContactListingsMessage:IContactListingMessage
    {
        public Facility Facility
        {
            get
            {
                var facility = Demographic?.GetFacility();
                if (null != facility)
                    return facility;
                return new Facility();
            }
        }
        public PatientExtractView Demographic { get; set; }
        [JsonProperty(PropertyName = "ContactListingExtracts")]
        public List<ContactListingExtractView> Extracts { get; } = new List<ContactListingExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PContactListingExtracts")]
        public List<ContactListingExtractView> ContactListingExtracts { get; set; } = new List<ContactListingExtractView>();
        public bool HasContents => null != ContactListingExtracts && ContactListingExtracts.Any();

        public ContactListingsMessage()
        {
        }

        public ContactListingsMessage(ContactListingExtractView patient)
        {
            patient.PatientExtractView.ContactListingExtracts=new List<ContactListingExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            ContactListingExtracts.Add(patient);
        }

        public ContactListingsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.ContactListingExtracts.ToList();
            ContactListingExtracts = patient.ContactListingExtracts.ToList();
        }

        public IMessage<ContactListingExtractView> Generate(ContactListingExtractView extract)
        {
            return new ContactListingsMessage(extract);
        }

        public List<IMessage<ContactListingExtractView>> GenerateMessages(List<ContactListingExtractView> extracts)
        {
            var messages = new List<IMessage<ContactListingExtractView>>();
            extracts.ForEach(e => messages.Add(Generate(e)));
            return messages;
        }

        private List<Guid> GetIds()
        {
            if (Extracts.Any())
                return Extracts.Select(x => x.Id).ToList();

            return new List<Guid>();
        }
    }
}
