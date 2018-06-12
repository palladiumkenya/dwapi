using System.Collections.Generic;
using System.Linq;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class StatusMessage
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
        public List<PatientStatusExtractView> StatusExtracts { get; set; } = new List<PatientStatusExtractView>();

        public StatusMessage()
        {
        }

        public StatusMessage(PatientExtractView patient)
        {
            Demographic = patient;
            StatusExtracts = patient.PatientStatusExtracts.ToList();
        }

    }
}