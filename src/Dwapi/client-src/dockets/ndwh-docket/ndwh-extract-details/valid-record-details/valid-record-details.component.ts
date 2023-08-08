import {ChangeDetectionStrategy, Component, Input, OnDestroy, OnInit} from '@angular/core';
import {NdwhPatientsExtractService} from '../../../services/ndwh-patients-extract.service';
import {NdwhPatientArtService} from '../../../services/ndwh-patient-art.service';
import {NdwhPatientBaselineService} from '../../../services/ndwh-patient-baseline.service';
import {NdwhPatientLaboratoryService} from '../../../services/ndwh-patient-laboratory.service';
import {NdwhPatientPharmacyService} from '../../../services/ndwh-patient-pharmacy.service';
import {NdwhPatientStatusService} from '../../../services/ndwh-patient-status.service';
import {NdwhPatientVisitService} from '../../../services/ndwh-patient-visit.service';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {NdwhPatientAdverseEventService} from '../../../services/ndwh-patient-adverse-event.service';
import {PageModel} from '../../../models/page-model';
import {NdwhSummaryService} from '../../../services/ndwh-summary.service';

@Component({
    selector: 'liveapp-valid-record-details',
    templateUrl: './valid-record-details.component.html',
    styleUrls: ['./valid-record-details.component.scss']
})
export class ValidRecordDetailsComponent implements OnInit, OnDestroy {
    private _patientExtractsService: NdwhPatientsExtractService;
    private _patientArtService: NdwhPatientArtService;
    private _patientBaselineService: NdwhPatientBaselineService;
    private _patientLabService: NdwhPatientLaboratoryService;
    private _patientPharmacyService: NdwhPatientPharmacyService;
    private _patientStatusService: NdwhPatientStatusService;
    private _patientVisitService: NdwhPatientVisitService;
    private _patientAdverseEventService: NdwhPatientAdverseEventService;
    public validExtracts: any[] = [];
    public cols: any[];
    public errorMessage: Message[];
    public otherMessage: Message[];
    public getValid$: Subscription;
    public getValidCount$: Subscription;
    private exName: string;
    public pageModel: PageModel;
    public initialRows: number = 10;
    public loadingData = false;
    public recordCount = 0;
    private _preventLoad = true;

    constructor(patientExtractsService: NdwhPatientsExtractService, patientArtService: NdwhPatientArtService,
                patientBaselineService: NdwhPatientBaselineService, patientLabService: NdwhPatientLaboratoryService,
                patientPharmacyService: NdwhPatientPharmacyService, patientStatusService: NdwhPatientStatusService,
                patientVisitService: NdwhPatientVisitService, patientAdverseEventService: NdwhPatientAdverseEventService,
                private summaryService: NdwhSummaryService) {
        this._patientExtractsService = patientExtractsService;
        this._patientArtService = patientArtService;
        this._patientBaselineService = patientBaselineService;
        this._patientLabService = patientLabService;
        this._patientPharmacyService = patientPharmacyService;
        this._patientStatusService = patientStatusService;
        this._patientVisitService = patientVisitService;
        this._patientAdverseEventService = patientAdverseEventService;

        this.pageModel = {
            page: 1,
            pageSize: this.initialRows
        };

    }

