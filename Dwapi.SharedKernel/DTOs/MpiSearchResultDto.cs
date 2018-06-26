using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dwapi.SharedKernel.DTOs
{
    public class MpiSearchResultDto
    {
        public int MflCode { get; set; }
        public string RegisteredFacility { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeCounty { get; set; }
        public string HomeSubCounty { get; set; }
        public string NationalId { get; set; }
        [Column("NHIFNumber")]
        public string NhifNumber { get; set; }
        [Column("CCCNumber")]
        public string CccNumber { get; set; }
        public string MatchingScore { get; set; }
    }
}