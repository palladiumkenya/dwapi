import {Component, Input, OnChanges, OnInit, SimpleChange} from '@angular/core';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {HtsClientService} from '../../../services/hts-client.service';
import {HtsClientLinkageService} from '../../../services/hts-client-linkage.service';
import {HtsClientPartnerService} from '../../../services/hts-client-partner.service';

@Component({
    selector: 'liveapp-hts-valid',
    templateUrl: './hts-valid.component.html',
    styleUrls: ['./hts-valid.component.scss']
})
export class HtsValidComponent implements OnInit, OnChanges {

    @Input() extract: string;
    private htsClientService: HtsClientService;
    private htsClientLinkageService: HtsClientLinkageService;
    private htsClientPartnerService: HtsClientPartnerService;

    public validExtracts: any[] = [];
    public cols: any[];
    public errorMessage: Message[];
    public otherMessage: Message[];
    public getValid$: Subscription;

    constructor(htsClientService: HtsClientService, htsClientLinkageService: HtsClientLinkageService,
                htsClientPartnerService: HtsClientPartnerService) {
        this.htsClientService = htsClientService;
        this.htsClientLinkageService = htsClientLinkageService;
        this.htsClientPartnerService = htsClientPartnerService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.cols = [];
        this.validExtracts = [];
        this.getColumns();
        this.getValidExtracts();
    }

    ngOnInit() {
        this.getClientColumns();
        this.getValidHtsClientExtracts();
    }

    public getValidExtracts(): void {
        if (this.extract === 'HTS Client Extracts') {
            this.getValidHtsClientExtracts();
        }
        if (this.extract === 'HTS Linkage Extracts') {
            this.getValidLinkageExtracts();
        }
        if (this.extract === 'HTS Partner Extracts') {
            this.getValidPartnerExtracts();
        }
    }

    private getColumns(): void {
        if (this.extract === 'HTS Client Extracts') {
            this.getClientColumns();
        }
        if (this.extract === 'HTS Linkage Extracts') {
            this.getLinkageColumns();
        }
        if (this.extract === 'HTS Partner Extracts') {
            this.getPartnerColumns();
        }
    }

