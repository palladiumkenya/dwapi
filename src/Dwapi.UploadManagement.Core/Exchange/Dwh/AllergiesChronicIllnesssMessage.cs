﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class AllergiesChronicIllnesssMessage:IAllergiesChronicIllnessMessage
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
        [JsonProperty(PropertyName = "AllergiesChronicIllnessExtracts")]
        public List<AllergiesChronicIllnessExtractView> Extracts { get; } = new List<AllergiesChronicIllnessExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PAllergiesChronicIllnessExtracts")]
        public List<AllergiesChronicIllnessExtractView> AllergiesChronicIllnessExtracts { get; set; } = new List<AllergiesChronicIllnessExtractView>();
        public bool HasContents => null != AllergiesChronicIllnessExtracts && AllergiesChronicIllnessExtracts.Any();

        public AllergiesChronicIllnesssMessage()
        {
        }

        public AllergiesChronicIllnesssMessage(AllergiesChronicIllnessExtractView patient)
        {
            patient.PatientExtractView.AllergiesChronicIllnessExtracts=new List<AllergiesChronicIllnessExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            AllergiesChronicIllnessExtracts.Add(patient);
        }

        public AllergiesChronicIllnesssMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.AllergiesChronicIllnessExtracts.ToList();
            AllergiesChronicIllnessExtracts = patient.AllergiesChronicIllnessExtracts.ToList();
        }

        public IMessage<AllergiesChronicIllnessExtractView> Generate(AllergiesChronicIllnessExtractView extract)
        {
            return new AllergiesChronicIllnesssMessage(extract);
        }

        public List<IMessage<AllergiesChronicIllnessExtractView>> GenerateMessages(List<AllergiesChronicIllnessExtractView> extracts)
        {
            var messages = new List<IMessage<AllergiesChronicIllnessExtractView>>();
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
