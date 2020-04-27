using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Cbs
{
    public class TempMasterPatientIndexProfile : Profile
    {
        public TempMasterPatientIndexProfile()
        {
            CreateMap<IDataRecord, TempMasterPatientIndex>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMasterPatientIndex.PatientPk))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMasterPatientIndex.SiteCode))))
                .ForMember(x => x.Serial, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.Serial))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.FacilityName))))
                .ForMember(x => x.FirstName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.FirstName))))
                .ForMember(x => x.MiddleName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.MiddleName))))
                .ForMember(x => x.LastName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.LastName))))
                .ForMember(x => x.FirstName_Normalized, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.FirstName_Normalized))))
                .ForMember(x => x.MiddleName_Normalized, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.MiddleName_Normalized))))
                .ForMember(x => x.LastName_Normalized, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.LastName_Normalized))))
                .ForMember(x => x.PatientPhoneNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.PatientPhoneNumber))))
                .ForMember(x => x.PatientAlternatePhoneNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.PatientAlternatePhoneNumber))))
                .ForMember(x => x.Gender, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.Gender))))
                .ForMember(x => x.DOB, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMasterPatientIndex.DOB))))
                .ForMember(x => x.MaritalStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.MaritalStatus))))
                .ForMember(x => x.PatientSource, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.PatientSource))))
                .ForMember(x => x.PatientCounty, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.PatientCounty))))
                .ForMember(x => x.PatientSubCounty, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.PatientSubCounty))))
                .ForMember(x => x.PatientVillage, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.PatientVillage))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.PatientID))))
                .ForMember(x => x.National_ID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.National_ID))))
                .ForMember(x => x.NHIF_Number, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.NHIF_Number))))
                .ForMember(x => x.Birth_Certificate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.Birth_Certificate))))
                .ForMember(x => x.CCC_Number, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.CCC_Number))))
                .ForMember(x => x.TB_Number, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.TB_Number))))
                .ForMember(x => x.ContactName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.ContactName))))
                .ForMember(x => x.ContactRelation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.ContactRelation))))
                .ForMember(x => x.ContactPhoneNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.ContactPhoneNumber))))
                .ForMember(x => x.ContactAddress, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.Serial))))
                .ForMember(x => x.DateConfirmedHIVPositive, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMasterPatientIndex.DateConfirmedHIVPositive))))
                .ForMember(x => x.StartARTDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMasterPatientIndex.StartARTDate))))
                .ForMember(x => x.StartARTRegimenCode, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.StartARTRegimenCode))))
                .ForMember(x => x.StartARTRegimenDesc, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.StartARTRegimenDesc))))
                .ForMember(x => x.dmFirstName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.dmFirstName))))
                .ForMember(x => x.dmLastName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.dmLastName))))
                .ForMember(x => x.sxFirstName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.sxFirstName))))
                .ForMember(x => x.sxLastName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.sxLastName))))
                .ForMember(x => x.sxPKValue, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.sxPKValue))))
                .ForMember(x => x.dmPKValue, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.dmPKValue))))
                .ForMember(x => x.sxdmPKValue, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.sxdmPKValue))))
                .ForMember(x => x.sxMiddleName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.sxMiddleName))))
                .ForMember(x => x.dmMiddleName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.dmMiddleName))))
                .ForMember(x => x.sxPKValueDoB, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.sxPKValueDoB))))
                .ForMember(x => x.dmPKValueDoB, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.dmPKValueDoB))))
                .ForMember(x => x.sxdmPKValueDoB, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMasterPatientIndex.sxdmPKValueDoB))));

            CreateMap<TempMasterPatientIndex, MasterPatientIndex>();
        }
    }
}