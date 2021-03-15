﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class DepressionScreeningsMessage:IDepressionScreeningMessage
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
        [JsonProperty(PropertyName = "DepressionScreeningExtracts")]
        public List<DepressionScreeningExtractView> Extracts { get; } = new List<DepressionScreeningExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PDepressionScreeningExtracts")]
        public List<DepressionScreeningExtractView> DepressionScreeningExtracts { get; set; } = new List<DepressionScreeningExtractView>();
        public bool HasContents => null != DepressionScreeningExtracts && DepressionScreeningExtracts.Any();

        public DepressionScreeningsMessage()
        {
        }

        public DepressionScreeningsMessage(DepressionScreeningExtractView patient)
        {
            patient.PatientExtractView.DepressionScreeningExtracts=new List<DepressionScreeningExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            DepressionScreeningExtracts.Add(patient);
        }

        public DepressionScreeningsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.DepressionScreeningExtracts.ToList();
            DepressionScreeningExtracts = patient.DepressionScreeningExtracts.ToList();
        }

        public IMessage<DepressionScreeningExtractView> Generate(DepressionScreeningExtractView extract)
        {
            return new DepressionScreeningsMessage(extract);
        }

        public List<IMessage<DepressionScreeningExtractView>> GenerateMessages(List<DepressionScreeningExtractView> extracts)
        {
            var messages = new List<IMessage<DepressionScreeningExtractView>>();
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
