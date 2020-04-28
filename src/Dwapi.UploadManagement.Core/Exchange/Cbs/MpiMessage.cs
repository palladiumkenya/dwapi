using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;

namespace Dwapi.UploadManagement.Core.Exchange.Cbs
{
    public class MpiMessage
    {
        public List<MasterPatientIndexDto> MasterPatientIndices { get; set; }

        public MpiMessage()
        {
        }

        public MpiMessage(List<MasterPatientIndexDto> masterPatientIndices)
        {
            MasterPatientIndices = masterPatientIndices;
        }

        public static List<MpiMessage> Create(List<MasterPatientIndexDto> masterPatientIndices)
        {
            var list=new List<MpiMessage>();
            var chunks = masterPatientIndices.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new MpiMessage(chunk));
            }

            return list;
        }
    }
}
