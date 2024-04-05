using Dwapi.ExtractsManagement.Core.Interfaces.Reader;
using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.ExtractsManagement.Core.ComandHandlers
{
    public class AutoloadEtlRefresh
    {
        private readonly IAutoloadSourceReader _sourcereader;

        public AutoloadEtlRefresh(IAutoloadSourceReader sourcereader)
        {
            _sourcereader = sourcereader;
        }
        
        public string refreshEMRETL(DatabaseProtocol protocol)
        {
            // var refresh = _sourcereader.RefreshEtlTtables(protocol);
            return "here";
        }

    }
}