using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Dwh
{
    public class TempExtractProfile : Profile
    {
        public TempExtractProfile()
        {
            CreateMap<IDataRecord, TempPatientExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientExtract.PatientPK))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.FacilityName))))
                .ForMember(x => x.DOB, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientExtract.DOB))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientExtract.SiteCode))))
                .ForMember(x => x.SatelliteName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.SatelliteName))))
                .ForMember(x => x.Gender, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.Gender))))
                .ForMember(x => x.RegistrationDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientExtract.RegistrationDate))))
                .ForMember(x => x.RegistrationAtCCC, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientExtract.RegistrationAtCCC))))
                .ForMember(x => x.RegistrationAtTBClinic, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientExtract.RegistrationAtTBClinic))))
                .ForMember(x => x.RegistrationAtPMTCT, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientExtract.RegistrationAtPMTCT))))
                .ForMember(x => x.PatientSource, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PatientSource))))
                .ForMember(x => x.Region, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.Region))))
                .ForMember(x => x.District, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.District))))
                .ForMember(x => x.Village, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.Village))))
                .ForMember(x => x.ContactRelation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.ContactRelation))))
                .ForMember(x => x.LastVisit, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientExtract.LastVisit))))
                .ForMember(x => x.MaritalStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.MaritalStatus))))
                .ForMember(x => x.EducationLevel, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.EducationLevel))))
                .ForMember(x => x.DateConfirmedHIVPositive, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientExtract.DateConfirmedHIVPositive))))
                .ForMember(x => x.PreviousARTExposure, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PreviousARTExposure))))
                .ForMember(x => x.PreviousARTStartDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientExtract.PreviousARTStartDate))))
                .ForMember(x => x.StatusAtCCC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.StatusAtCCC))))
                .ForMember(x => x.StatusAtPMTCT, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.StatusAtPMTCT))))
                .ForMember(x => x.StatusAtTBClinic, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.StatusAtTBClinic))))
                .ForMember(x => x.EMR, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.EMR))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.Project))));
        }
    }
}