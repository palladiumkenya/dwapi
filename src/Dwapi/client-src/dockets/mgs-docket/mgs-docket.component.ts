import {Component, OnDestroy, OnInit} from '@angular/core';
import {ConfirmationService, Message} from 'primeng/api';
import {EmrConfigService} from '../../settings/services/emr-config.service';
import {Subscription} from 'rxjs/Subscription';
import {EmrSystem} from '../../settings/model/emr-system';
import {EmrMetrics} from '../../settings/model/emr-metrics';
import {BreadcrumbService} from '../../app/breadcrumb.service';

@Component({
  selector: 'liveapp-mgs-docket',
  templateUrl: './mgs-docket.component.html',
  styleUrls: ['./mgs-docket.component.scss']
})
export class MgsDocketComponent implements OnInit, OnDestroy {
    private _confirmationService: ConfirmationService;
    private _emrConfigService: EmrConfigService;

    public getEmr$: Subscription;
    public getMetrics$: Subscription;
    public emrSystem: EmrSystem;
    public errorMessage: Message[];
    public otherMessage: Message[];
    public loadingData: boolean;
    public emrMetric: EmrMetrics;
    public emrVersion: string;
    public metricMessages: Message[];

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
                    this.loadMetrics();
                }
            );


    }

    public loadMetrics(): void {

        this.getMetrics$ = this._emrConfigService.loadMetrics(this.emrSystem)
            .subscribe(
                p => {
                    this.emrMetric = p;
                },
                e => {
                    this.metricMessages = [];
                },
                () => {
                    if (this.emrMetric) {
                        this.emrVersion = this.emrMetric.emrVersion;
                    }
                }
            );
    }

    // tslint:disable-next-line:use-life-cycle-interface
    public ngOnDestroy(): void {
        if (this.getEmr$) {
            this.getEmr$.unsubscribe();
        }
        if (this.getMetrics$) {
            this.getMetrics$.unsubscribe();
        }
    }

}
