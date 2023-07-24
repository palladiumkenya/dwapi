import {Component, OnInit, OnChanges, SimpleChange, Input} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import {PatientExtract} from '../../../models/patient-extract';
import {Message} from 'primeng/api';
import {NdwhPatientsExtractService} from '../../../services/ndwh-patients-extract.service';
import {NdwhPatientArtService} from '../../../services/ndwh-patient-art.service';
import {NdwhPatientBaselineService} from '../../../services/ndwh-patient-baseline.service';
import {NdwhPatientLaboratoryService} from '../../../services/ndwh-patient-laboratory.service';
import {NdwhPatientPharmacyService} from '../../../services/ndwh-patient-pharmacy.service';
import {NdwhPatientStatusService} from '../../../services/ndwh-patient-status.service';
import {NdwhPatientVisitService} from '../../../services/ndwh-patient-visit.service';
import {Extract} from '../../../../settings/model/extract';
import {NdwhPatientAdverseEventService} from '../../../services/ndwh-patient-adverse-event.service';
import {NdwhSummaryService} from '../../../services/ndwh-summary.service';

@Component({
    selector: 'liveapp-invalid-record-details',
    templateUrl: './invalid-record-details.component.html',
    styleUrls: ['./invalid-record-details.component.scss']
})
export class InvalidRecordDetailsComponent implements OnInit {

    private exName: string;
    private _patientExtractsService: NdwhPatientsExtractService;
    private _patientArtService: NdwhPatientArtService;
    private _patientBaselineService: NdwhPatientBaselineService;
    private _patientLabService: NdwhPatientLaboratoryService;
    private _patientPharmacyService: NdwhPatientPharmacyService;
    private _patientStatusService: NdwhPatientStatusService;
    private _patientVisitService: NdwhPatientVisitService;
    private _patientAdverseEventService: NdwhPatientAdverseEventService;
    public invalidExtracts: any[] = [];
    public cols: any[];
    public getInvalid$: Subscription;
    public errorMessage: Message[];
    public otherMessage: Message[];

    public loadingData = false;
    private _preventLoad = true;

    constructor(patientExtractsService: NdwhPatientsExtractService, patientArtService: NdwhPatientArtService,
                patientBaselineService: NdwhPatientBaselineService, patientLabService: NdwhPatientLaboratoryService,
                patientPharmacyService: NdwhPatientPharmacyService, patientStatusService: NdwhPatientStatusService,
                patientVisitService: NdwhPatientVisitService, patientAdverseEventService: NdwhPatientAdverseEventService,
                private summaryService: NdwhSummaryService) {
        this._patientExtractsService = patientExtractsService;
        this._patientArtService = patientArtService;
        this._patientBaselineService = patientBaselineService;
        this._patientLabService = patientLabService;
        this._patientPharmacyService = patientPharmacyService;
        this._patientStatusService = patientStatusService;
        this._patientVisitService = patientVisitService;
        this._patientAdverseEventService = patientAdverseEventService;
    }

    get preventLoad(): boolean {
        return this._preventLoad;
    }

    @Input()
    set preventLoad(allow: boolean) {
        this._preventLoad = allow;
    }

    get extract(): string {
        return this.exName;
    }

    @Input()
    set extract(extract: string) {
        if (extract) {
            this.exName = extract;
            this.cols = [];
            this.invalidExtracts = [];
            this.getColumns();
            this.getInvalidExtracts();
        }
    }

    ngOnInit() {
        this.getPatientColumns();
        // this.getInvalidPatientExtracts();
    }

