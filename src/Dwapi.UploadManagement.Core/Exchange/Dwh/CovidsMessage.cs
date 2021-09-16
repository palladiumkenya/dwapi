﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class CovidsMessage:ICovidMessage
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
        [JsonProperty(PropertyName = "CovidExtracts")]
        public List<CovidExtractView> Extracts { get; } = new List<CovidExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PCovidExtracts")]
        public List<CovidExtractView> CovidExtracts { get; set; } = new List<CovidExtractView>();
        public bool HasContents => null != CovidExtracts && CovidExtracts.Any();

        public CovidsMessage()
        {
        }

        public CovidsMessage(CovidExtractView patient)
        {
            patient.PatientExtractView.CovidExtracts=new List<CovidExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            CovidExtracts.Add(patient);
        }

        public CovidsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.CovidExtracts.ToList();
            CovidExtracts = patient.CovidExtracts.ToList();
        }

        public IMessage<CovidExtractView> Generate(CovidExtractView extract)
        {
            return new CovidsMessage(extract);
        }

        public List<IMessage<CovidExtractView>> GenerateMessages(List<CovidExtractView> extracts)
        {
            var messages = new List<IMessage<CovidExtractView>>();
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
