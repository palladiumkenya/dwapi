using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Dwh
{
    public class TempExtractProfile : Profile
    {
        public TempExtractProfile()
        {
            //Patient Extract
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

            CreateMap<TempPatientExtract, PatientExtract>()
               .ForMember(x => x.DatePreviousARTStart, o => o.MapFrom(s => s.PreviousARTStartDate));

            // Patient Art Extract
            CreateMap<IDataRecord, TempPatientArtExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.PatientPK))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.FacilityName))))
                .ForMember(x => x.DOB, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.DOB))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.SiteCode))))
                .ForMember(x => x.Gender, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.Gender))))
                .ForMember(x => x.RegistrationDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.RegistrationDate))))
                .ForMember(x => x.PatientSource, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.PatientSource))))
                .ForMember(x => x.PreviousARTStartDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.PreviousARTStartDate))))
                .ForMember(x => x.LastVisit, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.LastVisit))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.Project))))
                .ForMember(x => x.AgeEnrollment, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.AgeEnrollment))))
                .ForMember(x => x.AgeARTStart, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.AgeARTStart))))
                .ForMember(x => x.AgeLastVisit, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.AgeLastVisit))))
                .ForMember(x => x.RegistrationDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.RegistrationDate))))
                .ForMember(x => x.PatientSource, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.PatientSource))))
                .ForMember(x => x.StartARTDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.StartARTDate))))
                .ForMember(x => x.PreviousARTRegimen, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.PreviousARTRegimen))))
                .ForMember(x => x.StartARTAtThisFacility, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.StartARTAtThisFacility))))
                .ForMember(x => x.StartRegimen, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.StartRegimen))))
                .ForMember(x => x.StartRegimenLine, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.StartRegimenLine))))
                .ForMember(x => x.LastARTDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.LastARTDate))))
                .ForMember(x => x.LastRegimen, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.LastRegimen))))
                .ForMember(x => x.LastRegimenLine, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.LastRegimenLine))))
                .ForMember(x => x.Duration, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.Duration))))
                .ForMember(x => x.ExpectedReturn, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.ExpectedReturn))))
                .ForMember(x => x.Provider, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.Provider))))
                .ForMember(x => x.ExitReason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.ExitReason))))
                .ForMember(x => x.ExitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.ExitDate))));

            CreateMap<TempPatientArtExtract, PatientArtExtract>();

            //Patient Baseline Extract
            CreateMap<IDataRecord, TempPatientBaselinesExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientBaselinesExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientBaselinesExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientBaselinesExtract.Project))))
                .ForMember(x => x.bCD4, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.bCD4))))
                .ForMember(x => x.bCD4Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.bCD4Date))))
                .ForMember(x => x.bWAB, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.bWAB))))
                .ForMember(x => x.bWABDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.bWABDate))))
                .ForMember(x => x.bWHO, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.bWHO))))
                .ForMember(x => x.bWHODate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.bWHODate))))
                .ForMember(x => x.eWAB, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.eWAB))))
                .ForMember(x => x.eWABDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.eWABDate))))
                .ForMember(x => x.eCD4, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.eCD4))))
                .ForMember(x => x.eCD4Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.eCD4Date))))
                .ForMember(x => x.eWHO, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.eWHO))))
                .ForMember(x => x.lastWHO, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.lastWHO))))
                .ForMember(x => x.lastWHODate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.lastWHODate))))
                .ForMember(x => x.lastCD4, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.lastCD4))))
                .ForMember(x => x.lastCD4Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.lastCD4Date))))
                .ForMember(x => x.lastWAB, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.lastWAB))))
                .ForMember(x => x.lastWABDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.lastWABDate))))
                .ForMember(x => x.m12CD4, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.m12CD4))))
                .ForMember(x => x.m12CD4Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.m12CD4Date))))
                .ForMember(x => x.m6CD4, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.m6CD4))))
                .ForMember(x => x.m6CD4Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.m6CD4Date))));

            CreateMap<TempPatientBaselinesExtract, PatientBaselinesExtract>();

            //Patient Laboratory Extract
            CreateMap<IDataRecord, TempPatientLaboratoryExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientLaboratoryExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientLaboratoryExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientLaboratoryExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientLaboratoryExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientLaboratoryExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientLaboratoryExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientLaboratoryExtract.FacilityName))))
                .ForMember(x => x.SatelliteName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientLaboratoryExtract.SatelliteName))))
                .ForMember(x => x.VisitId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientLaboratoryExtract.VisitId))))
                .ForMember(x => x.OrderedByDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientLaboratoryExtract.OrderedByDate))))
                .ForMember(x => x.ReportedByDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientLaboratoryExtract.ReportedByDate))))
                .ForMember(x => x.TestName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientLaboratoryExtract.TestName))))
                .ForMember(x => x.EnrollmentTest, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientLaboratoryExtract.EnrollmentTest))))
                .ForMember(x => x.TestResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientLaboratoryExtract.TestResult))));

            CreateMap<TempPatientLaboratoryExtract, PatientLaboratoryExtract>();

            // Patient Pharmacy Extract
            CreateMap<IDataRecord, TempPatientPharmacyExtract>()
               .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientPharmacyExtract.PatientPK))))
               .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.PatientID))))
               .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientPharmacyExtract.FacilityId))))
               .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientPharmacyExtract.SiteCode))))
               .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.Emr))))
               .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.Project))))
               .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientPharmacyExtract.VisitID))))
               .ForMember(x => x.Drug, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.Drug))))
               .ForMember(x => x.Provider, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.Provider))))
               .ForMember(x => x.DispenseDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientPharmacyExtract.DispenseDate))))
               .ForMember(x => x.Duration, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientPharmacyExtract.Duration))))
               .ForMember(x => x.ExpectedReturn, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientPharmacyExtract.ExpectedReturn))))
               .ForMember(x => x.TreatmentType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.TreatmentType))))
               .ForMember(x => x.RegimenLine, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.RegimenLine))))
                .ForMember(x => x.PeriodTaken, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.PeriodTaken))))
                .ForMember(x => x.ProphylaxisType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.ProphylaxisType))));

            CreateMap<TempPatientPharmacyExtract, PatientPharmacyExtract>();

            //Patient Status Extract
            CreateMap<IDataRecord, TempPatientStatusExtract>()
               .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientStatusExtract.PatientPK))))
               .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.PatientID))))
               .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientStatusExtract.FacilityId))))
               .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientStatusExtract.SiteCode))))
               .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.Emr))))
               .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.Project))))
               .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.FacilityName))))
               .ForMember(x => x.ExitDescription, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.ExitDescription))))
               .ForMember(x => x.ExitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientStatusExtract.ExitDate))))
               .ForMember(x => x.ExitReason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.ExitReason))));

            CreateMap<TempPatientStatusExtract, PatientStatusExtract>();

            // Patient Visit Extract
            CreateMap<IDataRecord, TempPatientVisitExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientVisitExtract.PatientPK))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.FacilityName))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientVisitExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientVisitExtract.SiteCode))))
                .ForMember(x => x.VisitId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientVisitExtract.VisitId))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientVisitExtract.VisitDate))))
                .ForMember(x => x.Service, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.Service))))
                .ForMember(x => x.VisitType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.VisitType))))
                .ForMember(x => x.WHOStage, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientVisitExtract.WHOStage))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.Project))))
                .ForMember(x => x.WABStage, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.WABStage))))
                .ForMember(x => x.Pregnant, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.Pregnant))))
                .ForMember(x => x.LMP, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientVisitExtract.LMP))))
                .ForMember(x => x.EDD, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientVisitExtract.EDD))))
                .ForMember(x => x.Height, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientVisitExtract.Height))))
                .ForMember(x => x.Weight, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientVisitExtract.Weight))))
                .ForMember(x => x.BP, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.BP))))
                .ForMember(x => x.OI, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.OI))))
                .ForMember(x => x.OIDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientVisitExtract.OIDate))))
                .ForMember(x => x.Adherence, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.Adherence))))
                .ForMember(x => x.AdherenceCategory, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.AdherenceCategory))))
                .ForMember(x => x.SubstitutionFirstlineRegimenDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientVisitExtract.SubstitutionFirstlineRegimenDate))))
                .ForMember(x => x.SubstitutionFirstlineRegimenReason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.SubstitutionFirstlineRegimenReason))))
                .ForMember(x => x.SubstitutionSecondlineRegimenDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientVisitExtract.SubstitutionSecondlineRegimenDate))))
                .ForMember(x => x.SubstitutionSecondlineRegimenReason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.SubstitutionSecondlineRegimenReason))))
                .ForMember(x => x.SecondlineRegimenChangeDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientVisitExtract.SecondlineRegimenChangeDate))))
                .ForMember(x => x.SecondlineRegimenChangeReason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.SecondlineRegimenChangeReason))))
                .ForMember(x => x.FamilyPlanningMethod, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.FamilyPlanningMethod))))
                .ForMember(x => x.PwP, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.PwP))))
                .ForMember(x => x.GestationAge, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientVisitExtract.GestationAge))))
                .ForMember(x => x.NextAppointmentDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientVisitExtract.NextAppointmentDate))));



            CreateMap<TempPatientVisitExtract, PatientVisitExtract>();
        }
    }
}