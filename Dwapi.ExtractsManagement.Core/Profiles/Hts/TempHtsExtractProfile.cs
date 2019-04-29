using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Hts
{
    public class TempHtsExtractProfile : Profile
    {
        public TempHtsExtractProfile()
        {
            // HTS Client Extract

            CreateMap<IDataRecord, TempHTSClientExtract>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientExtract.PatientPk))))
                .ForMember(x => x.EncounterId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientExtract.EncounterId))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientExtract.VisitDate))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.FacilityName))))
                .ForMember(x => x.Dob, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientExtract.Dob))))
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.HtsNumber))))
                .ForMember(x => x.Gender, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.Gender))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientExtract.SiteCode))))
                .ForMember(x => x.MaritalStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.MaritalStatus))))
                .ForMember(x => x.TestedBefore, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestedBefore))))
                .ForMember(x => x.MonthsLastTested, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientExtract.MonthsLastTested))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.Project))))
                .ForMember(x => x.ClientTestedAs, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.ClientTestedAs))))
                .ForMember(x => x.StrategyHTS, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.StrategyHTS))))
                .ForMember(x => x.TestKitName1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitName1))))
                .ForMember(x => x.TestKitLotNumber1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitLotNumber1))))
                .ForMember(x => x.TestKitExpiryDate1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitExpiryDate1))))
                .ForMember(x => x.TestResultsHTS1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestResultsHTS1))))
                .ForMember(x => x.TestKitName2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitName2))))
                .ForMember(x => x.TestKitLotNumber2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitLotNumber2))))
                .ForMember(x => x.TestKitExpiryDate2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitExpiryDate2))))
                .ForMember(x => x.TestResultsHTS2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestResultsHTS2))))
                .ForMember(x => x.FinalResultHTS, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.FinalResultHTS))))
                .ForMember(x => x.FinalResultsGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.FinalResultsGiven))))
                .ForMember(x => x.TBScreeningHTS, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TBScreeningHTS))))
                .ForMember(x => x.ClientSelfTested, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.ClientSelfTested))))
                .ForMember(x => x.CoupleDiscordant, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.CoupleDiscordant))))
                .ForMember(x => x.TestType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestType))))
                .ForMember(x => x.KeyPopulationType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.KeyPopulationType))))
                .ForMember(x => x.PopulationType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.PopulationType))))
                .ForMember(x => x.PatientDisabled, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.PatientDisabled))))
                .ForMember(x => x.DisabilityType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.DisabilityType))))
                .ForMember(x => x.PatientConsented, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.PatientConsented))));
            CreateMap<TempHTSClientExtract, HTSClientExtract>();

            // HTS Client Linkage Extract

            CreateMap<IDataRecord, TempHTSClientLinkageExtract>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientLinkageExtract.PatientPk))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientLinkageExtract.FacilityName))))
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientLinkageExtract.HtsNumber))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientLinkageExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientLinkageExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientLinkageExtract.Project))))
                .ForMember(x => x.PhoneTracingDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientLinkageExtract.PhoneTracingDate))))
                .ForMember(x => x.PhysicalTracingDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientLinkageExtract.PhysicalTracingDate))))
                .ForMember(x => x.TracingOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientLinkageExtract.TracingOutcome))))
                .ForMember(x => x.CccNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientLinkageExtract.CccNumber))))
                .ForMember(x => x.ReferralDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientLinkageExtract.ReferralDate))))
                .ForMember(x => x.DateEnrolled, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientLinkageExtract.DateEnrolled))))
                .ForMember(x => x.EnrolledFacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientLinkageExtract.EnrolledFacilityName))));
            CreateMap<TempHTSClientLinkageExtract, HTSClientLinkageExtract>();

             // HTS Client Partner Extract

            CreateMap<IDataRecord, TempHTSClientPartnerExtract>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientPartnerExtract.PatientPk))))
                .ForMember(x => x.PartnerPatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientPartnerExtract.PartnerPatientPk))))
                .ForMember(x => x.PartnerPersonId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientPartnerExtract.PartnerPersonId))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.FacilityName))))
                .ForMember(x => x.RelationshipToIndexClient, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.RelationshipToIndexClient))))
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.HtsNumber))))
                .ForMember(x => x.ScreenedForIpv, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.ScreenedForIpv))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientPartnerExtract.SiteCode))))
                .ForMember(x => x.IpvScreeningOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.IpvScreeningOutcome))))
                .ForMember(x => x.CurrentlyLivingWithIndexClient, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.CurrentlyLivingWithIndexClient))))
                .ForMember(x => x.KnowledgeOfHivStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.KnowledgeOfHivStatus))))
                .ForMember(x => x.PnsApproach, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.PnsApproach))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.Project))))
                .ForMember(x => x.Trace1Outcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.Trace1Outcome))))
                .ForMember(x => x.Trace1Type, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.Trace1Type))))
                .ForMember(x => x.Trace1Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientPartnerExtract.Trace1Date))))
                .ForMember(x => x.Trace2Outcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.Trace2Outcome))))
                .ForMember(x => x.Trace2Type, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.Trace2Type))))
                .ForMember(x => x.Trace2Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientPartnerExtract.Trace2Date))))
                .ForMember(x => x.Trace3Outcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.Trace3Outcome))))
                .ForMember(x => x.Trace3Type, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.Trace3Type))))
                .ForMember(x => x.Trace3Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientPartnerExtract.Trace3Date))))
                .ForMember(x => x.PnsConsent, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.PnsConsent))))
                .ForMember(x => x.Linked, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.Linked))))
                .ForMember(x => x.LinkDateLinkedToCare, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientPartnerExtract.LinkDateLinkedToCare))))
                .ForMember(x => x.CccNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.CccNumber))))
                .ForMember(x => x.Sex, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientPartnerExtract.Sex))))
                .ForMember(x => x.Age, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientPartnerExtract.Age))));
              CreateMap<TempHTSClientPartnerExtract, HTSClientPartnerExtract>();
         }
    }
}
