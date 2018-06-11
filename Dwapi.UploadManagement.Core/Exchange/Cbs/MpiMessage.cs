using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.UploadManagement.Core.Exchange.Cbs
{
    public class MpiMessage
    {
        public IEnumerable<MasterPatientIndex> MasterPatientIndices { get; set; }

        public MpiMessage()
        {
        }

        public MpiMessage(IEnumerable<MasterPatientIndex> masterPatientIndices)
        {
            MasterPatientIndices = masterPatientIndices;
        }

        public static IEnumerable<MpiMessage> Create(IEnumerable<MasterPatientIndex> masterPatientIndices)
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