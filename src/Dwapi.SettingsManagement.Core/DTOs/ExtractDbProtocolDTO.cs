using Dwapi.SettingsManagement.Core.Model;

namespace Dwapi.SettingsManagement.Core.DTOs
{
    public class ExtractDbProtocolDTO
    {
        public Extract Extract { get; set; }
        public DatabaseProtocol DatabaseProtocol { get; set; }

        public ExtractDbProtocolDTO()
        {
        }

        public ExtractDbProtocolDTO(Extract extract, DatabaseProtocol databaseProtocol)
        {
            Extract = extract;
            DatabaseProtocol = databaseProtocol;
        }

        public bool IsValid()
        {
            return null != Extract && null != DatabaseProtocol;
        }
    }
}