import { Component, OnInit, OnChanges, SimpleChange, Input } from '@angular/core';
import { PatientExtract } from '../../../models/patient-extract';
import { NdwhPatientsExtractService } from '../../../services/ndwh-patients-extract.service';
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
    public validPatientExtracts: PatientExtract[] = [];
    public cols: any[];
    public errorMessage: Message[];
    public otherMessage: Message[];
    public getValid$: Subscription;

    constructor(patientExtractsService: NdwhPatientsExtractService) {
        this._patientExtractsService = patientExtractsService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
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
        } else {
            this.validPatientExtracts = [];
        }
    }

    private getColumns(): void {
        if (this.extract === 'All Patients') {
            this.getPatientColumns();
        } else {
            this.cols = [];
        }
    }

    private getValidPatientExtracts(): void {
        this.getValid$ = this._patientExtractsService.loadValid().subscribe(
            p => {
                this.validPatientExtracts = p;
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
}
