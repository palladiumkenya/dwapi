﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class OvcsMessage:IOvcMessage
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
        [JsonProperty(PropertyName = "OvcExtracts")]
        public List<OvcExtractView> Extracts { get; } = new List<OvcExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "POvcExtracts")]
        public List<OvcExtractView> OvcExtracts { get; set; } = new List<OvcExtractView>();
        public bool HasContents => null != OvcExtracts && OvcExtracts.Any();

        public OvcsMessage()
        {
        }

        public OvcsMessage(OvcExtractView patient)
        {
            patient.PatientExtractView.OvcExtracts=new List<OvcExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            OvcExtracts.Add(patient);
        }

        public OvcsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.OvcExtracts.ToList();
            OvcExtracts = patient.OvcExtracts.ToList();
        }

        public IMessage<OvcExtractView> Generate(OvcExtractView extract)
        {
            return new OvcsMessage(extract);
        }

        public List<IMessage<OvcExtractView>> GenerateMessages(List<OvcExtractView> extracts)
        {
            var messages = new List<IMessage<OvcExtractView>>();
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
