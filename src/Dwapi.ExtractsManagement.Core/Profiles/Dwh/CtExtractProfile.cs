﻿using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Dwh
{
    public class CtExtractProfile : Profile
    {
        public CtExtractProfile()
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
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.Emr))))
                .ForMember(x => x.Inschool, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.Inschool))))
                .ForMember(x => x.KeyPopulationType, o =>  o.UseValue(string.Empty))
                .ForMember(x => x.Orphan, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.Orphan))))
                .ForMember(x => x.PatientResidentCounty, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PatientResidentCounty))))
                .ForMember(x => x.PatientResidentLocation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PatientResidentLocation))))
                .ForMember(x => x.PatientResidentSubCounty, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PatientResidentSubCounty))))
                .ForMember(x => x.PatientResidentSubLocation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PatientResidentSubLocation))))
                .ForMember(x => x.PatientResidentVillage, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PatientResidentVillage))))
                .ForMember(x => x.PatientResidentWard, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PatientResidentWard))))
                .ForMember(x => x.PatientType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PatientType))))
                .ForMember(x => x.PopulationType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.PopulationType))))
                .ForMember(x => x.TransferInDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.TransferInDate))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.Project))))
                .ForMember(x => x.NUPI, o => o.MapFrom(s => s.GetOptionalStringOrDefault(nameof(TempPatientExtract.NUPI))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientExtract.RecordUUID)))); 
            CreateMap<TempPatientExtract, PatientExtract>()
               .ForMember(x => x.DatePreviousARTStart, o => o.MapFrom(s => s.PreviousARTStartDate));
            

            // Patient Art Extract
            CreateMap<IDataRecord, TempPatientArtExtract>()
                .ForMember(x => x.PatientPK,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.PatientPK))))
                .ForMember(x => x.FacilityName,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.FacilityName))))
                .ForMember(x => x.DOB, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.DOB))))
                .ForMember(x => x.PatientID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.PatientID))))
                .ForMember(x => x.FacilityId,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.FacilityId))))
                .ForMember(x => x.SiteCode,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.SiteCode))))
                .ForMember(x => x.Gender,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.Gender))))
                .ForMember(x => x.RegistrationDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.RegistrationDate))))
                .ForMember(x => x.PatientSource,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.PatientSource))))
                .ForMember(x => x.PreviousARTStartDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.PreviousARTStartDate))))
                .ForMember(x => x.LastVisit,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.LastVisit))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.Emr))))
                .ForMember(x => x.Project,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.Project))))
                .ForMember(x => x.AgeEnrollment,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.AgeEnrollment))))
                .ForMember(x => x.AgeARTStart,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.AgeARTStart))))
                .ForMember(x => x.AgeLastVisit,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.AgeLastVisit))))
                .ForMember(x => x.RegistrationDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.RegistrationDate))))
                .ForMember(x => x.PatientSource,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.PatientSource))))
                .ForMember(x => x.StartARTDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.StartARTDate))))
                .ForMember(x => x.PreviousARTRegimen,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.PreviousARTRegimen))))
                .ForMember(x => x.StartARTAtThisFacility,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.StartARTAtThisFacility))))
                .ForMember(x => x.StartRegimen,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.StartRegimen))))
                .ForMember(x => x.StartRegimenLine,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.StartRegimenLine))))
                .ForMember(x => x.LastARTDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.LastARTDate))))
                .ForMember(x => x.LastRegimen,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.LastRegimen))))
                .ForMember(x => x.LastRegimenLine,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.LastRegimenLine))))
                .ForMember(x => x.Duration,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.Duration))))
                .ForMember(x => x.ExpectedReturn,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.ExpectedReturn))))
                .ForMember(x => x.Provider,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.Provider))))
                .ForMember(x => x.ExitReason,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.ExitReason))))
                .ForMember(x => x.ExitDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientArtExtract.ExitDate))))
                .ForMember(x => x.Date_Created,
                    o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientArtExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified,
                    o => o.MapFrom(
                        s => s.GetOptionalNullDateOrDefault(nameof(TempPatientArtExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientArtExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientArtExtract.RecordUUID))));
            CreateMap<TempPatientArtExtract, PatientArtExtract>();

            //Patient Baseline Extract
            CreateMap<IDataRecord, TempPatientBaselinesExtract>()
                .ForMember(x => x.PatientPK,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.PatientPK))))
                .ForMember(x => x.PatientID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientBaselinesExtract.PatientID))))
                .ForMember(x => x.FacilityId,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.FacilityId))))
                .ForMember(x => x.SiteCode,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.SiteCode))))
                .ForMember(x => x.Emr,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientBaselinesExtract.Emr))))
                .ForMember(x => x.Project,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientBaselinesExtract.Project))))
                .ForMember(x => x.bCD4,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.bCD4))))
                .ForMember(x => x.bCD4Date,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.bCD4Date))))
                .ForMember(x => x.bWAB,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.bWAB))))
                .ForMember(x => x.bWABDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.bWABDate))))
                .ForMember(x => x.bWHO,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.bWHO))))
                .ForMember(x => x.bWHODate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.bWHODate))))
                .ForMember(x => x.eWAB,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.eWAB))))
                .ForMember(x => x.eWABDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.eWABDate))))
                .ForMember(x => x.eCD4,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.eCD4))))
                .ForMember(x => x.eCD4Date,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.eCD4Date))))
                .ForMember(x => x.eWHO,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.eWHO))))
                .ForMember(x => x.lastWHO,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.lastWHO))))
                .ForMember(x => x.lastWHODate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.lastWHODate))))
                .ForMember(x => x.lastCD4,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.lastCD4))))
                .ForMember(x => x.lastCD4Date,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.lastCD4Date))))
                .ForMember(x => x.lastWAB,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.lastWAB))))
                .ForMember(x => x.lastWABDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.lastWABDate))))
                .ForMember(x => x.m12CD4,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.m12CD4))))
                .ForMember(x => x.m12CD4Date,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.m12CD4Date))))
                .ForMember(x => x.m6CD4,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.m6CD4))))
                .ForMember(x => x.m6CD4Date,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.m6CD4Date))))
                .ForMember(x => x.eWHODate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientBaselinesExtract.eWHODate))))
                .ForMember(x => x.Date_Created,
                    o => o.MapFrom(
                        s => s.GetOptionalNullDateOrDefault(nameof(TempPatientBaselinesExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified,
                    o => o.MapFrom(s =>
                        s.GetOptionalNullDateOrDefault(nameof(TempPatientBaselinesExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientBaselinesExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientBaselinesExtract.RecordUUID))));


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
                .ForMember(x => x.Reason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientLaboratoryExtract.Reason))))
                .ForMember(x => x.EnrollmentTest, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientLaboratoryExtract.EnrollmentTest))))
                .ForMember(x => x.TestResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientLaboratoryExtract.TestResult))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientLaboratoryExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientLaboratoryExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientLaboratoryExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientLaboratoryExtract.RecordUUID))));

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
                .ForMember(x => x.ProphylaxisType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.ProphylaxisType))))
               .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientPharmacyExtract.Date_Created))))
               .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientPharmacyExtract.Date_Last_Modified))))
               .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientPharmacyExtract.Voided))))
               .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientPharmacyExtract.RecordUUID))));

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
               .ForMember(x => x.ExitReason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.ExitReason))))
               //TODO:Check why these were NOT mapped INFORM PARTNERS
               .ForMember(x => x.TOVerified, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.TOVerified))))
               .ForMember(x => x.TOVerifiedDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientStatusExtract.TOVerifiedDate))))
               .ForMember(x => x.ReEnrollmentDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientStatusExtract.ReEnrollmentDate))))
               .ForMember(x => x.ReasonForDeath, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.ReasonForDeath))))
               .ForMember(x => x.SpecificDeathReason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.SpecificDeathReason))))
               .ForMember(x => x.DeathDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientStatusExtract.DeathDate))))
               .ForMember(x => x.EffectiveDiscontinuationDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientStatusExtract.EffectiveDiscontinuationDate))))
               .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientStatusExtract.Date_Created))))
               .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientStatusExtract.Date_Last_Modified))))
               .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientStatusExtract.Voided))))
               .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientStatusExtract.RecordUUID))));

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
                .ForMember(x => x.Height, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempPatientVisitExtract.Height))))
                .ForMember(x => x.Weight, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempPatientVisitExtract.Weight))))
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
                .ForMember(x => x.GestationAge, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempPatientVisitExtract.GestationAge))))

                .ForMember(x => x.DifferentiatedCare, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.DifferentiatedCare))))
                .ForMember(x => x.KeyPopulationType, o =>  o.UseValue(string.Empty))
                .ForMember(x => x.PopulationType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.PopulationType))))
                .ForMember(x => x.StabilityAssessment, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.StabilityAssessment))))
                .ForMember(x => x.RefillDate, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientVisitExtract.RefillDate))))
                .ForMember(x => x.ZScore, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.ZScore))))
                .ForMember(x => x.ZScoreAbsolute, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientVisitExtract.ZScoreAbsolute))))
                .ForMember(x => x.PaedsDisclosure, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.PaedsDisclosure))))

                .ForMember(x => x.NextAppointmentDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientVisitExtract.NextAppointmentDate))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientVisitExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientVisitExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientVisitExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.RecordUUID))))
                .ForMember(x => x.WHOStagingOI, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.WHOStagingOI))))
                .ForMember(x => x.WantsToGetPregnant, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.WantsToGetPregnant))))
                .ForMember(x => x.AppointmentReminderWillingness, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientVisitExtract.AppointmentReminderWillingness))));
            CreateMap<TempPatientVisitExtract, PatientVisitExtract>();

            //Patient Adverse Event Extract
            CreateMap<IDataRecord, TempPatientAdverseEventExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientAdverseEventExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientAdverseEventExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientAdverseEventExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientAdverseEventExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientAdverseEventExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientAdverseEventExtract.Project))))
                .ForMember(x => x.AdverseEvent, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientAdverseEventExtract.AdverseEvent))))
                .ForMember(x => x.AdverseEventClinicalOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientAdverseEventExtract.AdverseEventClinicalOutcome))))
                .ForMember(x => x.AdverseEventActionTaken, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientAdverseEventExtract.AdverseEventActionTaken))))
                .ForMember(x => x.AdverseEventIsPregnant, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientAdverseEventExtract.AdverseEventIsPregnant))))
                .ForMember(x => x.AdverseEventStartDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientAdverseEventExtract.AdverseEventStartDate))))
                .ForMember(x => x.AdverseEventCause, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientAdverseEventExtract.AdverseEventCause))))
                .ForMember(x => x.AdverseEventRegimen, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientAdverseEventExtract.AdverseEventRegimen))))
                .ForMember(x => x.AdverseEventEndDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientAdverseEventExtract.AdverseEventEndDate))))
                .ForMember(x => x.Severity, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientAdverseEventExtract.Severity))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientAdverseEventExtract.VisitDate))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientAdverseEventExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempPatientAdverseEventExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientAdverseEventExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientAdverseEventExtract.RecordUUID))));

            CreateMap<TempPatientAdverseEventExtract, PatientAdverseEventExtract>();

            


            #region NewCT

            CreateMap<IDataRecord,TempAllergiesChronicIllnessExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAllergiesChronicIllnessExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempAllergiesChronicIllnessExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAllergiesChronicIllnessExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.FacilityName))))
                .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempAllergiesChronicIllnessExtract.VisitID))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempAllergiesChronicIllnessExtract.VisitDate))))
                .ForMember(x => x.ChronicIllness, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.ChronicIllness))))
                .ForMember(x => x.ChronicOnsetDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.ChronicOnsetDate))))
                .ForMember(x => x.knownAllergies, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.knownAllergies))))
                .ForMember(x => x.AllergyCausativeAgent, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.AllergyCausativeAgent))))
                .ForMember(x => x.AllergicReaction, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.AllergicReaction))))
                .ForMember(x => x.AllergySeverity, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.AllergySeverity))))
                .ForMember(x => x.AllergyOnsetDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempAllergiesChronicIllnessExtract.AllergyOnsetDate))))
                .ForMember(x => x.Skin, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.Skin))))
                .ForMember(x => x.Eyes, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.Eyes))))
                .ForMember(x => x.ENT, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.ENT))))
                .ForMember(x => x.Chest, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.Chest))))
                .ForMember(x => x.CVS, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.CVS))))
                .ForMember(x => x.Abdomen, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.Abdomen))))
                .ForMember(x => x.CNS, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.CNS))))
                .ForMember(x => x.Genitourinary, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.Genitourinary))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempAllergiesChronicIllnessExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempAllergiesChronicIllnessExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAllergiesChronicIllnessExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.RecordUUID))))
                .ForMember(x => x.Controlled, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.Controlled))))
                .ForMember(x => x.PatientHasChronicIllness, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAllergiesChronicIllnessExtract.PatientHasChronicIllness))));


            CreateMap<TempAllergiesChronicIllnessExtract,AllergiesChronicIllnessExtract>();

            
  CreateMap<IDataRecord,TempContactListingExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempContactListingExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempContactListingExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempContactListingExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.FacilityName))))
                .ForMember(x => x.PartnerPersonID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempContactListingExtract.PartnerPersonID))))
                .ForMember(x => x.ContactAge, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.ContactAge))))
                .ForMember(x => x.ContactSex, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.ContactSex))))
                .ForMember(x => x.ContactMaritalStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.ContactMaritalStatus))))
                .ForMember(x => x.RelationshipWithPatient, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.RelationshipWithPatient))))
                .ForMember(x => x.ScreenedForIpv, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.ScreenedForIpv))))
                .ForMember(x => x.IpvScreening, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.IpvScreening))))
                .ForMember(x => x.IPVScreeningOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.IPVScreeningOutcome))))
                .ForMember(x => x.CurrentlyLivingWithIndexClient, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.CurrentlyLivingWithIndexClient))))
                .ForMember(x => x.KnowledgeOfHivStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.KnowledgeOfHivStatus))))
                .ForMember(x => x.PnsApproach, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.PnsApproach))))
                .ForMember(x => x.ContactPatientPK, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempContactListingExtract.ContactPatientPK))))
                
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempContactListingExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempContactListingExtract.Date_Last_Modified))))
                                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempContactListingExtract.Voided))))

                .ForMember(x => x.RecordUUID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempContactListingExtract.RecordUUID))));

            CreateMap<TempContactListingExtract,ContactListingExtract>();


                ;
            CreateMap<IDataRecord,TempDepressionScreeningExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempDepressionScreeningExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempDepressionScreeningExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempDepressionScreeningExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.Project))))
                .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempDepressionScreeningExtract.VisitID))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.VisitDate))))
                .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.VisitID))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempDepressionScreeningExtract.VisitDate))))
                .ForMember(x => x.PHQ9_1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ9_1))))
                .ForMember(x => x.PHQ9_2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ9_2))))
                .ForMember(x => x.PHQ9_3, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ9_3))))
                .ForMember(x => x.PHQ9_4, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ9_4))))
                .ForMember(x => x.PHQ9_5, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ9_5))))
                .ForMember(x => x.PHQ9_6, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ9_6))))
                .ForMember(x => x.PHQ9_7, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ9_7))))
                .ForMember(x => x.PHQ9_8, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ9_8))))
                .ForMember(x => x.PHQ9_9, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ9_9))))
                .ForMember(x => x.PHQ9_9, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ9_9))))
                .ForMember(x => x.PHQ_9_rating, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.PHQ_9_rating))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempDepressionScreeningExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempDepressionScreeningExtract.Date_Last_Modified))))
                 .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempDepressionScreeningExtract.Voided))))

                .ForMember(x => x.RecordUUID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDepressionScreeningExtract.RecordUUID))));

            CreateMap<TempDepressionScreeningExtract,DepressionScreeningExtract>();

            CreateMap<IDataRecord,TempDrugAlcoholScreeningExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempDrugAlcoholScreeningExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDrugAlcoholScreeningExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempDrugAlcoholScreeningExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempDrugAlcoholScreeningExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDrugAlcoholScreeningExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDrugAlcoholScreeningExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDrugAlcoholScreeningExtract.FacilityName))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDrugAlcoholScreeningExtract.FacilityName))))
                .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempDrugAlcoholScreeningExtract.VisitID))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempDrugAlcoholScreeningExtract.VisitDate))))
                .ForMember(x => x.DrinkingAlcohol, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDrugAlcoholScreeningExtract.DrinkingAlcohol))))
                .ForMember(x => x.Smoking, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDrugAlcoholScreeningExtract.Smoking))))
                .ForMember(x => x.DrugUse, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDrugAlcoholScreeningExtract.DrugUse))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempDrugAlcoholScreeningExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempDrugAlcoholScreeningExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempDrugAlcoholScreeningExtract.Voided))))
                .ForMember(x => x.RecordUUID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDrugAlcoholScreeningExtract.RecordUUID))))
                ;
            CreateMap<TempDrugAlcoholScreeningExtract,DrugAlcoholScreeningExtract>();

            CreateMap<IDataRecord, TempEnhancedAdherenceCounsellingExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.FacilityName))))
.ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.VisitID))))
.ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.VisitDate))))
.ForMember(x => x.SessionNumber, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.SessionNumber))))
.ForMember(x => x.DateOfFirstSession, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.DateOfFirstSession))))
.ForMember(x => x.PillCountAdherence, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.PillCountAdherence))))
.ForMember(x => x.MMAS4_1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.MMAS4_1))))
.ForMember(x => x.MMAS4_2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.MMAS4_2))))
.ForMember(x => x.MMAS4_3, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.MMAS4_3))))
.ForMember(x => x.MMAS4_4, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.MMAS4_4))))
.ForMember(x => x.MMSA8_1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.MMSA8_1))))
.ForMember(x => x.MMSA8_2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.MMSA8_2))))
.ForMember(x => x.MMSA8_3, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.MMSA8_3))))
.ForMember(x => x.MMSA8_4, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.MMSA8_4))))
.ForMember(x => x.MMSAScore, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.MMSAScore))))
.ForMember(x => x.EACRecievedVL, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACRecievedVL))))
.ForMember(x => x.EACVL, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACVL))))
.ForMember(x => x.EACVLConcerns, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACVLConcerns))))
.ForMember(x => x.EACVLThoughts, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACVLThoughts))))
.ForMember(x => x.EACWayForward, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACWayForward))))
.ForMember(x => x.EACCognitiveBarrier, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACCognitiveBarrier))))
.ForMember(x => x.EACBehaviouralBarrier_1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACBehaviouralBarrier_1))))
.ForMember(x => x.EACBehaviouralBarrier_2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACBehaviouralBarrier_2))))
.ForMember(x => x.EACBehaviouralBarrier_3, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACBehaviouralBarrier_3))))
.ForMember(x => x.EACBehaviouralBarrier_4, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACBehaviouralBarrier_4))))
.ForMember(x => x.EACBehaviouralBarrier_5, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACBehaviouralBarrier_5))))
.ForMember(x => x.EACEmotionalBarriers_1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACEmotionalBarriers_1))))
.ForMember(x => x.EACEmotionalBarriers_2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACEmotionalBarriers_2))))
.ForMember(x => x.EACEconBarrier_1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACEconBarrier_1))))
.ForMember(x => x.EACEconBarrier_2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACEconBarrier_2))))
.ForMember(x => x.EACEconBarrier_3, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACEconBarrier_3))))
.ForMember(x => x.EACEconBarrier_4, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACEconBarrier_4))))
.ForMember(x => x.EACEconBarrier_5, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACEconBarrier_5))))
.ForMember(x => x.EACEconBarrier_6, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACEconBarrier_6))))
.ForMember(x => x.EACEconBarrier_7, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACEconBarrier_7))))
.ForMember(x => x.EACEconBarrier_8, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACEconBarrier_8))))
.ForMember(x => x.EACReviewImprovement, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACReviewImprovement))))
.ForMember(x => x.EACReviewMissedDoses, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACReviewMissedDoses))))
.ForMember(x => x.EACReviewStrategy, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACReviewStrategy))))
.ForMember(x => x.EACReferral, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACReferral))))
.ForMember(x => x.EACReferralApp, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACReferralApp))))
.ForMember(x => x.EACReferralExperience, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACReferralExperience))))
.ForMember(x => x.EACHomevisit, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACHomevisit))))
.ForMember(x => x.EACAdherencePlan, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACAdherencePlan))))
.ForMember(x => x.EACFollowupDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.EACFollowupDate))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.Voided))))
                .ForMember(x => x.RecordUUID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempEnhancedAdherenceCounsellingExtract.RecordUUID))))
                ;
            CreateMap<TempEnhancedAdherenceCounsellingExtract,EnhancedAdherenceCounsellingExtract>();

            CreateMap<IDataRecord,TempGbvScreeningExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempGbvScreeningExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempGbvScreeningExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempGbvScreeningExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempGbvScreeningExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempGbvScreeningExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempGbvScreeningExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempGbvScreeningExtract.FacilityName))))
                .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempGbvScreeningExtract.VisitID))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempGbvScreeningExtract.VisitDate))))
                .ForMember(x => x.IPV, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempGbvScreeningExtract.IPV))))
                .ForMember(x => x.PhysicalIPV, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempGbvScreeningExtract.PhysicalIPV))))
                .ForMember(x => x.EmotionalIPV, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempGbvScreeningExtract.EmotionalIPV))))
                .ForMember(x => x.SexualIPV, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempGbvScreeningExtract.SexualIPV))))
                .ForMember(x => x.IPVRelationship, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempGbvScreeningExtract.IPVRelationship))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempGbvScreeningExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempGbvScreeningExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempGbvScreeningExtract.Voided))))
                .ForMember(x => x.RecordUUID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempGbvScreeningExtract.RecordUUID))))
                ;
            CreateMap<TempGbvScreeningExtract,GbvScreeningExtract>();

            CreateMap<IDataRecord, TempIptExtract>()
                .ForMember(x => x.PatientPK,
                    o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempIptExtract.PatientPK))))
                .ForMember(x => x.PatientID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.PatientID))))
                .ForMember(x => x.FacilityId,
                    o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempIptExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempIptExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.Project))))
                .ForMember(x => x.FacilityName,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.FacilityName))))
                .ForMember(x => x.VisitID,
                    o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempIptExtract.VisitID))))
                .ForMember(x => x.VisitDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempIptExtract.VisitDate))))
                .ForMember(x => x.OnTBDrugs,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.OnTBDrugs))))
                .ForMember(x => x.OnIPT, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.OnIPT))))
                .ForMember(x => x.EverOnIPT,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.EverOnIPT))))
                .ForMember(x => x.Cough, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.Cough))))
                .ForMember(x => x.Fever, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.Fever))))
                .ForMember(x => x.NoticeableWeightLoss,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.NoticeableWeightLoss))))
                .ForMember(x => x.NightSweats,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.NightSweats))))
                .ForMember(x => x.Lethargy, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.Lethargy))))
                .ForMember(x => x.ICFActionTaken,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.ICFActionTaken))))
                .ForMember(x => x.TestResult,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.TestResult))))
                .ForMember(x => x.TBClinicalDiagnosis,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.TBClinicalDiagnosis))))
                .ForMember(x => x.ContactsInvited,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.ContactsInvited))))
                .ForMember(x => x.EvaluatedForIPT,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.EvaluatedForIPT))))
                .ForMember(x => x.StartAntiTBs,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.StartAntiTBs))))
                .ForMember(x => x.TBRxStartDate,
                    o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempIptExtract.TBRxStartDate))))
                .ForMember(x => x.TBScreening,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.TBScreening))))
                .ForMember(x => x.IPTClientWorkUp,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.IPTClientWorkUp))))
                .ForMember(x => x.StartIPT,
                    o => o.MapFrom(s => s.GetOptionalStringOrDefault(nameof(TempIptExtract.StartIPT))))
                .ForMember(x => x.IndicationForIPT,
                    o => o.MapFrom(s => s.GetOptionalStringOrDefault(nameof(TempIptExtract.IndicationForIPT))))
                .ForMember(x => x.Date_Created,
                    o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempIptExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified,
                    o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempIptExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempIptExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.RecordUUID))))
                .ForMember(x => x.TPTInitiationDate, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempIptExtract.TPTInitiationDate))))
                .ForMember(x => x.IPTDiscontinuation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.IPTDiscontinuation))))
                .ForMember(x => x.DateOfDiscontinuation, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempIptExtract.DateOfDiscontinuation))))
                .ForMember(x => x.Hepatoxicity, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.Hepatoxicity))))
                .ForMember(x => x.PeripheralNeuropathy, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.PeripheralNeuropathy))))
                .ForMember(x => x.Rash, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.Rash))))
                .ForMember(x => x.Adherence, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIptExtract.Adherence))));
            CreateMap<TempIptExtract,IptExtract>();


            CreateMap<IDataRecord,TempOtzExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempOtzExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOtzExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempOtzExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempOtzExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOtzExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOtzExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOtzExtract.FacilityName))))
                .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempOtzExtract.VisitID))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempOtzExtract.VisitDate))))
                .ForMember(x => x.OTZEnrollmentDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempOtzExtract.OTZEnrollmentDate))))
                .ForMember(x => x.TransferInStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOtzExtract.TransferInStatus))))
                .ForMember(x => x.ModulesPreviouslyCovered, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOtzExtract.ModulesPreviouslyCovered))))
                .ForMember(x => x.ModulesCompletedToday, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOtzExtract.ModulesCompletedToday))))
                .ForMember(x => x.SupportGroupInvolvement, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOtzExtract.SupportGroupInvolvement))))
                .ForMember(x => x.Remarks, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOtzExtract.Remarks))))
                .ForMember(x => x.TransitionAttritionReason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOtzExtract.TransitionAttritionReason))))
                .ForMember(x => x.OutcomeDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempOtzExtract.OutcomeDate))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempOvcExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempOvcExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempOvcExtract.Voided))))
                .ForMember(x => x.RecordUUID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.RecordUUID))))
                ;
            CreateMap<TempOtzExtract,OtzExtract>();

            CreateMap<IDataRecord,TempOvcExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempOvcExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempOvcExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempOvcExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.FacilityName))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.FacilityName))))
                .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempOvcExtract.VisitID))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempOvcExtract.VisitDate))))
                .ForMember(x => x.OVCEnrollmentDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempOvcExtract.OVCEnrollmentDate))))
                .ForMember(x => x.RelationshipToClient, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.RelationshipToClient))))
                .ForMember(x => x.EnrolledinCPIMS, o => o.MapFrom(s => s.GetOptionalStringOrDefault(nameof(TempOvcExtract.EnrolledinCPIMS))))
                .ForMember(x => x.CPIMSUniqueIdentifier, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.CPIMSUniqueIdentifier))))
                .ForMember(x => x.PartnerOfferingOVCServices, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.PartnerOfferingOVCServices))))
                .ForMember(x => x.OVCExitReason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.OVCExitReason))))
                .ForMember(x => x.ExitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempOvcExtract.ExitDate))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempOvcExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempOvcExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempOvcExtract.Voided))))
                .ForMember(x => x.RecordUUID,
                    o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempOvcExtract.RecordUUID))));
