import {Component, Input, OnChanges, OnInit, SimpleChange} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import { Message } from 'primeng/api';
import {PrepSummaryService} from "../../../services/prep-summary.service";



@Component({
    selector: 'liveapp-prep-invalid',
    templateUrl: './prep-invalid.component.html',
    styleUrls: ['./prep-invalid.component.scss']
})
export class PrepInvalidComponent implements OnInit, OnChanges {

    private exName: string;
    public invalidExtracts: any[] = [];
    public cols: any[];
    public getInvalid$: Subscription;
    public errorMessage: Message[];
    public otherMessage: Message[];
    private _preventLoad = true;
    public loadingData = false;

    constructor(private summaryService: PrepSummaryService) {
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.cols = [];
        this.invalidExtracts = [];
        this.getColumns();
        this.getInvalidExtracts();
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
        this.getPatientPrepExtractColumns();
    }

    public getInvalidExtracts(): void {
        if (this.extract === 'Prep Patient') {this.getSummaryInvalidExtracts('PatientPrep');return;}
        if (this.extract === 'Prep Adverse Events') {this.getSummaryInvalidExtracts('PrepAdverseEvent');return;}
        if (this.extract === 'Prep Behaviour Risk') {this.getSummaryInvalidExtracts('PrepBehaviourRisk');return;}
        if (this.extract === 'Prep Care Termination') {this.getSummaryInvalidExtracts('PrepCareTermination');return;}
        if (this.extract === 'Prep Pharmacy') {this.getSummaryInvalidExtracts('PrepPharmacy');return;}
        if (this.extract === 'Prep Labs') {this.getSummaryInvalidExtracts('PrepLab');return;}
        if (this.extract === 'Prep Visit') {this.getSummaryInvalidExtracts('PrepVisit');return;}
    }

    private getColumns(): void {
        if (this.extract === 'Prep Patient') {this.getPatientPrepExtractColumns();return;}
        if (this.extract === 'Prep Adverse Events') {this.getPrepAdverseEventExtractColumns();return;}
        if (this.extract === 'Prep Behaviour Risk') {this.getPrepBehaviourRiskExtractColumns();return;}
        if (this.extract === 'Prep Care Termination') {this.getPrepCareTerminationExtractColumns();return;}
        if (this.extract === 'Prep Pharmacy') {this.getPrepPharmacyExtractColumns();return;}
        if (this.extract === 'Prep Labs') {this.getPrepLabExtractColumns();return;}
        if (this.extract === 'Prep Visit') {this.getPrepVisitExtractColumns();return;}

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

    private getPatientPrepExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'facilityName', header: 'Facility Name'}
        ]
    }

    private getPrepAdverseEventExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'facilityName', header: 'Facility Name'}
        ]
    }

    private getPrepBehaviourRiskExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'facilityName', header: 'Facility Name'}
        ]
    }

    private getPrepCareTerminationExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'facilityName', header: 'Facility Name'}
        ]
    }

    private getPrepLabExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'facilityName', header: 'Facility Name'}
        ]
    }

    private getPrepPharmacyExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'facilityName', header: 'Facility Name'}
        ]
    }

    private getPrepVisitExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'facilityName', header: 'Facility Name'}
        ]
    }
}
