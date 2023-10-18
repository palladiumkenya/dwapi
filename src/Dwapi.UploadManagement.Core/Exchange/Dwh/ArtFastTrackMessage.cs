﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class ArtFastTrackMessage:IArtFastTrackMessage
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
        [JsonProperty(PropertyName = "ArtFastTrackExtracts")]
        public List<ArtFastTrackExtractView> Extracts { get; } = new List<ArtFastTrackExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PArtFastTrackExtracts")]
        public List<ArtFastTrackExtractView> ArtFastTrackExtracts { get; set; } = new List<ArtFastTrackExtractView>();
        public bool HasContents => null != ArtFastTrackExtracts && ArtFastTrackExtracts.Any();

        public ArtFastTrackMessage()
        {
        }

        public ArtFastTrackMessage(ArtFastTrackExtractView patient)
        {
            patient.PatientExtractView.ArtFastTrackExtracts=new List<ArtFastTrackExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            ArtFastTrackExtracts.Add(patient);
        }

        public ArtFastTrackMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.ArtFastTrackExtracts.ToList();
            ArtFastTrackExtracts = patient.ArtFastTrackExtracts.ToList();
        }

        public IMessage<ArtFastTrackExtractView> Generate(ArtFastTrackExtractView extract)
        {
            return new ArtFastTrackMessage(extract);
        }

        public List<IMessage<ArtFastTrackExtractView>> GenerateMessages(List<ArtFastTrackExtractView> extracts)
        {
            var messages = new List<IMessage<ArtFastTrackExtractView>>();
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
