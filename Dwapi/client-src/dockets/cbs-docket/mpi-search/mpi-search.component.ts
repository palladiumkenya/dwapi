import { Component, OnInit, OnDestroy } from '@angular/core';
import { Message } from 'primeng/api';
import { EmrSystem } from '../../../settings/model/emr-system';
import { Subscription } from 'rxjs/Subscription';
import { EmrConfigService } from '../../../settings/services/emr-config.service';
import { MpiSearchService } from '../../services/mpi-search.service';
import { MasterPatientIndex } from '../../models/master-patient-index';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'liveapp-mpi-search',
    templateUrl: './mpi-search.component.html',
    styleUrls: ['./mpi-search.component.scss']
})
export class MpiSearchComponent implements OnInit, OnDestroy  {

    public getEmr$: Subscription;
    public getSearchresults: Subscription;
    public emrSystem: EmrSystem;
    public messages: Message[] = [];
    public notifications: Message[] = [];
    private _emrConfigService: EmrConfigService;
    private _mpiSearchService: MpiSearchService;
    public searchResultDetails: MasterPatientIndex[] = [];
    public gender: any[] = [];
    public selectedGender: any;
    public searchForm: FormGroup;
    dateTime = new Date();
    public loadingData: boolean;
    public searchDetails: MasterPatientIndex[] = [];

    constructor(emrConfigService: EmrConfigService, private formBuilder: FormBuilder, mpiSearchService: MpiSearchService) {
      this._emrConfigService = emrConfigService;
      this._mpiSearchService = mpiSearchService;
      this.gender = [{name: 'Female', code: 'F'}, {name: 'Male', code: 'M'}];
      this.searchForm = this.formBuilder.group({
        firstName: ['', [Validators.required]],
        middleName: [''],
        lastName: ['', [Validators.required]],
        dob: ['', [Validators.required]],
        gender: ['', [Validators.required]],
        county: [''],
        phoneNumber: [''],
        nationalId: ['']
    });
    }

    ngOnInit() {
      this.loadData();
    }

    public ngOnDestroy(): void {

        if (this.getEmr$) {
            this.getEmr$.unsubscribe();
        }
    }

    public loadData(): void {
        this.getEmr$ = this._emrConfigService.getDefault().subscribe(
            p => {
                this.emrSystem = p;
            },
            e => {
                this.messages = [];
                this.messages.push({
                    severity: 'error',
                    summary: 'Error Loading data',
                    detail: <any>e
                });
            },
            () => {
            }
        );
    }

    public search(): void {
        this.loadingData = true;
        this.getSearchresults = this._mpiSearchService.search(this.searchForm.value)
          .subscribe(
              p => {
                  this.searchDetails = p;
              },
              e => {
                  this.messages.push({severity: 'error', summary: 'Error getting search results', detail: <any>e});
                  this.loadingData = false;
              },
              () => {
                this.messages.push({severity: 'success', summary: 'Data loaded successfully'});
                this.loadingData = false;
              }
          );
    }
}
