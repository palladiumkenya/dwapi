import { Component, OnInit, OnDestroy } from '@angular/core';
import { Message, SelectItem } from 'primeng/api';
import { EmrSystem } from '../../../settings/model/emr-system';
import { Subscription } from 'rxjs/Subscription';
import { EmrConfigService } from '../../../settings/services/emr-config.service';
import { MpiSearchService } from '../../services/mpi-search.service';
import { MasterPatientIndex } from '../../models/master-patient-index';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SearchPackage } from '../../models/mpi-search-package';
import { CentralRegistry } from '../../../settings/model/central-registry';
import {RegistryConfigService} from '../../../settings/services/registry-config.service';

@Component({
    selector: 'liveapp-mpi-search',
    templateUrl: './mpi-search.component.html',
    styleUrls: ['./mpi-search.component.scss']
})
export class MpiSearchComponent implements OnInit, OnDestroy  {

    public getEmr$: Subscription;
    public getSearchresults: Subscription;
    public loadRegistry$: Subscription;
    public emrSystem: EmrSystem;
    public messages: Message[];
    public notifications: Message[];
    private _emrConfigService: EmrConfigService;
    private _mpiSearchService: MpiSearchService;
    public searchResultDetails: MasterPatientIndex[] = [];
    public gender: SelectItem[] = [];
    public selectedGender: any;
    public searchForm: FormGroup;
    dateTime = new Date();
    public loadingData: boolean;
    public canSend: boolean = false;
    public searchDetails: MasterPatientIndex[] = [];
    private centralRegistry: CentralRegistry;
    public searchPackage: SearchPackage;

    constructor(private _registryConfigService: RegistryConfigService,
        emrConfigService: EmrConfigService, private formBuilder: FormBuilder, mpiSearchService: MpiSearchService) {
      this._emrConfigService = emrConfigService;
      this._mpiSearchService = mpiSearchService;
      this.gender = [{label: 'Female', value: 0}, {label: 'Male', value: 1}];
      this.searchForm = this.formBuilder.group({
        firstName: ['', [Validators.required]],
        middleName: [''],
        lastName: ['', [Validators.required]],
        dob: ['', [Validators.required]],
        gender: ['', [Validators.required]],
        county: [''],
        phoneNumber: [''],
        nationalId: [''],
        nhifNumber: ['']
    });
    }

    ngOnInit() {
      this.loadData();
      this.loadRegistry();
    }

    public ngOnDestroy(): void {

        if (this.getEmr$) {
            this.getEmr$.unsubscribe();
        }
        if (this.getSearchresults) {
            this.getSearchresults.unsubscribe();
        }
        if (this.loadRegistry$) {
            this.loadRegistry$.unsubscribe();
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
        this.messages = [];
        this.loadingData = true;
        this.searchPackage = this.getSearchPackage();
        this.getSearchresults = this._mpiSearchService.search(this.searchPackage )
          .subscribe(
              p => {
                  this.searchResultDetails = p;
              },
              e => {
                  this.messages.push({severity: 'error', summary: 'Error getting search results', detail: <any>e});
                  this.loadingData = false;
              },
              () => {
                this.messages.push({severity: 'success', summary: 'Search completed successfully'});
                this.loadingData = false;
              }
          );
    }

    public loadRegistry(): void {
        this.messages = [];
        this.loadRegistry$ = this._registryConfigService.get('CBS').subscribe(
            p => {
                this.centralRegistry = p;
            },
            e => {
                this.messages = [];
                this.messages.push({
                    severity: 'error',
                    summary: 'Error loading regisrty ',
                    detail: <any>e
                });
            },
            () => {
                if (this.centralRegistry) {
                    this.canSend = true;
                }
            }
        );
    }

    private getSearchPackage(): SearchPackage {
        return {
            destination: this.centralRegistry,
            mpiSearch: this.searchForm.value,
        };
    }
}
