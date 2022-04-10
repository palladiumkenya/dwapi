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
        if (this.extract === 'Pnc Visit') {this.getSummaryInvalidExtracts('PncVisit');return;}
        if (this.extract === 'Mother Baby Pair') {this.getSummaryInvalidExtracts('MotherBabyPair');return;}
        if (this.extract === 'Prep Patient') {this.getSummaryInvalidExtracts('PatientPrep');return;}
        if (this.extract === 'Prep Labs') {this.getSummaryInvalidExtracts('PrepLab');return;}
        if (this.extract === 'Prep Enrolment') {this.getSummaryInvalidExtracts('PrepEnrolment');return;}
        if (this.extract === 'Prep Art') {this.getSummaryInvalidExtracts('PrepArt');return;}
        if (this.extract === 'Mat Visit') {this.getSummaryInvalidExtracts('MatVisit');return;}
        if (this.extract === 'Hei') {this.getSummaryInvalidExtracts('Hei');return;}
        if (this.extract === 'Cwc Visit') {this.getSummaryInvalidExtracts('CwcVisit');return;}
        if (this.extract === 'Cwc Enrolment') {this.getSummaryInvalidExtracts('CwcEnrolment');return;}
        if (this.extract === 'Anc Visit') {this.getSummaryInvalidExtracts('AncVisit');return;}
    }

    private getColumns(): void {
        if (this.extract === 'Pnc Visit') {this.getPncVisitExtractColumns();return;}
        if (this.extract === 'Mother Baby Pair') {this.getMotherBabyPairExtractColumns();return;}
        if (this.extract === 'Prep Patient') {this.getPatientPrepExtractColumns();return;}
        if (this.extract === 'Prep Labs') {this.getPrepLabExtractColumns();return;}
        if (this.extract === 'Prep Enrolment') {this.getPrepEnrolmentExtractColumns();return;}
        if (this.extract === 'Prep Art') {this.getPrepArtExtractColumns();return;}
        if (this.extract === 'Mat Visit') {this.getMatVisitExtractColumns();return;}
        if (this.extract === 'Hei') {this.getHeiExtractColumns();return;}
        if (this.extract === 'Cwc Visit') {this.getCwcVisitExtractColumns();return;}
        if (this.extract === 'Cwc Enrolment') {this.getCwcEnrolmentExtractColumns();return;}
        if (this.extract === 'Anc Visit') {this.getPncVisitExtractColumns();return;}

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

    private getPncVisitExtractColumns() {

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

    private getMotherBabyPairExtractColumns() {
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

    private getPrepEnrolmentExtractColumns() {
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

    private getPrepArtExtractColumns() {
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

    private getMatVisitExtractColumns() {
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

    private getHeiExtractColumns() {
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

    private getCwcVisitExtractColumns() {
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

    private getCwcEnrolmentExtractColumns() {
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
