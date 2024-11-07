using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;

namespace Dwapi.UploadManagement.Core.Profiles
{
   public class MasterPatientIndexProfile : Profile
    {
        public MasterPatientIndexProfile()
        {
            CreateMap<MasterPatientIndex, MasterPatientIndexDto>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => string.Empty))
                .ForMember(d => d.FirstName_Normalized, o => o.MapFrom(s => string.Empty))
                .ForMember(d=> d.LastName,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.LastName_Normalized,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.MiddleName,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.MiddleName_Normalized,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.ContactAddress,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.ContactPhoneNumber,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.ContactName,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.NHIF_Number,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.PatientPhoneNumber,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.National_ID,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.Birth_Certificate,o=> o.MapFrom(s => string.Empty))
                .ForMember(d=> d.PatientAlternatePhoneNumber,o=> o.MapFrom(s => string.Empty))

                
                ;
        }
    }
}
