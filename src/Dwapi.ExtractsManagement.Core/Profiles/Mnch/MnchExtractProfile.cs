using System.Data;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mnch;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Mnch;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.ExtractsManagement.Core.Profiles.Mnch
{
    public class MnchExtractProfile : Profile
    {
        public MnchExtractProfile()
        {
     CreateMap<IDataRecord, TempPatientMnchExtract>()
         .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
         .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
         .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
         .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
         .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))

                     .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.FacilityName))))
                     .ForMember(x => x.Pkv, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Pkv))))
                     .ForMember(x => x.PatientMnchID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.PatientMnchID))))
                     .ForMember(x => x.PatientHeiID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.PatientHeiID))))
                     .ForMember(x => x.Gender, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Gender))))
                     .ForMember(x => x.DOB, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientMnchExtract.DOB))))
                     .ForMember(x => x.FirstEnrollmentAtMnch, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPatientMnchExtract.FirstEnrollmentAtMnch))))
                     .ForMember(x => x.Occupation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Occupation))))
                     .ForMember(x => x.MaritalStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.MaritalStatus))))
                     .ForMember(x => x.EducationLevel, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.EducationLevel))))
                     .ForMember(x => x.PatientResidentCounty, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.PatientResidentCounty))))
                     .ForMember(x => x.PatientResidentSubCounty, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.PatientResidentSubCounty))))
                     .ForMember(x => x.PatientResidentWard, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.PatientResidentWard))))
                     .ForMember(x => x.InSchool, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.InSchool))));



                 CreateMap<IDataRecord, TempMnchEnrolmentExtract>()

                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
                     .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))

                     .ForMember(x => x.PatientMnchID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchEnrolmentExtract.PatientMnchID))))
                     .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchEnrolmentExtract.FacilityName))))
                     .ForMember(x => x.ServiceType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchEnrolmentExtract.ServiceType))))
                     .ForMember(x => x.EnrollmentDateAtMnch, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchEnrolmentExtract.EnrollmentDateAtMnch))))
                     .ForMember(x => x.MnchNumber, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchEnrolmentExtract.MnchNumber))))
                     .ForMember(x => x.FirstVisitAnc, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchEnrolmentExtract.FirstVisitAnc))))
                     .ForMember(x => x.Parity, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchEnrolmentExtract.Parity))))
                     .ForMember(x => x.Gravidae, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchEnrolmentExtract.Gravidae))))
                     .ForMember(x => x.LMP, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchEnrolmentExtract.LMP))))
                     .ForMember(x => x.EDDFromLMP, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchEnrolmentExtract.EDDFromLMP))))
                     .ForMember(x => x.HIVStatusBeforeANC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchEnrolmentExtract.HIVStatusBeforeANC))))
                     .ForMember(x => x.HIVTestDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchEnrolmentExtract.HIVTestDate))))
                     .ForMember(x => x.PartnerHIVStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchEnrolmentExtract.PartnerHIVStatus))))
                     .ForMember(x => x.PartnerHIVTestDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchEnrolmentExtract.PartnerHIVTestDate))))
                     .ForMember(x => x.BloodGroup, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchEnrolmentExtract.BloodGroup))))
                     .ForMember(x => x.StatusAtMnch, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchEnrolmentExtract.StatusAtMnch))));


                 CreateMap<IDataRecord, TempMnchArtExtract>().ForMember(x => x.Pkv, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchArtExtract.Pkv))))

                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
                     .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))

                     .ForMember(x => x.PatientMnchID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchArtExtract.PatientMnchID))))
                     .ForMember(x => x.PatientHeiID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchArtExtract.PatientHeiID))))
                     .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchArtExtract.FacilityName))))
                     .ForMember(x => x.RegistrationAtCCC, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchArtExtract.RegistrationAtCCC))))
                     .ForMember(x => x.StartARTDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchArtExtract.StartARTDate))))
                     .ForMember(x => x.StartRegimen, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchArtExtract.StartRegimen))))
                     .ForMember(x => x.StartRegimenLine, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchArtExtract.StartRegimenLine))))
                     .ForMember(x => x.StatusAtCCC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchArtExtract.StatusAtCCC))))
                     .ForMember(x => x.LastARTDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchArtExtract.LastARTDate))))
                     .ForMember(x => x.LastRegimen, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchArtExtract.LastRegimen))))
                     .ForMember(x => x.LastRegimenLine, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchArtExtract.LastRegimenLine))));



                 CreateMap<IDataRecord, TempAncVisitExtract>()

                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
                     .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))

     .ForMember(x => x.PatientMnchID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.PatientMnchID))))
     .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.FacilityName))))
     .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAncVisitExtract.VisitID))))
     .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempAncVisitExtract.VisitDate))))
     .ForMember(x => x.ANCClinicNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.ANCClinicNumber))))
     .ForMember(x => x.ANCVisitNo, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAncVisitExtract.ANCVisitNo))))
     .ForMember(x => x.GestationWeeks, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAncVisitExtract.GestationWeeks))))
     .ForMember(x => x.Height, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempAncVisitExtract.Height))))
     .ForMember(x => x.Weight, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempAncVisitExtract.Weight))))
     .ForMember(x => x.Temp, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempAncVisitExtract.Temp))))
     .ForMember(x => x.PulseRate, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAncVisitExtract.PulseRate))))
     .ForMember(x => x.RespiratoryRate, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAncVisitExtract.RespiratoryRate))))
     .ForMember(x => x.OxygenSaturation, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempAncVisitExtract.OxygenSaturation))))
     .ForMember(x => x.MUAC, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAncVisitExtract.MUAC))))
     .ForMember(x => x.BP, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAncVisitExtract.BP))))
     .ForMember(x => x.BreastExam, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.BreastExam))))
     .ForMember(x => x.AntenatalExercises, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.AntenatalExercises))))
     .ForMember(x => x.FGM, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.FGM))))
     .ForMember(x => x.FGMComplications, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.FGMComplications))))
     .ForMember(x => x.Haemoglobin, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempAncVisitExtract.Haemoglobin))))
     .ForMember(x => x.DiabetesTest, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.DiabetesTest))))
     .ForMember(x => x.TBScreening, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.TBScreening))))
     .ForMember(x => x.CACxScreen, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.CACxScreen))))
     .ForMember(x => x.CACxScreenMethod, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.CACxScreenMethod))))
     .ForMember(x => x.WHOStaging, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempAncVisitExtract.WHOStaging))))
     .ForMember(x => x.VLSampleTaken, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.VLSampleTaken))))
     .ForMember(x => x.VLDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempAncVisitExtract.VLDate))))
     .ForMember(x => x.VLResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.VLResult))))
     .ForMember(x => x.SyphilisTreatment, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.SyphilisTreatment))))
     .ForMember(x => x.HIVStatusBeforeANC , o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.HIVStatusBeforeANC ))))
     .ForMember(x => x.HIVTestingDone, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.HIVTestingDone))))
     .ForMember(x => x.HIVTestType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.HIVTestType))))
     .ForMember(x => x.HIVTest1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.HIVTest1))))
     .ForMember(x => x.HIVTest1Result, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.HIVTest1Result))))
     .ForMember(x => x.HIVTest2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.HIVTest2))))
     .ForMember(x => x.HIVTestFinalResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.HIVTestFinalResult))))
     .ForMember(x => x.SyphilisTestDone, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.SyphilisTestDone))))
     .ForMember(x => x.SyphilisTestType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.SyphilisTestType))))
     .ForMember(x => x.SyphilisTestResults, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.SyphilisTestResults))))
     .ForMember(x => x.SyphilisTreated, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.SyphilisTreated))))
     .ForMember(x => x.MotherProphylaxisGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.MotherProphylaxisGiven))))
     .ForMember(x => x.MotherGivenHAART, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempAncVisitExtract.MotherGivenHAART))))
     .ForMember(x => x.AZTBabyDispense, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.AZTBabyDispense))))
     .ForMember(x => x.NVPBabyDispense, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.NVPBabyDispense))))
     .ForMember(x => x.ChronicIllness, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.ChronicIllness))))
     .ForMember(x => x.CounselledOn, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.CounselledOn))))
     .ForMember(x => x.PartnerHIVTestingANC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.PartnerHIVTestingANC))))
     .ForMember(x => x.PartnerHIVStatusANC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.PartnerHIVStatusANC))))
     .ForMember(x => x.PostParturmFP, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.PostParturmFP))))
     .ForMember(x => x.Deworming, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.Deworming))))
     .ForMember(x => x.MalariaProphylaxis, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.MalariaProphylaxis))))
     .ForMember(x => x.TetanusDose, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.TetanusDose))))
     .ForMember(x => x.IronSupplementsGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.IronSupplementsGiven))))
     .ForMember(x => x.ReceivedMosquitoNet, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.ReceivedMosquitoNet))))
     .ForMember(x => x.PreventiveServices, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.PreventiveServices))))
     .ForMember(x => x.UrinalysisVariables, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.UrinalysisVariables))))
     .ForMember(x => x.ReferredFrom, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.ReferredFrom))))
     .ForMember(x => x.ReferredTo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.ReferredTo))))
     .ForMember(x => x.ReferralReasons, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.ReferralReasons))))
     .ForMember(x => x.NextAppointmentANC, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempAncVisitExtract.NextAppointmentANC))))
     .ForMember(x => x.ClinicalNotes, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempAncVisitExtract.ClinicalNotes))));



                 CreateMap<IDataRecord, TempMatVisitExtract>()

                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
                     .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))

                     .ForMember(x => x.PatientMnchID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.PatientMnchID))))
     .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.FacilityName))))
     .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMatVisitExtract.VisitID))))
     .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMatVisitExtract.VisitDate))))
     .ForMember(x => x.AdmissionNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.AdmissionNumber))))
     .ForMember(x => x.ANCVisits, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMatVisitExtract.ANCVisits))))
     .ForMember(x => x.DateOfDelivery, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMatVisitExtract.DateOfDelivery))))
     .ForMember(x => x.DurationOfDelivery, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMatVisitExtract.DurationOfDelivery))))
     .ForMember(x => x.GestationAtBirth, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMatVisitExtract.GestationAtBirth))))
     .ForMember(x => x.ModeOfDelivery, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.ModeOfDelivery))))
     .ForMember(x => x.PlacentaComplete, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.PlacentaComplete))))
     .ForMember(x => x.UterotonicGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.UterotonicGiven))))
     .ForMember(x => x.VaginalExamination, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.VaginalExamination))))
     .ForMember(x => x.BloodLoss, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMatVisitExtract.BloodLoss))))
     .ForMember(x => x.BloodLossVisual, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.BloodLossVisual))))
     .ForMember(x => x.ConditonAfterDelivery, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.ConditonAfterDelivery))))
     .ForMember(x => x.MaternalDeath, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMatVisitExtract.MaternalDeath))))
     .ForMember(x => x.DeliveryComplications, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.DeliveryComplications))))
     .ForMember(x => x.NoBabiesDelivered, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMatVisitExtract.NoBabiesDelivered))))
     .ForMember(x => x.BabyBirthNumber, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMatVisitExtract.BabyBirthNumber))))
     .ForMember(x => x.SexBaby, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.SexBaby))))
     .ForMember(x => x.BirthWeight, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.BirthWeight))))
     .ForMember(x => x.BirthOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.BirthOutcome))))
     .ForMember(x => x.BirthWithDeformity, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.BirthWithDeformity))))
     .ForMember(x => x.TetracyclineGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.TetracyclineGiven))))
     .ForMember(x => x.InitiatedBF, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.InitiatedBF))))
     .ForMember(x => x.ApgarScore1, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMatVisitExtract.ApgarScore1))))
     .ForMember(x => x.ApgarScore5, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMatVisitExtract.ApgarScore5))))
     .ForMember(x => x.ApgarScore10, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMatVisitExtract.ApgarScore10))))
     .ForMember(x => x.KangarooCare, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.KangarooCare))))
     .ForMember(x => x.ChlorhexidineApplied, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.ChlorhexidineApplied))))
     .ForMember(x => x.VitaminKGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.VitaminKGiven))))
     .ForMember(x => x.StatusBabyDischarge, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.StatusBabyDischarge))))
     .ForMember(x => x.MotherDischargeDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.MotherDischargeDate))))
     .ForMember(x => x.SyphilisTestResults, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.SyphilisTestResults))))
     .ForMember(x => x.HIVStatusLastANC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.HIVStatusLastANC))))
     .ForMember(x => x.HIVTestingDone, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.HIVTestingDone))))
     .ForMember(x => x.HIVTest1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.HIVTest1))))
     .ForMember(x => x.HIV1Results, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.HIV1Results))))
     .ForMember(x => x.HIVTest2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.HIVTest2))))
     .ForMember(x => x.HIV2Results, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.HIV2Results))))
     .ForMember(x => x.HIVTestFinalResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.HIVTestFinalResult))))
     .ForMember(x => x.OnARTANC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.OnARTANC))))
     .ForMember(x => x.BabyGivenProphylaxis, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.BabyGivenProphylaxis))))
     .ForMember(x => x.MotherGivenCTX, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.MotherGivenCTX))))
     .ForMember(x => x.PartnerHIVTestingMAT, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.PartnerHIVTestingMAT))))
     .ForMember(x => x.PartnerHIVStatusMAT, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.PartnerHIVStatusMAT))))
     .ForMember(x => x.CounselledOn, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.CounselledOn))))
     .ForMember(x => x.ReferredFrom, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.ReferredFrom))))
     .ForMember(x => x.ReferredTo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.ReferredTo))))
     .ForMember(x => x.ClinicalNotes, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMatVisitExtract.ClinicalNotes))));


                 CreateMap<IDataRecord, TempPncVisitExtract>()
                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
                     .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))

                     .ForMember(x => x.PatientMnchID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.PatientMnchID))))
     .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPncVisitExtract.VisitID))))
     .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPncVisitExtract.VisitDate))))
     .ForMember(x => x.PNCRegisterNumber, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.PNCRegisterNumber))))
     .ForMember(x => x.PNCVisitNo, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPncVisitExtract.PNCVisitNo))))
     .ForMember(x => x.DeliveryDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPncVisitExtract.DeliveryDate))))
     .ForMember(x => x.ModeOfDelivery, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.ModeOfDelivery))))
     .ForMember(x => x.PlaceOfDelivery, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.PlaceOfDelivery))))
     .ForMember(x => x.Height, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempPncVisitExtract.Height))))
     .ForMember(x => x.Weight, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempPncVisitExtract.Weight))))
     .ForMember(x => x.Temp, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempPncVisitExtract.Temp))))
     .ForMember(x => x.PulseRate, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPncVisitExtract.PulseRate))))
     .ForMember(x => x.RespiratoryRate, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPncVisitExtract.RespiratoryRate))))
     .ForMember(x => x.OxygenSaturation, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempPncVisitExtract.OxygenSaturation))))
     .ForMember(x => x.MUAC, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPncVisitExtract.MUAC))))
     .ForMember(x => x.BP, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPncVisitExtract.BP))))
     .ForMember(x => x.BreastExam, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.BreastExam))))
     .ForMember(x => x.GeneralCondition, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.GeneralCondition))))
     .ForMember(x => x.HasPallor, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.HasPallor))))
     .ForMember(x => x.Pallor, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.Pallor))))
     .ForMember(x => x.Breast, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.Breast))))
     .ForMember(x => x.PPH, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.PPH))))
     .ForMember(x => x.CSScar, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.CSScar))))
     .ForMember(x => x.UterusInvolution, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.UterusInvolution))))
     .ForMember(x => x.Episiotomy, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.Episiotomy))))
     .ForMember(x => x.Lochia, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.Lochia))))
     .ForMember(x => x.Fistula, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.Fistula))))
     .ForMember(x => x.MaternalComplications, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.MaternalComplications))))
     .ForMember(x => x.TBScreening, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.TBScreening))))
     .ForMember(x => x.ClientScreenedCACx, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.ClientScreenedCACx))))
     .ForMember(x => x.CACxScreenMethod, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.CACxScreenMethod))))
     .ForMember(x => x.CACxScreenResults, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.CACxScreenResults))))
     .ForMember(x => x.PriorHIVStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.PriorHIVStatus))))
     .ForMember(x => x.HIVTestingDone, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.HIVTestingDone))))
     .ForMember(x => x.HIVTest1, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.HIVTest1))))
     .ForMember(x => x.HIVTest1Result, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.HIVTest1Result))))
     .ForMember(x => x.HIVTest2, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.HIVTest2))))
     .ForMember(x => x.HIVTest2Result, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.HIVTest2Result))))
     .ForMember(x => x.HIVTestFinalResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.HIVTestFinalResult))))
     .ForMember(x => x.InfantProphylaxisGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.InfantProphylaxisGiven))))
     .ForMember(x => x.MotherProphylaxisGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.MotherProphylaxisGiven))))
     .ForMember(x => x.CoupleCounselled, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.CoupleCounselled))))
     .ForMember(x => x.PartnerHIVTestingPNC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.PartnerHIVTestingPNC))))
     .ForMember(x => x.PartnerHIVResultPNC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.PartnerHIVResultPNC))))
     .ForMember(x => x.CounselledOnFP, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.CounselledOnFP))))
     .ForMember(x => x.ReceivedFP, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.ReceivedFP))))
     .ForMember(x => x.HaematinicsGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.HaematinicsGiven))))
     .ForMember(x => x.DeliveryOutcome, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.DeliveryOutcome))))
     .ForMember(x => x.BabyConditon, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.BabyConditon))))
     .ForMember(x => x.BabyFeeding, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.BabyFeeding))))
     .ForMember(x => x.UmbilicalCord, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.UmbilicalCord))))
     .ForMember(x => x.Immunization, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.Immunization))))
     .ForMember(x => x.InfantFeeding, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.InfantFeeding))))
     .ForMember(x => x.PreventiveServices, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.PreventiveServices))))
     .ForMember(x => x.ReferredFrom, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.ReferredFrom))))
     .ForMember(x => x.ReferredTo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.ReferredTo))))
     .ForMember(x => x.NextAppointmentPNC, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempPncVisitExtract.NextAppointmentPNC))))
     .ForMember(x => x.ClinicalNotes, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPncVisitExtract.ClinicalNotes))));


                 CreateMap<IDataRecord, TempMotherBabyPairExtract>()
                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
                     .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))


                     .ForMember(x => x.BabyPatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMotherBabyPairExtract.BabyPatientPK))))
                     .ForMember(x => x.MotherPatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMotherBabyPairExtract.MotherPatientPK))))
                     .ForMember(x => x.BabyPatientMncHeiID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMotherBabyPairExtract.BabyPatientMncHeiID))))
                     .ForMember(x => x.MotherPatientMncHeiID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMotherBabyPairExtract.MotherPatientMncHeiID))))
                     .ForMember(x => x.PatientIDCCC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMotherBabyPairExtract.PatientIDCCC))));



                 CreateMap<IDataRecord, TempCwcEnrolmentExtract>()

                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
                     .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))

                     .ForMember(x => x.Pkv, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.Pkv))))
     .ForMember(x => x.PatientIDCWC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.PatientIDCWC))))
     .ForMember(x => x.HEIID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.HEIID))))
     .ForMember(x => x.MothersPkv, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.MothersPkv))))
     .ForMember(x => x.RegistrationAtCWC, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.RegistrationAtCWC))))
     .ForMember(x => x.RegistrationAtHEI, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.RegistrationAtHEI))))
     .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempCwcEnrolmentExtract.VisitID))))
     .ForMember(x => x.Gestation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.Gestation))))
     .ForMember(x => x.BirthWeight, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.BirthWeight))))
     .ForMember(x => x.BirthLength, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempCwcEnrolmentExtract.BirthLength))))
     .ForMember(x => x.BirthOrder, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempCwcEnrolmentExtract.BirthOrder))))
     .ForMember(x => x.BirthType, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.BirthType))))
     .ForMember(x => x.PlaceOfDelivery, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.PlaceOfDelivery))))
     .ForMember(x => x.ModeOfDelivery, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.ModeOfDelivery))))
     .ForMember(x => x.SpecialNeeds, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.SpecialNeeds))))
     .ForMember(x => x.SpecialCare, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.SpecialCare))))
     .ForMember(x => x.HEI, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.HEI))))
     .ForMember(x => x.MotherAlive, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.MotherAlive))))
     .ForMember(x => x.MothersCCCNo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.MothersCCCNo))))
     .ForMember(x => x.TransferIn, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.TransferIn))))
     .ForMember(x => x.TransferInDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.TransferInDate))))
     .ForMember(x => x.TransferredFrom, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.TransferredFrom))))
     .ForMember(x => x.HEIDate, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.HEIDate))))
     .ForMember(x => x.NVP, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.NVP))))
     .ForMember(x => x.BreastFeeding, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.BreastFeeding))))
     .ForMember(x => x.ReferredFrom, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.ReferredFrom))))
     .ForMember(x => x.MothersCCCNo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.MothersCCCNo))))
     .ForMember(x => x.ARTMother, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.ARTMother))))
     .ForMember(x => x.ARTRegimenMother, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.ARTRegimenMother))))
     .ForMember(x => x.ARTStartDateMother, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcEnrolmentExtract.ARTStartDateMother))));



                 CreateMap<IDataRecord, TempCwcVisitExtract>()
                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
                     .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))

                     .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.FacilityName))))
     .ForMember(x => x.PatientMnchID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.PatientMnchID))))
     .ForMember(x => x.VisitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempCwcVisitExtract.VisitDate))))
     .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempCwcVisitExtract.VisitID))))
     .ForMember(x => x.Height, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempCwcVisitExtract.Height))))
     .ForMember(x => x.Weight, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempCwcVisitExtract.Weight))))
     .ForMember(x => x.Temp, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempCwcVisitExtract.Temp))))
     .ForMember(x => x.PulseRate, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempCwcVisitExtract.PulseRate))))
     .ForMember(x => x.RespiratoryRate, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempCwcVisitExtract.RespiratoryRate))))
     .ForMember(x => x.OxygenSaturation, o => o.MapFrom(s => s.GetNullDecimalOrDefault(nameof(TempCwcVisitExtract.OxygenSaturation))))
     .ForMember(x => x.MUAC, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempCwcVisitExtract.MUAC))))
     .ForMember(x => x.WeightCategory, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.WeightCategory))))
     .ForMember(x => x.Stunted, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.Stunted))))
     .ForMember(x => x.InfantFeeding, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.InfantFeeding))))
     .ForMember(x => x.MedicationGiven, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.MedicationGiven))))
     .ForMember(x => x.TBAssessment, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.TBAssessment))))
     .ForMember(x => x.MNPsSupplementation, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.MNPsSupplementation))))
     .ForMember(x => x.Immunization, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.Immunization))))
     .ForMember(x => x.DangerSigns, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.DangerSigns))))
     .ForMember(x => x.Milestones, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.Milestones))))
     .ForMember(x => x.VitaminA, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.VitaminA))))
     .ForMember(x => x.Disability, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.Disability))))
     .ForMember(x => x.ReceivedMosquitoNet, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.ReceivedMosquitoNet))))
     .ForMember(x => x.Dewormed, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.Dewormed))))
     .ForMember(x => x.ReferredFrom, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.ReferredFrom))))
     .ForMember(x => x.ReferredTo, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.ReferredTo))))
     .ForMember(x => x.ReferralReasons, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.ReferralReasons))))
     .ForMember(x => x.FollowUP, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempCwcVisitExtract.FollowUP))))
     .ForMember(x => x.NextAppointment, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempCwcVisitExtract.NextAppointment))));



                 CreateMap<IDataRecord, TempHeiExtract>()
                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
                     .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))

                     .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHeiExtract.FacilityName))))
                     .ForMember(x => x.PatientMnchID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHeiExtract.PatientMnchID))))
                     .ForMember(x => x.DNAPCR1Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.DNAPCR1Date))))
                     .ForMember(x => x.DNAPCR2Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.DNAPCR2Date))))
                     .ForMember(x => x.DNAPCR3Date, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.DNAPCR3Date))))
                     .ForMember(x => x.ConfirmatoryPCRDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.ConfirmatoryPCRDate))))
                     .ForMember(x => x.BasellineVLDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.BasellineVLDate))))
                     .ForMember(x => x.FinalyAntibodyDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.FinalyAntibodyDate))))
                     .ForMember(x => x.DNAPCR1, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.DNAPCR1))))
                     .ForMember(x => x.DNAPCR2, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.DNAPCR2))))
                     .ForMember(x => x.DNAPCR3, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.DNAPCR3))))
                     .ForMember(x => x.ConfirmatoryPCR, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.ConfirmatoryPCR))))
                     .ForMember(x => x.BasellineVL, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.BasellineVL))))
                     .ForMember(x => x.FinalyAntibody, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.FinalyAntibody))))
                     .ForMember(x => x.HEIExitDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempHeiExtract.HEIExitDate))))
                     .ForMember(x => x.HEIHIVStatus, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHeiExtract.HEIHIVStatus)))).ForMember(x => x.HEIExitCritearia, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempHeiExtract.HEIExitCritearia))));



                 CreateMap<IDataRecord, TempMnchLabExtract>()

                     .ForMember(x => x.PatientPK, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.PatientPK))))
                     .ForMember(x => x.SiteCode, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.SiteCode))))
                     .ForMember(x => x.FacilityId, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempPatientMnchExtract.FacilityId))))
                     .ForMember(x => x.Project, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Project))))
                     .ForMember(x => x.Emr, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempPatientMnchExtract.Emr))))

                     .ForMember(x => x.PatientMNCH_ID, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchLabExtract.PatientMNCH_ID))))
                     .ForMember(x => x.FacilityName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchLabExtract.FacilityName))))
                     .ForMember(x => x.SatelliteName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchLabExtract.SatelliteName))))
                     .ForMember(x => x.VisitID, o => o.MapFrom(s => s.GetNullIntOrDefault(nameof(TempMnchLabExtract.VisitID))))
                     .ForMember(x => x.OrderedbyDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchLabExtract.OrderedbyDate))))
                     .ForMember(x => x.ReportedbyDate, o => o.MapFrom(s => s.GetNullDateOrDefault(nameof(TempMnchLabExtract.ReportedbyDate))))
                     .ForMember(x => x.TestName, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchLabExtract.TestName))))
                     .ForMember(x => x.TestResult, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchLabExtract.TestResult))))
                     .ForMember(x => x.LabReason, o => o.MapFrom(s => s.GetStringOrDefault(nameof(TempMnchLabExtract.LabReason))));


                 CreateMap<TempPatientMnchExtract, PatientMnchExtract>();


                 CreateMap<TempMnchEnrolmentExtract, MnchEnrolmentExtract>();


                 CreateMap<TempMnchArtExtract, MnchArtExtract>();


                 CreateMap<TempAncVisitExtract, AncVisitExtract>();


                 CreateMap<TempMatVisitExtract, MatVisitExtract>();


                 CreateMap<TempPncVisitExtract, PncVisitExtract>();


                 CreateMap<TempMotherBabyPairExtract, MotherBabyPairExtract>();


                 CreateMap<TempCwcEnrolmentExtract, CwcEnrolmentExtract>();


                 CreateMap<TempCwcVisitExtract, CwcVisitExtract>();


                 CreateMap<TempHeiExtract, HeiExtract>();


                 CreateMap<TempMnchLabExtract, MnchLabExtract>();

        }
    }
}
