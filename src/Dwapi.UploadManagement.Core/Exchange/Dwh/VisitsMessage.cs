﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class VisitsMessage:IVisitMessage
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
        [JsonProperty(PropertyName = "VisitExtracts")]
        public List<PatientVisitExtractView> Extracts { get; } = new List<PatientVisitExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PVisitExtracts")]
        public List<PatientVisitExtractView> VisitExtracts { get; set; } = new List<PatientVisitExtractView>();
        public bool HasContents => null != VisitExtracts && VisitExtracts.Any();

        public VisitsMessage()
        {
        }

        public VisitsMessage(PatientVisitExtractView patient)
        {
            patient.PatientExtractView.PatientVisitExtracts=new List<PatientVisitExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            VisitExtracts.Add(patient);
        }

        public VisitsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.PatientVisitExtracts.ToList();
            VisitExtracts = patient.PatientVisitExtracts.ToList();
        }

        public IMessage<PatientVisitExtractView> Generate(PatientVisitExtractView extract)
        {
            return new VisitsMessage(extract);
        }

        public List<IMessage<PatientVisitExtractView>> GenerateMessages(List<PatientVisitExtractView> extracts)
        {
            var messages = new List<IMessage<PatientVisitExtractView>>();
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
