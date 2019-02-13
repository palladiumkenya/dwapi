import { Component, OnInit, OnChanges, SimpleChange, Input } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { PatientExtract } from '../../../models/patient-extract';
import { Message } from 'primeng/api';
import { NdwhPatientsExtractService } from '../../../services/ndwh-patients-extract.service';
import { NdwhPatientArtService } from '../../../services/ndwh-patient-art.service';
import { NdwhPatientBaselineService } from '../../../services/ndwh-patient-baseline.service';
import { NdwhPatientLaboratoryService } from '../../../services/ndwh-patient-laboratory.service';
import { NdwhPatientPharmacyService } from '../../../services/ndwh-patient-pharmacy.service';
import { NdwhPatientStatusService } from '../../../services/ndwh-patient-status.service';
import { NdwhPatientVisitService } from '../../../services/ndwh-patient-visit.service';
import {Extract} from '../../../../settings/model/extract';
import { NdwhPatientAdverseEventService } from '../../../services/ndwh-patient-adverse-event.service';

@Component({
    selector: 'liveapp-invalid-record-details',
    templateUrl: './invalid-record-details.component.html',
    styleUrls: ['./invalid-record-details.component.scss']
})
export class InvalidRecordDetailsComponent implements OnInit, OnChanges {

    @Input() extract: string;
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

    constructor(patientExtractsService: NdwhPatientsExtractService, patientArtService: NdwhPatientArtService,
        patientBaselineService: NdwhPatientBaselineService, patientLabService: NdwhPatientLaboratoryService,
        patientPharmacyService: NdwhPatientPharmacyService, patientStatusService: NdwhPatientStatusService,
        patientVisitService: NdwhPatientVisitService, patientAdverseEventService: NdwhPatientAdverseEventService) {
        this._patientExtractsService = patientExtractsService;
        this._patientArtService = patientArtService;
        this._patientBaselineService = patientBaselineService;
        this._patientLabService = patientLabService;
        this._patientPharmacyService = patientPharmacyService;
        this._patientStatusService = patientStatusService;
        this._patientVisitService = patientVisitService;
        this._patientAdverseEventService = patientAdverseEventService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.cols = [];
        this.invalidExtracts = [];
        this.getColumns();
        this.getInvalidExtracts();
    }

    ngOnInit() {
        this.getPatientColumns();
        this.getInvalidPatientExtracts();
    }

    public getInvalidExtracts(): void {
        if (this.extract === 'All Patients') {
            this.getInvalidPatientExtracts();
        } if (this.extract === 'ART Patients') {
            this.getInvalidPatientArtExtracts();
        }  if (this.extract === 'Patient Baselines') {
            this.getInvalidPatientBaselineExtracts();
        }  if (this.extract === 'Patient Labs') {
            this.getInvalidPatientLabExtracts();
        }  if (this.extract === 'Patient Pharmacy') {
            this.getInvalidPatientPharmacyExtracts();
        }  if (this.extract === 'Patient Status') {
            this.getInvalidPatientStatusExtracts();
        }  if (this.extract === 'Patient Visits') {
            this.getInvalidPatientVisitExtracts();
        }  if (this.extract === 'Patient Adverse Events') {
            this.getInvalidPatientAdverseEventExtracts();
        }
    }

    private getColumns(): void {
        if (this.extract === 'All Patients') {
            this.getPatientColumns();
        } if (this.extract === 'ART Patients') {
            this.getPatientArtColumns();
        }  if (this.extract === 'Patient Baselines') {
            this.getPatientBaselineColumns();
        }  if (this.extract === 'Patient Labs') {
            this.getPatientLaboratoryColumns();
        }  if (this.extract === 'Patient Pharmacy') {
            this.getPatientPharmacyColumns();
        }  if (this.extract === 'Patient Status') {
            this.getPatientStatusColumns();
        }  if (this.extract === 'Patient Visits') {
            this.getPatientVisitColumns();
        }  if (this.extract === 'Patient Adverse Events') {
            this.getPatientAdverseEventColumns();
        }
    }

