﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class IptsMessage:IIptMessage
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
        [JsonProperty(PropertyName = "IptExtracts")]
        public List<IptExtractView> Extracts { get; } = new List<IptExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PIptExtracts")]
        public List<IptExtractView> IptExtracts { get; set; } = new List<IptExtractView>();
        public bool HasContents => null != IptExtracts && IptExtracts.Any();

        public IptsMessage()
        {
        }

        public IptsMessage(IptExtractView patient)
        {
            patient.PatientExtractView.IptExtracts=new List<IptExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            IptExtracts.Add(patient);
        }

        public IptsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.IptExtracts.ToList();
            IptExtracts = patient.IptExtracts.ToList();
        }

        public IMessage<IptExtractView> Generate(IptExtractView extract)
        {
            return new IptsMessage(extract);
        }

        public List<IMessage<IptExtractView>> GenerateMessages(List<IptExtractView> extracts)
        {
            var messages = new List<IMessage<IptExtractView>>();
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