    public getInvalidExtracts(): void {

        if (this.extract === 'All Patients') {
            this.getInvalidPatientExtracts();
            return;
        }
        if (this.extract === 'ART Patients') {
            this.getInvalidPatientArtExtracts();
            return;
        }
        if (this.extract === 'Patient Baselines') {
            this.getInvalidPatientBaselineExtracts();
            return;
        }
        if (this.extract === 'Patient Labs') {
            this.getInvalidPatientLabExtracts();
            return;
        }
        if (this.extract === 'Patient Pharmacy') {
            this.getInvalidPatientPharmacyExtracts();
            return;
        }
        if (this.extract === 'Patient Status') {
            this.getInvalidPatientStatusExtracts();
            return;
        }
        if (this.extract === 'Patient Visit') {
            this.getInvalidPatientVisitExtracts();
            return;
        }
        if (this.extract === 'Patient Adverse Events') {
            this.getInvalidPatientAdverseEventExtracts();
            return;
        }

        if (this.extract === 'Allergies Chronic Illness') {
            this.getSummaryInvalidExtracts('AllergiesChronicIllness');
            return;
        }
        if (this.extract === 'Contact Listing') {
            this.getSummaryInvalidExtracts('ContactListing');
            return;
        }
        if (this.extract === 'Depression Screening') {
            this.getSummaryInvalidExtracts('DepressionScreening');
            return;
        }
        if (this.extract === 'Drug and Alcohol Screening') {
            this.getSummaryInvalidExtracts('DrugAlcoholScreening');
            return;
        }
        if (this.extract === 'Enhanced Adherence Counselling') {
            this.getSummaryInvalidExtracts('EnhancedAdherenceCounselling');
            return;
        }
        if (this.extract === 'GBV Screening') {
            this.getSummaryInvalidExtracts('GbvScreening');
            return;
        }
        if (this.extract === 'IPT') {
            this.getSummaryInvalidExtracts('Ipt');
            return;
        }
        if (this.extract === 'OTZ') {
            this.getSummaryInvalidExtracts('Otz');
            return;
        }
        if (this.extract === 'OVC') {
            this.getSummaryInvalidExtracts('Ovc');
            return;
        }

        if (this.extract === 'Covid') {
            this.getSummaryInvalidExtracts('Covid');
            return;
        }
        if (this.extract === 'Defaulter Tracing') {
            this.getSummaryInvalidExtracts('DefaulterTracing');
            return;
        }
        if (this.extract === 'Cervical Cancer Screening') {
            this.getSummaryInvalidExtracts('CervicalCancerScreening');
            return;
        }
    }

    private getColumns(): void {

        if (this.extract === 'All Patients') {
            this.getPatientColumns();
            return;
        }
        if (this.extract === 'ART Patients') {
            this.getPatientArtColumns();
            return;
        }
        if (this.extract === 'Patient Baselines') {
            this.getPatientBaselineColumns();
            return;
        }
        if (this.extract === 'Patient Labs') {
            this.getPatientLaboratoryColumns();
            return;
        }
        if (this.extract === 'Patient Pharmacy') {
            this.getPatientPharmacyColumns();
            return;
        }
        if (this.extract === 'Patient Status') {
            this.getPatientStatusColumns();
            return;
        }
        if (this.extract === 'Patient Visit') {
            this.getPatientVisitColumns();
            return;
        }
        if (this.extract === 'Patient Adverse Events') {
            this.getPatientAdverseEventColumns();
            return;
        }

        if (this.extract === 'Allergies Chronic Illness') {
            this.getAllergiesChronicIllnessColumns();
            return;
        }
        if (this.extract === 'Contact Listing') {
            this.getContactListingColumns();
            return;
        }
        if (this.extract === 'Depression Screening') {
            this.getDepressionScreeningColumns();
            return;
        }
        if (this.extract === 'Drug and Alcohol Screening') {
            this.getDrugAlcoholScreeningColumns();
            return;
        }
        if (this.extract === 'Enhanced Adherence Counselling') {
            this.getEnhancedAdherenceCounsellingColumns();
            return;
        }
        if (this.extract === 'GBV Screening') {
            this.getGbvScreeningColumns();
            return;
        }
        if (this.extract === 'IPT') {
            this.getIptColumns();
            return;
        }
        if (this.extract === 'OTZ') {
            this.getOtzColumns();
            return;
        }
        if (this.extract === 'OVC') {
            this.getOvcColumns();
            return;
        }
        if (this.extract === 'Covid') {
            this.getCovidColumns();
            return;
        }
        if (this.extract === 'Defaulter Tracing') {
            this.getDefaulterTracingColumns();
            return;
        }
        if (this.extract === 'Cervical Cancer Screening') {
            this.getCervicalCancerScreeningColumns();
            return;
        }
    }

    private getInvalidPatientExtracts(): void {
        this.loadingData = true;
        this.getInvalid$ = this._patientExtractsService.loadValidations().subscribe(
            p => {
                this.invalidExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
                this.loadingData = false;
            }
        );
    }

