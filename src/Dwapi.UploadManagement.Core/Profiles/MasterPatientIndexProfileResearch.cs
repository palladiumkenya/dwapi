using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;

namespace Dwapi.UploadManagement.Core.Profiles
{
    public class MasterPatientIndexProfileResearch : Profile
    {
        public MasterPatientIndexProfileResearch()
        {
            CreateMap<MasterPatientIndex, MasterPatientIndexDto>()
                /*
                    .ForMember(d => d.FirstName, o => o.UseValue(string.Empty))
                    .ForMember(d => d.FirstName_Normalized, o => o.UseValue(string.Empty))
                    .ForMember(d=> d.LastName,o=> o.UseValue(string.Empty))
                    .ForMember(d=> d.LastName_Normalized,o=> o.UseValue(string.Empty))
                    .ForMember(d=> d.MiddleName,o=> o.UseValue(string.Empty))
                    .ForMember(d=> d.MiddleName_Normalized,o=> o.UseValue(string.Empty))
                    .ForMember(d => d.NHIF_Number, o => o.UseValue(string.Empty))
                    .ForMember(d => d.PatientPhoneNumber, o => o.UseValue(string.Empty))
                    .ForMember(d => d.National_ID, o => o.UseValue(string.Empty))
                    .ForMember(d => d.Birth_Certificate, o => o.UseValue(string.Empty));
                */
                .ForMember(d => d.ContactAddress, o => o.UseValue(string.Empty))
                .ForMember(d => d.ContactPhoneNumber, o => o.UseValue(string.Empty))
                .ForMember(d => d.ContactName, o => o.UseValue(string.Empty));
        }
    }
}
