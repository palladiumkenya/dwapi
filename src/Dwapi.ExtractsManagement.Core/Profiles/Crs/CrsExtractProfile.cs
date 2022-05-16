using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Crs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Crs;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Crs
{
    public class CrsExtractProfile : Profile
    {
        public CrsExtractProfile()
        {
            CreateMap<IDataRecord, TempClientRegistryExtract>()
                .ForMember(x => x.CCCNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.CCCNumber))))
                .ForMember(x => x.NationalId, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.NationalId))))
                .ForMember(x => x.Passport, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.Passport))))
                .ForMember(x => x.HudumaNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.HudumaNumber))))
                .ForMember(x => x.BirthCertificateNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.BirthCertificateNumber))))
                .ForMember(x => x.AlienIdNo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.AlienIdNo))))
                .ForMember(x => x.DrivingLicenseNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.DrivingLicenseNumber))))
                .ForMember(x => x.PatientClinicNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.PatientClinicNumber))))

                .ForMember(x => x.FirstName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.FirstName))))
                .ForMember(x => x.MiddleName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.MiddleName))))
                .ForMember(x => x.LastName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.LastName))))
                .ForMember(x => x.DateOfBirth, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempClientRegistryExtract.DateOfBirth))))
                .ForMember(x => x.Sex, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.Sex))))
                .ForMember(x => x.MaritalStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.MaritalStatus))))
                .ForMember(x => x.Occupation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.Occupation))))
                .ForMember(x => x.HighestLevelOfEducation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.HighestLevelOfEducation))))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.PhoneNumber))))
                .ForMember(x => x.AlternativePhoneNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.AlternativePhoneNumber))))
                .ForMember(x => x.SpousePhoneNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.SpousePhoneNumber))))
                .ForMember(x => x.NameOfNextOfKin, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.NameOfNextOfKin))))
                .ForMember(x => x.NextOfKinRelationship, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.NextOfKinRelationship))))
                .ForMember(x => x.NextOfKinTelNo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.NextOfKinTelNo))))
                .ForMember(x => x.County, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.County))))
                .ForMember(x => x.SubCounty, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.SubCounty))))
                .ForMember(x => x.Ward, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.Ward))))
                .ForMember(x => x.Location, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.Location))))
                .ForMember(x => x.Village, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.Village))))
                .ForMember(x => x.Landmark, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.Landmark))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.FacilityName))))
                .ForMember(x => x.MFLCode, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.MFLCode))))
                .ForMember(x => x.DateOfInitiation, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempClientRegistryExtract.DateOfInitiation))))
                .ForMember(x => x.TreatmentOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.TreatmentOutcome))))
                .ForMember(x => x.DateOfLastEncounter, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempClientRegistryExtract.DateOfLastEncounter))))
                .ForMember(x => x.DateOfLastViralLoad, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempClientRegistryExtract.DateOfLastViralLoad))))
                .ForMember(x => x.NextAppointmentDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempClientRegistryExtract.NextAppointmentDate))))
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempClientRegistryExtract.PatientPK))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempClientRegistryExtract.FacilityId))))

                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempClientRegistryExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.Project))))
                .ForMember(x => x.LastRegimen, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.LastRegimen))))
                .ForMember(x => x.LastRegimenLine, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.LastRegimenLine))))
                .ForMember(x => x.CurrentOnART, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempClientRegistryExtract.CurrentOnART))));

  
            CreateMap<TempClientRegistryExtract, ClientRegistryExtract>();
        }
    }
}
