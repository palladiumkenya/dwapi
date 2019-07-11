using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Hts
{

            
        public abstract class TempHtsClientLinkage : TempHTSExtract
        {

            public virtual DateTime  DatePrefferedToBeEnrolled { get; set; }
            public virtual string FacilityReferredTo { get; set; }
            public virtual string  HandedOverTo { get; set; }
            public virtual string HandedOverToCadre { get; set; }
            public virtual string EnrolledFacilityName { get; set; }
            public virtual DateTime ReferralDate { get; set; }
            public virtual DateTime DateEnrolled { get; set; }
            public virtual string ReportedCCCNumber { get; set; }
            public virtual DateTime ReportedStartARTDate { get; set; }
                
            public override string ToString()
            {
                return $"{SiteCode}-{HtsNumber}";
            }
        }
}
