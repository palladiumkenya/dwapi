using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.DTOs
{
    public class MpiSearchPackageDto
    {

        public MpiSearchPackageDto(Registry destination, MpiSearch mpiSearch)
        {
            Destination = destination;
            MpiSearch = mpiSearch;
        }

        public MpiSearchPackageDto()
        {
        }

        public Registry Destination { get; set; }
        public MpiSearch  MpiSearch { get; set; }
        public string Endpoint { get; set; }

        public bool IsValid()
        {
            ValidationContext context = new ValidationContext(this);
            List<ValidationResult> results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, context, results, true);
        }

        public string GetUrl(string endPoint = "")
        {
            Endpoint = string.IsNullOrWhiteSpace(endPoint) ? string.Empty : endPoint.HasToStartWith("/");
            var url = $"{Destination.Url}{Endpoint}";
            return url;
        }

    }

    public class MpiSearch
    {
        public MpiSearch(string firstName, string middleName, string lastName, DateTime dob, Gender gender, string county, string phoneNumber, string nationalId, string nhifNumber)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Dob = dob;
            Gender = gender;
            County = county;
            PhoneNumber = phoneNumber;
            NationalId = nationalId;
            NhifNumber = nhifNumber;
        }

        public MpiSearch()
        {
        }

        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string County { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string NhifNumber { get; set; }
    }
}