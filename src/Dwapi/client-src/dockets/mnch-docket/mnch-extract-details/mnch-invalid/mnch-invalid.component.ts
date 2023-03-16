import {Component, Input, OnChanges, OnInit, SimpleChange} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import { Message } from 'primeng/api';
import {MnchSummaryService} from "../../../services/mnch-summary.service";



@Component({
    selector: 'liveapp-mnch-invalid',
    templateUrl: './mnch-invalid.component.html',
    styleUrls: ['./mnch-invalid.component.scss']
})
export class MnchInvalidComponent implements OnInit, OnChanges {

    private exName: string;
    public invalidExtracts: any[] = [];
    public cols: any[];
    public getInvalid$: Subscription;
    public errorMessage: Message[];
    public otherMessage: Message[];
    private _preventLoad = true;
    public loadingData = false;

    constructor(private summaryService: MnchSummaryService) {
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
        this.getPatientMnchExtractColumns();
    }

    public getInvalidExtracts(): void {
        if (this.extract === 'Pnc Visit') {this.getSummaryInvalidExtracts('PncVisit');return;}
        if (this.extract === 'Mother Baby Pair') {this.getSummaryInvalidExtracts('MotherBabyPair');return;}
        if (this.extract === 'Mnch Patient') {this.getSummaryInvalidExtracts('PatientMnch');return;}
        if (this.extract === 'Mnch Labs') {this.getSummaryInvalidExtracts('MnchLab');return;}
        if (this.extract === 'Mnch Enrolment') {this.getSummaryInvalidExtracts('MnchEnrolment');return;}
        if (this.extract === 'Mnch Art') {this.getSummaryInvalidExtracts('MnchArt');return;}
        if (this.extract === 'Mat Visit') {this.getSummaryInvalidExtracts('MatVisit');return;}
        if (this.extract === 'Hei') {this.getSummaryInvalidExtracts('Hei');return;}
        if (this.extract === 'Cwc Visit') {this.getSummaryInvalidExtracts('CwcVisit');return;}
        if (this.extract === 'Cwc Enrolment') {this.getSummaryInvalidExtracts('CwcEnrolment');return;}
        if (this.extract === 'Anc Visit') {this.getSummaryInvalidExtracts('AncVisit');return;}
        if (this.extract === 'Mnch Immunization') {this.getSummaryInvalidExtracts('MnchImmunization');return;}
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
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'patientHeiId', header: 'Patient Hei Id'}

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

}
