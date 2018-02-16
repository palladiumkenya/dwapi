using System;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.DTOs
{
    public class SendPackageDTO
    {
        public Registry Destination { get; set; }
        public string Docket { get; set; }
        public Guid? ExtractId { get; set; }
        public string Endpoint { get; set; }

        public bool IsValid()
        {
            return null != Destination && !string.IsNullOrWhiteSpace(Docket) && !ExtractId.IsNullOrEmpty();
        }

        public string GetUrl(string endPoint="")
        {
            Endpoint= string.IsNullOrWhiteSpace(endPoint) ? string.Empty : endPoint.HasToStartWith("/");
            var url= $"{Destination.Url}{Endpoint}";
            return url;
        }
    }
}