    private getInvalidPatientArtExtracts(): void {
        this.loadingData = true;
        this.getInvalid$ = this._patientArtService.loadValidations().subscribe(
            p => {
                this.invalidExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
                this.loadingData = false;
            }
        );
    }

    private getInvalidPatientBaselineExtracts(): void {
        this.loadingData = true;
        this.getInvalid$ = this._patientBaselineService.loadValidations().subscribe(
            p => {
                this.invalidExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
                this.loadingData = false;
            }
        );
    }

    private getInvalidPatientLabExtracts(): void {
        this.loadingData = true;
        this.getInvalid$ = this._patientLabService.loadValidations().subscribe(
            p => {
                this.invalidExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
                this.loadingData = false;
            }
        );
    }

    private getInvalidPatientPharmacyExtracts(): void {
        this.loadingData = true;
        this.getInvalid$ = this._patientPharmacyService.loadValidations().subscribe(
            p => {
                this.invalidExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
                this.loadingData = false;
            }
        );
    }

    private getInvalidPatientStatusExtracts(): void {
        this.loadingData = true;
        this.getInvalid$ = this._patientStatusService.loadValidations().subscribe(
            p => {
                this.invalidExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
                this.loadingData = false;
            }
        );
    }

    private getInvalidPatientVisitExtracts(): void {
        this.loadingData = true;
        this.getInvalid$ = this._patientVisitService.loadValidations().subscribe(
            p => {
                this.invalidExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
                this.loadingData = false;
            }
        );
    }

    private getInvalidPatientAdverseEventExtracts(): void {
        this.loadingData = true;
        this.getInvalid$ = this._patientAdverseEventService.loadValidations().subscribe(
            p => {
                this.invalidExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
                this.loadingData = false;
            }
        );
    }

    private getSummaryInvalidExtracts(ex: string): void {
        this.loadingData = true;
        this.getInvalid$ = this.summaryService.loadValidations(ex).subscribe(
            p => {
                this.invalidExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
                this.loadingData = false;
            }
        );
    }

    private getPatientColumns(): void {
        this.cols = [

            {field: 'PatientID', header: 'Patient ID'},
            {field: 'Summary', header: 'Summary'},
            {field: 'Extract', header: 'Extract'},
            {field: 'Field', header: 'Field'},
            {field: 'Type', header: 'Type'},
            {field: 'DOB', header: 'DOB'},
            {field: 'Gender', header: 'Gender'},
            {field: 'LastVisit', header: 'Last Visit'},
            {field: 'RegistrationAtCCC', header: 'CCC Registration Date'},
            {field: 'DateGenerated', header: 'Date Generated'},
            {field: 'PatientPK', header: 'Patient PK'},
            {field: 'FacilityId', header: 'Facility Id'},
            {field: 'SiteCode', header: 'Site Code'},
            {field: 'FacilityName', header: 'Facility Name'}
        ];
    }

    private getPatientArtColumns(): void {
        this.cols = [
            {field: 'PatientID', header: 'Patient ID'},
            {field: 'Summary', header: 'Summary'},
            {field: 'Extract', header: 'Extract'},
            {field: 'Field', header: 'Field'},
            {field: 'Type', header: 'Type'},
            {field: 'StartRegimen', header: 'Start Regimen'},
            {field: 'StartARTDate', header: 'Start ART Date'},
            {field: 'StartRegimenLine', header: 'Start Regimen Line'},
            {field: 'DOB', header: 'DOB'},
            {field: 'Gender', header: 'Gender'},
            {field: 'PatientSource', header: 'Patient Source'},
            {field: 'RegistrationDate', header: 'Registration Date'},

            {field: 'Summary', header: 'Summary'},
            {field: 'DateGenerated', header: 'Date Generated'},
            {field: 'PatientPK', header: 'Patient PK'},
            {field: 'FacilityId', header: 'Facility Id'},
            {field: 'SiteCode', header: 'Site Code'},
            {field: 'FacilityName', header: 'Facility Name'},
            {field: 'AgeLastVisit', header: 'Age Last Visit'},
            {field: 'PreviousARTStartDate', header: 'Previous ART Start Date'},
            {field: 'PreviousARTRegimen', header: 'Previous ART Regimen'},
            {field: 'StartARTAtThisFacility', header: 'Start ART At This Facility'},

            {field: 'LastARTDate', header: 'Last ART Date'},
            {field: 'LastRegimen', header: 'Last Regimen'},
            {field: 'LastRegimenLine', header: 'Last Regimen Line'},
            {field: 'LastVisit', header: 'Last Visit'},
            {field: 'ExpectedReturn', header: 'Expected Return'},
            {field: 'ExitReason', header: 'Exit Reason'},
            {field: 'ExitDate', header: 'Exit Date'}
        ];
    }

