import {Component, Input, OnChanges, OnInit, SimpleChange} from '@angular/core';
import {NdwhPatientsExtractService} from '../../../services/ndwh-patients-extract.service';
import {NdwhPatientArtService} from '../../../services/ndwh-patient-art.service';
import {NdwhPatientBaselineService} from '../../../services/ndwh-patient-baseline.service';
import {NdwhPatientLaboratoryService} from '../../../services/ndwh-patient-laboratory.service';
import {NdwhPatientPharmacyService} from '../../../services/ndwh-patient-pharmacy.service';
import {NdwhPatientStatusService} from '../../../services/ndwh-patient-status.service';
import {NdwhPatientVisitService} from '../../../services/ndwh-patient-visit.service';
import {NdwhPatientAdverseEventService} from '../../../services/ndwh-patient-adverse-event.service';
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
export class HtsValidComponent implements OnInit , OnChanges {

    @Input() extract: string;
    private _patientExtractsService: HtsClientService;
    private _patientArtService: HtsClientLinkageService;
    private _patientBaselineService: HtsClientPartnerService;

    public validExtracts: any[] = [];
    public cols: any[];
    public errorMessage: Message[];
    public otherMessage: Message[];
    public getValid$: Subscription;

    constructor(patientExtractsService: HtsClientService, patientArtService: HtsClientLinkageService,
                patientBaselineService: HtsClientPartnerService) {
        this._patientExtractsService = patientExtractsService;
        this._patientArtService = patientArtService;
        this._patientBaselineService = patientBaselineService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.cols = [];
        this.validExtracts = [];
        this.getColumns();
        this.getValidExtracts();
    }

    ngOnInit() {
        this.getPatientColumns();
        this.getValidPatientExtracts();
    }

    public getValidExtracts(): void {
        if (this.extract === 'HTS Client Extracts') {
            this.getValidPatientExtracts();
        }
        if (this.extract === 'HTS Client Extracts') {
            this.getValidPatientArtExtracts();
        }
        if (this.extract === 'HTS Client Partner') {
            this.getValidPatientBaselineExtracts();
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

    private getValidPatientExtracts(): void {
        this.getValid$ = this._patientExtractsService.loadValid().subscribe(
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

    private getValidPatientArtExtracts(): void {
        this.getValid$ = this._patientArtService.loadValid().subscribe(
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

    private getValidPatientBaselineExtracts(): void {
        this.getValid$ = this._patientBaselineService.loadValid().subscribe(
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

    private getPatientColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'gender', header: 'Gender'},
            {field: 'dob', header: 'DOB'},
            {field: 'registrationDate', header: 'Registration Date'},
            {field: 'registrationAtCCC', header: 'Registration At CCC'},
            {field: 'registrationAtPMTCT', header: 'Registration At PMTCT'},
            {
                field: 'registrationAtTBClinic',
                header: 'Registration At TB Clinic'
            },
            {field: 'patientSource', header: 'Patient Source'},
            {field: 'region', header: 'Region'},
            {field: 'district', header: 'District'},
            {field: 'village', header: 'Village'},
            {field: 'contactRelation', header: 'Contact Relation'},
            {field: 'lastVisit', header: 'Last Visit'},
            {field: 'maritalStatus', header: 'Marital Status'},
            {field: 'educationLevel', header: 'Education Level'},
            {
                field: 'dateConfirmedHIVPositive',
                header: 'Date Confirmed HIV Positive'
            },
            {field: 'previousARTExposure', header: 'Previous ART Exposure'},
            {
                field: 'previousARTStartDate',
                header: 'Previous ART Start Date'
            },
            {field: 'statusAtCCC', header: 'Status At CCC'},
            {field: 'statusAtPMTCT', header: 'Status At PMTCT'},
            {field: 'statusAtTBClinic', header: 'Status At TB Clinic'},
            {field: 'satelliteName', header: 'Satellite Name'}
        ];
    }

    private getPatientArtColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'dOB', header: 'DOB'},
            {field: 'ageEnrollment', header: 'Enrollment Age'},
            {field: 'ageARTStart', header: 'Age ART Start'},
            {field: 'ageLastVisit', header: 'Age Last Visit'},
            {field: 'registrationDate', header: 'Registration Date'},
            {field: 'patientSource', header: 'Patient Source'},
            {field: 'gender', header: 'Gender'},
            {field: 'startARTDate', header: 'Start ART Date'},
            {field: 'previousARTStartDate', header: 'Previous ART Start Date'},
            {field: 'previousARTRegimen', header: 'Previous ART Regimen'},
            {field: 'startARTAtThisFacility', header: 'Start ART At This Facility'},
            {field: 'startRegimen', header: 'Start Regimen'},
            {field: 'startRegimenLine', header: 'Start Regimen Line'},
            {field: 'lastARTDate', header: 'Last ART Date'},
            {field: 'lastRegimen', header: 'Last Regimen'},
            {field: 'lastRegimenLine', header: 'Last Regimen Line'},
            {field: 'duration', header: 'Duration'},
            {field: 'expectedReturn', header: 'Expected Return'},
            {field: 'provider', header: 'Provider'},
            {field: 'lastVisit', header: 'Last Visit'},
            {field: 'exitReason', header: 'Exit Reason'},
            {field: 'exitDate', header: 'Exit Date'},
            {field: 'dateExtracted', header: 'Date Extracted'}
        ];
    }

    private getPatientBaselineColumns(): void {
        this.cols = [

            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'bCD4', header: 'Baseline CD4'},
            {field: 'bCD4Date', header: 'Baseline CD4 Date'},
            {field: 'bWAB', header: 'Baseline WAB'},
            {field: 'bWABDate', header: 'Baseline WAB Date'},
            {field: 'bWHO', header: 'Baseline WHO'},
            {field: 'bWHODate', header: 'Baseline WHO Date'},
            {field: 'eWAB', header: 'Enrollment WAB'},
            {field: 'eWABDate', header: 'Enrollment WAB Date'},
            {field: 'eCD4', header: 'Enrollment CD4'},
            {field: 'eCD4Date', header: 'Enrollment CD4 Date'},
            {field: 'eWHO', header: 'Enrollment WHO'},
            {field: 'eWHODate', header: 'Enrollment WHO Date'},
            {field: 'lastWHO', header: 'Last WHO'},
            {field: 'lastWHODate', header: 'Last WHO Date'},
            {field: 'lastCD4', header: 'Last CD4'},
            {field: 'lastCD4Date', header: 'Last CD4 Date'},
            {field: 'lastWAB', header: 'Last WAB'},
            {field: 'lastWABDate', header: 'Last WAB Date'},
            {field: 'm12CD4', header: 'm12CD4'},
            {field: 'm12CD4Date', header: 'm12CD4Date'},
            {field: 'm6CD4', header: 'm6CD4'},
            {field: 'm6CD4Date', header: 'm6CD4Date'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'checkError', header: 'Check Error'}
        ];
    }
}
