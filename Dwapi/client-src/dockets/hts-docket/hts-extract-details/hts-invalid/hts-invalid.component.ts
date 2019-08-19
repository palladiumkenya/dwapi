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
import {Message} from 'primeng/api';
import {HtsClientService} from '../../../services/hts-client.service';
import {HtsClientLinkageService} from '../../../services/hts-client-linkage.service';
import {HtsClientPartnerService} from '../../../services/hts-client-partner.service';

@Component({
    selector: 'liveapp-hts-invalid',
    templateUrl: './hts-invalid.component.html',
    styleUrls: ['./hts-invalid.component.scss']
})
export class HtsInvalidComponent implements OnInit, OnChanges {

    @Input() extract: string;
    private htsClientService: HtsClientService;
    private htsClientLinkageService: HtsClientLinkageService;
    private htsClientPartnerService: HtsClientPartnerService;

    public invalidExtracts: any[] = [];
    public cols: any[];
    public getInvalid$: Subscription;
    public errorMessage: Message[];
    public otherMessage: Message[];

    constructor(patientExtractsService: HtsClientService, patientArtService: HtsClientLinkageService,
                patientBaselineService: HtsClientPartnerService) {
        this.htsClientService = patientExtractsService;
        this.htsClientLinkageService = patientArtService;
        this.htsClientPartnerService = patientBaselineService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.cols = [];
        this.invalidExtracts = [];
        this.getColumns();
        this.getInvalidExtracts();
    }

    ngOnInit() {
        this.getClientColumns();
        this.getClientExtracts();
    }

    public getInvalidExtracts(): void {
        if (this.extract === 'HTS Client Extracts') {
            this.getClientExtracts();
        }
        if (this.extract === 'HTS Linkage Extracts') {
            this.getLinkageExtracts();
        }
        if (this.extract === 'HTS Partner Extracts') {
            this.getPartnerExtracts();
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

    private getClientExtracts(): void {
        this.getInvalid$ = this.htsClientService.loadValidations().subscribe(
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

    private getLinkageExtracts(): void {
        this.getInvalid$ = this.htsClientLinkageService.loadValidations().subscribe(
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

    private getPartnerExtracts(): void {
        this.getInvalid$ = this.htsClientPartnerService.loadValidations().subscribe(
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

    private getClientColumns(): void {
        this.cols = [
            { field: 'summary', header: 'summary' },
            { field: 'htsNumber', header: 'htsNumber' },
            { field: 'visitDate', header: 'visitDate' },
            { field: 'dob', header: 'dob' },
            { field: 'gender', header: 'gender' },
            { field: 'maritalStatus', header: 'maritalStatus' },
            { field: 'keyPop', header: 'keyPop' },
            { field: 'testedBefore', header: 'testedBefore' },
            { field: 'monthsLastTested', header: 'monthsLastTested' },
            { field: 'clientTestedAs', header: 'clientTestedAs' },
            { field: 'strategyHTS', header: 'EntryPoint' },
            { field: 'testKitName1', header: 'testKitName1' },
            { field: 'testKitLotNumber1', header: 'testKitLotNumber1' },
            { field: 'testKitExpiryDate1', header: 'testKitExpiryDate1' },
            { field: 'testResultsHTS1', header: 'testResultsHTS1' },
            { field: 'testKitName2', header: 'testKitName2' },
            { field: 'testKitLotNumber2', header: 'testKitLotNumber2' },
            { field: 'testKitExpiryDate2', header: 'testKitExpiryDate2' },
            { field: 'testResultsHTS2', header: 'testResultsHTS2' },
            { field: 'finalResultHTS', header: 'finalResultHTS' },
            { field: 'finalResultsGiven', header: 'finalResultsGiven' },
            { field: 'tbScreeningHTS', header: 'tbScreeningHTS' },
            { field: 'clientSelfTested', header: 'clientSelfTested' },
            { field: 'coupleDiscordant', header: 'coupleDiscordant' },
            {field: 'dateGenerated', header: 'dateGenerated'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientPK', header: 'patientPK'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'recordId', header: 'recordId'},
            {field: 'id', header: 'id'},
            {field: 'encounterId', header: 'encounterId'},
            {field: 'testType', header: 'testType'},
            {field: 'extract', header: 'extract'},
            {field: 'field', header: 'field'},
            {field: 'type', header: 'type'}
        ];
    }

    private getLinkageColumns(): void {
        this.cols = [
            { field: 'summary', header: 'summary' },
            { field: 'htsNumber', header: 'htsNumber' },
            { field: 'phoneTracingDate', header: 'phoneTracingDate' },
            { field: 'physicalTracingDate', header: 'physicalTracingDate' },
            { field: 'tracingOutcome', header: 'tracingOutcome' },
            { field: 'cccNumber', header: 'cccNumber' },
            { field: 'referralDate', header: 'referralDate' },
            { field: 'dateEnrolled', header: 'dateEnrolled' },
            { field: 'enrolledFacilityName', header: 'enrolledFacilityName' },
            {field: 'dateGenerated', header: 'dateGenerated'},
            {field: 'siteCode', header: 'siteCode'},
            { field: 'patientPK', header: 'patientPK' },
            {field: 'facilityName', header: 'facilityName'},
            {field: 'recordId', header: 'recordId'},
            {field: 'id', header: 'id'},
            {field: 'extract', header: 'extract'},
            {field: 'field', header: 'field'},
            {field: 'type', header: 'type'}
        ];
    }

    private getPartnerColumns(): void {
        this.cols = [
            { field: 'summary', header: 'summary' },
            { field: 'htsNumber', header: 'htsNumber' },
            {field: 'dateGenerated', header: 'dateGenerated'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientPK', header: 'patientPK'},
            { field: 'partnerPatientPk', header: 'partnerPatientPk' },
            { field: 'partnerPersonId', header: 'partnerPersonId' },
            { field: 'relationshipToIndexClient', header: 'relationshipToIndexClient' },
            { field: 'screenedForIpv', header: 'screenedForIpv' },
            { field: 'ipvScreeningOutcome', header: 'ipvScreeningOutcome' },
            { field: 'currentlyLivingWithIndexClient', header: 'currentlyLivingWithIndexClient' },
            { field: 'knowledgeOfHivStatus', header: 'knowledgeOfHivStatus' },
            { field: 'pnsApproach', header: 'pnsApproach' },
            { field: 'trace1Outcome', header: 'trace1Outcome' },
            { field: 'trace1Type', header: 'trace1Type' },
            { field: 'trace1Date', header: 'trace1Date' },
            { field: 'trace2Outcome', header: 'trace2Outcome' },
            { field: 'trace2Type', header: 'trace2Type' },
            { field: 'trace2Date', header: 'trace2Date' },
            { field: 'trace3Outcome', header: 'trace3Outcome' },
            { field: 'trace3Type', header: 'trace3Type' },
            { field: 'trace3Date', header: 'trace3Date' },
            { field: 'pnsConsent', header: 'pnsConsent' },
            { field: 'linked', header: 'linked' },
            { field: 'linkDateLinkedToCare', header: 'linkDateLinkedToCare' },
            { field: 'cccNumber', header: 'cccNumber' },
            { field: 'age', header: 'age' },
            { field: 'sex', header: 'sex' },
            {field: 'facilityName', header: 'facilityName'},
            {field: 'recordId', header: 'recordId'},
            {field: 'id', header: 'id'},
            {field: 'extract', header: 'extract'},
            {field: 'field', header: 'field'},
            {field: 'type', header: 'type'}
        ];
    }
}