    private getPatientBaselineColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'extract', header: 'Extract'},
            {field: 'field', header: 'Field'},
            {field: 'type', header: 'Type'},
            {field: 'summary', header: 'Summary'},
            {field: 'dateGenerated', header: 'Date Generated'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'recordId', header: 'Record Id'},
            {field: 'bCD4', header: 'bCD4'},
            {field: 'bCD4Date', header: 'bCD4 Date'},
            {field: 'bWAB', header: 'bWAB'},
            {field: 'bWABDate', header: 'bWAB Date'},
            {field: 'bWHO', header: 'bWHO'},
            {field: 'bWHODate', header: 'bWHO Date'},
            {field: 'eWAB', header: 'eWAB'},
            {field: 'eWABDate', header: 'eWAB Date'},
            {field: 'eCD4', header: 'eCD4'},
            {field: 'eCD4Date', header: 'eCD4 Date'},
            {field: 'eWHO', header: 'eWHO'},
            {field: 'eWHODate', header: 'eWHO Date'},
            {field: 'lastWHO', header: 'lastWHO'},
            {field: 'lastWHODate', header: 'last WHO Date'},
            {field: 'lastCD4', header: 'lastCD4'},
            {field: 'lastCD4Date', header: 'last CD4 Date'},
            {field: 'lastWAB', header: 'lastWAB'},
            {field: 'lastWABDate', header: 'last WAB Date'},
            {field: 'm12CD4', header: 'm12CD4'},
            {field: 'm12CD4Date', header: 'm12CD4 Date'},
            {field: 'm6CD4', header: 'm6CD4'},
            {field: 'm6CD4Date', header: 'm6CD4 Date'}
        ];
    }

    private getPatientLaboratoryColumns(): void {
        this.cols = [

            {field: 'patientID', header: 'Patient ID'},
            {field: 'summary', header: 'Summary'},
            {field: 'extract', header: 'Extract'},
            {field: 'field', header: 'Field'},
            {field: 'type', header: 'Type'},
            {field: 'summary', header: 'Summary'},
            {field: 'orderedByDate', header: 'Ordered By Date'},
            {field: 'reportedByDate', header: 'Reported By Date'},
            {field: 'testName', header: 'Test Name'},
            {field: 'dateGenerated', header: 'Date Generated'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'reason', header: 'Lab Reason'},
            {field: 'enrollmentTest', header: 'Enrollment Test'},
            {field: 'testResult', header: 'Test Result'},
            {field: 'visitId', header: 'Visit Id'}
        ];
    }

    private getPatientPharmacyColumns(): void {
        this.cols = [
            {field: 'patientID', header: 'Patient ID'},
            {field: 'summary', header: 'Summary'},
            {field: 'extract', header: 'Extract'},
            {field: 'field', header: 'Field'},
            {field: 'type', header: 'Type'},
            {field: 'summary', header: 'Summary'},
            {field: 'drug', header: 'Drug'},
            {field: 'dispenseDate', header: 'Dispense Date'},
            {field: 'duration', header: 'Duration'},
            {field: 'expectedReturn', header: 'Expected Return'},
            {field: 'treatmentType', header: 'Treatment Type'},
            {field: 'regimenLine', header: 'Regimen Line'},
            {field: 'periodTaken', header: 'Period Taken'},
            {field: 'prophylaxisType', header: 'ProphylaxisT ype'},
            {field: 'provider', header: 'Provider'},
            {field: 'visitID', header: 'VisitID'},
            {field: 'dateGenerated', header: 'Date Generated'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},

        ];
    }

    private getPatientStatusColumns(): void {
        this.cols = [
            {field: 'patientID', header: 'Patient ID'},
            {field: 'summary', header: 'Summary'},
            {field: 'extract', header: 'Extract'},
            {field: 'field', header: 'Field'},
            {field: 'type', header: 'Type'},
            {field: 'summary', header: 'Summary'},
            {field: 'exitDescription', header: 'Exit Description'},
            {field: 'exitDate', header: 'Exit Date'},
            {field: 'exitReason', header: 'Exit Reason'},
            {field: 'dateGenerated', header: 'Date Generated'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'facilityName', header: 'Facility Name'},

        ];
    }

    private getPatientVisitColumns(): void {
        this.cols = [

            {field: 'patientID', header: 'Patient ID'},
            {field: 'summary', header: 'Summary'},
            {field: 'extract', header: 'Extract'},
            {field: 'field', header: 'Field'},
            {field: 'type', header: 'Type'},
            {field: 'summary', header: 'Summary'},
            {field: 'visitDate', header: 'Visit Date'},
            {field: 'dateGenerated', header: 'Date Generated'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'service', header: 'Service'},
            {field: 'zScore', header: 'ZScore'},
            {field: 'paedsDisclosure', header: 'PaedsDisclosure'},
            {field: 'visitType', header: 'Visit Type'},
            {field: 'wHOStage', header: 'WHO Stage'},
            {field: 'wABStage', header: 'WAB Stage'},
            {field: 'pregnant', header: 'Pregnant'},
            {field: 'lMP', header: 'LMP'},
            {field: 'eDD', header: 'EDD'},
            {field: 'height', header: 'Height'},
            {field: 'weight', header: 'Weight'},
            {field: 'bP', header: 'BP'},
            {field: 'oI', header: 'OI'},
            {field: 'oIDate', header: 'OI Date'},
            {field: 'adherence', header: 'Adherence'},
            {field: 'adherenceCategory', header: 'Adherence Category'},
            {field: 'substitutionFirstlineRegimenDate', header: 'Substitution Firstline Regimen Date'},
            {field: 'substitutionFirstlineRegimenReason', header: 'Substitution Firstline Regimen Reason'},
            {field: 'substitutionSecondlineRegimenDate', header: 'Substitution Secondline Regimen Date'},
            {field: 'substitutionSecondlineRegimenReason', header: 'Substitution Secondline Regimen Reason'},
            {field: 'secondlineRegimenChangeDate', header: 'Secondline Regimen Change Date'},
            {field: 'secondlineRegimenChangeReason', header: 'Secondline RegimenChange Reason'},
            {field: 'familyPlanningMethod', header: 'Family Planning Method'},
            {field: 'pwP', header: 'PwP'},
            {field: 'gestationAge', header: 'Gestation Age'},
            {field: 'nextAppointmentDate', header: 'Next Appointment Date'},
            {field: 'visitId', header: 'Visit Id'}
        ];
    }

    private getPatientAdverseEventColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'adverseEvent', header: 'Adverse Event'},
            {field: 'adverseEventStartDate', header: 'Start Date'},
            {field: 'adverseEventEndDate', header: 'End Date'},
            {field: 'severity', header: 'Severity'},
            {field: 'adverseEventRegimen', header: 'Regimen'},
            {field: 'adverseEventCause', header: 'Cause'},
            {field: 'adverseEventClinicalOutcome', header: 'Clinical Outcome'},
            {field: 'adverseEventActionTaken', header: 'Action Taken'},
            {field: 'adverseEventIsPregnant', header: 'Is Pregnant?'},
            {field: 'visitDate', header: 'Visit Date'}
        ];
    }

    private getAllergiesChronicIllnessColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'}, {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'chronicIllness', header: 'chronicIllness'},
            {field: 'chronicOnsetDate', header: 'chronicOnsetDate'},
            {field: 'knownAllergies', header: 'knownAllergies'},
            {field: 'allergyCausativeAgent', header: 'allergyCausativeAgent'},
            {field: 'allergicReaction', header: 'allergicReaction'},
            {field: 'allergySeverity', header: 'allergySeverity'},
            {field: 'allergyOnsetDate', header: 'allergyOnsetDate'},
            {field: 'skin', header: 'skin'},
            {field: 'eyes', header: 'eyes'},
            {field: 'ent', header: 'ent'},
            {field: 'chest', header: 'chest'},
            {field: 'cvs', header: 'cvs'},
            {field: 'abdomen', header: 'abdomen'},
            {field: 'cns', header: 'cns'},
            {field: 'genitourinary', header: 'genitourinary'}

        ];
    }
    private getContactListingColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'partnerPersonID', header: 'partnerPersonID'},
            {field: 'contactAge', header: 'contactAge'},
            {field: 'contactSex', header: 'contactSex'},
            {field: 'contactMaritalStatus', header: 'contactMaritalStatus'},
            {field: 'relationshipWithPatient', header: 'relationshipWithPatient'},
            {field: 'screenedForIpv', header: 'screenedForIpv'},
            {field: 'ipvScreening', header: 'ipvScreening'},
            {field: 'ipvScreeningOutcome', header: 'ipvScreeningOutcome'},
            {field: 'currentlyLivingWithIndexClient', header: 'currentlyLivingWithIndexClient'},
            {field: 'knowledgeOfHivStatus', header: 'knowledgeOfHivStatus'},
            {field: 'pnsApproach', header: 'pnsApproach'}
        ];
    }

    private getDepressionScreeningColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'phQ9_1', header: 'phQ9_1'},
            {field: 'phQ9_2', header: 'phQ9_2'},
            {field: 'phQ9_3', header: 'phQ9_3'},
            {field: 'phQ9_4', header: 'phQ9_4'},
            {field: 'phQ9_5', header: 'phQ9_5'},
            {field: 'phQ9_6', header: 'phQ9_6'},
            {field: 'phQ9_7', header: 'phQ9_7'},
            {field: 'phQ9_8', header: 'phQ9_8'},
            {field: 'phQ9_9', header: 'phQ9_9'},
            {field: 'phQ_9_rating', header: 'phQ_9_rating'},
            {field: 'depressionAssesmentScore', header: 'depressionAssesmentScore'}
        ];
    }

    private getDrugAlcoholScreeningColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'drinkingAlcohol', header: 'drinkingAlcohol'},
            {field: 'smoking', header: 'smoking'},
            {field: 'drugUse', header: 'drugUse'}
        ];
    }
    private getEnhancedAdherenceCounsellingColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'sessionNumber', header: 'sessionNumber'},
            {field: 'dateOfFirstSession', header: 'dateOfFirstSession'},
            {field: 'pillCountAdherence', header: 'pillCountAdherence'},
            {field: 'mmaS4_1', header: 'mmaS4_1'},
            {field: 'mmaS4_2', header: 'mmaS4_2'},
            {field: 'mmaS4_3', header: 'mmaS4_3'},
            {field: 'mmaS4_4', header: 'mmaS4_4'},
            {field: 'mmsA8_1', header: 'mmsA8_1'},
            {field: 'mmsA8_2', header: 'mmsA8_2'},
            {field: 'mmsA8_3', header: 'mmsA8_3'},
            {field: 'mmsA8_4', header: 'mmsA8_4'},
            {field: 'mmsaScore', header: 'mmsaScore'},
            {field: 'eacRecievedVL', header: 'eacRecievedVL'},
            {field: 'eacvl', header: 'eacvl'},
            {field: 'eacvlConcerns', header: 'eacvlConcerns'},
            {field: 'eacvlThoughts', header: 'eacvlThoughts'},
            {field: 'eacWayForward', header: 'eacWayForward'},
            {field: 'eacCognitiveBarrier', header: 'eacCognitiveBarrier'},
            {field: 'eacBehaviouralBarrier_1', header: 'eacBehaviouralBarrier_1'},
            {field: 'eacBehaviouralBarrier_2', header: 'eacBehaviouralBarrier_2'},
            {field: 'eacBehaviouralBarrier_3', header: 'eacBehaviouralBarrier_3'},
            {field: 'eacBehaviouralBarrier_4', header: 'eacBehaviouralBarrier_4'},
            {field: 'eacBehaviouralBarrier_5', header: 'eacBehaviouralBarrier_5'},
            {field: 'eacEmotionalBarriers_1', header: 'eacEmotionalBarriers_1'},
            {field: 'eacEmotionalBarriers_2', header: 'eacEmotionalBarriers_2'},
            {field: 'eacEconBarrier_1', header: 'eacEconBarrier_1'},
            {field: 'eacEconBarrier_2', header: 'eacEconBarrier_2'},
            {field: 'eacEconBarrier_3', header: 'eacEconBarrier_3'},
            {field: 'eacEconBarrier_4', header: 'eacEconBarrier_4'},
            {field: 'eacEconBarrier_5', header: 'eacEconBarrier_5'},
            {field: 'eacEconBarrier_6', header: 'eacEconBarrier_6'},
            {field: 'eacEconBarrier_7', header: 'eacEconBarrier_7'},
            {field: 'eacEconBarrier_8', header: 'eacEconBarrier_8'},
            {field: 'eacReviewImprovement', header: 'eacReviewImprovement'},
            {field: 'eacReviewMissedDoses', header: 'eacReviewMissedDoses'},
            {field: 'eacReviewStrategy', header: 'eacReviewStrategy'},
            {field: 'eacReferral', header: 'eacReferral'},
            {field: 'eacReferralApp', header: 'eacReferralApp'},
            {field: 'eacReferralExperience', header: 'eacReferralExperience'},
            {field: 'eacHomevisit', header: 'eacHomevisit'},
            {field: 'eacAdherencePlan', header: 'eacAdherencePlan'},
            {field: 'eacFollowupDate', header: 'eacFollowupDate'}
        ];
    }
    private getGbvScreeningColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'ipv', header: 'ipv'},
            {field: 'physicalIPV', header: 'physicalIPV'},
            {field: 'emotionalIPV', header: 'emotionalIPV'},
            {field: 'sexualIPV', header: 'sexualIPV'},
            {field: 'ipvRelationship', header: 'ipvRelationship'}
        ];
    }

    private getIptColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'onTBDrugs', header: 'onTBDrugs'},
            {field: 'onIPT', header: 'onIPT'},
            {field: 'everOnIPT', header: 'everOnIPT'},
            {field: 'cough', header: 'cough'},
            {field: 'fever', header: 'fever'},
            {field: 'noticeableWeightLoss', header: 'noticeableWeightLoss'},
            {field: 'nightSweats', header: 'nightSweats'},
            {field: 'lethargy', header: 'lethargy'},
            {field: 'icfActionTaken', header: 'icfActionTaken'},
            {field: 'testResult', header: 'testResult'},
            {field: 'tbClinicalDiagnosis', header: 'tbClinicalDiagnosis'},
            {field: 'contactsInvited', header: 'contactsInvited'},
            {field: 'evaluatedForIPT', header: 'evaluatedForIPT'},
            {field: 'startAntiTBs', header: 'startAntiTBs'},
            {field: 'tbRxStartDate', header: 'tbRxStartDate'},
            {field: 'tbScreening', header: 'tbScreening'},
            {field: 'iptClientWorkUp', header: 'iptClientWorkUp'},
            {field: 'startIPT', header: 'startIPT'},
            {field: 'indicationForIPT', header: 'indicationForIPT'}
        ];
    }
    private getOtzColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'otzEnrollmentDate', header: 'otzEnrollmentDate'},
            {field: 'transferInStatus', header: 'transferInStatus'},
            {field: 'modulesPreviouslyCovered', header: 'modulesPreviouslyCovered'},
            {field: 'modulesCompletedToday', header: 'modulesCompletedToday'},
            {field: 'supportGroupInvolvement', header: 'supportGroupInvolvement'},
            {field: 'remarks', header: 'remarks'},
            {field: 'transitionAttritionReason', header: 'transitionAttritionReason'},
            {field: 'outcomeDate', header: 'outcomeDate'}
        ];
    }
    private getOvcColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'ovcEnrollmentDate', header: 'ovcEnrollmentDate'},
            {field: 'relationshipToClient', header: 'relationshipToClient'},
            {field: 'enrolledinCPIMS', header: 'enrolledinCPIMS'},
            {field: 'cpimsUniqueIdentifier', header: 'cpimsUniqueIdentifier'},
            {field: 'partnerOfferingOVCServices', header: 'partnerOfferingOVCServices'},
            {field: 'ovcExitReason', header: 'ovcExitReason'},
            {field: 'exitDate', header: 'exitDate'}
        ];
    }

    private getCovidColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'covid19AssessmentDate', header: 'covid19AssessmentDate'},
            {field: 'receivedCOVID19Vaccine', header: 'receivedCOVID19Vaccine'},
            {field: 'dateGivenFirstDose', header: 'dateGivenFirstDose'},
            {field: 'firstDoseVaccineAdministered', header: 'firstDoseVaccineAdministered'},
            {field: 'dateGivenSecondDose', header: 'dateGivenSecondDose'},
            {field: 'secondDoseVaccineAdministered', header: 'secondDoseVaccineAdministered'},
            {field: 'vaccinationStatus', header: 'vaccinationStatus'},
            {field: 'vaccineVerification', header: 'vaccineVerification'},
            {field: 'boosterGiven', header: 'boosterGiven'},
            {field: 'boosterDose', header: 'boosterDose'},
            {field: 'boosterDoseDate', header: 'boosterDoseDate'},
            {field: 'everCOVID19Positive', header: 'everCOVID19Positive'},
            {field: 'coviD19TestDate', header: 'coviD19TestDate'},
            {field: 'patientStatus', header: 'patientStatus'},
            {field: 'admissionStatus', header: 'admissionStatus'},
            {field: 'admissionUnit', header: 'admissionUnit'},
            {field: 'missedAppointmentDueToCOVID19', header: 'missedAppointmentDueToCOVID19'},
            {field: 'coviD19PositiveSinceLasVisit', header: 'coviD19PositiveSinceLasVisit'},
            {field: 'coviD19TestDateSinceLastVisit', header: 'coviD19TestDateSinceLastVisit'},
            {field: 'patientStatusSinceLastVisit', header: 'patientStatusSinceLastVisit'},
            {field: 'admissionStatusSinceLastVisit', header: 'admissionStatusSinceLastVisit'},
            {field: 'admissionStartDate', header: 'admissionStartDate'},
            {field: 'admissionEndDate', header: 'admissionEndDate'},
            {field: 'admissionUnitSinceLastVisit', header: 'admissionUnitSinceLastVisit'},
            {field: 'supplementalOxygenReceived', header: 'supplementalOxygenReceived'},
            {field: 'patientVentilated', header: 'patientVentilated'},
            {field: 'tracingFinalOutcome', header: 'tracingFinalOutcome'},
            {field: 'causeOfDeath', header: 'causeOfDeath'},
            {field: 'emr', header: 'emr'},
            {field: 'project', header: 'project'},
            {field: 'id', header: 'id'}
        ];
    }

    private getDefaulterTracingColumns(): void {
        this.cols = [
            {field: 'summary', header: 'Summary'},
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'encounterId', header: 'encounterId'},
            {field: 'tracingType', header: 'tracingType'},
            {field: 'tracingOutcome', header: 'tracingOutcome'},
            {field: 'attemptNumber', header: 'attemptNumber'},
            {field: 'isFinalTrace', header: 'isFinalTrace'},
            {field: 'trueStatus', header: 'trueStatus'},
            {field: 'causeOfDeath', header: 'causeOfDeath'},
            {field: 'comments', header: 'comments'},
            {field: 'bookingDate', header: 'bookingDate'},
            {field: 'id', header: 'id'}
        ];
    }

    private getCervicalCancerScreeningColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'VisitType', header: 'VisitType'},
            {field: 'screeningMethod', header: 'ScreeningMethod'},
            {field: 'treatmentToday', header: 'TreatmentToday'},
            {field: 'referredOut', header: 'ReferredOut'},
            {field: 'nextAppointmentDate', header: 'NextAppointmentDate'},
            {field: 'screeningType', header: 'ScreeningType'},
            {field: 'screeningResult', header: 'ScreeningResult'},
            {field: 'postTreatmentComplicationCause', header: 'PostTreatmentComplicationCause'},
            {field: 'otherPostTreatmentComplication', header: 'OtherPostTreatmentComplication'},
            {field: 'referralReason', header: 'ReferralReason'}

        ];
    }

    canExport(): boolean {
        return !(this.invalidExtracts.length > 0);
    }
}
