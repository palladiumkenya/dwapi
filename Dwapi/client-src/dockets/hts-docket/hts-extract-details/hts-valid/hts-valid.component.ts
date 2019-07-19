import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {HtsClientService} from '../../../services/hts-client.service';
import {HtsClientLinkageService} from '../../../services/hts-client-linkage.service';
import {HtsClientPartnerService} from '../../../services/hts-client-partner.service';
import {PageModel} from '../../../models/page-model';

@Component({
    selector: 'liveapp-hts-valid',
    templateUrl: './hts-valid.component.html',
    styleUrls: ['./hts-valid.component.scss']
})
export class HtsValidComponent implements OnInit, OnDestroy {
    private htsClientService: HtsClientService;
    private htsClientLinkageService: HtsClientLinkageService;
    private htsClientPartnerService: HtsClientPartnerService;

    public validExtracts: any[] = [];
    public recordCount = 0;
    public cols: any[];
    public errorMessage: Message[];
    public otherMessage: Message[];
    public getValid$: Subscription;
    public getValidCount$: Subscription;
    public pageModel: PageModel;
    public initialRows: number = 20;
    private exName: string;
    public loadingData = false;

    constructor(htsClientService: HtsClientService, htsClientLinkageService: HtsClientLinkageService,
                htsClientPartnerService: HtsClientPartnerService) {
        this.htsClientService = htsClientService;
        this.htsClientLinkageService = htsClientLinkageService;
        this.htsClientPartnerService = htsClientPartnerService;
    }

    get extract(): string {
        return this.exName;
    }

    @Input()
    set extract(extract: string) {
        if (extract) {
            this.exName = extract;
            console.log(extract);
            this.cols = [];
            this.validExtracts = [];
            this.pageModel = {
                page: 1,
                pageSize: this.initialRows
            };
            this.getColumns();
            this.getValidExtracts();
        }
    }

    ngOnInit() {
        this.exName = 'HTS Client Extracts';
        this.pageModel = {
            page: 1,
            pageSize: this.initialRows
        };
        this.getClientColumns();
        this.getValidHtsClients();
    }

    public getValidExtracts(): void {
        console.log('loading>', this.extract);
        if (this.extract === 'HTS Client Extracts') {
            this.getValidHtsClients();
        }
        if (this.extract === 'HTS Linkage Extracts') {
            this.getValidLinkages();
        }
        if (this.extract === 'HTS Partner Extracts') {
            this.getValidPartners();
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

    private getValidHtsClients(): void {
        this.loadingData = true;
        this.getValidCount$ = this.htsClientService.loadValidCount().subscribe(
            p => {
                this.recordCount = p;
                this.getValidHtsClientExtracts();
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

    private getValidHtsClientExtracts(): void {
        this.loadingData = true;
        this.getValid$ = this.htsClientService.loadValid(this.pageModel).subscribe(
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

    private getValidLinkages(): void {
        this.loadingData = true;
        this.getValid$ = this.htsClientLinkageService.loadValidCount().subscribe(
            p => {
                this.recordCount = p;
                this.getValidLinkageExtracts();
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

    private getValidLinkageExtracts(): void {
        this.getValid$ = this.htsClientLinkageService.loadValid(this.pageModel).subscribe(
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

    private getValidPartners(): void {
        this.loadingData = true;
        this.getValid$ = this.htsClientPartnerService.loadValidCount().subscribe(
            p => {
                this.recordCount = p;
                this.getValidPartnerExtracts();
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

    private getValidPartnerExtracts(): void {
        this.getValid$ = this.htsClientPartnerService.loadValid(this.pageModel).subscribe(
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
            { field: 'htsNumber', header: 'htsNumber' },
            { field: 'visitDate', header: 'visitDate' }, 
            {field: 'dob', header: 'dob'},
            {field: 'gender', header: 'gender'},
            {field: 'maritalStatus', header: 'maritalStatus'},
            {field: 'keyPop', header: 'keyPop'},
            {field: 'testedBefore', header: 'testedBefore'},
            {field: 'monthsLastTested', header: 'monthsLastTested'},
            {field: 'clientTestedAs', header: 'clientTestedAs'},
            { field: 'strategyHTS', header: 'EntryPoint'},
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
            { field: 'patientPk', header: 'patientPk' },
            { field: 'encounterId', header: 'encounterId' },
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
            { field: 'htsNumber', header: 'htsNumber' },
            { field: 'phoneTracingDate', header: 'phoneTracingDate' },
            {field: 'physicalTracingDate', header: 'physicalTracingDate'},
            {field: 'tracingOutcome', header: 'tracingOutcome'},
            {field: 'cccNumber', header: 'cccNumber'},
            {field: 'enrolledFacilityName', header: 'enrolledFacilityName'},
            {field: 'referralDate', header: 'referralDate'},
            {field: 'dateEnrolled', header: 'dateEnrolled'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientPk', header: 'patientPk'},
            
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
            { field: 'htsNumber', header: 'htsNumber' },
            { field: 'partnerPatientPk', header: 'partnerPatientPk' },
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
