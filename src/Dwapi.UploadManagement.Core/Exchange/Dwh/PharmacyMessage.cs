﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class PharmacyMessage:IPharmacyMessage
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
        [JsonProperty(PropertyName = "PharmacyExtracts")]
        public List<PatientPharmacyExtractView> Extracts { get; } = new List<PatientPharmacyExtractView>();

        public List<Guid> SendIds => GetIds();
        [JsonProperty(PropertyName = "PPharmacyExtracts")]
        public List<PatientPharmacyExtractView> PharmacyExtracts { get; set; } = new List<PatientPharmacyExtractView>();
        public bool HasContents => null != PharmacyExtracts && PharmacyExtracts.Any();

        public PharmacyMessage()
        {
        }

        public PharmacyMessage(PatientPharmacyExtractView patient)
        {
            patient.PatientExtractView.PatientPharmacyExtracts=new List<PatientPharmacyExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            PharmacyExtracts.Add(patient);
        }

        public PharmacyMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.PatientPharmacyExtracts.ToList();
            PharmacyExtracts = patient.PatientPharmacyExtracts.ToList();
        }

        public IMessage<PatientPharmacyExtractView> Generate(PatientPharmacyExtractView extract)
        {
            return new PharmacyMessage(extract);
        }

        public List<IMessage<PatientPharmacyExtractView>> GenerateMessages(List<PatientPharmacyExtractView> extracts)
        {
            var messages = new List<IMessage<PatientPharmacyExtractView>>();
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
