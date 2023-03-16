import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {PageModel} from '../../../models/page-model';
import {MnchSummaryService} from "../../../services/mnch-summary.service";

@Component({
    selector: 'liveapp-mnch-valid',
    templateUrl: './mnch-valid.component.html',
    styleUrls: ['./mnch-valid.component.scss']
})
export class MnchValidComponent implements OnInit, OnDestroy {
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

    constructor(private summaryService: MnchSummaryService) {
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
        this.exName = 'Mnch Patient';
        this.getPatientMnchExtractColumns();
    }

    public getValidExtracts(): void {
        if (this.extract === 'Pnc Visit') {this.getSummaryExtracts('PncVisit');return;}
        if (this.extract === 'Mother Baby Pair') {this.getSummaryExtracts('MotherBabyPair');return;}
        if (this.extract === 'Mnch Patient') {this.getSummaryExtracts('PatientMnch');return;}
        if (this.extract === 'Mnch Labs') {this.getSummaryExtracts('MnchLab');return;}
        if (this.extract === 'Mnch Enrolment') {this.getSummaryExtracts('MnchEnrolment');return;}
        if (this.extract === 'Mnch Art') {this.getSummaryExtracts('MnchArt');return;}
        if (this.extract === 'Mat Visit') {this.getSummaryExtracts('MatVisit');return;}
        if (this.extract === 'Hei') {this.getSummaryExtracts('Hei');return;}
        if (this.extract === 'Cwc Visit') {this.getSummaryExtracts('CwcVisit');return;}
        if (this.extract === 'Cwc Enrolment') {this.getSummaryExtracts('CwcEnrolment');return;}
        if (this.extract === 'Anc Visit') {this.getSummaryExtracts('AncVisit');return;}
        if (this.extract === 'Mnch Immunization') {this.getSummaryExtracts('MnchImmunization');return;}

    }

    private getColumns(): void {
        if (this.extract === 'Pnc Visit') {this.getPncVisitExtractColumns();return;}
        if (this.extract === 'Mother Baby Pair') {this.getMotherBabyPairExtractColumns();return;}
        if (this.extract === 'Mnch Patient') {this.getPatientMnchExtractColumns();return;}
        if (this.extract === 'Mnch Labs') {this.getMnchLabExtractColumns();return;}
        if (this.extract === 'Mnch Enrolment') {this.getMnchEnrolmentExtractColumns();return;}
        if (this.extract === 'Mnch Art') {this.getMnchArtExtractColumns();return;}
        if (this.extract === 'Mat Visit') {this.getMatVisitExtractColumns();return;}
        if (this.extract === 'Hei') {this.getHeiExtractColumns();return;}
        if (this.extract === 'Cwc Visit') {this.getCwcVisitExtractColumns();return;}
        if (this.extract === 'Cwc Enrolment') {this.getCwcEnrolmentExtractColumns();return;}
        if (this.extract === 'Anc Visit') {this.getPncVisitExtractColumns();return;}
        if (this.extract === 'Mnch Immunization') {this.getMnchImmunizationExtractColumns();return;}


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

    private getPatientMnchExtractColumns() {
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

    private getMnchLabExtractColumns() {
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

    private getMnchEnrolmentExtractColumns() {
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

    private getMnchArtExtractColumns() {
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

    private getMnchImmunizationExtractColumns() {
        this.cols = [
            {field: 'visitDate', header: 'Visit Date'},
            {field: 'visitID', header: 'Visit ID'},
            {field: 'revisitThisYear', header: 'RevisitThisYear'},
            {field: 'height', header: 'Height'},
            {field: 'weight', header: 'Weight'},
            {field: 'heightLength', header: 'Height Length'},
            {field: 'pemp', header: 'Temp'},
            {field: 'pulseRate', header: 'PulseRate'},
            {field: 'respiratoryRate', header: 'Respiratory Rate'},
            {field: 'oxygenSaturation', header: 'Oxygen Saturation'},
            {field: 'muac', header: 'MUAC'},
            {field: 'weightCategory', header: 'Weight Category'},
            {field: 'stunted', header: 'Stunted'},
            {field: 'infantFeeding', header: 'Infant Feeding'},
            {field: 'medicationGiven', header: 'Medication Given'},
            {field: 'tbAssessment', header: 'TBAssessment'},
            {field: 'mnpsSupplementation', header: 'MNPsSupplementation'},
            {field: 'immunization', header: 'Immunization'},
            {field: 'immunizationGiven', header: 'Immunization Given'},
            {field: 'dangerSigns', header: 'Danger Signs'},
            {field: 'milestones', header: 'Milestones'},
            {field: 'vitaminA', header: 'VitaminA'},
            {field: 'disability', header: 'Disability'},
            {field: 'receivedMosquitoNet', header: 'ReceivedMosquitoNet'},
            {field: 'dewormed', header: 'Dewormed'},
            {field: 'referredFrom', header: 'Referred From'},
            {field: 'referredTo', header: 'Referred To'},
            {field: 'refferred', header: 'Refferred'},
            {field: 'referralReasons', header: 'Referral Reasons'},
            {field: 'followUP', header: 'Follow UP'},
            {field: 'nextAppointment', header: 'Next Appointment'},
            {field: 'dnapcr', header: 'dnapcr'},
            {field: 'dnapcrdate', header: 'dnapcrdate'}
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
