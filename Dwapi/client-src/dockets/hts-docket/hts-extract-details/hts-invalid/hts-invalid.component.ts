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
export class HtsInvalidComponent implements OnInit , OnChanges {

    @Input() extract: string;
    private _patientExtractsService: HtsClientService;
    private _patientArtService: HtsClientLinkageService;
    private _patientBaselineService: HtsClientPartnerService;

    public invalidExtracts: any[] = [];
    public cols: any[];
    public getInvalid$: Subscription;
    public errorMessage: Message[];
    public otherMessage: Message[];

    constructor(patientExtractsService: HtsClientService, patientArtService: HtsClientLinkageService,
                patientBaselineService: HtsClientPartnerService) {
        this._patientExtractsService = patientExtractsService;
        this._patientArtService = patientArtService;
        this._patientBaselineService = patientBaselineService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.cols = [];
        this.invalidExtracts = [];
        this.getColumns();
        this.getInvalidExtracts();
    }

    ngOnInit() {
        this.getPatientColumns();
        this.getInvalidPatientExtracts();
    }

    public getInvalidExtracts(): void {
        if (this.extract === 'HTS Client Extracts') {
            this.getInvalidPatientExtracts();
        }
        if (this.extract === 'HTS Client Extracts') {
            this.getInvalidPatientArtExtracts();
        }
        if (this.extract === 'HTS Client Partner') {
            this.getInvalidPatientBaselineExtracts();
        }
    }

    private getColumns(): void {
        if (this.extract === 'HTS Client Extracts') {
            this.getPatientColumns();
        }
        if (this.extract === 'HTS Client Extracts') {
            this.getPatientArtColumns();
        }
        if (this.extract === 'HTS Client Partner') {
            this.getPatientBaselineColumns();
        }
    }

    private getInvalidPatientExtracts(): void {
        this.getInvalid$ = this._patientExtractsService.loadValidations().subscribe(
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

    private getInvalidPatientArtExtracts(): void {
        this.getInvalid$ = this._patientArtService.loadValidations().subscribe(
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

    private getInvalidPatientBaselineExtracts(): void {
        this.getInvalid$ = this._patientBaselineService.loadValidations().subscribe(
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

    private getPatientColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' },
            { field: 'summary', header: 'Summary' },
            { field: 'dateGenerated', header: 'DateGenerated' },
            { field: 'patientPK', header: 'PatientPK' },
            { field: 'facilityId', header: 'FacilityId' },
            { field: 'patientID', header: 'PatientID' },
            { field: 'siteCode', header: 'SiteCode' },
            { field: 'facilityName', header: 'FacilityName' }
        ];
    }

    private getPatientArtColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' },
            { field: 'summary', header: 'Summary' },
            { field: 'dateGenerated', header: 'Date Generated' },
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'facilityName', header: 'Facility Name' },
            { field: 'dob', header: 'DOB' },
            { field: 'gender', header: 'Gender' },
            { field: 'patientSource', header: 'Patient Source' },
            { field: 'registrationDate', header: 'Registration Date' },
            { field: 'ageLastVisit', header: 'Age Last Visit' },
            { field: 'previousARTStartDate', header: 'Previous ART Start Date' },
            { field: 'previousARTRegimen', header: 'Previous ART Regimen' },
            { field: 'startARTAtThisFacility', header: 'Start ART At This Facility' },
            { field: 'startARTDate', header: 'Start ART Date' },
            { field: 'startRegimen', header: 'Start Regimen' },
            { field: 'startRegimenLine', header: 'Start Regimen Line' },
            { field: 'lastARTDate', header: 'Last ART Date' },
            { field: 'lastRegimen', header: 'Last Regimen' },
            { field: 'lastRegimenLine', header: 'Last Regimen Line' },
            { field: 'lastVisit', header: 'Last Visit' },
            { field: 'exitReason', header: 'Exit Reason' },
            { field: 'exitDate', header: 'Exit Date' }
        ];
    }

    private getPatientBaselineColumns(): void {
        this.cols = [
            { field: 'summary', header: 'Summary' },
            { field: 'extract', header: 'Extract' },
            { field: 'field', header: 'Field' },
            { field: 'type', header: 'Type' },
            { field: 'summary', header: 'Summary' },
            { field: 'dateGenerated', header: 'Date Generated' },
            { field: 'patientPK', header: 'Patient PK' },
            { field: 'facilityId', header: 'Facility Id' },
            { field: 'patientID', header: 'Patient ID' },
            { field: 'siteCode', header: 'Site Code' },
            { field: 'recordId', header: 'Record Id' },
            { field: 'bCD4', header: 'bCD4' },
            { field: 'bCD4Date', header: 'bCD4 Date' },
            { field: 'bWAB', header: 'bWAB' },
            { field: 'bWABDate', header: 'bWAB Date' },
            { field: 'bWHO', header: 'bWHO' },
            { field: 'bWHODate', header: 'bWHO Date' },
            { field: 'eWAB', header: 'eWAB' },
            { field: 'eWABDate', header: 'eWAB Date' },
            { field: 'eCD4', header: 'eCD4' },
            { field: 'eCD4Date', header: 'eCD4 Date' },
            { field: 'eWHO', header: 'eWHO' },
            { field: 'eWHODate', header: 'eWHO Date' },
            { field: 'lastWHO', header: 'lastWHO' },
            { field: 'lastWHODate', header: 'last WHO Date' },
            { field: 'lastCD4', header: 'lastCD4' },
            { field: 'lastCD4Date', header: 'last CD4 Date' },
            { field: 'lastWAB', header: 'lastWAB' },
            { field: 'lastWABDate', header: 'last WAB Date' },
            { field: 'm12CD4', header: 'm12CD4' },
            { field: 'm12CD4Date', header: 'm12CD4 Date' },
            { field: 'm6CD4', header: 'm6CD4' },
            { field: 'm6CD4Date', header: 'm6CD4 Date' }
        ];
    }
}
