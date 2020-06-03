﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class AdverseEventsMessage:IAdverseEventsMessage
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
        [JsonProperty(PropertyName = "AdverseEventExtracts")]
        public List<PatientAdverseEventView> Extracts { get; }=new List<PatientAdverseEventView>();
        public List<Guid> SendIds => GetIds();
        [JsonProperty(PropertyName = "PAdverseEventExtracts")]
        public List<PatientAdverseEventView> AdverseEventExtracts { get; set; } = new List<PatientAdverseEventView>();
        public bool HasContents => null != AdverseEventExtracts && AdverseEventExtracts.Any();

        public AdverseEventsMessage()
        {
        }
        public AdverseEventsMessage(PatientAdverseEventView patient)
        {
            patient.PatientExtractView.PatientAdverseEventExtracts=new List<PatientAdverseEventView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            AdverseEventExtracts.Add(patient);
        }
        public AdverseEventsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.PatientAdverseEventExtracts.ToList();
            AdverseEventExtracts = patient.PatientAdverseEventExtracts.ToList();
        }
        public IMessage<PatientAdverseEventView> Generate(PatientAdverseEventView extract)
        {
             return new AdverseEventsMessage(extract);
        }
        public List<IMessage<PatientAdverseEventView>> GenerateMessages(List<PatientAdverseEventView> extracts)
        {
            var messages = new List<IMessage<PatientAdverseEventView>>();
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
