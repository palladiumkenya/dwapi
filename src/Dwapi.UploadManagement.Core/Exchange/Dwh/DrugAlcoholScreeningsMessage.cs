﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Interfaces.Exchange;
using Dwapi.UploadManagement.Core.Interfaces.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Model.Dwh;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class DrugAlcoholScreeningsMessage:IDrugAlcoholScreeningMessage
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
        [JsonProperty(PropertyName = "DrugAlcoholScreeningExtracts")]
        public List<DrugAlcoholScreeningExtractView> Extracts { get; } = new List<DrugAlcoholScreeningExtractView>();

        public List<Guid> SendIds => GetIds();
        public List<Guid> PatientIds { get; }

        [JsonProperty(PropertyName = "PDrugAlcoholScreeningExtracts")]
        public List<DrugAlcoholScreeningExtractView> DrugAlcoholScreeningExtracts { get; set; } = new List<DrugAlcoholScreeningExtractView>();
        public bool HasContents => null != DrugAlcoholScreeningExtracts && DrugAlcoholScreeningExtracts.Any();

        public DrugAlcoholScreeningsMessage()
        {
        }

        public DrugAlcoholScreeningsMessage(DrugAlcoholScreeningExtractView patient)
        {
            patient.PatientExtractView.DrugAlcoholScreeningExtracts=new List<DrugAlcoholScreeningExtractView>();
            Demographic = patient.PatientExtractView;
            Extracts.Add(patient);
            DrugAlcoholScreeningExtracts.Add(patient);
        }

        public DrugAlcoholScreeningsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            Extracts = patient.DrugAlcoholScreeningExtracts.ToList();
            DrugAlcoholScreeningExtracts = patient.DrugAlcoholScreeningExtracts.ToList();
        }

        public IMessage<DrugAlcoholScreeningExtractView> Generate(DrugAlcoholScreeningExtractView extract)
        {
            return new DrugAlcoholScreeningsMessage(extract);
        }

        public List<IMessage<DrugAlcoholScreeningExtractView>> GenerateMessages(List<DrugAlcoholScreeningExtractView> extracts)
        {
            var messages = new List<IMessage<DrugAlcoholScreeningExtractView>>();
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