    get preventLoad(): boolean {
        return this._preventLoad;
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
            this._preventLoad = false;
            this.exName = extract;
            this.cols = [];
            this.validExtracts = [];

            this.pageModel = {
                page: 1,
                pageSize: this.initialRows
            };

            this.getColumns();
            this.getValidExtracts();
        } else {
            this._preventLoad = true;
        }
    }

    ngOnInit() {
        this.exName = 'Patient Adverse Events';
        this.getPatientColumns();
    }

    public getValidExtracts(): void {
        if (this.extract === 'All Patients') {
            this.getPatients();
            return;
        }
        if (this.extract === 'ART Patients') {
            this.getPatientsART();
            return;
        }
        if (this.extract === 'Patient Baselines') {
            this.getPatientsBases();
            return;
        }
        if (this.extract === 'Patient Labs') {
            this.getPatientsLabs();
            return;
        }
        if (this.extract === 'Patient Pharmacy') {
            this.getPatientsPharms();
            return;
        }
        if (this.extract === 'Patient Status') {
            this.getPatientsStats();
            return;
        }
        if (this.extract === 'Patient Visit') {
            this.getPatientsVisits();
            return;
        }
        if (this.extract === 'Patient Adverse Events') {
            this.getPatientsAdverse();
            return;
        }

        if (this.extract === 'Allergies Chronic Illness') {
            this.getSummaryExtracts('AllergiesChronicIllness');
            return;
        }
        if (this.extract === 'Contact Listing') {
            this.getSummaryExtracts('ContactListing');
            return;
        }
        if (this.extract === 'Depression Screening') {
            this.getSummaryExtracts('DepressionScreening');
            return;
        }
        if (this.extract === 'Drug and Alcohol Screening') {
            this.getSummaryExtracts('DrugAlcoholScreening');
            return;
        }
        if (this.extract === 'Enhanced Adherence Counselling') {
            this.getSummaryExtracts('EnhancedAdherenceCounselling');
            return;
        }
        if (this.extract === 'GBV Screening') {
            this.getSummaryExtracts('GbvScreening');
            return;
        }
        if (this.extract === 'IPT') {
            this.getSummaryExtracts('Ipt');
            return;
        }
        if (this.extract === 'OTZ') {
            this.getSummaryExtracts('Otz');
            return;
        }
        if (this.extract === 'OVC') {
            this.getSummaryExtracts('Ovc');
            return;
        }

        if (this.extract === 'Covid') {
            this.getSummaryExtracts('Covid');
            return;
        }
        if (this.extract === 'Defaulter Tracing') {
            this.getSummaryExtracts('DefaulterTracing');
            return;
        }
        if (this.extract === 'Cervical Cancer Screening') {
            this.getSummaryExtracts('CervicalCancerScreening');
            return;
        }
        if (this.extract === 'IIT Risk Scores') {
            this.getSummaryExtracts('IITRiskScores');
            return;
        }
    }

    private getColumns(): void {
        if (this.extract === 'All Patients') {
            this.getPatientColumns();
            return;
        }
        if (this.extract === 'ART Patients') {
            this.getPatientArtColumns();
            return;
        }
        if (this.extract === 'Patient Baselines') {
            this.getPatientBaselineColumns();
            return;
        }
        if (this.extract === 'Patient Labs') {
            this.getPatientLaboratoryColumns();
            return;
        }
        if (this.extract === 'Patient Pharmacy') {
            this.getPatientPharmacyColumns();
            return;
        }
        if (this.extract === 'Patient Status') {
            this.getPatientStatusColumns();
            return;
        }
        if (this.extract === 'Patient Visit') {
            this.getPatientVisitColumns();
            return;
        }
        if (this.extract === 'Patient Adverse Events') {
            this.getPatientAdverseEventColumns();
            return;
        }

        if (this.extract === 'Allergies Chronic Illness') {
            this.getAllergiesChronicIllnessColumns();
            return;
        }
        if (this.extract === 'Contact Listing') {
            this.getContactListingColumns();
            return;
        }
        if (this.extract === 'Depression Screening') {
            this.getDepressionScreeningColumns();
            return;
        }
        if (this.extract === 'Drug and Alcohol Screening') {
            this.getDrugAlcoholScreeningColumns();
            return;
        }
        if (this.extract === 'Enhanced Adherence Counselling') {
            this.getEnhancedAdherenceCounsellingColumns();
            return;
        }
        if (this.extract === 'GBV Screening') {
            this.getGbvScreeningColumns();
            return;
        }
        if (this.extract === 'IPT') {
            this.getIptColumns();
            return;
        }
        if (this.extract === 'OTZ') {
            this.getOtzColumns();
            return;
        }
        if (this.extract === 'OVC') {
            this.getOvcColumns();
            return;
        }
        if (this.extract === 'Covid') {
            this.getCovidColumns();
            return;
        }
        if (this.extract === 'Defaulter Tracing') {
            this.getDefaulterTracingColumns();
            return;
        }
        if (this.extract === 'Cervical Cancer Screening') {
            this.getCervicalCancerScreeningColumns();
            return;
        }
        if (this.extract === 'IIT Risk Scores') {
            this.getIITRiskScoresColumns();
            return;
        }
    }

    private getPatients(): void {
        this.loadingData = true;
        this.getValidCount$ = this._patientExtractsService.loadValidCount()
            .subscribe(
                p => {
                    this.recordCount = p;
                    this.getValidPatientExtracts();
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

    private getPatientsART(): void {
        this.loadingData = true;
        this.getValidCount$ = this._patientArtService.loadValidCount()
            .subscribe(
                p => {
                    this.recordCount = p;
                    this.getValidPatientArtExtracts();
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

    private getPatientsBases(): void {
        this.loadingData = true;

        this.getValidCount$ = this._patientBaselineService.loadValidCount()
            .subscribe(
                p => {
                    this.recordCount = p;
                    this.getValidPatientBaselineExtracts();
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

    private getPatientsLabs(): void {
        this.loadingData = true;
        this.getValidCount$ = this._patientLabService.loadValidCount()
            .subscribe(
                p => {
                    this.recordCount = p;
                    this.getValidPatientLabExtracts();
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

    private getPatientsPharms(): void {
        this.loadingData = true;
        this.getValidCount$ = this._patientPharmacyService.loadValidCount()
            .subscribe(
                p => {
                    this.recordCount = p;
                    this.getValidPatientPharmacyExtracts();
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

    private getPatientsStats(): void {
        this.loadingData = true;
        this.getValidCount$ = this._patientStatusService.loadValidCount()
            .subscribe(
                p => {
                    this.recordCount = p;
                    this.getValidPatientStatusExtracts();
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

    private getPatientsVisits(): void {
        this.loadingData = true;
        this.getValidCount$ = this._patientVisitService.loadValidCount()
            .subscribe(
                p => {
                    this.recordCount = p;
                    this.getValidPatientVisitExtracts();
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

    private getPatientsAdverse(): void {
        this.loadingData = true;
        this.getValidCount$ = this._patientAdverseEventService.loadValidCount()
            .subscribe(
                p => {
                    this.recordCount = p;
                    this.getValidPatientAdverseEventExtracts();
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

    private getSummaryExtracts(ex: string): void {
        this.loadingData = true;
        this.getValidCount$ = this.summaryService.loadValidCount(ex)
            .subscribe(
                p => {
                    this.recordCount = p;
                    this.getValidSummaryExtracts(ex);
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
    private getValidPatientExtracts(): void {
        this.getValid$ = this._patientExtractsService.loadValid(this.pageModel).subscribe(
            pc => {
                this.validExtracts = pc;
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
        this.loadingData = true;
        this.getValid$ = this._patientArtService.loadValid(this.pageModel).subscribe(
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

    private getValidPatientBaselineExtracts(): void {
        this.loadingData = true;
        this.getValid$ = this._patientBaselineService.loadValid(this.pageModel).subscribe(
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

    private getValidPatientLabExtracts(): void {
        this.loadingData = true;
        this.getValid$ = this._patientLabService.loadValid(this.pageModel).subscribe(
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

    private getValidPatientPharmacyExtracts(): void {
        this.loadingData = true;
        this.getValid$ = this._patientPharmacyService.loadValid(this.pageModel).subscribe(
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

    private getValidPatientStatusExtracts(): void {
        this.loadingData = true;
        this.getValid$ = this._patientStatusService.loadValid(this.pageModel).subscribe(
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

    private getValidPatientVisitExtracts(): void {
        this.loadingData = true;
        this.getValid$ = this._patientVisitService.loadValid(this.pageModel).subscribe(
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

    private getValidPatientAdverseEventExtracts(): void {
        this.loadingData = true;
        this.getValid$ = this._patientAdverseEventService.loadValid(this.pageModel).subscribe(
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
    private getValidSummaryExtracts(ex: string): void {
        this.loadingData = true;
        this.getValid$ = this.summaryService.loadValid(ex, this.pageModel).subscribe(
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

    private getPatientColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'emr', header: 'EMR'},
            {field: 'project', header: 'Project'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'gender', header: 'Gender'},
            {field: 'dob', header: 'DOB'},
            {field: 'nupi', header: 'NUPI'},
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
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'dob', header: 'DOB'},
            {field: 'registrationDate', header: 'Registration Date'},
            {field: 'patientSource', header: 'Patient Source'},
            {field: 'gender', header: 'Gender'},
            {field: 'ageEnrollment', header: 'Enrollment Age'},
            {field: 'ageARTStart', header: 'Age ART Start'},
            {field: 'ageLastVisit', header: 'Age Last Visit'},
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
            {field: 'patientUUID', header: 'PatientUUID'},

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

    private getPatientLaboratoryColumns(): void {
        this.cols = [

            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'checkError', header: 'Check Error'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'satelliteName', header: 'Satellite Name'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'visitId', header: 'VisitId'},
            {field: 'orderedByDate', header: 'Ordered By Date'},
            {field: 'reportedByDate', header: 'Reported By Date'},
            {field: 'testName', header: 'Test Name'},
            {field: 'reason', header: 'Lab Reason'},
            {field: 'enrollmentTest', header: 'Enrollment Test'},
            {field: 'testResult', header: 'Test Result'},
            {field: 'dateExtracted', header: 'Date Extracted'}
        ];
    }

    private getPatientPharmacyColumns(): void {
        this.cols = [

            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'Visit ID'},
            {field: 'drug', header: 'Drug'},
            {field: 'provider', header: 'Provider'},
            {field: 'dispenseDate', header: 'Dispense Date'},
            {field: 'duration', header: 'Duration'},
            {field: 'expectedReturn', header: 'Expected Return'},
            {field: 'treatmentType', header: 'Treatment Type'},
            {field: 'regimenLine', header: 'Regimen Line'},
            {field: 'periodTaken', header: 'Period Taken'},
            {field: 'prophylaxisType', header: 'Prophylaxis Type'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'checkError', header: 'Check Error'}
        ];
    }

    private getPatientStatusColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'exitDescription', header: 'Exit Description'},
            {field: 'exitDate', header: 'Exit Date'},
            {field: 'exitReason', header: 'Exit Reason'},
            {field: 'checkError', header: 'CheckError'}
        ];
    }

    private getPatientVisitColumns(): void {
        this.cols = [

            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'checkError', header: 'Check Error'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'emr', header: 'Emr'},
            {field: 'nextAppointmentDate', header: 'NextAppointmentDate'},
            {field: 'project', header: 'Project'},
            {field: 'zScore', header: 'ZScore'},
            {field: 'paedsDisclosure', header: 'PaedsDisclosure'},
            {field: 'visitId', header: 'Visit Id'},
            {field: 'visitDate', header: 'Visit Date'},
            {field: 'service', header: 'Service'},
            {field: 'visitType', header: 'Visit Type'},
            {field: 'wHOStage', header: 'WHOS tage'},
            {field: 'wABStage', header: 'WAB Stage'},
            {field: 'pregnant', header: 'Pregnant'},
            {field: 'lMP', header: 'LMP'},
            {field: 'eDD', header: 'EDD'},
            {field: 'height', header: 'Height'},
            {field: 'weight', header: 'Weight'},
            {field: 'bP', header: 'BP'},
            {field: 'oI', header: 'OI'},
            {field: 'oIDate', header: 'OI Date'},
            {field: 'adherence', header: 'Adherence'},
            {field: 'adherenceCategory', header: 'Adherence Category'},
            {field: 'substitutionFirstlineRegimenDate', header: 'SubstitutionFirstlineRegimenDate'},
            {field: 'substitutionFirstlineRegimenReason', header: 'SubstitutionFirstlineRegimenReason'},
            {field: 'substitutionSecondlineRegimenDate', header: 'SubstitutionSecondlineRegimenDate'},
            {field: 'substitutionSecondlineRegimenReason', header: 'SubstitutionSecondlineRegimenReason'},
            {field: 'secondlineRegimenChangeDate', header: 'SecondlineRegimenChangeDate'},
            {field: 'secondlineRegimenChangeReason', header: 'SecondlineRegimenChangeReason'},
            {field: 'familyPlanningMethod', header: 'FamilyPlanningMethod'},
            {field: 'pwP', header: 'PwP'},
            {field: 'gestationAge', header: 'GestationAge'}
        ];
    }

    private getPatientAdverseEventColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'dateExtracted', header: 'Date Extracted'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'adverseEvent', header: 'Adverse Event'},
            {field: 'adverseEventStartDate', header: 'Start Date'},
            {field: 'adverseEventEndDate', header: 'End Date'},
            {field: 'severity', header: 'Severity'},
            {field: 'adverseEventRegimen', header: 'Regimen'},
            {field: 'adverseEventCause', header: 'Cause'},
            {field: 'adverseEventClinicalOutcome', header: 'Clinical Outcome'},
            {field: 'adverseEventActionTaken', header: 'Action Taken'},
            {field: 'adverseEventIsPregnant', header: 'Is Pregnant?'},
            {field: 'visitDate', header: 'Visit Date'}
        ];
    }

    private getAllergiesChronicIllnessColumns(): void {
        this.cols = [
            {field: 'facilityName', header: 'facilityName'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'patientPK', header: 'patientPK'},
            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'chronicIllness', header: 'chronicIllness'},
            {field: 'chronicOnsetDate', header: 'chronicOnsetDate'},
            {field: 'knownAllergies', header: 'knownAllergies'},
            {field: 'allergyCausativeAgent', header: 'allergyCausativeAgent'},
            {field: 'allergicReaction', header: 'allergicReaction'},
            {field: 'allergySeverity', header: 'allergySeverity'},
            {field: 'allergyOnsetDate', header: 'allergyOnsetDate'},
            {field: 'skin', header: 'skin'},
            {field: 'eyes', header: 'eyes'},
            {field: 'ent', header: 'ent'},
            {field: 'chest', header: 'chest'},
            {field: 'cvs', header: 'cvs'},
            {field: 'abdomen', header: 'abdomen'},
            {field: 'cns', header: 'cns'},
            {field: 'genitourinary', header: 'genitourinary'}
        ];
    }
    private getContactListingColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'partnerPersonID', header: 'partnerPersonID'},
            {field: 'contactAge', header: 'contactAge'},
            {field: 'contactSex', header: 'contactSex'},
            {field: 'contactMaritalStatus', header: 'contactMaritalStatus'},
            {field: 'relationshipWithPatient', header: 'relationshipWithPatient'},
            {field: 'screenedForIpv', header: 'screenedForIpv'},
            {field: 'ipvScreening', header: 'ipvScreening'},
            {field: 'ipvScreeningOutcome', header: 'ipvScreeningOutcome'},
            {field: 'currentlyLivingWithIndexClient', header: 'currentlyLivingWithIndexClient'},
            {field: 'knowledgeOfHivStatus', header: 'knowledgeOfHivStatus'},
            {field: 'pnsApproach', header: 'pnsApproach'},
            {field: 'contactPatientPK', header: 'ContactPatientPK'},
        ];
    }

    private getDepressionScreeningColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'phQ9_1', header: 'phQ9_1'},
            {field: 'phQ9_2', header: 'phQ9_2'},
            {field: 'phQ9_3', header: 'phQ9_3'},
            {field: 'phQ9_4', header: 'phQ9_4'},
            {field: 'phQ9_5', header: 'phQ9_5'},
            {field: 'phQ9_6', header: 'phQ9_6'},
            {field: 'phQ9_7', header: 'phQ9_7'},
            {field: 'phQ9_8', header: 'phQ9_8'},
            {field: 'phQ9_9', header: 'phQ9_9'},
            {field: 'phQ_9_rating', header: 'phQ_9_rating'},
            {field: 'depressionAssesmentScore', header: 'depressionAssesmentScore'}
        ];
    }

    private getDrugAlcoholScreeningColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'drinkingAlcohol', header: 'drinkingAlcohol'},
            {field: 'smoking', header: 'smoking'},
            {field: 'drugUse', header: 'drugUse'}
        ];
    }
    private getEnhancedAdherenceCounsellingColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'sessionNumber', header: 'sessionNumber'},
            {field: 'dateOfFirstSession', header: 'dateOfFirstSession'},
            {field: 'pillCountAdherence', header: 'pillCountAdherence'},
            {field: 'mmaS4_1', header: 'mmaS4_1'},
            {field: 'mmaS4_2', header: 'mmaS4_2'},
            {field: 'mmaS4_3', header: 'mmaS4_3'},
            {field: 'mmaS4_4', header: 'mmaS4_4'},
            {field: 'mmsA8_1', header: 'mmsA8_1'},
            {field: 'mmsA8_2', header: 'mmsA8_2'},
            {field: 'mmsA8_3', header: 'mmsA8_3'},
            {field: 'mmsA8_4', header: 'mmsA8_4'},
            {field: 'mmsaScore', header: 'mmsaScore'},
            {field: 'eacRecievedVL', header: 'eacRecievedVL'},
            {field: 'eacvl', header: 'eacvl'},
            {field: 'eacvlConcerns', header: 'eacvlConcerns'},
            {field: 'eacvlThoughts', header: 'eacvlThoughts'},
            {field: 'eacWayForward', header: 'eacWayForward'},
            {field: 'eacCognitiveBarrier', header: 'eacCognitiveBarrier'},
            {field: 'eacBehaviouralBarrier_1', header: 'eacBehaviouralBarrier_1'},
            {field: 'eacBehaviouralBarrier_2', header: 'eacBehaviouralBarrier_2'},
            {field: 'eacBehaviouralBarrier_3', header: 'eacBehaviouralBarrier_3'},
            {field: 'eacBehaviouralBarrier_4', header: 'eacBehaviouralBarrier_4'},
            {field: 'eacBehaviouralBarrier_5', header: 'eacBehaviouralBarrier_5'},
            {field: 'eacEmotionalBarriers_1', header: 'eacEmotionalBarriers_1'},
            {field: 'eacEmotionalBarriers_2', header: 'eacEmotionalBarriers_2'},
            {field: 'eacEconBarrier_1', header: 'eacEconBarrier_1'},
            {field: 'eacEconBarrier_2', header: 'eacEconBarrier_2'},
            {field: 'eacEconBarrier_3', header: 'eacEconBarrier_3'},
            {field: 'eacEconBarrier_4', header: 'eacEconBarrier_4'},
            {field: 'eacEconBarrier_5', header: 'eacEconBarrier_5'},
            {field: 'eacEconBarrier_6', header: 'eacEconBarrier_6'},
            {field: 'eacEconBarrier_7', header: 'eacEconBarrier_7'},
            {field: 'eacEconBarrier_8', header: 'eacEconBarrier_8'},
            {field: 'eacReviewImprovement', header: 'eacReviewImprovement'},
            {field: 'eacReviewMissedDoses', header: 'eacReviewMissedDoses'},
            {field: 'eacReviewStrategy', header: 'eacReviewStrategy'},
            {field: 'eacReferral', header: 'eacReferral'},
            {field: 'eacReferralApp', header: 'eacReferralApp'},
            {field: 'eacReferralExperience', header: 'eacReferralExperience'},
            {field: 'eacHomevisit', header: 'eacHomevisit'},
            {field: 'eacAdherencePlan', header: 'eacAdherencePlan'},
            {field: 'eacFollowupDate', header: 'eacFollowupDate'}
        ];
    }
    private getGbvScreeningColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'ipv', header: 'ipv'},
            {field: 'physicalIPV', header: 'physicalIPV'},
            {field: 'emotionalIPV', header: 'emotionalIPV'},
            {field: 'sexualIPV', header: 'sexualIPV'},
            {field: 'ipvRelationship', header: 'ipvRelationship'}
        ];
    }

    private getIptColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'onTBDrugs', header: 'onTBDrugs'},
            {field: 'onIPT', header: 'onIPT'},
            {field: 'everOnIPT', header: 'everOnIPT'},
            {field: 'cough', header: 'cough'},
            {field: 'fever', header: 'fever'},
            {field: 'noticeableWeightLoss', header: 'noticeableWeightLoss'},
            {field: 'nightSweats', header: 'nightSweats'},
            {field: 'lethargy', header: 'lethargy'},
            {field: 'icfActionTaken', header: 'icfActionTaken'},
            {field: 'testResult', header: 'testResult'},
            {field: 'tbClinicalDiagnosis', header: 'tbClinicalDiagnosis'},
            {field: 'contactsInvited', header: 'contactsInvited'},
            {field: 'evaluatedForIPT', header: 'evaluatedForIPT'},
            {field: 'startAntiTBs', header: 'startAntiTBs'},
            {field: 'tbRxStartDate', header: 'tbRxStartDate'},
            {field: 'tbScreening', header: 'tbScreening'},
            {field: 'iptClientWorkUp', header: 'iptClientWorkUp'},
            {field: 'startIPT', header: 'startIPT'},
            {field: 'indicationForIPT', header: 'indicationForIPT'}
        ];
    }
    private getOtzColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'otzEnrollmentDate', header: 'otzEnrollmentDate'},
            {field: 'transferInStatus', header: 'transferInStatus'},
            {field: 'modulesPreviouslyCovered', header: 'modulesPreviouslyCovered'},
            {field: 'modulesCompletedToday', header: 'modulesCompletedToday'},
            {field: 'supportGroupInvolvement', header: 'supportGroupInvolvement'},
            {field: 'remarks', header: 'remarks'},
            {field: 'transitionAttritionReason', header: 'transitionAttritionReason'},
            {field: 'outcomeDate', header: 'outcomeDate'}
        ];
    }
    private getOvcColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'ovcEnrollmentDate', header: 'ovcEnrollmentDate'},
            {field: 'relationshipToClient', header: 'relationshipToClient'},
            {field: 'enrolledinCPIMS', header: 'enrolledinCPIMS'},
            {field: 'cpimsUniqueIdentifier', header: 'cpimsUniqueIdentifier'},
            {field: 'partnerOfferingOVCServices', header: 'partnerOfferingOVCServices'},
            {field: 'ovcExitReason', header: 'ovcExitReason'},
            {field: 'exitDate', header: 'exitDate'}
        ];
    }

    private getCovidColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},


            {field: 'covid19AssessmentDate', header: 'covid19AssessmentDate'},
            {field: 'receivedCOVID19Vaccine', header: 'receivedCOVID19Vaccine'},
            {field: 'dateGivenFirstDose', header: 'dateGivenFirstDose'},
            {field: 'firstDoseVaccineAdministered', header: 'firstDoseVaccineAdministered'},
            {field: 'dateGivenSecondDose', header: 'dateGivenSecondDose'},
            {field: 'secondDoseVaccineAdministered', header: 'secondDoseVaccineAdministered'},
            {field: 'vaccinationStatus', header: 'vaccinationStatus'},
            {field: 'vaccineVerification', header: 'vaccineVerification'},
            {field: 'boosterGiven', header: 'boosterGiven'},
            {field: 'boosterDose', header: 'boosterDose'},
            {field: 'boosterDoseDate', header: 'boosterDoseDate'},
            {field: 'everCOVID19Positive', header: 'everCOVID19Positive'},
            {field: 'coviD19TestDate', header: 'coviD19TestDate'},
            {field: 'patientStatus', header: 'patientStatus'},
            {field: 'admissionStatus', header: 'admissionStatus'},
            {field: 'admissionUnit', header: 'admissionUnit'},
            {field: 'missedAppointmentDueToCOVID19', header: 'missedAppointmentDueToCOVID19'},
            {field: 'coviD19PositiveSinceLasVisit', header: 'coviD19PositiveSinceLasVisit'},
            {field: 'coviD19TestDateSinceLastVisit', header: 'coviD19TestDateSinceLastVisit'},
            {field: 'patientStatusSinceLastVisit', header: 'patientStatusSinceLastVisit'},
            {field: 'admissionStatusSinceLastVisit', header: 'admissionStatusSinceLastVisit'},
            {field: 'admissionStartDate', header: 'admissionStartDate'},
            {field: 'admissionEndDate', header: 'admissionEndDate'},
            {field: 'admissionUnitSinceLastVisit', header: 'admissionUnitSinceLastVisit'},
            {field: 'supplementalOxygenReceived', header: 'supplementalOxygenReceived'},
            {field: 'patientVentilated', header: 'patientVentilated'},
            {field: 'tracingFinalOutcome', header: 'tracingFinalOutcome'},
            {field: 'causeOfDeath', header: 'causeOfDeath'},
            {field: 'emr', header: 'emr'},
            {field: 'project', header: 'project'},
            {field: 'id', header: 'id'}
         ];
    }

    private getDefaulterTracingColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},

            {field: 'encounterId', header: 'encounterId'},
            {field: 'tracingType', header: 'tracingType'},
            {field: 'tracingOutcome', header: 'tracingOutcome'},
            {field: 'attemptNumber', header: 'attemptNumber'},
            {field: 'isFinalTrace', header: 'isFinalTrace'},
            {field: 'trueStatus', header: 'trueStatus'},
            {field: 'causeOfDeath', header: 'causeOfDeath'},
            {field: 'comments', header: 'comments'},
            {field: 'bookingDate', header: 'bookingDate'},
            {field: 'id', header: 'id'}
        ];
    }

    private getCervicalCancerScreeningColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},
            {field: 'patientUUID', header: 'PatientUUID'},

            {field: 'visitID', header: 'visitID'},
            {field: 'visitDate', header: 'visitDate'},
            {field: 'VisitType', header: 'VisitType'},
            {field: 'screeningMethod', header: 'ScreeningMethod'},
            {field: 'treatmentToday', header: 'TreatmentToday'},
            {field: 'referredOut', header: 'ReferredOut'},
            {field: 'nextAppointmentDate', header: 'NextAppointmentDate'},
            {field: 'screeningType', header: 'ScreeningType'},
            {field: 'screeningResult', header: 'ScreeningResult'},
            {field: 'postTreatmentComplicationCause', header: 'PostTreatmentComplicationCause'},
            {field: 'otherPostTreatmentComplication', header: 'OtherPostTreatmentComplication'},
            {field: 'referralReason', header: 'ReferralReason'}

        ];
    }

    private getIITRiskScoresColumns(): void {
        this.cols = [
            {field: 'patientPK', header: 'patientPK'},
            {field: 'siteCode', header: 'siteCode'},
            {field: 'patientID', header: 'patientID'},
            {field: 'facilityId', header: 'facilityId'},
            {field: 'facilityName', header: 'facilityName'},

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
        if (!this.preventLoad) {
            this.getValidExtracts();
        }
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
