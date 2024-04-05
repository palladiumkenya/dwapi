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
        if (this.extract === 'Anc Visit') {this.getAncVisitExtractColumns();return;}
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
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

            {field: 'visitTimingMother', header: 'VisitTimingMother'},
            {field: 'visitTimingBaby', header: 'VisitTimingBaby'},
            {field: 'motherCameForHIVTest', header: 'MotherCameForHIVTest'},
            {field: 'infactCameForHAART', header: 'InfactCameForHAART'},
            {field: 'motherGivenHAART', header: 'MotherGivenHAART'},

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
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},
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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

            {field: 'facilityName', header: 'Facility Name'}
        ]
    }

    private getMnchArtExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

            {field: 'facilityReceivingARTCare', header: 'FacilityReceivingARTCare'},

        ]
    }

    private getMatVisitExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

            {field: 'emr', header: 'EMR'},
            {field: 'lmp', header: 'LMP'},
            {field: 'edd', header: 'EDD'},
            {field: 'maternalDeathAudited', header: 'Maternal Death Audited'},
            {field: 'referralReason', header: 'Referral Reason'},
            {field: 'onARTMat', header: 'On ART Mat'},
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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

            {field: 'facilityName', header: 'Facility Name'}
        ]
    }

    private getCwcVisitExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

            {field: 'revisitThisYear', header: 'RevisitThisYear'},
            {field: 'refferred', header: 'Refferred'},
            {field: 'heightLength', header: 'HeightLength'},


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
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

            {field: 'facilityName', header: 'Facility Name'}
        ]
    }

    private getAncVisitExtractColumns() {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

            {field: 'hepatitisBScreening', header: 'HepatitisB Screening'},
            {field: 'treatedHepatitisB', header: 'Treated HepatitisB'},
            {field: 'presumptiveTreatmentGiven', header: 'Presumptive Treatment Given'},
            {field: 'presumptiveTreatmentDose', header: 'Presumptive Treatment Dose'},
            {field: 'miminumPackageOfCareReceived', header: 'Miminum Package Of Care Received'},
            {field: 'miminumPackageOfCareServices', header: 'Miminum Package Of Care Services'}
        ]
    }

    private getMnchImmunizationExtractColumns() {
        this.cols = [
            {field:'facilityName',header:'FacilityName'},
            {field:'patientMnchID',header:'PatientMnchID'},
            {field:'bcg',header:'BCG'},
            {field:'opVatBirth',header:'OPVatBirth'},
            {field: 'recordUUID', header: 'RecordUUID'},
            {field: 'voided', header: 'Voided'},

            {field:'opV1',header:'OPV1'},
            {field:'opV2',header:'OPV2'},
            {field:'opV3',header:'OPV3'},
            {field:'ipv',header:'IPV'},
            {field:'dptHepBHIB1',header:'DPTHepBHIB1'},
            {field:'dptHepBHIB2',header:'DPTHepBHIB2'},
            {field:'dptHepBHIB3',header:'DPTHepBHIB3'},
            {field:'pcV101',header:'PCV101'},
            {field:'pcV102',header:'PCV102'},
            {field:'pcV103',header:'PCV103'},
            {field:'rotA1',header:'ROTA1'},
            {field:'measlesReubella1',header:'MeaslesReubella1'},
            {field:'yellowFever',header:'YellowFever'},
            {field:'measlesReubella2',header:'MeaslesReubella2'},
            {field:'measlesAt6Months',header:'MeaslesAt6Months'},
            {field:'rotA2',header:'ROTA2'},
            {field:'dateOfNextVisit',header:'DateOfNextVisit'},
            {field:'bcgScarChecked',header:'BCGScarChecked'},
            {field:'dateChecked',header:'DateChecked'},
            {field:'dateBCGrepeated',header:'DateBCGrepeated'},
            {field:'vitaminAAt6Months',header:'VitaminAAt6Months'},
            {field:'vitaminAAt1Yr',header:'VitaminAAt1Yr'},
            {field:'vitaminAAt18Months',header:'VitaminAAt18Months'},
            {field:'vitaminAAt2Years',header:'VitaminAAt2Years'},
            {field:'vitaminAAt2To5Years',header:'VitaminAAt2To5Years'},
            {field:'fullyImmunizedChild',header:'FullyImmunizedChild'}
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
