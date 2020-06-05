﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class LabMessage:ILaboratoryMessage
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
        public PatientExtractView Demographic { get;  }
        [JsonProperty(PropertyName = "LaboratoryExtracts")]
        public List<PatientLaboratoryExtractView> Extracts { get; }=new List<PatientLaboratoryExtractView>();
        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PLaboratoryExtracts")]
        public List<PatientLaboratoryExtractView> LaboratoryExtracts { get; set; } = new List<PatientLaboratoryExtractView>();
        public bool HasContents => null != LaboratoryExtracts && LaboratoryExtracts.Any();

        public LabMessage()
        {
        }
        public LabMessage(PatientLaboratoryExtractView patient)
        {
            patient.PatientExtractView.PatientLaboratoryExtracts=new List<PatientLaboratoryExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            LaboratoryExtracts.Add(patient);
        }

        public LabMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.PatientLaboratoryExtracts.ToList();
            LaboratoryExtracts = patient.PatientLaboratoryExtracts.ToList();
        }

        public IMessage<PatientLaboratoryExtractView> Generate(PatientLaboratoryExtractView extract)
        {
            return new LabMessage(extract);
        }

        public List<IMessage<PatientLaboratoryExtractView>> GenerateMessages(List<PatientLaboratoryExtractView> extracts)
        {
            var messages = new List<IMessage<PatientLaboratoryExtractView>>();
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
