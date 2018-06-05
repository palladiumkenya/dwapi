import { Component, OnInit, OnChanges, SimpleChange, Input } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { PatientExtract } from '../../../models/patient-extract';
import { Message } from 'primeng/api';
import { NdwhPatientsExtractService } from '../../../services/ndwh-patients-extract.service';
import {Extract} from '../../../../settings/model/extract';

@Component({
    selector: 'liveapp-invalid-record-details',
    templateUrl: './invalid-record-details.component.html',
    styleUrls: ['./invalid-record-details.component.scss']
})
export class InvalidRecordDetailsComponent implements OnInit, OnChanges {

    @Input() extract: string;
    private _patientExtractsService: NdwhPatientsExtractService;
    public invalidPatientExtracts: PatientExtract[] = [];
    public cols: any[];
    public getInvalid$: Subscription;
    public errorMessage: Message[];
    public otherMessage: Message[];

    constructor(patientExtractsService: NdwhPatientsExtractService) {
        this._patientExtractsService = patientExtractsService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.getColumns();
        this.getInalidExtracts();
    }

    ngOnInit() {
        this.getColumns();
        this.getInalidExtracts();
    }

    public getInalidExtracts(): void {
        this.getInvalid$ = this._patientExtractsService.loadErrors().subscribe(
            p => {
                this.invalidPatientExtracts = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {}
        );
    }

    private getColumns(): void {
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
