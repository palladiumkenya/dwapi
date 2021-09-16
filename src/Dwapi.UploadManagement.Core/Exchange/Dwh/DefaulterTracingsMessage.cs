﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class DefaulterTracingsMessage:IDefaulterTracingMessage
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
        [JsonProperty(PropertyName = "DefaulterTracingExtracts")]
        public List<DefaulterTracingExtractView> Extracts { get; } = new List<DefaulterTracingExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PDefaulterTracingExtracts")]
        public List<DefaulterTracingExtractView> DefaulterTracingExtracts { get; set; } = new List<DefaulterTracingExtractView>();
        public bool HasContents => null != DefaulterTracingExtracts && DefaulterTracingExtracts.Any();

        public DefaulterTracingsMessage()
        {
        }

        public DefaulterTracingsMessage(DefaulterTracingExtractView patient)
        {
            patient.PatientExtractView.DefaulterTracingExtracts=new List<DefaulterTracingExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            DefaulterTracingExtracts.Add(patient);
        }

        public DefaulterTracingsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.DefaulterTracingExtracts.ToList();
            DefaulterTracingExtracts = patient.DefaulterTracingExtracts.ToList();
        }

        public IMessage<DefaulterTracingExtractView> Generate(DefaulterTracingExtractView extract)
        {
            return new DefaulterTracingsMessage(extract);
        }

        public List<IMessage<DefaulterTracingExtractView>> GenerateMessages(List<DefaulterTracingExtractView> extracts)
        {
            var messages = new List<IMessage<DefaulterTracingExtractView>>();
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
