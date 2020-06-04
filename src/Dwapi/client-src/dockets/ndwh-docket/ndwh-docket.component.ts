import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreadcrumbService} from '../../app/breadcrumb.service';
import {EmrSystem} from '../../settings/model/emr-system';
import {EmrConfigService} from '../../settings/services/emr-config.service';
import {ConfirmationService, Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {EmrMetrics} from '../../settings/model/emr-metrics';

@Component({
    selector: 'liveapp-ndwh-docket',
    templateUrl: './ndwh-docket.component.html',
    styleUrls: ['./ndwh-docket.component.scss']
})
export class NdwhDocketComponent implements OnInit, OnDestroy {

    private _confirmationService: ConfirmationService;
    private _emrConfigService: EmrConfigService;

    public getEmr$: Subscription;
    public emrSystem: EmrSystem;
    public errorMessage: Message[];
    public otherMessage: Message[];
    public loadingData: boolean;
    public emrMetric: EmrMetrics;
    public emrVersion: string;
    public metricMessages: Message[];
    isLoaded = false;

    public constructor(public breadcrumbService: BreadcrumbService,
                       confirmationService: ConfirmationService, emrConfigService: EmrConfigService) {
        this.breadcrumbService.setItems([
            {label: ''}
        ]);
        this._confirmationService = confirmationService;
        this._emrConfigService = emrConfigService;
    }

    public ngOnInit() {
        this.isLoaded = true;
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
                    this.errorMessage.push({severity: 'error', summary: 'Error Loading Emr settings', detail: <any>e});
                    this.loadingData = false;
                },
                () => {
                    this.loadMetrics();
                }
            );


    }

    public loadMetrics(): void {

        this.getEmr$ = this._emrConfigService.loadMetrics(this.emrSystem)
            .subscribe(
                p => {
                    this.emrMetric = p;
                },
                e => {
                    this.metricMessages = [];
                    // this.metricMessages.push({severity: 'warn', summary: 'Could not load EMR metrics', detail: <any>e});
                },
                () => {
                    if (this.emrMetric) {
                        this.emrVersion = this.emrMetric.emrVersion;
                    }
                }
            );
    }

    public ngOnDestroy(): void {
        if (this.getEmr$) {
            this.getEmr$.unsubscribe();
        }
    }
}
