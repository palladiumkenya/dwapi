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
    public extracts: Extract[] = [];
    public validPatientExtracts: PatientExtract[];
    public invalidPatientExtracts: PatientExtract[];
    public errorMessage: Message[];
    public otherMessage: Message[];
    private patExtract: PatientExtract;
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
       // console.log(this.validPatientExtracts);
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
        this._patientExtractsService.loadValid()
          .subscribe(
              p => {
                this.validPatientExtracts = p;
              },
              e => {
                  this.errorMessage = [];
                  this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
              },
              () => {
                  console.log('this.validPatientExtracts', this.validPatientExtracts );
              }
          );
    }

    public getInalidExtracts(): void {
        this._patientExtractsService.loadErrors()
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
            {field: 'PatientPK', header: 'PatientPK'},
            {field: 'PatientID', header: 'PatientID'},
            {field: 'FacilityId', header: 'BrFacilityIdand'},
            {field: 'SiteCode', header: 'SiteCode'},
            {field: 'EMR', header: 'EMR'},
            {field: 'Project', header: 'Project'},
            {field: 'FacilityName', header: 'FacilityName'},
            {field: 'Gender', header: 'Gender'},
            {field: 'DOB', header: 'DOB'},
            {field: 'RegistrationDate', header: 'RegistrationDate'},
            {field: 'RegistrationAtCCC', header: 'RegistrationAtCCC'},
            {field: 'RegistrationAtPMTCT', header: 'RegistrationAtPMTCT'},
            {field: 'RegistrationAtTBClinic', header: 'RegistrationAtTBClinic'},
            {field: 'PatientSource', header: 'PatientSource'},
            {field: 'Region', header: 'Region'},
            {field: 'District', header: 'District'},
            {field: 'Village', header: 'Village'},
            {field: 'ContactRelation', header: 'ContactRelation'},
            {field: 'LastVisit', header: 'LastVisit'},
            {field: 'MaritalStatus', header: 'MaritalStatus'},
            {field: 'EducationLevel', header: 'EducationLevel'},
            {field: 'DateConfirmedHIVPositive', header: 'DateConfirmedHIVPositive'},
            {field: 'PreviousARTExposure', header: 'PreviousARTExposure'},
            {field: 'PreviousARTStartDate', header: 'PreviousARTStartDate'},
            {field: 'StatusAtCCC', header: 'StatusAtCCC'},
            {field: 'StatusAtPMTCT', header: 'StatusAtPMTCT'},
            {field: 'StatusAtTBClinic', header: 'StatusAtTBClinic'},
            {field: 'SatelliteName', header: 'SatelliteName'}
        ];
    }
}