    private getValidHtsClientExtracts(): void {
        this.getValid$ = this.htsClientService.loadValid().subscribe(
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

    private getValidLinkageExtracts(): void {
        this.getValid$ = this.htsClientLinkageService.loadValid().subscribe(
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

    private getValidPartnerExtracts(): void {
        this.getValid$ = this.htsClientPartnerService.loadValid().subscribe(
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

    private getClientColumns(): void {
        this.cols = [
            {field: 'encounterId', header: 'encounterId'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'dob', header: 'dob'},
            {field: 'gender', header: 'gender'},
            {field: 'maritalStatus', header: 'maritalStatus'},
            {field: 'keyPop', header: 'keyPop'},
            {field: 'testedBefore', header: 'testedBefore'},
            {field: 'monthsLastTested', header: 'monthsLastTested'},
            {field: 'clientTestedAs', header: 'clientTestedAs'},
            {field: 'strategyHTS', header: 'strategyHTS'},
            {field: 'testKitName1', header: 'testKitName1'},
            {field: 'testKitLotNumber1', header: 'testKitLotNumber1'},
            {field: 'testKitExpiryDate1', header: 'testKitExpiryDate1'},
            {field: 'testResultsHTS1', header: 'testResultsHTS1'},
            {field: 'testKitName2', header: 'testKitName2'},
            {field: 'testKitLotNumber2', header: 'testKitLotNumber2'},
            {field: 'testKitExpiryDate2', header: 'testKitExpiryDate2'},
            {field: 'testResultsHTS2', header: 'testResultsHTS2'},
            {field: 'finalResultHTS', header: 'finalResultHTS'},
            {field: 'finalResultsGiven', header: 'finalResultsGiven'},
            {field: 'tbScreeningHTS', header: 'tbScreeningHTS'},
            {field: 'clientSelfTested', header: 'clientSelfTested'},
            {field: 'coupleDiscordant', header: 'coupleDiscordant'},
            {field: 'testType', header: 'testType'},
            {field: 'keyPopulationType', header: 'keyPopulationType'},
            {field: 'populationType', header: 'populationType'},
            {field: 'patientDisabled', header: 'patientDisabled'},
            {field: 'disabilityType', header: 'disabilityType'},
            {field: 'patientConsented', header: 'patientConsented'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientPk', header: 'patientPk'},
            {field: 'htsNumber', header: 'htsNumber'},
            {field: 'emr', header: 'emr'},
            {field: 'project', header: 'project'},
            {field: 'processed', header: 'processed'},
            {field: 'queueId', header: 'queueId'},
            {field: 'status', header: 'status'},
            {field: 'statusDate', header: 'statusDate'},
            {field: 'dateExtracted', header: 'dateExtracted'},
            {field: 'isSent', header: 'isSent'},
            {field: 'id', header: 'id'}
        ];
    }

    private getLinkageColumns(): void {
        this.cols = [
            {field: 'phoneTracingDate', header: 'phoneTracingDate'},
            {field: 'physicalTracingDate', header: 'physicalTracingDate'},
            {field: 'tracingOutcome', header: 'tracingOutcome'},
            {field: 'cccNumber', header: 'cccNumber'},
            {field: 'enrolledFacilityName', header: 'enrolledFacilityName'},
            {field: 'referralDate', header: 'referralDate'},
            {field: 'dateEnrolled', header: 'dateEnrolled'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientPk', header: 'patientPk'},
            {field: 'htsNumber', header: 'htsNumber'},
            {field: 'emr', header: 'emr'},
            {field: 'project', header: 'project'},
            {field: 'processed', header: 'processed'},
            {field: 'queueId', header: 'queueId'},
            {field: 'status', header: 'status'},
            {field: 'statusDate', header: 'statusDate'},
            {field: 'dateExtracted', header: 'dateExtracted'},
            {field: 'isSent', header: 'isSent'},
            {field: 'id', header: 'id'}
        ];
    }

    private getPartnerColumns(): void {
        this.cols = [
            {field: 'partnerPatientPk', header: 'partnerPatientPk'},
            {field: 'partnerPersonId', header: 'partnerPersonId'},
            {field: 'relationshipToIndexClient', header: 'relationshipToIndexClient'},
            {field: 'screenedForIpv', header: 'screenedForIpv'},
            {field: 'ipvScreeningOutcome', header: 'ipvScreeningOutcome'},
            {field: 'currentlyLivingWithIndexClient', header: 'currentlyLivingWithIndexClient'},
            {field: 'knowledgeOfHivStatus', header: 'knowledgeOfHivStatus'},
            {field: 'pnsApproach', header: 'pnsApproach'},
            {field: 'trace1Outcome', header: 'trace1Outcome'},
            {field: 'trace1Type', header: 'trace1Type'},
            {field: 'trace1Date', header: 'trace1Date'},
            {field: 'trace2Outcome', header: 'trace2Outcome'},
            {field: 'trace2Type', header: 'trace2Type'},
            {field: 'trace2Date', header: 'trace2Date'},
            {field: 'trace3Outcome', header: 'trace3Outcome'},
            {field: 'trace3Type', header: 'trace3Type'},
            {field: 'trace3Date', header: 'trace3Date'},
            {field: 'pnsConsent', header: 'pnsConsent'},
            {field: 'linked', header: 'linked'},
            {field: 'linkDateLinkedToCare', header: 'linkDateLinkedToCare'},
            {field: 'cccNumber', header: 'cccNumber'},
            {field: 'age', header: 'age'},
            {field: 'sex', header: 'sex'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientPk', header: 'patientPk'},
            {field: 'htsNumber', header: 'htsNumber'},
            {field: 'emr', header: 'emr'},
            {field: 'project', header: 'project'},
            {field: 'processed', header: 'processed'},
            {field: 'queueId', header: 'queueId'},
            {field: 'status', header: 'status'},
            {field: 'statusDate', header: 'statusDate'},
            {field: 'dateExtracted', header: 'dateExtracted'},
            {field: 'isSent', header: 'isSent'},
            {field: 'id', header: 'id'}
        ];
    }
}
