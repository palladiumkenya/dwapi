using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;
using Dwapi.UploadManagement.Core.Model.Crs;
using Dwapi.UploadManagement.Core.Model.Crs.Dtos;

namespace Dwapi.UploadManagement.Core.Exchange.Crs
{
    public class CrsMessage
    {
        public List<ClientRegistryExtractView> ClientRegistryExtracts { get; set; }

        public CrsMessage()
        {
        }

        public CrsMessage(List<ClientRegistryExtractView> clientRegistryExtracts)
        {
            ClientRegistryExtracts = clientRegistryExtracts;
        }

        public static List<CrsMessage> Create(List<ClientRegistryExtractView> clientRegistryExtracts)
        {
            var list=new List<CrsMessage>();
            var chunks = clientRegistryExtracts.ToList().ChunkBy(500);
            foreach (var chunk in chunks)
            {
                list.Add(new CrsMessage(chunk));
            }

            return list;
        }
    }
}
