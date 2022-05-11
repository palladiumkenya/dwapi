using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Newtonsoft.Json;

namespace Dwapi.UploadManagement.Core.Model.Crs
{
    [Table("ClientRegistryExtracts")]
    public class ClientRegistryExtractView: ClientRegistryExtract
    {
        [JsonProperty ("SatelliteId")]
        public override int? FacilityId { get; set; }
    }
}