    private getInvalidPatientExtracts(): void {
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
            }
        );
    }

    private getInvalidPatientArtExtracts(): void {
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
            }
        );
    }

    private getInvalidPatientBaselineExtracts(): void {
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
            }
        );
    }

    private getInvalidPatientLabExtracts(): void {
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
            }
        );
    }

    private getInvalidPatientPharmacyExtracts(): void {
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
            }
        );
    }

    private getInvalidPatientStatusExtracts(): void {
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
            }
        );
    }

    private getInvalidPatientVisitExtracts(): void {
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
            }
        );
    }
    private getInvalidPatientAdverseEventExtracts(): void {
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
            }
        );
    }

    private getPatientColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' },
            { field: 'summary', header: 'Summary' },
            { field: 'dateGenerated', header: 'DateGenerated' },
            { field: 'patientPK', header: 'PatientPK' },
            { field: 'facilityId', header: 'FacilityId' },
            { field: 'patientID', header: 'PatientID' },
            { field: 'siteCode', header: 'SiteCode' },
            { field: 'facilityName', header: 'FacilityName' }
        ];
    }

    private getPatientArtColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' },
            { field: 'summary', header: 'Summary' },
            { field: 'dateGenerated', header: 'Date Generated' },
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'facilityName', header: 'Facility Name' },
            { field: 'dob', header: 'DOB' },
            { field: 'gender', header: 'Gender' },
            { field: 'patientSource', header: 'Patient Source' },
            { field: 'registrationDate', header: 'Registration Date' },
            { field: 'ageLastVisit', header: 'Age Last Visit' },
            { field: 'previousARTStartDate', header: 'Previous ART Start Date' },
            { field: 'previousARTRegimen', header: 'Previous ART Regimen' },
            { field: 'startARTAtThisFacility', header: 'Start ART At This Facility' },
            { field: 'startARTDate', header: 'Start ART Date' },
            { field: 'startRegimen', header: 'Start Regimen' },
            { field: 'startRegimenLine', header: 'Start Regimen Line' },
            { field: 'lastARTDate', header: 'Last ART Date' },
            { field: 'lastRegimen', header: 'Last Regimen' },
            { field: 'lastRegimenLine', header: 'Last Regimen Line' },
            { field: 'lastVisit', header: 'Last Visit' },
            { field: 'exitReason', header: 'Exit Reason' },
            { field: 'exitDate', header: 'Exit Date' }
        ];
    }

    private getPatientBaselineColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' },
            { field: 'summary', header: 'Summary' },
            { field: 'dateGenerated', header: 'Date Generated' },
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'recordId', header: 'Record Id' },
            { field: 'bCD4', header: 'bCD4' },
            { field: 'bCD4Date', header: 'bCD4 Date' },
            { field: 'bWAB', header: 'bWAB' },
            { field: 'bWABDate', header: 'bWAB Date' },
            { field: 'bWHO', header: 'bWHO' },
            { field: 'bWHODate', header: 'bWHO Date' },
            { field: 'eWAB', header: 'eWAB' },
            { field: 'eWABDate', header: 'eWAB Date' },
            { field: 'eCD4', header: 'eCD4' },
            { field: 'eCD4Date', header: 'eCD4 Date' },
            { field: 'eWHO', header: 'eWHO' },
            { field: 'eWHODate', header: 'eWHO Date' },
            { field: 'lastWHO', header: 'lastWHO' },
            { field: 'lastWHODate', header: 'last WHO Date' },
            { field: 'lastCD4', header: 'lastCD4' },
            { field: 'lastCD4Date', header: 'last CD4 Date' },
            { field: 'lastWAB', header: 'lastWAB' },
            { field: 'lastWABDate', header: 'last WAB Date' },
            { field: 'm12CD4', header: 'm12CD4' },
            { field: 'm12CD4Date', header: 'm12CD4 Date' },
            { field: 'm6CD4', header: 'm6CD4' },
            { field: 'm6CD4Date', header: 'm6CD4 Date' }
        ];
    }

    private getPatientLaboratoryColumns(): void {
        this.cols = [
        { field: 'summary', header: 'Summary' },
        { field: 'extract', header: 'Extract' },
        { field: 'field', header: 'Field' },
        { field: 'type', header: 'Type' },
        { field: 'summary', header: 'Summary' },
        { field: 'dateGenerated', header: 'Date Generated' },
        { field: 'patientPK', header: 'Patient PK' },
        { field: 'facilityId', header: 'Facility Id' },
        { field: 'patientID', header: 'Patient ID' },
        { field: 'siteCode', header: 'Site Code' },
        { field: 'facilityName', header: 'Facility Name' },
        { field: 'orderedByDate', header: 'Ordered By Date' },
        { field: 'reportedByDate', header: 'Reported By Date' },
        { field: 'testName', header: 'Test Name' },
        { field: 'enrollmentTest', header: 'Enrollment Test' },
        { field: 'testResult', header: 'Test Result' },
        { field: 'visitId', header: 'Visit Id' }
        ];
    }

    private getPatientPharmacyColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' },
            { field: 'summary', header: 'Summary' },
            { field: 'dateGenerated', header: 'Date Generated' },
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'drug', header: 'Drug' },
            { field: 'dispenseDate', header: 'Dispense Date' },
            { field: 'duration', header: 'Duration' },
            { field: 'expectedReturn', header: 'Expected Return' },
            { field: 'treatmentType', header: 'Treatment Type' },
            { field: 'regimenLine', header: 'Regimen Line' },
            { field: 'periodTaken', header: 'Period Taken' },
            { field: 'prophylaxisType', header: 'ProphylaxisT ype' },
            { field: 'provider', header: 'Provider' },
            { field: 'visitID', header: 'VisitID' }
        ];
    }

    private getPatientStatusColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' },
            { field: 'summary', header: 'Summary' },
            { field: 'dateGenerated', header: 'Date Generated' },
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'facilityName', header: 'Facility Name' },
            { field: 'exitDescription', header: 'Exit Description' },
            { field: 'exitDate', header: 'Exit Date' },
            { field: 'exitReason', header: 'Exit Reason' }
        ];
    }

    private getPatientVisitColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' },
            { field: 'summary', header: 'Summary' },
            { field: 'dateGenerated', header: 'Date Generated' },
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'facilityName', header: 'Facility Name' },
            { field: 'visitDate', header: 'Visit Date' },
            { field: 'service', header: 'Service' },
            { field: 'visitType', header: 'Visit Type' },
            { field: 'wHOStage', header: 'WHO Stage' },
            { field: 'wABStage', header: 'WAB Stage' },
            { field: 'pregnant', header: 'Pregnant' },
            { field: 'lMP', header: 'LMP' },
            { field: 'eDD', header: 'EDD' },
            { field: 'height', header: 'Height' },
            { field: 'weight', header: 'Weight' },
            { field: 'bP', header: 'BP' },
            { field: 'oI', header: 'OI' },
            { field: 'oIDate', header: 'OI Date' },
            { field: 'adherence', header: 'Adherence' },
            { field: 'adherenceCategory', header: 'Adherence Category' },
            { field: 'substitutionFirstlineRegimenDate', header: 'Substitution Firstline Regimen Date' },
            { field: 'substitutionFirstlineRegimenReason', header: 'Substitution Firstline Regimen Reason' },
            { field: 'substitutionSecondlineRegimenDate', header: 'Substitution Secondline Regimen Date' },
            { field: 'substitutionSecondlineRegimenReason', header: 'Substitution Secondline Regimen Reason' },
            { field: 'secondlineRegimenChangeDate', header: 'Secondline Regimen Change Date' },
            { field: 'secondlineRegimenChangeReason', header: 'Secondline RegimenChange Reason' },
            { field: 'familyPlanningMethod', header: 'Family Planning Method' },
            { field: 'pwP', header: 'PwP' },
            { field: 'gestationAge', header: 'Gestation Age' },
            { field: 'nextAppointmentDate', header: 'Next Appointment Date' },
            { field: 'visitId', header: 'Visit Id' }
        ];
    }

    private getPatientAdverseEventColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'dateExtracted', header: 'Date Extracted' },
            { field: 'emr', header: 'Emr' },
            { field: 'project', header: 'Project' },
            { field: 'adverseEvent', header: 'Adverse Event' },
            { field: 'adverseEventStartDate', header: 'Start Date' },
            { field: 'adverseEventEndDate', header: 'End Date' },
            { field: 'severity', header: 'Severity' },
            { field: 'adverseEventRegimen', header: 'Regimen' },
            { field: 'adverseEventCause', header: 'Cause' },
            { field: 'adverseEventClinicalOutcome', header: 'Clinical Outcome' },
            { field: 'adverseEventActionTaken', header: 'Action Taken' },
            { field: 'adverseEventIsPregnant', header: 'Is Pregnant?' },
            { field: 'visitDate', header: 'Visit Date' }
        ];
    }
}
