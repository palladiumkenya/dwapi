import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreadcrumbService} from '../app/breadcrumb.service';
import {EmrSystem} from '../settings/model/emr-system';
import {EmrConfigService} from '../settings/services/emr-config.service';
import {ConfirmationService, Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';

@Component({
  selector: 'liveapp-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {

    private _confirmationService: ConfirmationService;
    private _emrConfigService: EmrConfigService;

    public getEmr$: Subscription;
    public getMiddleware$: Subscription;
    public emrSystem: EmrSystem;
    public middlewareSystem: EmrSystem;
    public errorMessage: Message[];
    public otherMessage: Message[];
    public loadingData: boolean;

    public constructor(public breadcrumbService: BreadcrumbService,
                       confirmationService: ConfirmationService, emrConfigService: EmrConfigService) {
        this.breadcrumbService.setItems([
            {label: ''}
        ]);
        this._confirmationService = confirmationService;
        this._emrConfigService = emrConfigService;
    }

    public ngOnInit() {
        this.loadData();
    }

    public loadData(): void {

        this.loadingData = true;

        this.getEmr$ = this._emrConfigService.getDefault()
            .subscribe(
                p => {
                    this.emrSystem = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
                    this.loadingData = false;
                },
                () => {

                }
            );


    }

    public loadMiddleware() {
        this.getMiddleware$ = this._emrConfigService.getMiddleware()
            .subscribe(
                p => {
                    this.middlewareSystem = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
                    this.loadingData = false;
                },
                () => {
                    this.loadingData = false;
                }
            );
    }
    public ngOnDestroy(): void {
        if (this.getEmr$) {
            this.getEmr$.unsubscribe();
        }
        if (this.getMiddleware$) {
            this.getMiddleware$.unsubscribe();
        }
    }
}
