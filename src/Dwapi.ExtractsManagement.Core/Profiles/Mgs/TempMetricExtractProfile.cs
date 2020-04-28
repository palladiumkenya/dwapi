using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mgs;
using Dwapi.ExtractsManagement.Core.Model.Source.Mgs;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Mgs
{
    public class TempMetricExtractProfile : Profile
    {
        public TempMetricExtractProfile()
        {
            // HTS Client Extract

            CreateMap<IDataRecord, TempMetricMigrationExtract>()
                .ForMember(x => x.MetricId,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMetricMigrationExtract.MetricId))))
                .ForMember(x => x.CreateDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMetricMigrationExtract.CreateDate))))
                .ForMember(x => x.FacilityName,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMetricMigrationExtract.FacilityName))))
                .ForMember(x => x.Dataset,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMetricMigrationExtract.Dataset))))
                .ForMember(x => x.SiteCode,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMetricMigrationExtract.SiteCode))))
                .ForMember(x => x.Metric,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMetricMigrationExtract.Metric))))
                .ForMember(x => x.MetricValue,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMetricMigrationExtract.MetricValue))))
                .ForMember(x => x.Emr,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMetricMigrationExtract.Emr))))
                .ForMember(x => x.Project,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMetricMigrationExtract.Project))));
            CreateMap<TempMetricMigrationExtract, MetricMigrationExtract>();
        }
    }
}
