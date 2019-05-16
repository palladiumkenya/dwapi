using System.Collections.Generic;
using Dwapi.UploadManagement.Core.Model.Hts;

namespace Dwapi.UploadManagement.Core.Interfaces.Reader.Hts
{
    public interface IHtsExtractReader
    {
        IEnumerable<HTSClientExtractView> ReadAllClients();
        IEnumerable<HTSClientPartnerExtractView> ReadAllPartners();
        IEnumerable<HTSClientLinkageExtractView> ReadAllLinkages();
    }
}
