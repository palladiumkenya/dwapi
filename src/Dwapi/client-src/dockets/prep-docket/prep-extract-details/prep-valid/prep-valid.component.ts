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
        if (this.extract === 'Prep Patient') {this.getSummaryExtracts('PatientPrep');return;}
        if (this.extract === 'Prep Adverse Events') {this.getSummaryExtracts('PrepAdverseEvent');return;}
        if (this.extract === 'Prep Behaviour Risk') {this.getSummaryExtracts('PrepBehaviourRisk');return;}
        if (this.extract === 'Prep Care Termination') {this.getSummaryExtracts('PrepCareTermination');return;}
        if (this.extract === 'Prep Pharmacy') {this.getSummaryExtracts('PrepPharmacy');return;}
        if (this.extract === 'Prep Labs') {this.getSummaryExtracts('PrepLab');return;}
        if (this.extract === 'Prep Visit') {this.getSummaryExtracts('PrepVisit');return;}
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

    private getPatientPrepExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

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
