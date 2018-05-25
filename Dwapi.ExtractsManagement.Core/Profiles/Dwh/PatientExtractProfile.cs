using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Dwh
{
    public class PatientExtractProfile : Profile
    {
        public PatientExtractProfile()
        {
            CreateMap<IDataRecord, PatientExtract>()
                .ForMember(x => x.DatePreviousARTStart, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientExtract.PreviousARTStartDate))));
        }
    }
}