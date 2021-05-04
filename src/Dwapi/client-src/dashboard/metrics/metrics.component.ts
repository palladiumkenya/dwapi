import {Component, OnDestroy, OnInit} from '@angular/core';
import {Message} from 'primeng/api';
import {MetricsService} from '../services/metrics.service';
import {Subscription} from 'rxjs/Subscription';
import {AppMetric} from '../models/app-metric';
import {AppCheck} from '../models/app-check';
import {Indicator} from '../models/indicator';

@Component({
    selector: 'liveapp-metrics',
    templateUrl: './metrics.component.html',
    styleUrls: ['./metrics.component.scss']
})
export class MetricsComponent implements OnInit, OnDestroy {
    sysMessages: Message[] = [];
    public liveups$: Subscription;
    public get$: Subscription;
    public getChecks$: Subscription;
    public getIndicators$: Subscription;
    appMetrics: AppMetric[] = [];
    appChecks: AppCheck[] = [];
    indicators: Indicator[] = [];
    loadingData: boolean;

    constructor(private service: MetricsService) {
    }

    ngOnInit() {
        this.loadingData = true;

        if(!this.isUpdatesChecked()) {

            this.get$ = this.service.checkUpdates()
                .subscribe(
                    p => {
                        this.udpdateCheckStatus();
                    },
                    e => {
                        this.sysMessages = [];
                        // this.sysMessages.push({severity: 'error', summary: 'Error Loading metrics', detail: <any>e});
                        this.loadingData = false;
                        this.appMetrics = null;
                    },
                    () => {
                    }
                );
        }


        this.get$ = this.service.getMetrics()
            .subscribe(
                p => {
                    this.appMetrics = p;
                },
                e => {
                    this.sysMessages = [];
                    // this.sysMessages.push({severity: 'error', summary: 'Error Loading metrics', detail: <any>e});
                    this.loadingData = false;
                    this.appMetrics = null;
                },
                () => {
                }
            );

        this.getChecks$ = this.service.getChecks()
            .subscribe(
                p => {
                    this.appChecks = p;
                },
                e => {
                    this.sysMessages = [];
                    // this.sysMessages.push({severity: 'error', summary: 'Error Loading metrics', detail: <any>e});
                    this.loadingData = false;
                    this.appChecks = [];
                },
                () => {
                }
            );

        this.getIndicators$ = this.service.getIndicators()
            .subscribe(
                p => {
                    this.indicators = p;
                },
                e => {
                    this.sysMessages = [];
                    // this.sysMessages.push({severity: 'error', summary: 'Error Loading metrics', detail: <any>e});
                    this.loadingData = false;
                    this.indicators = [];
                },
                () => {
                    this.loadingData = false;
                }
            );
    }

    private udpdateCheckStatus() {
        const key = `dwapi.liveupdate`;
        const when = `${new Date().getFullYear()}-${new Date().getMonth()}-${new Date().getDate()}`;
        localStorage.setItem(key, when);
    }

    private isUpdatesChecked() {
        const key = `dwapi.liveupdate`;
        const when = `${new Date().getFullYear()}-${new Date().getMonth()}-${new Date().getDate()}`;
        const found = localStorage.getItem(key);
        return found && found === when;
    }

    ngOnDestroy(): void {
        if (this.liveups$) {
            this.liveups$.unsubscribe();
        }
        if (this.get$) {
            this.get$.unsubscribe();
        }
        if (this.getChecks$) {
            this.getChecks$.unsubscribe();
        }
        if (this.getIndicators$) {
            this.getIndicators$.unsubscribe();
        }
    }
}