;
            CreateMap<TempOvcExtract,OvcExtract>();

            #endregion

            #region Covid
            
            CreateMap<IDataRecord,TempCovidExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.PatientPK))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.SiteCode))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.PatientID))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.FacilityName))))
                .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.VisitID))))
                .ForMember(x => x.Covid19AssessmentDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.Covid19AssessmentDate))))
                .ForMember(x => x.ReceivedCOVID19Vaccine, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.ReceivedCOVID19Vaccine))))
                .ForMember(x => x.DateGivenFirstDose, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.DateGivenFirstDose))))
                .ForMember(x => x.FirstDoseVaccineAdministered, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.FirstDoseVaccineAdministered))))
                .ForMember(x => x.DateGivenSecondDose, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.DateGivenSecondDose))))
                .ForMember(x => x.SecondDoseVaccineAdministered, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.SecondDoseVaccineAdministered))))
                .ForMember(x => x.VaccinationStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.VaccinationStatus))))
                .ForMember(x => x.VaccineVerification, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.VaccineVerification))))
                .ForMember(x => x.BoosterGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.BoosterGiven))))
                .ForMember(x => x.BoosterDose, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.BoosterDose))))
                .ForMember(x => x.BoosterDoseDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.BoosterDoseDate))))
                .ForMember(x => x.EverCOVID19Positive, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.EverCOVID19Positive))))
                .ForMember(x => x.COVID19TestDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.COVID19TestDate))))
                .ForMember(x => x.PatientStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.PatientStatus))))
                .ForMember(x => x.AdmissionStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.AdmissionStatus))))
                .ForMember(x => x.AdmissionUnit, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.AdmissionUnit))))
                .ForMember(x => x.MissedAppointmentDueToCOVID19, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.MissedAppointmentDueToCOVID19))))
                .ForMember(x => x.COVID19PositiveSinceLasVisit, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.COVID19PositiveSinceLasVisit))))
                .ForMember(x => x.COVID19TestDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.COVID19TestDate))))
                .ForMember(x => x.PatientStatusSinceLastVisit, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.PatientStatusSinceLastVisit))))
                .ForMember(x => x.AdmissionStatusSinceLastVisit, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.AdmissionStatusSinceLastVisit))))
                .ForMember(x => x.AdmissionStartDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.AdmissionStartDate))))
                .ForMember(x => x.AdmissionEndDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.AdmissionEndDate))))
                .ForMember(x => x.AdmissionUnitSinceLastVisit, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.AdmissionUnitSinceLastVisit))))
                .ForMember(x => x.SupplementalOxygenReceived, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.SupplementalOxygenReceived))))
                .ForMember(x => x.PatientVentilated, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.PatientVentilated))))
                .ForMember(x => x.TracingFinalOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.TracingFinalOutcome))))
                .ForMember(x => x.CauseOfDeath, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.CauseOfDeath))))
                .ForMember(x => x.Sequence, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.Sequence))))
                .ForMember(x => x.COVID19TestResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.COVID19TestResult))))
                                
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempCovidExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempCovidExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempCovidExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCovidExtract.RecordUUID))));
            CreateMap<TempCovidExtract,CovidExtract>();

            
            CreateMap<IDataRecord,TempDefaulterTracingExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.PatientPK))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.SiteCode))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.PatientID))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.FacilityName))))
                .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.VisitID))))
                .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.VisitDate))))
                .ForMember(x => x.EncounterId, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.EncounterId))))
                .ForMember(x => x.TracingType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.TracingType))))
                .ForMember(x => x.TracingOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.TracingOutcome))))
                .ForMember(x => x.AttemptNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.AttemptNumber))))
                .ForMember(x => x.IsFinalTrace, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.IsFinalTrace))))
                .ForMember(x => x.TrueStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.TrueStatus))))
                .ForMember(x => x.CauseOfDeath, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.CauseOfDeath))))
                .ForMember(x => x.Comments, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.Comments))))
                .ForMember(x => x.BookingDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.BookingDate))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempDefaulterTracingExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempDefaulterTracingExtract.Date_Last_Modified))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempDefaulterTracingExtract.Voided))))
                .ForMember(x => x.RecordUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.RecordUUID))))
                .ForMember(x => x.DatePromisedToCome, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempDefaulterTracingExtract.DatePromisedToCome))))
                .ForMember(x => x.ReasonForMissedAppointment, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempDefaulterTracingExtract.ReasonForMissedAppointment))))
                .ForMember(x => x.DateOfMissedAppointment, o => o.MapFrom(s => s.GetOptionalNullDateOrDefault(nameof(TempDefaulterTracingExtract.DateOfMissedAppointment))));
            CreateMap<TempDefaulterTracingExtract,DefaulterTracingExtract>();
         
            #endregion
            
                        //Patient  Cancer Screening Extract
                        CreateMap<IDataRecord, TempCancerScreeningExtract>()
                            .ForMember(x => x.PatientPK,
                                o => o.MapFrom(s =>
                                    s.GetNullIntOrDefault(nameof(TempCancerScreeningExtract.PatientPK))))
                            .ForMember(x => x.PatientID,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.PatientID))))
                            .ForMember(x => x.FacilityId,
                                o => o.MapFrom(s =>
                                    s.GetNullIntOrDefault(nameof(TempCancerScreeningExtract.FacilityId))))
                            .ForMember(x => x.SiteCode,
                                o => o.MapFrom(s =>
                                    s.GetNullIntOrDefault(nameof(TempCancerScreeningExtract.SiteCode))))
                            .ForMember(x => x.Emr,
                                o => o.MapFrom(
                                    s => s.GetStringOrDefault(nameof(TempCancerScreeningExtract.Emr))))
                            .ForMember(x => x.Project,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.Project))))
                            .ForMember(x => x.VisitType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCancerScreeningExtract.VisitType))))

                            .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetOptionalNullIntOrDefault(nameof(TempCancerScreeningExtract.VisitID))))
                            .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempCancerScreeningExtract.VisitDate))))

                            .ForMember(x => x.SmokesCigarette,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.SmokesCigarette))))
                            .ForMember(x => x.NumberYearsSmoked,
                                o => o.MapFrom(s =>
                                    s.GetOptionalNullIntOrDefault(nameof(TempCancerScreeningExtract.NumberYearsSmoked))))
                            .ForMember(x => x.NumberCigarettesPerDay,
                                o => o.MapFrom(s =>
                                    s.GetOptionalNullIntOrDefault(nameof(TempCancerScreeningExtract.NumberCigarettesPerDay))))
                            .ForMember(x => x.OtherFormTobacco,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.OtherFormTobacco))))
                            .ForMember(x => x.TakesAlcohol,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TakesAlcohol))))
                            .ForMember(x => x.HIVStatus,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.HIVStatus))))
                            .ForMember(x => x.FamilyHistoryOfCa,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.FamilyHistoryOfCa))))
                            .ForMember(x => x.PreviousCaTreatment,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.PreviousCaTreatment))))
                            .ForMember(x => x.SymptomsCa,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.SymptomsCa))))
                            .ForMember(x => x.CancerType,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.CancerType))))
                            .ForMember(x => x.FecalOccultBloodTest,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.FecalOccultBloodTest))))
                            .ForMember(x => x.TreatmentOccultBlood,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentOccultBlood))))
                            .ForMember(x => x.Colonoscopy,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.Colonoscopy))))
                            .ForMember(x => x.TreatmentColonoscopy,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentColonoscopy))))
                            .ForMember(x => x.EUA,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.EUA))))
                            .ForMember(x => x.TreatmentRetinoblastoma,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentRetinoblastoma))))
                            .ForMember(x => x.RetinoblastomaGene,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.RetinoblastomaGene))))
                            .ForMember(x => x.TreatmentEUA,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentEUA))))
                            .ForMember(x => x.DRE,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.DRE))))
                            .ForMember(x => x.TreatmentDRE,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentDRE))))
                            .ForMember(x => x.PSA,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.PSA))))
                            .ForMember(x => x.TreatmentPSA,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentPSA))))
                            .ForMember(x => x.VisualExamination,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.VisualExamination))))
                            .ForMember(x => x.TreatmentVE,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentVE))))
                            .ForMember(x => x.Cytology,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.Cytology))))
                            .ForMember(x => x.TreatmentCytology,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentCytology))))
                            .ForMember(x => x.Imaging,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.Imaging))))
                            .ForMember(x => x.TreatmentImaging,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentImaging))))
                            .ForMember(x => x.Biopsy,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.Biopsy))))
                            .ForMember(x => x.TreatmentBiopsy,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentBiopsy))))
                            .ForMember(x => x.PostTreatmentComplicationCause,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.PostTreatmentComplicationCause))))
                            .ForMember(x => x.OtherPostTreatmentComplication,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.OtherPostTreatmentComplication))))
                            .ForMember(x => x.ReferralReason,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.ReferralReason))))
                            
                           
                            .ForMember(x => x.NextAppointmentDate,
                                o => o.MapFrom(s =>
                                    s.GetNullDateOrDefault(nameof(TempCancerScreeningExtract.NextAppointmentDate))))
                            .ForMember(x => x.ScreeningType,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.ScreeningType))))
                            .ForMember(x => x.HPVScreeningResult,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.HPVScreeningResult))))
                            .ForMember(x => x.TreatmentHPV,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentHPV))))
                            
                            .ForMember(x => x.VIAVILIScreeningResult,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.VIAVILIScreeningResult))))
                            
                            .ForMember(x => x.PAPSmearScreeningResult,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.PAPSmearScreeningResult))))
                            .ForMember(x => x.TreatmentPapSmear,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentPapSmear))))
                            .ForMember(x => x.ReferalOrdered,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.ReferalOrdered))))
                            .ForMember(x => x.Colposcopy,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.Colposcopy))))
                            .ForMember(x => x.TreatmentColposcopy,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentColposcopy))))
                            
                            .ForMember(x => x.CBE,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.CBE))))
                            .ForMember(x => x.TreatmentCBE,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentCBE))))
                            .ForMember(x => x.Ultrasound,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.Ultrasound))))
                            .ForMember(x => x.TreatmentUltraSound,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.TreatmentUltraSound))))
                            .ForMember(x => x.IfTissueDiagnosis,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.IfTissueDiagnosis))))
                            .ForMember(x => x.DateTissueDiagnosis,
                                o => o.MapFrom(s =>
                                    s.GetNullDateOrDefault(nameof(TempCancerScreeningExtract.DateTissueDiagnosis))))
                            .ForMember(x => x.ReasonNotDone,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.ReasonNotDone))))
                            
                            .ForMember(x => x.Referred,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.Referred))))
                            .ForMember(x => x.ReasonForReferral,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.ReasonForReferral))))

                            .ForMember(x => x.Date_Created,
                                o => o.MapFrom(s =>
                                    s.GetNullDateOrDefault(nameof(TempCancerScreeningExtract.Date_Created))))
                            .ForMember(x => x.Date_Last_Modified,
                                o => o.MapFrom(s =>
                                    s.GetNullDateOrDefault(
                                        nameof(TempCancerScreeningExtract.Date_Last_Modified))))
                            .ForMember(x => x.Voided,
                                o => o.MapFrom(s =>
                                    s.GetNullIntOrDefault(nameof(TempCancerScreeningExtract.Voided))))
                            .ForMember(x => x.RecordUUID,
                                o => o.MapFrom(s =>
                                    s.GetStringOrDefault(nameof(TempCancerScreeningExtract.RecordUUID))));
          
            
            CreateMap<TempCancerScreeningExtract, CancerScreeningExtract>();
            
            
                    //Patient IIT risk scores Extract
            CreateMap<IDataRecord, TempIITRiskScoresExtract>()
                .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempIITRiskScoresExtract.PatientPK))))
                .ForMember(x => x.PatientID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIITRiskScoresExtract.PatientID))))
                .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempIITRiskScoresExtract.FacilityId))))
                .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempIITRiskScoresExtract.SiteCode))))
                .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIITRiskScoresExtract.Emr))))
                .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIITRiskScoresExtract.Project))))
                .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIITRiskScoresExtract.FacilityName))))
                .ForMember(x => x.RiskScore, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIITRiskScoresExtract.RiskScore))))
                .ForMember(x => x.RiskFactors, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIITRiskScoresExtract.RiskFactors))))
                .ForMember(x => x.RiskDescription, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIITRiskScoresExtract.RiskDescription))))
                .ForMember(x => x.RiskEvaluationDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempIITRiskScoresExtract.RiskEvaluationDate))))
                .ForMember(x => x.Date_Created, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempIITRiskScoresExtract.Date_Created))))
                .ForMember(x => x.Date_Last_Modified, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempIITRiskScoresExtract.Date_Last_Modified))))
                .ForMember(x => x.SourceSysUUID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempIITRiskScoresExtract.SourceSysUUID))))
                .ForMember(x => x.Voided, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempIITRiskScoresExtract.Voided))));

            CreateMap<TempIITRiskScoresExtract, IITRiskScoresExtract>();
            
            
            //Patient ART Fast TrackExtract
            CreateMap<IDataRecord, TempArtFastTrackExtract>()
                       .ForMember(x => x.PatientPK,
                           o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempArtFastTrackExtract.PatientPK))))
                       .ForMember(x => x.PatientID,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.PatientID))))
                       .ForMember(x => x.FacilityId,
                           o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempArtFastTrackExtract.FacilityId))))
                       .ForMember(x => x.SiteCode,
                           o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempArtFastTrackExtract.SiteCode))))
                       .ForMember(x => x.Emr,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.Emr))))
                       .ForMember(x => x.Project,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.Project))))
                       .ForMember(x => x.FacilityName,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.FacilityName))))

                       .ForMember(x => x.ARTRefillModel,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.ARTRefillModel))))
                       .ForMember(x => x.VisitDate,
                           o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempArtFastTrackExtract.VisitDate))))
                       .ForMember(x => x.CTXDispensed,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.CTXDispensed))))
                       .ForMember(x => x.DapsoneDispensed,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.DapsoneDispensed))))
                       .ForMember(x => x.CondomsDistributed,
                           o => o.MapFrom(s =>
                               s.GetStringOrDefault(nameof(TempArtFastTrackExtract.CondomsDistributed))))
                       .ForMember(x => x.OralContraceptivesDispensed,
                           o => o.MapFrom(s =>
                               s.GetStringOrDefault(nameof(TempArtFastTrackExtract.OralContraceptivesDispensed))))
                       .ForMember(x => x.MissedDoses,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.MissedDoses))))
                       .ForMember(x => x.Fatigue,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.Fatigue))))
                       .ForMember(x => x.Cough,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.Cough))))
                       .ForMember(x => x.Fever,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.Fever))))
                       .ForMember(x => x.Rash,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.Rash))))
                       .ForMember(x => x.NauseaOrVomiting,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.NauseaOrVomiting))))
                       .ForMember(x => x.GenitalSoreOrDischarge,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.GenitalSoreOrDischarge))))
                       .ForMember(x => x.Diarrhea,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.Diarrhea))))
                       .ForMember(x => x.OtherSymptoms,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.OtherSymptoms))))
                       .ForMember(x => x.PregnancyStatus,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.PregnancyStatus))))
                       .ForMember(x => x.FPStatus,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.FPStatus))))
                       .ForMember(x => x.FPMethod,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.FPMethod))))
                       .ForMember(x => x.ReasonNotOnFP,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.ReasonNotOnFP))))
                       .ForMember(x => x.ReferredToClinic,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.ReferredToClinic))))
                       .ForMember(x => x.ReturnVisitDate,
                           o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempArtFastTrackExtract.ReturnVisitDate))))
                       .ForMember(x => x.RecordUUID,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempArtFastTrackExtract.RecordUUID))))
                       .ForMember(x => x.Voided,
                           o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempArtFastTrackExtract.Voided))))
                       .ForMember(x => x.Date_Created,
                           o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempArtFastTrackExtract.Date_Created))))
                       .ForMember(x => x.Date_Last_Modified,
                           o => o.MapFrom(s =>
                               s.GetNullDateOrDefault(nameof(TempArtFastTrackExtract.Date_Last_Modified))));

            CreateMap<TempArtFastTrackExtract, ArtFastTrackExtract>();
            
            
            //Patient Relationships Extract
            CreateMap<IDataRecord, TempRelationshipsExtract>()
                       .ForMember(x => x.PatientPK,
                           o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempRelationshipsExtract.PatientPK))))
                       .ForMember(x => x.PatientID,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempRelationshipsExtract.PatientID))))
                       .ForMember(x => x.FacilityId,
                           o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempRelationshipsExtract.FacilityId))))
                       .ForMember(x => x.SiteCode,
                           o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempRelationshipsExtract.SiteCode))))
                       .ForMember(x => x.Emr,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempRelationshipsExtract.Emr))))
                       .ForMember(x => x.Project,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempRelationshipsExtract.Project))))
                       .ForMember(x => x.FacilityName,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempRelationshipsExtract.FacilityName))))
                       .ForMember(x => x.RelationshipToPatient,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempRelationshipsExtract.RelationshipToPatient))))
                       .ForMember(x => x.StartDate,
                           o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempRelationshipsExtract.StartDate))))
                       .ForMember(x => x.EndDate,
                           o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempRelationshipsExtract.EndDate))))
                       .ForMember(x => x.RecordUUID,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempRelationshipsExtract.RecordUUID))))
                       .ForMember(x => x.Voided,
                           o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempRelationshipsExtract.Voided))))
                       .ForMember(x => x.Date_Created,
                           o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempRelationshipsExtract.Date_Created))))
                       .ForMember(x => x.Date_Last_Modified,
                           o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempRelationshipsExtract.Date_Last_Modified))))
                       .ForMember(x => x.PatientRelationshipToOther,
                           o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempRelationshipsExtract.PatientRelationshipToOther))))
                       .ForMember(x => x.PersonAPatientPk,
                           o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempRelationshipsExtract.PersonAPatientPk))))
                       .ForMember(x => x.PersonBPatientPk,
                           o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempRelationshipsExtract.PersonBPatientPk))));
            CreateMap<TempRelationshipsExtract, RelationshipsExtract>();
            
        }

    }
}
