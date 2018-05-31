import {
    Component,
    OnInit,
    OnChanges,
    Input,
} from '@angular/core';
import {Extract} from '../../../settings/model/extract';
import { NdwhConsoleComponent } from '../ndwh-console/ndwh-console.component';
import { EmrSystem } from '../../../settings/model/emr-system';
import { EmrConfigService } from '../../../settings/services/emr-config.service';
import { Subscription } from 'rxjs/Subscription';
import { Message } from 'primeng/api';
import { PatientExtract } from '../../models/patient-extract';
import { NdwhPatientsExtractService } from '../../services/ndwh-patients-extract.service';


@Component({
    selector: 'liveapp-ndwh-extract-details',
    templateUrl: './ndwh-extract-details.component.html',
    styleUrls: ['./ndwh-extract-details.component.scss']
})
export class NdwhExtractDetailsComponent implements OnInit {
    private _emrConfigService: EmrConfigService;
    private _patientExtractsService: NdwhPatientsExtractService;
    public getEmr$: Subscription;
    public getValid$: Subscription;
    public getInvalid$: Subscription;
    public extracts: Extract[] = [];
    public validPatientExtracts: PatientExtract[] = [];
    public invalidPatientExtracts: PatientExtract[] = [];
    public errorMessage: Message[];
    public otherMessage: Message[];
    public cols: any[];

    public constructor(emrConfigService: EmrConfigService, patientExtractsService: NdwhPatientsExtractService) {
        this._emrConfigService = emrConfigService;
        this._patientExtractsService = patientExtractsService;
     }
    public ngOnInit() {
        this.getExtract();
        this.getColumns();
        this.getValidExtracts();
        this.getInalidExtracts();

    }

    public getExtract(): Extract[] {
        this.getEmr$ = this._emrConfigService.getDefault()
          .subscribe(
              p => {
                  this.extracts = p.extracts.filter(x => x.docketId === 'NDWH');
                  return this.extracts;
              },
              e => {
                  this.errorMessage = [];
                  this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
              },
              () => {
              }
          );
          return this.extracts;
    }

    public getValidExtracts(): void {
        this.getValid$ = this._patientExtractsService.loadValid()
          .subscribe(
              p => {
                this.validPatientExtracts = p;
              },
              e => {
                  this.errorMessage = [];
                  this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
              },
              () => {
                console.log(this.validPatientExtracts);
              }
          );
    }

    public getInalidExtracts(): void {
        this.getInvalid$ = this._patientExtractsService.loadErrors()
          .subscribe(
              p => {
                this.invalidPatientExtracts = p;
              },
              e => {
                  this.errorMessage = [];
                  this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
              },
              () => {
              }
          );
    }

    private getColumns(): void {
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
            {field: 'registrationAtTBClinic', header: 'Registration At TB Clinic'},
            {field: 'patientSource', header: 'Patient Source'},
            {field: 'region', header: 'Region'},
            {field: 'district', header: 'District'},
            {field: 'village', header: 'Village'},
            {field: 'contactRelation', header: 'Contact Relation'},
            {field: 'lastVisit', header: 'Last Visit'},
            {field: 'maritalStatus', header: 'Marital Status'},
            {field: 'educationLevel', header: 'Education Level'},
            {field: 'dateConfirmedHIVPositive', header: 'Date Confirmed HIV Positive'},
            {field: 'previousARTExposure', header: 'PreviousA RT Exposure'},
            {field: 'previousARTStartDate', header: 'Previous ART Start Date'},
            {field: 'statusAtCCC', header: 'Status At CCC'},
            {field: 'statusAtPMTCT', header: 'Status At PMTCT'},
            {field: 'statusAtTBClinic', header: 'Status At TB Clinic'},
            {field: 'satelliteName', header: 'Satellite Name'}
        ];
    }
}
