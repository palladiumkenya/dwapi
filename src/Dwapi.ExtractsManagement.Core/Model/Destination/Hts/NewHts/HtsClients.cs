using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts
{
    public  class HtsClients : HtsExtract
    {

        public  DateTime?  DoB	 { get; set; }
        public  string  Gender	 { get; set; }
        public  string MaritalStatus	 { get; set; }
        public  string  PopulationType	 { get; set; }
        public  string  KeyPopulationType	 { get; set; }
        public  string  PatientDisabled	 { get; set; }
        public  string County	 { get; set; }
        public  string  SubCounty	 { get; set; }
        public  string Ward	 { get; set; }

        public virtual ICollection<HtsClientTests> HtsClientTestss { get; set; } = new List<HtsClientTests>();
        public virtual ICollection<HtsClientTracing> HtsClientTracings { get; set; } = new List<HtsClientTracing>();
        public virtual ICollection<HtsPartnerTracing> HtsPartnerTracings { get; set; } = new List<HtsPartnerTracing>();
        public virtual ICollection<HtsTestKits> HtsTestKitss { get; set; } = new List<HtsTestKits>();
        public virtual ICollection<HtsClientLinkage> HtsClientLinkages { get; set; } = new List<HtsClientLinkage>();
        public virtual ICollection<HtsPartnerNotificationServices> HtsPartnerNotificationServicess { get; set; } = new List<HtsPartnerNotificationServices>();
    }
}
