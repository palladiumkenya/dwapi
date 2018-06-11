import { Component, OnInit, OnChanges, SimpleChange, Input } from '@angular/core';
import { PatientExtract } from '../../../models/patient-extract';
import { NdwhPatientsExtractService } from '../../../services/ndwh-patients-extract.service';
import { NdwhPatientArtService } from '../../../services/ndwh-patient-art.service';
import { NdwhPatientBaselineService } from '../../../services/ndwh-patient-baseline.service';
import { NdwhPatientLaboratoryService } from '../../../services/ndwh-patient-laboratory.service';
import { NdwhPatientPharmacyService } from '../../../services/ndwh-patient-pharmacy.service';
import { NdwhPatientStatusService } from '../../../services/ndwh-patient-status.service';
import { NdwhPatientVisitService } from '../../../services/ndwh-patient-visit.service';
import { Message } from 'primeng/api';
import { Subscription } from 'rxjs/Subscription';
import {Extract} from '../../../../settings/model/extract';

@Component({
    selector: 'liveapp-valid-record-details',
    templateUrl: './valid-record-details.component.html',
    styleUrls: ['./valid-record-details.component.scss']
})
export class ValidRecordDetailsComponent implements OnInit, OnChanges {

    @Input() extract: string;
    private _patientExtractsService: NdwhPatientsExtractService;
    private _patientArtService: NdwhPatientArtService;
    private _patientBaselineService: NdwhPatientBaselineService;
    private _patientLabService: NdwhPatientLaboratoryService;
    private _patientPharmacyService: NdwhPatientPharmacyService;
    private _patientStatusService: NdwhPatientStatusService;
    private _patientVisitService: NdwhPatientVisitService;
    public validExtracts: any[] = [];
    public cols: any[];
    public errorMessage: Message[];
    public otherMessage: Message[];
    public getValid$: Subscription;

    constructor(patientExtractsService: NdwhPatientsExtractService, patientArtService: NdwhPatientArtService,
        patientBaselineService: NdwhPatientBaselineService, patientLabService: NdwhPatientLaboratoryService,
        patientPharmacyService: NdwhPatientPharmacyService, patientStatusService: NdwhPatientStatusService,
        patientVisitService: NdwhPatientVisitService) {
        this._patientExtractsService = patientExtractsService;
        this._patientArtService = patientArtService;
        this._patientBaselineService = patientBaselineService;
        this._patientLabService = patientLabService;
        this._patientPharmacyService = patientPharmacyService;
        this._patientStatusService = patientStatusService;
        this._patientVisitService = patientVisitService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.cols = [];
        this.validExtracts = [];
        this.getColumns();
        this.getValidExtracts();
    }

    ngOnInit() {
        this.getPatientColumns();
        this.getValidPatientExtracts();
    }

    public getValidExtracts(): void {
        if (this.extract === 'All Patients') {
            this.getValidPatientExtracts();
        } if (this.extract === 'ART Patients') {
            this.getValidPatientArtExtracts();
        }  if (this.extract === 'Patient Baselines') {
            this.getValidPatientBaselineExtracts();
        }  if (this.extract === 'Patient Labs') {
            this.getValidPatientLabExtracts();
        }  if (this.extract === 'Patient Pharmacy') {
            this.getValidPatientPharmacyExtracts();
        }  if (this.extract === 'Patient Status') {
            this.getValidPatientStatusExtracts();
        }  if (this.extract === 'Patient Visits') {
            this.getValidPatientVisitExtracts();
        }  else {
            this.validExtracts = [];
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
        }  if (this.extract === 'Patient Visit') {
            this.getPatientVisitColumns();
        } else {
            this.cols = [];
        }
    }

    private getValidPatientExtracts(): void {
        this.getValid$ = this._patientExtractsService.loadValid().subscribe(
            p => {
                this.validExtracts = p;
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

    private getValidPatientArtExtracts(): void {
        this.getValid$ = this._patientArtService.loadValid().subscribe(
            p => {
                this.validExtracts = p;
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

    private getValidPatientBaselineExtracts(): void {
        this.getValid$ = this._patientBaselineService.loadValid().subscribe(
            p => {
                this.validExtracts = p;
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

    private getValidPatientLabExtracts(): void {
        this.getValid$ = this._patientLabService.loadValid().subscribe(
            p => {
                this.validExtracts = p;
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

    private getValidPatientPharmacyExtracts(): void {
        this.getValid$ = this._patientPharmacyService.loadValid().subscribe(
            p => {
                this.validExtracts = p;
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

    private getValidPatientStatusExtracts(): void {
        this.getValid$ = this._patientStatusService.loadValid().subscribe(
            p => {
                this.validExtracts = p;
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

    private getValidPatientVisitExtracts(): void {
        this.getValid$ = this._patientVisitService.loadValid().subscribe(
            p => {
                this.validExtracts = p;
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
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'emr', header: 'EMR' },
            { field: 'project', header: 'Project' },
            { field: 'facilityName', header: 'Facility Name' },
            { field: 'gender', header: 'Gender' },
            { field: 'dob', header: 'DOB' },
            { field: 'registrationDate', header: 'Registration Date' },
            { field: 'registrationAtCCC', header: 'Registration At CCC' },
            { field: 'registrationAtPMTCT', header: 'Registration At PMTCT' },
            {
                field: 'registrationAtTBClinic',
                header: 'Registration At TB Clinic'
            },
            { field: 'patientSource', header: 'Patient Source' },
            { field: 'region', header: 'Region' },
            { field: 'district', header: 'District' },
            { field: 'village', header: 'Village' },
            { field: 'contactRelation', header: 'Contact Relation' },
            { field: 'lastVisit', header: 'Last Visit' },
            { field: 'maritalStatus', header: 'Marital Status' },
            { field: 'educationLevel', header: 'Education Level' },
            {
                field: 'dateConfirmedHIVPositive',
                header: 'Date Confirmed HIV Positive'
            },
            { field: 'previousARTExposure', header: 'Previous ART Exposure' },
            {
                field: 'previousARTStartDate',
                header: 'Previous ART Start Date'
            },
            { field: 'statusAtCCC', header: 'Status At CCC' },
            { field: 'statusAtPMTCT', header: 'Status At PMTCT' },
            { field: 'statusAtTBClinic', header: 'Status At TB Clinic' },
            { field: 'satelliteName', header: 'Satellite Name' }
        ];
    }

    private getPatientArtColumns(): void {
        this.cols = [
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'emr', header: 'EMR' },
            { field: 'project', header: 'Project' },
        ];
    }

    private getPatientBaselineColumns(): void {
        this.cols = [
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'emr', header: 'EMR' },
            { field: 'project', header: 'Project' },
        ];
    }

    private getPatientLaboratoryColumns(): void {
        this.cols = [
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'emr', header: 'EMR' },
            { field: 'project', header: 'Project' },
        ];
    }

    private getPatientPharmacyColumns(): void {
        this.cols = [
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'emr', header: 'EMR' },
            { field: 'project', header: 'Project' },
        ];
    }

    private getPatientStatusColumns(): void {
        this.cols = [
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'emr', header: 'EMR' },
            { field: 'project', header: 'Project' },
        ];
    }

    private getPatientVisitColumns(): void {
        this.cols = [
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'emr', header: 'EMR' },
            { field: 'project', header: 'Project' },
        ];
    }
}
