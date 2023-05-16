﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class CervicalCancerScreeningsMessage:ICervicalCancerScreeningMessage
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
        [JsonProperty(PropertyName = "CervicalCancerScreeningExtracts")]
        public List<CervicalCancerScreeningExtractView> Extracts { get; } = new List<CervicalCancerScreeningExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PCervicalCancerScreeningExtracts")]
        public List<CervicalCancerScreeningExtractView> CervicalCancerScreeningExtracts { get; set; } = new List<CervicalCancerScreeningExtractView>();
        public bool HasContents => null != CervicalCancerScreeningExtracts && CervicalCancerScreeningExtracts.Any();

        public CervicalCancerScreeningsMessage()
        {
        }

        public CervicalCancerScreeningsMessage(CervicalCancerScreeningExtractView patient)
        {
            patient.PatientExtractView.CervicalCancerScreeningExtracts=new List<CervicalCancerScreeningExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            CervicalCancerScreeningExtracts.Add(patient);
        }

        public CervicalCancerScreeningsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.CervicalCancerScreeningExtracts.ToList();
            CervicalCancerScreeningExtracts = patient.CervicalCancerScreeningExtracts.ToList();
        }

        public IMessage<CervicalCancerScreeningExtractView> Generate(CervicalCancerScreeningExtractView extract)
        {
            return new CervicalCancerScreeningsMessage(extract);
        }

        public List<IMessage<CervicalCancerScreeningExtractView>> GenerateMessages(List<CervicalCancerScreeningExtractView> extracts)
        {
            var messages = new List<IMessage<CervicalCancerScreeningExtractView>>();
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
