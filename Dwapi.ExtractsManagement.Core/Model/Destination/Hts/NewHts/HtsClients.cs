using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    public  class HtsClients : TempHTSExtract
    {

        public  DateTime  DoB	 { get; set; }
        public  string  Gender	 { get; set; }
        public  string MaritalStatus	 { get; set; }
        public  string  PopulationType	 { get; set; }
        public  string  KeyPopulationType	 { get; set; }
        public  string  PatientDisabled	 { get; set; }
        public  string County	 { get; set; }
        public  string  SubCounty	 { get; set; }
        public  string Ward	 { get; set; }
        
        public override string ToString()
        {
            return $"{SiteCode}-{HtsNumber}";
        }
    }
}
