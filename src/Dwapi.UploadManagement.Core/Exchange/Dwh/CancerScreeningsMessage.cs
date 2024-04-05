﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class CancerScreeningsMessage:ICancerScreeningMessage
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
        [JsonProperty(PropertyName = "CancerScreeningExtracts")]
        public List<CancerScreeningExtractView> Extracts { get; } = new List<CancerScreeningExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "CancerScreeningExtracts")]
        public List<CancerScreeningExtractView> CancerScreeningExtracts { get; set; } = new List<CancerScreeningExtractView>();
        public bool HasContents => null != CancerScreeningExtracts && CancerScreeningExtracts.Any();

        public CancerScreeningsMessage()
        {
        }

        public CancerScreeningsMessage(CancerScreeningExtractView patient)
        {
            patient.PatientExtractView.CancerScreeningExtracts=new List<CancerScreeningExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            CancerScreeningExtracts.Add(patient);
        }

        public CancerScreeningsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.CancerScreeningExtracts.ToList();
            CancerScreeningExtracts = patient.CancerScreeningExtracts.ToList();
        }

        public IMessage<CancerScreeningExtractView> Generate(CancerScreeningExtractView extract)
        {
            return new CancerScreeningsMessage(extract);
        }

        public List<IMessage<CancerScreeningExtractView>> GenerateMessages(List<CancerScreeningExtractView> extracts)
        {
            var messages = new List<IMessage<CancerScreeningExtractView>>();
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
