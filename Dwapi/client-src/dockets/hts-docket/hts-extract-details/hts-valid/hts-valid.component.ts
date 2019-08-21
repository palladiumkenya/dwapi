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

    constructor(htsClientsService: HtsClientsService, htsClientTestsService: HtsClientTestsService, htsClientTracingService: HtsClientTracingService,
                htsPartnerTracingService: HtsPartnerTracingService, htsPartnerNotificationServicesService: HtsPartnerNotificationServicesService,
                htsTestKitsService: HtsTestKitsService, htsClientLinkageService: HtsClientsLinkageService) {
        this.htsClientsService = htsClientsService;
        this.htsClientTestsService = htsClientTestsService;
        this.htsClientTracingService = htsClientTracingService;
        this.htsPartnerTracingService = htsPartnerTracingService;
        this.htsPartnerNotificationServicesService = htsPartnerNotificationServicesService;
        this.htsTestKitsService = htsTestKitsService;
        this.htsClientsLinkageService = htsClientLinkageService;
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
        this.getClientsColumns();
        this.getValidHtsClients();
    }

    public getValidExtracts(): void {
        console.log('loading>', this.extract);
        if (this.extract === 'HTS Client Extracts') {
            this.getValidHtsClients();
        }
        if (this.extract === 'HTS Client Linkages Extracts') {
            this.getValidLinkages();
        }
        if (this.extract === 'HTS Client Tracing Extracts') {
            this.getValidClientTracing();
        }
        if (this.extract === 'HTS Partner Tracing Extracts') {
            this.getValidPartnerTracing();
        }
        if (this.extract === 'HTS Test Kits Extracts') {
            this.getValidTestKits();
        }
        if (this.extract === 'HTS Client Tests Extracts') {
            this.getValidClientTests();
        }
        if (this.extract === 'HTS Partner Notification Services Extracts') {
            this.getValidPartnerTracingExtracts();
        }
    }

    private getColumns(): void {
        if (this.extract === 'HTS Client Extracts') {
            this.getClientsColumns();
        }
        if (this.extract === 'HTS Client Linkages Extracts') {
            this.getClientLinkagesColumns();
        }
        if (this.extract === 'HTS Client Tracing Extracts') {
            this.getClientTracingColumns();
        }
        if (this.extract === 'HTS Partner Tracing Extracts') {
            this.getPartnerTracingColumns();
        }
        if (this.extract === 'HTS Test Kits Extracts') {
            this.getTestKitsColumns();
        }
        if (this.extract === 'HTS Client Tests Extracts') {
            this.getClientTestsColumns();
        }
        if (this.extract === 'HTS Partner Notification Services Extracts') {
            this.getPartnerNotificationServicesColumns();
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


    private getValidPartnerNotificationServicesService(): void {
        this.loadingData = true;
        this.getValid$ = this.htsPartnerNotificationServicesService.loadValidCount().subscribe(
            p => {
                this.recordCount = p;
                this.getValiPartnerNotificationServicesServiceExtracts();
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

    private getValiPartnerNotificationServicesServiceExtracts(): void {
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

    private getClientsColumns(): void {
        this.cols = [
            { field: 'Summary', header: 'Summary' },
            { field: 'HtsNumber', header: 'Hts Number' },
            { field: 'Dob', header: 'Dob' },
            { field: 'Gender', header: 'Gender' },
            { field: 'MaritalStatus', header: 'Marital Status' },
            { field: 'PopulationType', header: 'Population Type' },
            { field: 'KeyPopulationType', header: 'Key Population Type' },
            { field: 'County', header: 'County' },
            { field: 'SubCounty', header: 'SubCounty' },
            { field: 'Ward', header: 'Ward' },

            { field: 'PatientPK', header: 'PatientPK' },
            { field: 'RecordId', header: 'Record Id' },
            { field: 'Id', header: 'Id' },
            { field: 'Extract', header: 'Extract' },
            { field: 'Field', header: 'Field' },
            { field: 'Type', header: 'Type' }
        ];
    }

    private getClientTestsColumns(): void {
        this.cols = [
            { field: 'Summary', header: 'Summary' },
            { field: 'HtsNumber', header: 'Hts Number' },
            { field: 'TestDate', header: 'Test Date' },
            { field: 'EverTestedForHiv', header: 'Ever Tested For Hiv' },
            { field: 'MonthsSinceLastTest', header: 'Months Since Last Test' },
            { field: 'ClientTestedAs', header: 'Client Tested As' },
            { field: 'EntryPoint', header: 'Entry Point' },
            { field: 'TestStrategy', header: 'Test Strategy' },
            { field: 'TestResult1', header: 'Test Result 1' },
            { field: 'TestResult2', header: 'Test Result 2' },

            { field: 'FinalTestResult', header: 'Final Test Result' },
            { field: 'PatientPK', header: 'PatientPK' },
            { field: 'RecordId', header: 'Record Id' },
            { field: 'Id', header: 'Id' },
            { field: 'Extract', header: 'Extract' },
            { field: 'Field', header: 'Field' },
            { field: 'Type', header: 'Type' }
        ];
    }

    private getClientLinkagesColumns(): void {
        this.cols = [
            { field: 'Summary', header: 'Summary' },
            { field: 'HtsNumber', header: 'Hts Number' },
            { field: 'PhoneTracingDate', header: 'Phone Tracing Date' },
            { field: 'PhysicalTracingDate', header: 'Physical Tracing Date' },
            { field: 'TracingOutcome', header: 'Tracing Outcome' },
            { field: 'CccNumber', header: 'Ccc Number' },
            { field: 'EnrolledFacilityName', header: 'Enrolled Facility Name' },
            { field: 'ReferralDate', header: 'Referral Date' },
            { field: 'DateEnrolled', header: 'Date Enrolled' },

            { field: 'PatientPK', header: 'PatientPK' },
            { field: 'RecordId', header: 'Record Id' },
            { field: 'Id', header: 'Id' },
            { field: 'Extract', header: 'Extract' },
            { field: 'Field', header: 'Field' },
            { field: 'Type', header: 'Type' }
        ];
    }

    private getTestKitsColumns(): void {
        this.cols = [
            { field: 'Summary', header: 'Summary' },
            { field: 'TestKitName1', header: 'Test Kit Name 1' },
            { field: 'TestKitLotNumber1', header: 'Test Kit Lot Number 1' },
            { field: 'TestKitExpiry1', header: 'Test Kit Expiry 1' },
            { field: 'TestResult1', header: 'Test Result 1' },
            { field: 'TestKitName2', header: 'Test Kit Name 2' },
            { field: 'TestKitLotNumber2', header: 'Test Kit Lot Number 2' },
            { field: 'TestKitExpiry2', header: 'Test Kit Expiry 2' },
            { field: 'TestResult2', header: 'Test Result 2' },

            { field: 'PatientPK', header: 'PatientPK' },
            { field: 'RecordId', header: 'Record Id' },
            { field: 'Id', header: 'Id' },
            { field: 'Extract', header: 'Extract' },
            { field: 'Field', header: 'Field' },
            { field: 'Type', header: 'Type' }
        ];
    }

    private getClientTracingColumns(): void {
        this.cols = [
            { field: 'Summary', header: 'Summary' },
            { field: 'HtsNumber', header: 'Hts Number' },
            { field: 'TracingType', header: 'Tracing Type' },
            { field: 'TracingDate', header: 'Tracing Date' },
            { field: 'TracingOutcome', header: 'Tracing Outcome' },

            { field: 'PatientPK', header: 'PatientPK' },
            { field: 'RecordId', header: 'Record Id' },
            { field: 'Id', header: 'Id' },
            { field: 'Extract', header: 'Extract' },
            { field: 'Field', header: 'Field' },
            { field: 'Type', header: 'Type' }
        ];
    }

    private getPartnerTracingColumns(): void {
        this.cols = [
            { field: 'Summary', header: 'Summary' },
            { field: 'HtsNumber', header: 'Hts Number' },
            { field: 'TraceType', header: 'Trace Type' },
            { field: 'TraceDate', header: 'Trace Date' },
            { field: 'TraceOutcome', header: 'Trace Outcome' },
            { field: 'BookingDate', header: 'Booking Date' },

            { field: 'PatientPK', header: 'PatientPK' },
            { field: 'RecordId', header: 'Record Id' },
            { field: 'Id', header: 'Id' },
            { field: 'Extract', header: 'Extract' },
            { field: 'Field', header: 'Field' },
            { field: 'Type', header: 'Type' }
        ];
    }

    private getPartnerNotificationServicesColumns(): void {
        this.cols = [
            { field: 'Summary', header: 'Summary' },
            { field: 'HtsNumber', header: 'Hts Number' },
            { field: 'TestDate', header: 'Test Date' },
            { field: 'EntryPoint', header: 'Entry Point' },
            { field: 'TestStrategy', header: 'Test Strategy' },
            { field: 'TestResult1', header: 'Test Result 1' },
            { field: 'TestResult2', header: 'Test Result 2' },
            { field: 'FinalTestResult', header: 'Final Test Result' },
            { field: 'ClientSelfTested', header: 'Client Self Tested' },
            { field: 'FinalTestResult', header: 'Final Test Result' },

            { field: 'PatientPK', header: 'PatientPK' },
            { field: 'RecordId', header: 'Record Id' },
            { field: 'Id', header: 'Id' },
            { field: 'Extract', header: 'Extract' },
            { field: 'Field', header: 'Field' },
            { field: 'Type', header: 'Type' }
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
