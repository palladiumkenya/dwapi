﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class IITRiskScoresMessage:IIITRiskScoresMessage
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
        [JsonProperty(PropertyName = "IITRiskScoresExtracts")]
        public List<IITRiskScoresExtractView> Extracts { get; } = new List<IITRiskScoresExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "IITRiskScoresExtracts")]
        public List<IITRiskScoresExtractView> IITRiskScoresExtracts { get; set; } = new List<IITRiskScoresExtractView>();
        public bool HasContents => null != IITRiskScoresExtracts && IITRiskScoresExtracts.Any();

        public IITRiskScoresMessage()
        {
        }

        public IITRiskScoresMessage(IITRiskScoresExtractView patient)
        {
            patient.PatientExtractView.IITRiskScoresExtracts=new List<IITRiskScoresExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            IITRiskScoresExtracts.Add(patient);
        }

        public IITRiskScoresMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.IITRiskScoresExtracts.ToList();
            IITRiskScoresExtracts = patient.IITRiskScoresExtracts.ToList();
        }

        public IMessage<IITRiskScoresExtractView> Generate(IITRiskScoresExtractView extract)
        {
            return new IITRiskScoresMessage(extract);
        }

        public List<IMessage<IITRiskScoresExtractView>> GenerateMessages(List<IITRiskScoresExtractView> extracts)
        {
            var messages = new List<IMessage<IITRiskScoresExtractView>>();
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
