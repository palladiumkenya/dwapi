import {Component, OnInit, OnChanges, SimpleChange, Input, OnDestroy} from '@angular/core';
import {PatientExtract} from '../../../models/patient-extract';
import {NdwhPatientsExtractService} from '../../../services/ndwh-patients-extract.service';
import {NdwhPatientArtService} from '../../../services/ndwh-patient-art.service';
import {NdwhPatientBaselineService} from '../../../services/ndwh-patient-baseline.service';
import {NdwhPatientLaboratoryService} from '../../../services/ndwh-patient-laboratory.service';
import {NdwhPatientPharmacyService} from '../../../services/ndwh-patient-pharmacy.service';
import {NdwhPatientStatusService} from '../../../services/ndwh-patient-status.service';
import {NdwhPatientVisitService} from '../../../services/ndwh-patient-visit.service';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {Extract} from '../../../../settings/model/extract';
import {NdwhPatientAdverseEventService} from '../../../services/ndwh-patient-adverse-event.service';
import {PageModel} from '../../../models/page-model';

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
    public initialRows: number = 20;
    public loadingData = false;
    public recordCount = 0;

    constructor(patientExtractsService: NdwhPatientsExtractService, patientArtService: NdwhPatientArtService,
                patientBaselineService: NdwhPatientBaselineService, patientLabService: NdwhPatientLaboratoryService,
                patientPharmacyService: NdwhPatientPharmacyService, patientStatusService: NdwhPatientStatusService,
                patientVisitService: NdwhPatientVisitService, patientAdverseEventService: NdwhPatientAdverseEventService) {
        this._patientExtractsService = patientExtractsService;
        this._patientArtService = patientArtService;
        this._patientBaselineService = patientBaselineService;
        this._patientLabService = patientLabService;
        this._patientPharmacyService = patientPharmacyService;
        this._patientStatusService = patientStatusService;
        this._patientVisitService = patientVisitService;
        this._patientAdverseEventService = patientAdverseEventService;
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
        this.exName = 'All Patients'
        this.pageModel = {
            page: 1,
            pageSize: this.initialRows
        };

        this.getPatientColumns();
        this.getPatients();
    }

    public getValidExtracts(): void {
        //console.log('loading>', this.extract);
        if (this.extract === 'All Patients') {
            this.getPatients();
        }
        if (this.extract === 'ART Patients') {
            this.getPatientsART();
        }
        if (this.extract === 'Patient Baselines') {
            this.getPatientsBases();
        }
        if (this.extract === 'Patient Labs') {
            this.getPatientsLabs();
        }
        if (this.extract === 'Patient Pharmacy') {
            this.getPatientsPharms();
        }
        if (this.extract === 'Patient Status') {
            this.getPatientsStats();
        }
        if (this.extract === 'Patient Visit') {
            this.getPatientsVisits();
        }
        if (this.extract === 'Patient Adverse Events') {
            this.getPatientsAdverse();
        }
    }

    private getColumns(): void {
        if (this.extract === 'All Patients') {
            this.getPatientColumns();
        }
        if (this.extract === 'ART Patients') {
            this.getPatientArtColumns();
        }
        if (this.extract === 'Patient Baselines') {
            this.getPatientBaselineColumns();
        }
        if (this.extract === 'Patient Labs') {
            this.getPatientLaboratoryColumns();
        }
        if (this.extract === 'Patient Pharmacy') {
            this.getPatientPharmacyColumns();
        }
        if (this.extract === 'Patient Status') {
            this.getPatientStatusColumns();
        }
        if (this.extract === 'Patient Visit') {
            this.getPatientVisitColumns();
        }
        if (this.extract === 'Patient Adverse Events') {
            this.getPatientAdverseEventColumns();
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
            {field: 'dob', header: 'DOB'}, 
            {field: 'registrationDate', header: 'Registration Date'},
            {field: 'patientSource', header: 'Patient Source'},
            {field: 'gender', header: 'Gender'},
            { field: 'ageEnrollment', header: 'Enrollment Age' },
            { field: 'ageARTStart', header: 'Age ART Start' },
            { field: 'ageLastVisit', header: 'Age Last Visit' },
            { field: 'startARTDate', header: 'Start ART Date' },
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

    private getPatientLaboratoryColumns(): void {
        this.cols = [

            {field: 'patientPK', header: 'Patient PK'},
            {field: 'patientID', header: 'Patient ID'},
            {field: 'facilityId', header: 'Facility Id'},
            {field: 'siteCode', header: 'Site Code'},
            {field: 'checkError', header: 'Check Error'},
            {field: 'facilityName', header: 'Facility Name'},
            {field: 'satelliteName', header: 'Satellite Name'},
            {field: 'emr', header: 'Emr'},
            {field: 'project', header: 'Project'},
            {field: 'visitId', header: 'VisitId'},
            {field: 'orderedByDate', header: 'Ordered By Date'},
            {field: 'reportedByDate', header: 'Reported By Date'},
            { field: 'testName', header: 'Test Name' },
            { field: 'reason', header: 'Lab Reason' },
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
            {field: 'emr', header: 'Emr'},
            {field: 'nextAppointmentDate', header: 'NextAppointmentDate'},
            {field: 'project', header: 'Project'},
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
