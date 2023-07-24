import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
/*import {HtsClientService} from '../../../services/hts-client.service';
import {HtsClientLinkageService} from '../../../services/hts-client-linkage.service';
import {HtsClientPartnerService} from '../../../services/hts-client-partner.service';*/
import {PageModel} from '../../../models/page-model';
import { HtsClientsService } from '../../../services/hts-clients.service';
import { HtsClientTestsService } from '../../../services/hts-client-tests.service';
import { HtsClientTracingService } from '../../../services/hts-client-tracing.service';
import { HtsPartnerTracingService } from '../../../services/hts-partner-tracing.service';
import { HtsPartnerNotificationServicesService } from '../../../services/hts-partner-notification-services.service';
import { HtsTestKitsService } from '../../../services/hts-test-kits.service';
import { HtsClientsLinkageService } from '../../../services/hts-clients-linkage.service';
import {HtsEligibilityScreeningService} from "../../../services/hts-eligibility-screening.service";

@Component({
    selector: 'liveapp-hts-valid',
    templateUrl: './hts-valid.component.html',
    styleUrls: ['./hts-valid.component.scss']
})
export class HtsValidComponent implements OnInit, OnDestroy {
    //private htsClientService: HtsClientService;

    private htsClientsService: HtsClientsService;
    private htsClientTestsService: HtsClientTestsService;
    private htsClientTracingService: HtsClientTracingService;
    private htsPartnerTracingService: HtsPartnerTracingService;
    private htsPartnerNotificationServicesService: HtsPartnerNotificationServicesService;
    private htsTestKitsService: HtsTestKitsService;
    private htsClientsLinkageService: HtsClientsLinkageService;
    private htsEligibilityScreeningService: HtsEligibilityScreeningService;


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

    constructor(htsClientsService: HtsClientsService, htsClientTestsService: HtsClientTestsService, htsClientTracingService: HtsClientTracingService,
                htsPartnerTracingService: HtsPartnerTracingService, htsPartnerNotificationServicesService: HtsPartnerNotificationServicesService,
                htsTestKitsService: HtsTestKitsService, htsClientLinkageService: HtsClientsLinkageService,
                eligibilityScreeningService: HtsEligibilityScreeningService) {
        this.htsClientsService = htsClientsService;
        this.htsClientTestsService = htsClientTestsService;
        this.htsClientTracingService = htsClientTracingService;
        this.htsPartnerTracingService = htsPartnerTracingService;
        this.htsPartnerNotificationServicesService = htsPartnerNotificationServicesService;
        this.htsTestKitsService = htsTestKitsService;
        this.htsClientsLinkageService = htsClientLinkageService;
        this.htsEligibilityScreeningService = eligibilityScreeningService;
    }

    get extract(): string {
        return this.exName;
    }

