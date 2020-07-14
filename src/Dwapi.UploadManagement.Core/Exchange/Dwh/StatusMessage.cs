﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class StatusMessage:IStatusMessage
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
        [JsonProperty(PropertyName = "StatusExtracts")]
        public List<PatientStatusExtractView> Extracts { get; } = new List<PatientStatusExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PStatusExtracts")]
        public List<PatientStatusExtractView> StatusExtracts { get; set; } = new List<PatientStatusExtractView>();
        public bool HasContents => null != StatusExtracts && StatusExtracts.Any();

        public StatusMessage()
        {
        }

        public StatusMessage(PatientStatusExtractView patient)
        {
            patient.PatientExtractView.PatientStatusExtracts=new List<PatientStatusExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            StatusExtracts.Add(patient);
        }

        public StatusMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.PatientStatusExtracts.ToList();
            StatusExtracts = patient.PatientStatusExtracts.ToList();
        }

        public IMessage<PatientStatusExtractView> Generate(PatientStatusExtractView extract)
        {
            return new StatusMessage(extract);
        }

        public List<IMessage<PatientStatusExtractView>> GenerateMessages(List<PatientStatusExtractView> extracts)
        {
            var messages = new List<IMessage<PatientStatusExtractView>>();
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
