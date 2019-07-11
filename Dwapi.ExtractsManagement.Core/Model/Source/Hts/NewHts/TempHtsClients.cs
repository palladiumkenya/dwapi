using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{
    public abstract class TempHtsClients : TempHTSExtract
    {

        public virtual DateTime  DoB	 { get; set; }
        public virtual string  Gender	 { get; set; }
        public virtual string MaritalStatus	 { get; set; }
        public virtual string  PopulationType	 { get; set; }
        public virtual string  KeyPopulationType	 { get; set; }
        public virtual string  PatientDisabled	 { get; set; }
        public virtual string County	 { get; set; }
        public virtual string  SubCounty	 { get; set; }
        public virtual string Ward	 { get; set; }
        
        public override string ToString()
        {
            return $"{SiteCode}-{HtsNumber}";
        }
    }
}
