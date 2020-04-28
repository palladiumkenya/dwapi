using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.DTOs
{
    public class DbExtractProtocolDTO
    {
        public DbExtract Extract { get; set; }
        public DbProtocol DatabaseProtocol { get; set; }

        public DbExtractProtocolDTO()
        {
        }

        public DbExtractProtocolDTO(DbExtract extract, DbProtocol databaseProtocol)
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