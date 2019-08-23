import {Component, Input, OnChanges, OnInit, SimpleChange} from '@angular/core';
import {NdwhPatientsExtractService} from '../../../services/ndwh-patients-extract.service';
import {NdwhPatientArtService} from '../../../services/ndwh-patient-art.service';
import {NdwhPatientBaselineService} from '../../../services/ndwh-patient-baseline.service';
import {NdwhPatientLaboratoryService} from '../../../services/ndwh-patient-laboratory.service';
import {NdwhPatientPharmacyService} from '../../../services/ndwh-patient-pharmacy.service';
import {NdwhPatientStatusService} from '../../../services/ndwh-patient-status.service';
import {NdwhPatientVisitService} from '../../../services/ndwh-patient-visit.service';
import {NdwhPatientAdverseEventService} from '../../../services/ndwh-patient-adverse-event.service';
import {Subscription} from 'rxjs/Subscription';
import { Message } from 'primeng/api';

import { HtsClientsService } from '../../../services/hts-clients.service';
import { HtsClientTestsService } from '../../../services/hts-client-tests.service';
import { HtsClientsLinkageService } from '../../../services/hts-clients-linkage.service';
import { HtsTestKitsService } from '../../../services/hts-test-kits.service';
import { HtsClientTracingService } from '../../../services/hts-client-tracing.service';
import { HtsPartnerTracingService } from '../../../services/hts-partner-tracing.service';
import { HtsPartnerNotificationServicesService } from '../../../services/hts-partner-notification-services.service';


@Component({
    selector: 'liveapp-hts-invalid',
    templateUrl: './hts-invalid.component.html',
    styleUrls: ['./hts-invalid.component.scss']
})
export class HtsInvalidComponent implements OnInit, OnChanges {

    @Input() extract: string;
    private htsClientsService: HtsClientsService;
    private htsClientTestsService: HtsClientTestsService;
    private htsClientsLinkageService: HtsClientsLinkageService;
    private htsTestKitsService: HtsTestKitsService;
    private htsClientTracingService: HtsClientTracingService;
    private htsPartnerTracingService: HtsPartnerTracingService;
    private htsPartnerNotificationServicesService: HtsPartnerNotificationServicesService;


    public invalidExtracts: any[] = [];
    public cols: any[];
    public getInvalid$: Subscription;
    public errorMessage: Message[];
    public otherMessage: Message[];

    constructor(clientsExtractsService: HtsClientsService, clientTestsService: HtsClientTestsService,
        clientsLinkageService: HtsClientsLinkageService, testKitsService: HtsTestKitsService,
        clientTracingService: HtsClientTracingService, patientTracingService: HtsPartnerTracingService,
        partnerNotificationServicesService: HtsPartnerNotificationServicesService) {
        this.htsClientsService = clientsExtractsService;
        this.htsClientTestsService = clientTestsService;
        this.htsClientsLinkageService = clientsLinkageService;
        this.htsTestKitsService = testKitsService;
        this.htsClientTracingService = clientTracingService;
        this.htsPartnerTracingService = patientTracingService;
        this.htsPartnerNotificationServicesService = partnerNotificationServicesService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.cols = [];
        this.invalidExtracts = [];
        this.getColumns();
        this.getInvalidExtracts();
    }

    ngOnInit() {
        this.getClientsColumns();
        this.getClientsExtracts();
    }

    public getInvalidExtracts(): void {
        if (this.extract === 'Hts Clients') {
            this.getClientsExtracts();
        }
        if (this.extract === 'Hts Client Tests') {
            this.getClientTestsExtracts();
        }
        if (this.extract === 'Hts Client Linkage') {
            this.getClientLinkageExtracts();
        }
        if (this.extract === 'Hts Test Kits') {
            this.getTestKitsExtracts();
        }
        if (this.extract === 'Hts Client Tracing') {
            this.getClientTracingExtracts();
        }
        if (this.extract === 'Hts Partner Tracing') {
            this.getPartnerTracingExtracts();
        }
        if (this.extract === 'Hts Partner Notification Services') {
            this.getPartnerNotificationServicesExtracts();
        }
    }

    private getColumns(): void {
        if (this.extract === 'Hts Clients') {
            this.getClientsColumns();
        }
        if (this.extract === 'Hts Client Tests') {
            this.getClientTestsColumns();
        }
        if (this.extract === 'Hts Client Linkage') {
            this.getClientLinkagesColumns();
        }
        if (this.extract === 'Hts Test Kits') {
            this.getTestKitsColumns();
        }
        if (this.extract === 'Hts Client Tracing') {
            this.getClientTracingColumns();
        }
        if (this.extract === 'Hts Partner Tracing') {
            this.getPartnerTracingColumns();
        }
        if (this.extract === 'Hts Partner Notification Services') {
            this.getPartnerNotificationServicesColumns();
        }
    }

    private getClientsExtracts(): void {
        this.getInvalid$ = this.htsClientsService.loadValidations().subscribe(
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
            }
        );
    }

    private getClientTestsExtracts(): void {
        this.getInvalid$ = this.htsClientTestsService.loadValidations().subscribe(
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
            }
        );
    }

    private getClientLinkageExtracts(): void {
        this.getInvalid$ = this.htsClientsLinkageService.loadValidations().subscribe(
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
            }
        );
    }

    private getTestKitsExtracts(): void {
        this.getInvalid$ = this.htsTestKitsService.loadValidations().subscribe(
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
            }
        );
    }

    private getPartnerTracingExtracts(): void {
        this.getInvalid$ = this.htsPartnerTracingService.loadValidations().subscribe(
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
            }
        );
    }

    private getClientTracingExtracts(): void {
        this.getInvalid$ = this.htsClientTracingService.loadValidations().subscribe(
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
            }
        );
    }

    private getPartnerNotificationServicesExtracts(): void {
        this.getInvalid$ = this.htsPartnerNotificationServicesService.loadValidations().subscribe(
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

            { field: 'FinalTestResult', header: 'Final Test Result'},
            { field: 'PatientPK', header: 'PatientPK' }, 
            {field: 'RecordId', header: 'Record Id'},
            {field: 'Id', header: 'Id'},
            {field: 'Extract', header: 'Extract'},
            {field: 'Field', header: 'Field'},
            {field: 'Type', header: 'Type'}
        ];
    }

    private getClientLinkagesColumns(): void {
        this.cols = [
            { field: 'Summary', header: 'Summary' },
            { field: 'HtsNumber', header: 'Hts Number' },
            { field: 'PhoneTracingDate', header: 'Phone Tracing Date'},
            { field: 'PhysicalTracingDate', header: 'Physical Tracing Date'},
            { field: 'TracingOutcome', header: 'Tracing Outcome'},
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

            { field: 'PatientPK', header: 'PatientPK' },
            { field: 'RecordId', header: 'Record Id' },
            { field: 'Id', header: 'Id' },
            { field: 'Extract', header: 'Extract' },
            { field: 'Field', header: 'Field' },
            { field: 'Type', header: 'Type' }
        ];
    }
}
