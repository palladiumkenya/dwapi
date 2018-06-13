using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Model.Dwh;

namespace Dwapi.UploadManagement.Core.Exchange.Dwh
{
    public class BaselineMessage
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
        public List<PatientBaselinesExtractView> BaselinesExtracts { get; set; }=new List<PatientBaselinesExtractView>();

        public BaselineMessage()
        {
        }

        public BaselineMessage(PatientExtractView patient)
        {
            Demographic = patient;
            BaselinesExtracts = patient.PatientBaselinesExtracts.ToList();
        }
        public bool HasContents => null != BaselinesExtracts && BaselinesExtracts.Any();
    }
}