﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class ArtMessage : IArtMessage
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
        [JsonProperty(PropertyName = "ArtExtracts")]
        public List<PatientArtExtractView> Extracts { get; } = new List<PatientArtExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PArtExtracts")]
        public List<PatientArtExtractView> ArtExtracts { get; set; } = new List<PatientArtExtractView>();
        public bool HasContents => null != ArtExtracts && ArtExtracts.Any();

        public ArtMessage()
        {
        }

        public ArtMessage(PatientArtExtractView patient)
        {
            patient.PatientExtractView.PatientArtExtracts=new List<PatientArtExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            ArtExtracts.Add(patient);
        }

        public ArtMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.PatientArtExtracts.ToList();
            ArtExtracts = patient.PatientArtExtracts.ToList();
        }

        public IMessage<PatientArtExtractView> Generate(PatientArtExtractView extract)
        {
            return new ArtMessage(extract);
        }

        public List<IMessage<PatientArtExtractView>> GenerateMessages(List<PatientArtExtractView> extracts)
        {
            var messages = new List<IMessage<PatientArtExtractView>>();
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