    @Input()
    set extract(extract: string) {
        if (extract) {
            this.exName = extract;
            //console.log(extract);
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
        this.exName = 'Hts Clients';
        this.pageModel = {
            page: 1,
            pageSize: this.initialRows
        };
        this.getClientsColumns();
        this.getValidHtsClients();
    }

    public getValidExtracts(): void {
        //console.log('loading>', this.extract);
        if (this.extract === 'Hts Clients') {
            this.getValidHtsClients();
        }
        if (this.extract === 'Hts Client Linkage') {
            this.getValidLinkages();
        }
        if (this.extract === 'Hts Client Tracing') {
            this.getValidClientTracing();
        }
        if (this.extract === 'Hts Partner Tracing') {
            this.getValidPartnerTracing();
        }
        if (this.extract === 'Hts Test Kits') {
            this.getValidTestKits();
        }
        if (this.extract === 'Hts Client Tests') {
            this.getValidClientTests();
        }
        if (this.extract === 'Hts Partner Notification Services') {
            this.getValidPartnerNotificationServices();
        }
        if (this.extract === 'Hts Eligibility Screening') {
            this.getHtsEligibilityExtracts();
        }
    }

    private getColumns(): void {
        if (this.extract === 'Hts Clients') {
            this.getClientsColumns();
        }
        if (this.extract === 'Hts Client Linkage') {
            this.getClientLinkagesColumns();
        }
        if (this.extract === 'Hts Client Tracing') {
            this.getClientTracingColumns();
        }
        if (this.extract === 'Hts Partner Tracing') {
            this.getPartnerTracingColumns();
        }
        if (this.extract === 'Hts Test Kits') {
            this.getTestKitsColumns();
        }
        if (this.extract === 'Hts Client Tests') {
            this.getClientTestsColumns();
        }
        if (this.extract === 'Hts Partner Notification Services') {
            this.getPartnerNotificationServicesColumns();
        }
        if (this.extract === 'Hts Eligibility Screening') {
            this.getHtsEligibilityExtractsColumns();
        }
    }

    private getValidHtsClients(): void {
        this.loadingData = true;
        this.getValidCount$ = this.htsClientsService.loadValidCount().subscribe(
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
        this.getValid$ = this.htsClientsService.loadValid(this.pageModel).subscribe(
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
        this.getValid$ = this.htsClientsLinkageService.loadValidCount().subscribe(
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
        this.getValid$ = this.htsClientsLinkageService.loadValid(this.pageModel).subscribe(
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

    private getValidPartnerTracing(): void {
        this.loadingData = true;
        this.getValid$ = this.htsPartnerTracingService.loadValidCount().subscribe(
            p => {
                this.recordCount = p;
                this.getValidPartnerTracingExtracts();
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

    private getValidPartnerTracingExtracts(): void {
        this.getValid$ = this.htsPartnerTracingService.loadValid(this.pageModel).subscribe(
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

    private getValidClientTracing(): void {
        this.loadingData = true;
        this.getValid$ = this.htsClientTracingService.loadValidCount().subscribe(
            p => {
                this.recordCount = p;
                this.getValidClientTracingExtracts();
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

    private getValidClientTracingExtracts(): void {
        this.getValid$ = this.htsClientTracingService.loadValid(this.pageModel).subscribe(
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

    private getValidClientTests(): void {
        this.loadingData = true;
        this.getValid$ = this.htsClientTestsService.loadValidCount().subscribe(
            p => {
                this.recordCount = p;
                this.getValidClientTestsExtracts();
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

    private getValidClientTestsExtracts(): void {
        this.getValid$ = this.htsClientTestsService.loadValid(this.pageModel).subscribe(
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

    private getValidTestKits(): void {
        this.loadingData = true;
        this.getValid$ = this.htsTestKitsService.loadValidCount().subscribe(
            p => {
                this.recordCount = p;
                this.getValidTestKitsExtracts();
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

    private getValidTestKitsExtracts(): void {
        this.getValid$ = this.htsTestKitsService.loadValid(this.pageModel).subscribe(
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

    private getValidPartnerNotificationServices(): void {
        this.loadingData = true;
        this.getValid$ = this.htsPartnerNotificationServicesService.loadValidCount().subscribe(
            p => {
                this.recordCount = p;
                this.getValidPartnerNotificationServicesExtracts();
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

    private getValidPartnerNotificationServicesExtracts(): void {
        this.getValid$ = this.htsPartnerNotificationServicesService.loadValid(this.pageModel).subscribe(
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

    private getHtsEligibilityExtracts(): void {
        this.getValid$ = this.htsEligibilityScreeningService.loadValid(this.pageModel).subscribe(
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


    private getClientsColumns(): void {
        this.cols = [
            //{ field: 'summary', header: 'Summary' },
            { field: 'patientPk', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'doB', header: 'Dob' },
            { field: 'gender', header: 'Gender' },
            { field: 'maritalStatus', header: 'Marital Status' },
            { field: 'htsRecencyId', header: 'Hts Recency Id' },
            { field: 'occupation', header: 'Occupation' },
            { field: 'priorityPopulationType', header: 'Priority Population Type' },
            { field: 'populationType', header: 'Population Type' },
            { field: 'keyPopulationType', header: 'Key Population Type' },
            { field: 'county', header: 'County' },
            { field: 'subCounty', header: 'SubCounty' },
            { field: 'ward', header: 'Ward' },



        ];
    }

    private getClientTestsColumns(): void {
        this.cols = [
            //{ field: 'Summary', header: 'Summary' },
            { field: 'patientPk', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'testDate', header: 'Test Date' },
            { field: 'referredForServices', header: 'ReferredForServices' },
            { field: 'referredServices', header: 'ReferredServices' },
            { field: 'otherReferredServices', header: 'OtherReferredServices' },
            { field: 'setting', header: 'Setting' },
            { field: 'approach', header: 'Approach' },
            { field: 'htsRiskCategory', header: 'HTS Risk Category' },
            { field: 'htsRiskScore', header: 'HTS Risk Score' },
            { field: 'everTestedForHiv', header: 'Ever Tested For Hiv' },
            { field: 'monthsSinceLastTest', header: 'Months Since Last Test' },
            { field: 'clientTestedAs', header: 'Client Tested As' },
            { field: 'entryPoint', header: 'Entry Point' },
            { field: 'testStrategy', header: 'Test Strategy' },
            { field: 'testResult1', header: 'Test Result 1' },
            { field: 'testResult2', header: 'Test Result 2' },
            { field: 'finalTestResult', header: 'Final Test Result' },

        ];
    }

    private getClientLinkagesColumns(): void {
        this.cols = [
            //{ field: 'Summary', header: 'Summary' },
            { field: 'patientPk', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'datePrefferedToBeEnrolled', header: 'Date Preffered To Be Enrolled' },
            { field: 'facilityReferredTo', header: 'Facility Referred To' },
            { field: 'handedOverTo', header: 'Handed Over To' },
            { field: 'handedOverToCadre', header: 'Handed Over To Cadre' },
            { field: 'reportedCCCNumber', header: 'Reported Ccc Number' },
            { field: 'reportedStartARTDate', header: 'Reported ART Start Date' },
            { field: 'enrolledFacilityName', header: 'Enrolled Facility Name' },
            { field: 'referralDate', header: 'Referral Date' },
            { field: 'dateEnrolled', header: 'Date Enrolled' },
        ];
    }

    private getTestKitsColumns(): void {
        this.cols = [
            //{ field: 'Summary', header: 'Summary' },
            { field: 'patientPk', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'syphilisResult', header: 'Syphilis Result' },
            { field: 'testKitName1', header: 'Test Kit Name 1' },
            { field: 'testKitLotNumber1', header: 'Test Kit Lot Number 1' },
            { field: 'testKitExpiry1', header: 'Test Kit Expiry 1' },
            { field: 'testResult1', header: 'Test Result 1' },
            { field: 'testKitName2', header: 'Test Kit Name 2' },
            { field: 'testKitLotNumber2', header: 'Test Kit Lot Number 2' },
            { field: 'testKitExpiry2', header: 'Test Kit Expiry 2' },
            { field: 'testResult2', header: 'Test Result 2' },
        ];
    }

    private getClientTracingColumns(): void {
        this.cols = [
            //{ field: 'Summary', header: 'Summary' },
            { field: 'patientPk', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'tracingType', header: 'Tracing Type' },
            { field: 'tracingDate', header: 'Tracing Date' },
            { field: 'tracingOutcome', header: 'Tracing Outcome' },
        ];
    }

    private getPartnerTracingColumns(): void {
        this.cols = [
            //{ field: 'Summary', header: 'Summary' },
            { field: 'patientPk', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'traceType', header: 'Trace Type' },
            { field: 'traceDate', header: 'Trace Date' },
            { field: 'traceOutcome', header: 'Trace Outcome' },
            { field: 'bookingDate', header: 'Booking Date' },
            { field: 'partnerPersonId', header: 'Partner Person Id' },
        ];
    }

    private getPartnerNotificationServicesColumns(): void {
        this.cols = [
            //{ field: 'Summary', header: 'Summary' },
            { field: 'patientPk', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'partnerPersonID', header: 'Partner Person ID' },
            { field: 'age', header: 'Age' },
            { field: 'sex', header: 'Sex' },
            { field: 'relationsipToIndexClient', header: 'Relationsip To Index Client' },
            { field: 'screenedForIpv', header: 'Screened For IPV' },
            { field: 'ipvScreeningOutcome', header: 'IPV Screening Outcome' },
            { field: 'currentlyLivingWithIndexClient', header: 'currently Living With Index Client' },
            { field: 'pnsConsent', header: 'PNS Consent' },

        ];
    }

    private getHtsEligibilityExtractsColumns(): void {
        this.cols = [
            //{ field: 'Summary', header: 'Summary' },
            { field: 'patientPk', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'patientType', header: 'Patient Type' },
            { field: 'visitID', header: 'Visit ID' },
            { field: 'visitDate', header: 'Visit Date' },
            { field: 'htsRiskScore', header: 'HTS Risk Score' },
            { field: 'disability', header: 'Disability' },
            { field: 'disabilityType', header: 'DisabilityType' },
            { field: 'htsStrategy', header: 'HTSStrategy' },
            { field: 'htsEntryPoint', header: 'HTSEntryPoint' },
            { field: 'hivRiskCategory', header: 'HIVRiskCategory' },
            { field: 'reasonRefferredForTesting', header: 'ReasonRefferredForTesting' },
            { field: 'reasonNotReffered', header: 'ReasonNotReffered' },
            { field: 'relationshipWithContact', header: 'Relationship With Contact' },
            { field: 'isHealthWorker', header: 'Is Health Worker' },
            { field: 'testedHIVBefore', header: 'Tested HIV Before' },
            { field: 'partnerHivStatus', header: 'Partner Hiv Status' },
            { field: 'sexuallyActive', header: 'Sexually Active' },

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
