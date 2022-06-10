using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;
using Dwapi.UploadManagement.Core.Model.Crs.Dtos;

namespace Dwapi.UploadManagement.Core.Profiles
{
   public class ClientRegistryExtractProfile : Profile
    {
        public ClientRegistryExtractProfile()
        {
            CreateMap<ClientRegistryExtract, ClientRegistryExtractDto>()
                .ForMember(d => d.FirstName, o => o.UseValue(string.Empty))
                .ForMember(d => d.CCCNumber, o => o.UseValue(string.Empty))
                .ForMember(d => d.NationalId, o => o.UseValue(string.Empty))
                .ForMember(d => d.Passport, o => o.UseValue(string.Empty))
                .ForMember(d => d.HudumaNumber, o => o.UseValue(string.Empty))
                .ForMember(d => d.BirthCertificateNumber, o => o.UseValue(string.Empty))
                .ForMember(d => d.AlienIdNo, o => o.UseValue(string.Empty))
                .ForMember(d => d.MaritalStatus, o => o.UseValue(string.Empty))
                .ForMember(d => d.DrivingLicenseNumber, o => o.UseValue(string.Empty))
                .ForMember(d => d.FirstName, o => o.UseValue(string.Empty))
                .ForMember(d => d.MiddleName, o => o.UseValue(string.Empty))
                .ForMember(d => d.LastName, o => o.UseValue(string.Empty))
                .ForMember(d => d.DateOfBirth, o => o.UseValue(string.Empty))
                .ForMember(d => d.Sex, o => o.UseValue(string.Empty))
                .ForMember(d => d.MaritalStatus, o => o.UseValue(string.Empty))
                .ForMember(d => d.Occupation, o => o.UseValue(string.Empty))
                .ForMember(d => d.HighestLevelOfEducation, o => o.UseValue(string.Empty))
                .ForMember(d => d.PhoneNumber, o => o.UseValue(string.Empty))
                .ForMember(d => d.AlternativePhoneNumber, o => o.UseValue(string.Empty))
                .ForMember(d => d.SpousePhoneNumber, o => o.UseValue(string.Empty))
                .ForMember(d => d.NameOfNextOfKin, o => o.UseValue(string.Empty))
                .ForMember(d => d.NextOfKinRelationship, o => o.UseValue(string.Empty))
                .ForMember(d => d.NextOfKinTelNo, o => o.UseValue(string.Empty))
                .ForMember(d => d.County, o => o.UseValue(string.Empty))
                .ForMember(d => d.SubCounty, o => o.UseValue(string.Empty))
                .ForMember(d => d.Ward, o => o.UseValue(string.Empty))
                .ForMember(d => d.Location, o => o.UseValue(string.Empty))
                .ForMember(d => d.Village, o => o.UseValue(string.Empty))
                .ForMember(d => d.Landmark, o => o.UseValue(string.Empty))
                .ForMember(d => d.FacilityName, o => o.UseValue(string.Empty))
                .ForMember(d => d.MFLCode, o => o.UseValue(string.Empty))
                .ForMember(d => d.DateOfInitiation, o => o.UseValue(string.Empty))
                .ForMember(d => d.TreatmentOutcome, o => o.UseValue(string.Empty))
                .ForMember(d => d.DateOfLastEncounter, o => o.UseValue(string.Empty))
                .ForMember(d => d.DateOfLastViralLoad, o => o.UseValue(string.Empty))
                .ForMember(d => d.NextAppointmentDate, o => o.UseValue(string.Empty))
                .ForMember(d => d.PatientPK, o => o.UseValue(string.Empty))
                .ForMember(d => d.SiteCode, o => o.UseValue(string.Empty))
                .ForMember(d => d.FacilityID, o => o.UseValue(string.Empty))
                .ForMember(d => d.Emr, o => o.UseValue(string.Empty))
                .ForMember(d => d.Project, o => o.UseValue(string.Empty))
                .ForMember(d => d.LastRegimen, o => o.UseValue(string.Empty))
                .ForMember(d => d.LastRegimenLine, o => o.UseValue(string.Empty))
                .ForMember(d => d.CurrentOnART, o => o.UseValue(string.Empty))
                .ForMember(d => d.DateOfHIVdiagnosis, o => o.UseValue(string.Empty))
                .ForMember(d => d.LastViralLoadResult, o => o.UseValue(string.Empty))

                
                ;
        }
    }
}
