import { Component, OnInit } from '@angular/core';
import { Message } from 'primeng/api';
import { EmrSystem } from '../../../settings/model/emr-system';
import { Subscription } from 'rxjs/Subscription';
import { EmrConfigService } from '../../../settings/services/emr-config.service';
import { MasterPatientIndex } from '../../models/master-patient-index';

@Component({
    selector: 'liveapp-mpi-search',
    templateUrl: './mpi-search.component.html',
    styleUrls: ['./mpi-search.component.scss']
})
export class MpiSearchComponent implements OnInit {

    public getEmr$: Subscription;
    public emrSystem: EmrSystem;
    public messages: Message[];
    public notifications: Message[];
    private _emrConfigService: EmrConfigService;
    public searchResultDetails: MasterPatientIndex[] = [];

    constructor(emrConfigService: EmrConfigService) {
      this._emrConfigService = emrConfigService;
    }

    ngOnInit() {
      this.loadData();
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
}
