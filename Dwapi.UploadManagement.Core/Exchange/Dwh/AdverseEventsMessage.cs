using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class AdverseEventsMessage
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
        public List<PatientAdverseEventView> AdverseEventExtracts { get; set; } = new List<PatientAdverseEventView>();

        public AdverseEventsMessage()
        {
        }

        public AdverseEventsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            AdverseEventExtracts = patient.PatientAdverseEventExtracts.ToList();
        }
        public bool HasContents => null != AdverseEventExtracts && AdverseEventExtracts.Any();
    }
}