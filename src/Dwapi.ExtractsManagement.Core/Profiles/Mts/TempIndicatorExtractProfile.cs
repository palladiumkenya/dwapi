using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts.Dto;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Source.Mts;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Mts
{
    public class TempIndicatorExtractProfile : Profile
    {
        public TempIndicatorExtractProfile()
        {
            // HTS Client Extract

            CreateMap<IDataRecord, TempIndicatorExtract>()
                .ForMember(x => x.Indicator,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIndicatorExtract.Indicator))))
                .ForMember(x => x.IndicatorValue,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIndicatorExtract.IndicatorValue))))
                .ForMember(x => x.IndicatorDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempIndicatorExtract.IndicatorDate))))
                .ForMember(x => x.SiteCode,
                o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempIndicatorExtract.SiteCode))));
            
            CreateMap<TempIndicatorExtract, IndicatorExtract>();

            CreateMap<IndicatorExtract,IndicatorExtractDto>();
        }
    }
}
