using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.DTOs
{
    public class SendManifestPackageDTO
    {
        public Registry Destination { get; set; }
        public Guid ExtractId { get; set; }
        public string ExtractName { get; set; }
        public string Endpoint { get; private set; }
        public EmrSetup EmrSetup { get; set; }
        public string EmrName { get; set; }
        public Guid EmrId { get; set; }
        public List<ExtractDto> Extracts { get; set; } = new List<ExtractDto>();


        public SendManifestPackageDTO()
        {
        }

        public SendManifestPackageDTO(Registry destination)
        {
            Destination = destination;
        }

        public bool IsValid()
        {
            return null != Destination && !string.IsNullOrWhiteSpace(Destination.Url);
        }

        public string GetUrl(string endPoint = "",string version="")
        {
            Endpoint = string.IsNullOrWhiteSpace(endPoint) ? string.Empty : endPoint.HasToStartWith("/");
            var url = $"{Destination.Url}{Endpoint}";
            if(string.IsNullOrWhiteSpace(version))
                return url;

            return url.Replace("/api/", $"/api/v{version}/");
        }

        public EmrDto GetEmrDto()
        {
            return new EmrDto(EmrId, EmrName, EmrSetup);
        }

        public Guid GetExtractId(string name)
        {
            var extract = Extracts.FirstOrDefault(x => x.Name.IsSameAs(name));

            if (null != extract)
                return extract.Id;

            return ExtractId;
        }
    }
}
