﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class GbvScreeningsMessage:IGbvScreeningMessage
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
        [JsonProperty(PropertyName = "GbvScreeningExtracts")]
        public List<GbvScreeningExtractView> Extracts { get; } = new List<GbvScreeningExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PGbvScreeningExtracts")]
        public List<GbvScreeningExtractView> GbvScreeningExtracts { get; set; } = new List<GbvScreeningExtractView>();
        public bool HasContents => null != GbvScreeningExtracts && GbvScreeningExtracts.Any();

        public GbvScreeningsMessage()
        {
        }

        public GbvScreeningsMessage(GbvScreeningExtractView patient)
        {
            patient.PatientExtractView.GbvScreeningExtracts=new List<GbvScreeningExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            GbvScreeningExtracts.Add(patient);
        }

        public GbvScreeningsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.GbvScreeningExtracts.ToList();
            GbvScreeningExtracts = patient.GbvScreeningExtracts.ToList();
        }

        public IMessage<GbvScreeningExtractView> Generate(GbvScreeningExtractView extract)
        {
            return new GbvScreeningsMessage(extract);
        }

        public List<IMessage<GbvScreeningExtractView>> GenerateMessages(List<GbvScreeningExtractView> extracts)
        {
            var messages = new List<IMessage<GbvScreeningExtractView>>();
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
