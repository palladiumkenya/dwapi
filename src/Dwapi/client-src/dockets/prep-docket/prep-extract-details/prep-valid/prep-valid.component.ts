import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {PageModel} from '../../../models/page-model';
import {PrepSummaryService} from "../../../services/prep-summary.service";

@Component({
    selector: 'liveapp-prep-valid',
    templateUrl: './prep-valid.component.html',
    styleUrls: ['./prep-valid.component.scss']
})
export class PrepValidComponent implements OnInit, OnDestroy {
    public validExtracts: any[] = [];
    public recordCount = 0;
    public cols: any[];
    public errorMessage: Message[];
    public otherMessage: Message[];
    public getValid$: Subscription;
    public getValidCount$: Subscription;
    public pageModel: PageModel;
    public initialRows: number = 10;
    private exName: string;
    public loadingData = false;
    private _preventLoad = true;

    constructor(private summaryService: PrepSummaryService) {
        this.pageModel = {
            page: 1,
            pageSize: this.initialRows
        };
    }

    get extract(): string {
        return this.exName;
    }

    @Input()
    set extract(extract: string) {
        if (extract) {
            this.exName = extract;
            this.cols = [];
            this.validExtracts = [];
            this.pageModel = {
                page: 1,
                pageSize: this.initialRows
            };
            this.getColumns();
            this.getValidExtracts();
        }else {
            this._preventLoad = true;
        }
    }

    get preventLoad(): boolean {
        return this._preventLoad;
    }

    @Input()
    set preventLoad(allow: boolean) {
        this._preventLoad = allow;
    }

    ngOnInit() {
        this.exName = 'Prep Patient';
        this.getPatientPrepExtractColumns();
    }

    public getValidExtracts(): void {
        if (this.extract === 'Pnc Visit') {this.getSummaryExtracts('PncVisit');return;}
        if (this.extract === 'Mother Baby Pair') {this.getSummaryExtracts('MotherBabyPair');return;}
        if (this.extract === 'Prep Patient') {this.getSummaryExtracts('PatientPrep');return;}
        if (this.extract === 'Prep Labs') {this.getSummaryExtracts('PrepLab');return;}
        if (this.extract === 'Prep Enrolment') {this.getSummaryExtracts('PrepEnrolment');return;}
        if (this.extract === 'Prep Art') {this.getSummaryExtracts('PrepArt');return;}
        if (this.extract === 'Mat Visit') {this.getSummaryExtracts('MatVisit');return;}
        if (this.extract === 'Hei') {this.getSummaryExtracts('Hei');return;}
        if (this.extract === 'Cwc Visit') {this.getSummaryExtracts('CwcVisit');return;}
        if (this.extract === 'Cwc Enrolment') {this.getSummaryExtracts('CwcEnrolment');return;}
        if (this.extract === 'Anc Visit') {this.getSummaryExtracts('AncVisit');return;}
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

    private getSummaryExtracts(ex: string): void {
        this.loadingData = true;
        this.getValidCount$ = this.summaryService.loadValidCount(ex)
            .subscribe(
                p => {
                    this.recordCount = p;
                    this.getValidSummaryExtracts(ex);
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

    private getValidSummaryExtracts(ex: string): void {
        this.loadingData = true;
        this.getValid$ = this.summaryService.loadValid(ex, this.pageModel).subscribe(
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

    pageView(event: any) {
        this.pageModel = {
            page: event.first / event.rows + 1,
            pageSize: event.rows,
            sortField: event.sortField,
            sortOrder: event.sortOrder
        };
        this.getColumns();
        this.getValidExtracts();
    }

    ngOnDestroy(): void {
        if (this.getValid$) {
            this.getValid$.unsubscribe();
        }
        if (this.getValidCount$) {
            this.getValidCount$.unsubscribe();
        }
    }
}
