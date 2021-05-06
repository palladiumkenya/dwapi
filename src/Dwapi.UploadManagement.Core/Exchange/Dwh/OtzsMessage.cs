﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class OtzsMessage:IOtzMessage
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
        [JsonProperty(PropertyName = "OtzExtracts")]
        public List<OtzExtractView> Extracts { get; } = new List<OtzExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "POtzExtracts")]
        public List<OtzExtractView> OtzExtracts { get; set; } = new List<OtzExtractView>();
        public bool HasContents => null != OtzExtracts && OtzExtracts.Any();

        public OtzsMessage()
        {
        }

        public OtzsMessage(OtzExtractView patient)
        {
            patient.PatientExtractView.OtzExtracts=new List<OtzExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            OtzExtracts.Add(patient);
        }

        public OtzsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.OtzExtracts.ToList();
            OtzExtracts = patient.OtzExtracts.ToList();
        }

        public IMessage<OtzExtractView> Generate(OtzExtractView extract)
        {
            return new OtzsMessage(extract);
        }

        public List<IMessage<OtzExtractView>> GenerateMessages(List<OtzExtractView> extracts)
        {
            var messages = new List<IMessage<OtzExtractView>>();
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
