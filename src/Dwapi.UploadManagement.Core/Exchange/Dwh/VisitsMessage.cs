using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class VisitsMessage
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
        public List<PatientVisitExtractView> VisitExtracts { get; set; } = new List<PatientVisitExtractView>();

        public VisitsMessage()
        {
        }

        public VisitsMessage(PatientExtractView patient)
        {
            Demographic = patient;
            VisitExtracts = patient.PatientVisitExtracts.ToList();
        }
        public bool HasContents => null != VisitExtracts && VisitExtracts.Any();
    }
}