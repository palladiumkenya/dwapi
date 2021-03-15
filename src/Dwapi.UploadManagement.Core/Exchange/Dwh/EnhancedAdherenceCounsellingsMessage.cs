﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class EnhancedAdherenceCounsellingsMessage:IEnhancedAdherenceCounsellingMessage
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
        [JsonProperty(PropertyName = "EnhancedAdherenceCounsellingExtracts")]
        public List<EnhancedAdherenceCounsellingExtractView> Extracts { get; } = new List<EnhancedAdherenceCounsellingExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PEnhancedAdherenceCounsellingExtracts")]
        public List<EnhancedAdherenceCounsellingExtractView> EnhancedAdherenceCounsellingExtracts { get; set; } = new List<EnhancedAdherenceCounsellingExtractView>();
        public bool HasContents => null != EnhancedAdherenceCounsellingExtracts && EnhancedAdherenceCounsellingExtracts.Any();

        public EnhancedAdherenceCounsellingsMessage()
        {
        }

        public EnhancedAdherenceCounsellingsMessage(EnhancedAdherenceCounsellingExtractView patient)
        {
            patient.PatientExtractView.EnhancedAdherenceCounsellingExtracts=new List<EnhancedAdherenceCounsellingExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            EnhancedAdherenceCounsellingExtracts.Add(patient);
        }

        public EnhancedAdherenceCounsellingsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.EnhancedAdherenceCounsellingExtracts.ToList();
            EnhancedAdherenceCounsellingExtracts = patient.EnhancedAdherenceCounsellingExtracts.ToList();
        }

        public IMessage<EnhancedAdherenceCounsellingExtractView> Generate(EnhancedAdherenceCounsellingExtractView extract)
        {
            return new EnhancedAdherenceCounsellingsMessage(extract);
        }

        public List<IMessage<EnhancedAdherenceCounsellingExtractView>> GenerateMessages(List<EnhancedAdherenceCounsellingExtractView> extracts)
        {
            var messages = new List<IMessage<EnhancedAdherenceCounsellingExtractView>>();
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
