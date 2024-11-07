using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts.NewHts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts;
using Dwapi.ExtractsManagement.Core.Model.Source.Hts.NewHts;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Hts
{
    public class TempHtsExtractProfile : Profile
    {
        public TempHtsExtractProfile()
        {
            // HTS Client Extract

            CreateMap<IDataRecord, TempHTSClientExtract>()
                .ForMember(x => x.PatientPk,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientExtract.PatientPk))))
                .ForMember(x => x.EncounterId,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientExtract.EncounterId))))
                .ForMember(x => x.VisitDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientExtract.VisitDate))))
                .ForMember(x => x.FacilityName,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.FacilityName))))
                .ForMember(x => x.Dob, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHTSClientExtract.Dob))))
                .ForMember(x => x.HtsNumber,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.HtsNumber))))
                .ForMember(x => x.Gender,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.Gender))))
                .ForMember(x => x.SiteCode,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientExtract.SiteCode))))
                .ForMember(x => x.MaritalStatus,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.MaritalStatus))))
                .ForMember(x => x.TestedBefore,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestedBefore))))
                .ForMember(x => x.MonthsLastTested,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHTSClientExtract.MonthsLastTested))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.Emr))))
                .ForMember(x => x.Project,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.Project))))
                .ForMember(x => x.ClientTestedAs,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.ClientTestedAs))))
                .ForMember(x => x.StrategyHTS,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.StrategyHTS))))
                .ForMember(x => x.TestKitName1,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitName1))))
                .ForMember(x => x.TestKitLotNumber1,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitLotNumber1))))
                .ForMember(x => x.TestKitExpiryDate1,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitExpiryDate1))))
                .ForMember(x => x.TestResultsHTS1,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestResultsHTS1))))
                .ForMember(x => x.TestKitName2,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitName2))))
                .ForMember(x => x.TestKitLotNumber2,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitLotNumber2))))
                .ForMember(x => x.TestKitExpiryDate2,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestKitExpiryDate2))))
                .ForMember(x => x.TestResultsHTS2,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestResultsHTS2))))
                .ForMember(x => x.FinalResultHTS,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.FinalResultHTS))))
                .ForMember(x => x.FinalResultsGiven,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.FinalResultsGiven))))
                .ForMember(x => x.TBScreeningHTS,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TBScreeningHTS))))
                .ForMember(x => x.ClientSelfTested,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.ClientSelfTested))))
                .ForMember(x => x.CoupleDiscordant,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.CoupleDiscordant))))
                .ForMember(x => x.TestType,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.TestType))))
                .ForMember(x => x.KeyPopulationType,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.KeyPopulationType))))
                .ForMember(x => x.PopulationType,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.PopulationType))))
                .ForMember(x => x.PatientDisabled,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.PatientDisabled))))
                .ForMember(x => x.DisabilityType,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.DisabilityType))))
                .ForMember(x => x.PatientConsented,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHTSClientExtract.PatientConsented))))
                .ForMember(x => x.NUPI,
                    o => o.MapFrom(s => s.GetOptionalStringOrDefault(nameof(TempHTSClientExtract.NUPI))));
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

            //hts clients
            CreateMap<IDataRecord, TempHtsClients>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClients.PatientPk))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClients.SiteCode))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.FacilityName))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.Project))))
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.HtsNumber))))
                .ForMember(x => x.Dob, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsClients.Dob))))
                .ForMember(x => x.Gender, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.Gender))))
                .ForMember(x => x.MaritalStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.MaritalStatus))))
                .ForMember(x => x.PopulationType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.PopulationType))))
                .ForMember(x => x.KeyPopulationType, o =>  o.MapFrom(s => string.Empty))
                .ForMember(x => x.PatientDisabled, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.PatientDisabled))))
                .ForMember(x => x.County, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.County))))
                .ForMember(x => x.SubCounty, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.SubCounty))))
                .ForMember(x => x.Ward, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.Ward))))
                .ForMember(x => x.NUPI, o => o.MapFrom(s => s.GetOptionalStringOrDefault(nameof(TempHtsClients.NUPI))))
                .ForMember(x => x.Pkv, o => o.MapFrom(s => s.GetOptionalStringOrDefault(nameof(TempHtsClients.Pkv))))
                .ForMember(x => x.Occupation, o => o.MapFrom(s => s.GetOptionalStringOrDefault(nameof(TempHtsClients.Occupation))))
                .ForMember(x => x.PriorityPopulationType, o => o.MapFrom(s => s.GetOptionalStringOrDefault(nameof(TempHtsClients.PriorityPopulationType))))
                .ForMember(x => x.HtsRecencyId, o => o.MapFrom(s => s.GetOptionalStringOrDefault(nameof(TempHtsClients.HtsRecencyId))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClients.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClients.RecordUUID))));
                

            CreateMap<TempHtsClients, HtsClients>();

            //hts client tests
            CreateMap<IDataRecord, TempHtsClientTests>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientTests.PatientPk))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientTests.SiteCode))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.FacilityName))))
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.HtsNumber))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.Project))))
                .ForMember(x => x.EncounterId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientTests.EncounterId))))
                .ForMember(x => x.TestDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsClientTests.TestDate))))
                .ForMember(x => x.EverTestedForHiv, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.EverTestedForHiv))))
                .ForMember(x => x.MonthsSinceLastTest, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientTests.MonthsSinceLastTest))))
                .ForMember(x => x.ClientTestedAs, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.ClientTestedAs))))
                .ForMember(x => x.EntryPoint, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.EntryPoint))))
                .ForMember(x => x.TestStrategy, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.TestStrategy))))
                .ForMember(x => x.TestResult1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.TestResult1))))
                .ForMember(x => x.TestResult2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.TestResult2))))
                .ForMember(x => x.FinalTestResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.FinalTestResult))))
                .ForMember(x => x.PatientGivenResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.PatientGivenResult))))
                .ForMember(x => x.TbScreening, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.TbScreening))))
                .ForMember(x => x.ClientSelfTested, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.ClientSelfTested))))
                .ForMember(x => x.CoupleDiscordant, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.CoupleDiscordant))))
                .ForMember(x => x.TestType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.TestType))))
                .ForMember(x => x.Consent, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.Consent))))
                .ForMember(x => x.Setting, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.Setting))))
                .ForMember(x => x.Approach, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.Approach))))
                .ForMember(x => x.HtsRiskCategory, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.HtsRiskCategory))))
                .ForMember(x => x.HtsRiskScore, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.HtsRiskScore))))
                .ForMember(x => x.ReferredForServices, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.ReferredForServices))))
                .ForMember(x => x.ReferredServices, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.ReferredServices))))
                .ForMember(x => x.OtherReferredServices, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.OtherReferredServices))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientTests.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTests.RecordUUID))));
            CreateMap<TempHtsClientTests, HtsClientTests>();

            //hts client linkages
            CreateMap<IDataRecord, TempHtsClientLinkage>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientLinkage.PatientPk))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientLinkage.SiteCode))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientLinkage.FacilityName))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientLinkage.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientLinkage.Project))))
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientLinkage.HtsNumber))))
                .ForMember(x => x.DatePrefferedToBeEnrolled, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsClientLinkage.DatePrefferedToBeEnrolled))))
                .ForMember(x => x.FacilityReferredTo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientLinkage.FacilityReferredTo))))
                .ForMember(x => x.HandedOverTo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientLinkage.HandedOverTo))))
                .ForMember(x => x.HandedOverToCadre, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientLinkage.HandedOverToCadre))))
                .ForMember(x => x.EnrolledFacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientLinkage.EnrolledFacilityName))))
                .ForMember(x => x.ReferralDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsClientLinkage.ReferralDate))))
                .ForMember(x => x.DateEnrolled, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsClientLinkage.DateEnrolled))))
                .ForMember(x => x.ReportedCCCNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientLinkage.ReportedCCCNumber))))
                .ForMember(x => x.ReportedStartARTDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsClientLinkage.ReportedStartARTDate))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientLinkage.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientLinkage.RecordUUID))));
            CreateMap<TempHtsClientLinkage, HtsClientLinkage>();

            //hts test kits
            CreateMap<IDataRecord, TempHtsTestKits>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsTestKits.PatientPk))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsTestKits.SiteCode))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.FacilityName))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.Project))))
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.HtsNumber))))
                .ForMember(x => x.EncounterId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsTestKits.EncounterId))))
                .ForMember(x => x.TestKitName1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.TestKitName1))))
                .ForMember(x => x.TestKitLotNumber1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.TestKitLotNumber1))))
                .ForMember(x => x.TestKitExpiry1, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsTestKits.TestKitExpiry1))))
                .ForMember(x => x.TestResult1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.TestResult1))))
                .ForMember(x => x.TestKitName2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.TestKitName2))))
                .ForMember(x => x.TestKitLotNumber2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.TestKitLotNumber2))))
                .ForMember(x => x.TestKitExpiry2, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsTestKits.TestKitExpiry2))))
                .ForMember(x => x.TestResult2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.TestResult2))))
                .ForMember(x => x.SyphilisResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.SyphilisResult))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsTestKits.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsTestKits.RecordUUID))));
            CreateMap<TempHtsTestKits, HtsTestKits>();

            //hts client trace
            CreateMap<IDataRecord, TempHtsClientTracing>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientTracing.PatientPk))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientTracing.SiteCode))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTracing.FacilityName))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTracing.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTracing.Project))))
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTracing.HtsNumber))))
                .ForMember(x => x.TracingType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTracing.TracingType))))
                .ForMember(x => x.TracingDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsClientTracing.TracingDate))))
                .ForMember(x => x.TracingOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTracing.TracingOutcome))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsClientTracing.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsClientTracing.RecordUUID))));
            CreateMap<TempHtsClientTracing, HtsClientTracing>();

            //hts partner trace
            CreateMap<IDataRecord, TempHtsPartnerTracing>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerTracing.PatientPk))))
                .ForMember(x => x.PartnerPersonId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerTracing.PartnerPersonId))))
                .ForMember(x => x.TraceType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerTracing.TraceType)))) 
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerTracing.HtsNumber))))
                .ForMember(x => x.TraceDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsPartnerTracing.TraceDate)))) 
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerTracing.FacilityName))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerTracing.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerTracing.Project))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerTracing.SiteCode))))
                .ForMember(x => x.BookingDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsPartnerTracing.BookingDate))))
                .ForMember(x => x.TraceOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerTracing.TraceOutcome))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerTracing.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerTracing.RecordUUID))));
            CreateMap<TempHtsPartnerTracing, HtsPartnerTracing>();

            //hts client trace
            CreateMap<IDataRecord, TempHtsPartnerNotificationServices>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerNotificationServices.PatientPk))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerNotificationServices.SiteCode))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.FacilityName)))) 
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.HtsNumber))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.Project)))) 
                .ForMember(x => x.PartnerPatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerNotificationServices.PartnerPatientPk))))
                .ForMember(x => x.PartnerPersonID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerNotificationServices.PartnerPersonID))))
                .ForMember(x => x.Age, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerNotificationServices.Age))))
                .ForMember(x => x.Sex, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.Sex))))
                .ForMember(x => x.RelationsipToIndexClient, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.RelationsipToIndexClient))))
                .ForMember(x => x.ScreenedForIpv, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.ScreenedForIpv))))
                .ForMember(x => x.IpvScreeningOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.IpvScreeningOutcome))))
                .ForMember(x => x.CurrentlyLivingWithIndexClient, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.CurrentlyLivingWithIndexClient))))
                .ForMember(x => x.KnowledgeOfHivStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.KnowledgeOfHivStatus))))
                .ForMember(x => x.PnsApproach, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.PnsApproach))))
                .ForMember(x => x.PnsConsent, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.PnsConsent))))
                .ForMember(x => x.LinkedToCare, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.LinkedToCare))))
                .ForMember(x => x.LinkDateLinkedToCare, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsPartnerNotificationServices.LinkDateLinkedToCare))))
                .ForMember(x => x.CccNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.CccNumber))))
                .ForMember(x => x.FacilityLinkedTo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.FacilityLinkedTo))))
                .ForMember(x => x.Dob, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsPartnerNotificationServices.Dob))))
                .ForMember(x => x.DateElicited, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsPartnerNotificationServices.DateElicited))))
                .ForMember(x => x.MaritalStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.MaritalStatus))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerNotificationServices.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsPartnerNotificationServices.RecordUUID))))
                .ForMember(x => x.IndexPatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsPartnerNotificationServices.IndexPatientPk))))
                ;
            CreateMap<TempHtsPartnerNotificationServices, HtsPartnerNotificationServices>();
            
            //hts eligibility screening
            CreateMap<IDataRecord, TempHtsEligibilityExtract>()
                .ForMember(x => x.PatientPk, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsEligibilityExtract.PatientPk))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsEligibilityExtract.SiteCode))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.FacilityName))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.Project))))
                .ForMember(x => x.HtsNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.HtsNumber))))
                .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsEligibilityExtract.VisitID))))
                .ForMember(x => x.EncounterId, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.EncounterId))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsEligibilityExtract.VisitDate))))
                .ForMember(x => x.PopulationType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.PopulationType))))
                .ForMember(x => x.KeyPopulation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.KeyPopulation))))
                .ForMember(x => x.PriorityPopulation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.PriorityPopulation))))
                .ForMember(x => x.Department, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.Department))))
                .ForMember(x => x.PatientType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.PatientType))))
                .ForMember(x => x.IsHealthWorker, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.IsHealthWorker))))
                .ForMember(x => x.RelationshipWithContact, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.RelationshipWithContact))))
                .ForMember(x => x.TestedHIVBefore, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.TestedHIVBefore))))
                .ForMember(x => x.WhoPerformedTest, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.WhoPerformedTest))))
                .ForMember(x => x.ResultOfHIV, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ResultOfHIV))))
                .ForMember(x => x.DateTestedSelf, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsEligibilityExtract.DateTestedSelf ))))
                .ForMember(x => x.StartedOnART, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.StartedOnART))))
                .ForMember(x => x.CCCNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.CCCNumber))))
                .ForMember(x => x.EverHadSex, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.EverHadSex))))
                .ForMember(x => x.SexuallyActive, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.SexuallyActive))))
                .ForMember(x => x.NewPartner, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.NewPartner))))
                .ForMember(x => x.PartnerHIVStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.PartnerHIVStatus))))
                .ForMember(x => x.CoupleDiscordant, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.CoupleDiscordant))))
                .ForMember(x => x.MultiplePartners, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.MultiplePartners))))
                .ForMember(x => x.NumberOfPartners, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsEligibilityExtract.NumberOfPartners))))
                .ForMember(x => x.AlcoholSex, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.AlcoholSex))))
                .ForMember(x => x.MoneySex, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.MoneySex))))
                .ForMember(x => x.CondomBurst, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.CondomBurst))))
                .ForMember(x => x.UnknownStatusPartner, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.UnknownStatusPartner))))
                .ForMember(x => x.KnownStatusPartner, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.KnownStatusPartner))))
                .ForMember(x => x.Pregnant, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.Pregnant))))
                .ForMember(x => x.BreastfeedingMother, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.BreastfeedingMother))))
                .ForMember(x => x.ExperiencedGBV, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ExperiencedGBV))))
                .ForMember(x => x.CurrentlyOnPrep, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.CurrentlyOnPrep))))
                .ForMember(x => x.CurrentlyOnPep, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.CurrentlyOnPep))))
                .ForMember(x => x.CurrentlyHasSTI, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.CurrentlyHasSTI))))
                .ForMember(x => x.SharedNeedle, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.SharedNeedle))))
                .ForMember(x => x.NeedleStickInjuries, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.NeedleStickInjuries))))
                .ForMember(x => x.TraditionalProcedures, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.TraditionalProcedures))))
                .ForMember(x => x.ChildReasonsForIneligibility, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ChildReasonsForIneligibility))))
                .ForMember(x => x.EligibleForTest, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.EligibleForTest))))
                .ForMember(x => x.ReasonsForIneligibility, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ReasonsForIneligibility))))
                .ForMember(x => x.SpecificReasonForIneligibility, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsEligibilityExtract.SpecificReasonForIneligibility))))
                .ForMember(x => x.MothersStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.MothersStatus))))
                .ForMember(x => x.ResultOfHIVSelf, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ResultOfHIVSelf))))
                .ForMember(x => x.DateTestedProvider, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsEligibilityExtract.DateTestedProvider))))
                .ForMember(x => x.ScreenedTB, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ScreenedTB))))
                .ForMember(x => x.Cough, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.Cough))))
                .ForMember(x => x.Fever, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.Fever))))
                .ForMember(x => x.WeightLoss, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.WeightLoss))))
                .ForMember(x => x.NightSweats, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.NightSweats))))
                .ForMember(x => x.Lethargy, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.Lethargy))))
                .ForMember(x => x.TBStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.TBStatus))))
                .ForMember(x => x.ReferredForTesting, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ReferredForTesting))))
                .ForMember(x => x.AssessmentOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.AssessmentOutcome))))
                .ForMember(x => x.TypeGBV, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.TypeGBV))))
                .ForMember(x => x.ForcedSex, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ForcedSex))))
                .ForMember(x => x.ReceivedServices, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ReceivedServices))))
                .ForMember(x => x.Disability, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.Disability))))
                .ForMember(x => x.DisabilityType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.DisabilityType))))
                .ForMember(x => x.HTSStrategy, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.HTSStrategy))))
                .ForMember(x => x.HTSEntryPoint, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.HTSEntryPoint))))
                .ForMember(x => x.HIVRiskCategory, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.HIVRiskCategory))))
                .ForMember(x => x.ReasonRefferredForTesting, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ReasonRefferredForTesting))))
                .ForMember(x => x.ReasonNotReffered, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ReasonNotReffered))))

                .ForMember(x => x.DateCreated, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsEligibilityExtract.DateCreated))))
                .ForMember(x => x.DateLastModified, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHtsEligibilityExtract.DateLastModified))))
                .ForMember(x => x.ContactWithTBCase, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.ContactWithTBCase ))))
                .ForMember(x => x.HtsRiskScore, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.HtsRiskScore))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempHtsEligibilityExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHtsEligibilityExtract.RecordUUID))));
                ;
            CreateMap<TempHtsEligibilityExtract, HtsEligibilityExtract>();
            
        }

    }
}
