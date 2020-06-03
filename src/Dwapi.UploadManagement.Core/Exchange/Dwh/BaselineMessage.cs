﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class BaselineMessage : IBaselinesMessage
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

        public PatientExtractView Demographic { get; }

        [JsonProperty(PropertyName = "BaselinesExtracts")]
        public List<PatientBaselinesExtractView> Extracts { get; } = new List<PatientBaselinesExtractView>();

        public List<Guid> SendIds => GetIds();

        [JsonProperty(PropertyName = "PBaselinesExtracts")]
        public List<PatientBaselinesExtractView> BaselinesExtracts { get; set; } =
            new List<PatientBaselinesExtractView>();

        public bool HasContents => null != BaselinesExtracts && BaselinesExtracts.Any();

        public BaselineMessage()
        {
        }

        public BaselineMessage(PatientBaselinesExtractView patient)
        {
            patient.PatientExtractView.PatientBaselinesExtracts = new List<PatientBaselinesExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            BaselinesExtracts.Add(patient);
        }

        public BaselineMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.PatientBaselinesExtracts.ToList();
            BaselinesExtracts = patient.PatientBaselinesExtracts.ToList();
        }

        public IMessage<PatientBaselinesExtractView> Generate(PatientBaselinesExtractView extract)
        {
            return new BaselineMessage(extract);
        }

        public List<IMessage<PatientBaselinesExtractView>> GenerateMessages(List<PatientBaselinesExtractView> extracts)
        {
            var messages = new List<IMessage<PatientBaselinesExtractView>>();
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
