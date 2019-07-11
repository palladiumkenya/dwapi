using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;

namespace Dwapi.UploadManagement.Core.Profiles
{
   public class MasterPatientIndexProfile : Profile
    {
        public MasterPatientIndexProfile()
        {
            CreateMap<MasterPatientIndex, MasterPatientIndexDto>()
                .ForMember(d=>d.FirstName,o=>o.UseValue(string.Empty))
                .ForMember(d=>d.FirstName_Normalized,o=>o.UseValue(string.Empty));

            //   .ForMember(d=>d.FirstName,o=>o.UseValue(string.Empty))
        }
    }
}
