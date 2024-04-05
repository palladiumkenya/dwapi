﻿using System; 

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts
{
    public  class TempHtsClients : TempHTSExtract
    {

        public  DateTime?  Dob	 { get; set; }
        public  string  Gender	 { get; set; }
        public  string MaritalStatus	 { get; set; }
        public  string  PopulationType	 { get; set; }
        public  string  KeyPopulationType	 { get; set; }
        public  string  PatientDisabled	 { get; set; }
        public  string County	 { get; set; }
        public  string  SubCounty	 { get; set; }
        public  string Ward	 { get; set; }
        public string NUPI { get; set; }
        public string Pkv { get; set; }
        public string Occupation { get; set; }
        public string PriorityPopulationType { get; set; }
        public string HtsRecencyId { get; set; }
        public string RecordUUID { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
