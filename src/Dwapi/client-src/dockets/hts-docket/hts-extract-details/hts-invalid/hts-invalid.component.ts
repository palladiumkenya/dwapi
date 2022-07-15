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
import {HtsEligibilityScreeningService} from "../../../services/hts-eligibility-screening.service";


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
    private htsEligibilityScreeningService: HtsEligibilityScreeningService;


    public invalidExtracts: any[] = [];
    public cols: any[];
    public getInvalid$: Subscription;
    public errorMessage: Message[];
    public otherMessage: Message[];

    constructor(clientsExtractsService: HtsClientsService, clientTestsService: HtsClientTestsService,
        clientsLinkageService: HtsClientsLinkageService, testKitsService: HtsTestKitsService,
        clientTracingService: HtsClientTracingService, patientTracingService: HtsPartnerTracingService,
        partnerNotificationServicesService: HtsPartnerNotificationServicesService,
                eligibilityScreeningService: HtsEligibilityScreeningService) {
        this.htsClientsService = clientsExtractsService;
        this.htsClientTestsService = clientTestsService;
        this.htsClientsLinkageService = clientsLinkageService;
        this.htsTestKitsService = testKitsService;
        this.htsClientTracingService = clientTracingService;
        this.htsPartnerTracingService = patientTracingService;
        this.htsPartnerNotificationServicesService = partnerNotificationServicesService;
        this.htsEligibilityScreeningService = eligibilityScreeningService;

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
        if (this.extract === 'Hts Eligibility Screening') {
            this.getHtsEligibilityExtracts();
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
        if (this.extract === 'Hts Eligibility Screening') {
            this.getHtsEligibilityExtractsColumns();
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

    private getHtsEligibilityExtracts(): void {
        this.getInvalid$ = this.htsEligibilityScreeningService.loadValidations().subscribe(
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
            { field: 'summary', header: 'Summary' },
            { field: 'patientPK', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'doB', header: 'Dob' },
            { field: 'gender', header: 'Gender' },
            { field: 'maritalStatus', header: 'Marital Status' },
            { field: 'populationType', header: 'Population Type' },
            { field: 'keyPopulationType', header: 'Key Population Type' },
            { field: 'county', header: 'County' },
            { field: 'subCounty', header: 'SubCounty' },
            { field: 'ward', header: 'Ward' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' }
        ];
    }

    private getClientTestsColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'patientPK', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'testDate', header: 'Test Date' },
            { field: 'everTestedForHiv', header: 'Ever Tested For Hiv' },
            { field: 'monthsSinceLastTest', header: 'Months Since Last Test' },
            { field: 'clientTestedAs', header: 'Client Tested As' },
            { field: 'entryPoint', header: 'Entry Point' },
            { field: 'testStrategy', header: 'Test Strategy' },
            { field: 'testResult1', header: 'Test Result 1' },
            { field: 'testResult2', header: 'Test Result 2' },
            { field: 'finalTestResult', header: 'Final Test Result' },
            {field: 'extract', header: 'Extract'},
            {field: 'field', header: 'Field'},
            {field: 'type', header: 'Type'}
        ];
    }

    private getClientLinkagesColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
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
            { field: 'summary', header: 'Summary' },
            { field: 'patientPK', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'testKitName1', header: 'Test Kit Name 1' },
            { field: 'testKitLotNumber1', header: 'Test Kit Lot Number 1' },
            { field: 'testKitExpiry1', header: 'Test Kit Expiry 1' },
            { field: 'testResult1', header: 'Test Result 1' },
            { field: 'testKitName2', header: 'Test Kit Name 2' },
            { field: 'testKitLotNumber2', header: 'Test Kit Lot Number 2' },
            { field: 'testKitExpiry2', header: 'Test Kit Expiry 2' },
            { field: 'testResult2', header: 'Test Result 2' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' }
        ];
    }

    private getClientTracingColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'patientPK', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'tracingType', header: 'Tracing Type' },
            { field: 'tracingDate', header: 'Tracing Date' },
            { field: 'tracingOutcome', header: 'Tracing Outcome' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' }
        ];
    }

    private getPartnerTracingColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'patientPK', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'traceType', header: 'Trace Type' },
            { field: 'traceDate', header: 'Trace Date' },
            { field: 'traceOutcome', header: 'Trace Outcome' },
            { field: 'bookingDate', header: 'Booking Date' },
            { field: 'PartnerPersonId', header: 'Partner Person Id' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' }
        ];
    }

    private getPartnerNotificationServicesColumns(): void {
        this.cols = [
            { field: 'Summary', header: 'Summary' },
            { field: 'patientPK', header: 'PatientPK' },
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

    private getHtsEligibilityExtractsColumns(): void {
        this.cols = [
            { field: 'Summary', header: 'Summary' },
            { field: 'patientPk', header: 'PatientPK' },
            { field: 'htsNumber', header: 'Hts Number' },
            { field: 'patientType', header: 'Patient Type' },
            { field: 'visitID', header: 'Visit ID' },
            { field: 'visitDate', header: 'Visit Date' },
            { field: 'relationshipWithContact', header: 'Relationship With Contact' },
            { field: 'isHealthWorker', header: 'Is Health Worker' },
            { field: 'testedHIVBefore', header: 'Tested HIV Before' },
            { field: 'partnerHivStatus', header: 'Partner Hiv Status' },
            { field: 'sexuallyActive', header: 'Sexually Active' },
        ];
    }
}
