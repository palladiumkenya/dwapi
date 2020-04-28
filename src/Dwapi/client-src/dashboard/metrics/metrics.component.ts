import {Component, OnDestroy, OnInit} from '@angular/core';
import {Message} from 'primeng/api';
import {MetricsService} from '../services/metrics.service';
import {Subscription} from 'rxjs/Subscription';
import {AppMetric} from '../models/app-metric';

@Component({
    selector: 'liveapp-metrics',
    templateUrl: './metrics.component.html',
    styleUrls: ['./metrics.component.scss']
})
export class MetricsComponent implements OnInit, OnDestroy {
    sysMessages: Message[] = [];
    public get$: Subscription;
    appMetrics: AppMetric[] = [];
    loadingData: boolean;

    constructor(private service: MetricsService) {
    }

    ngOnInit() {
        this.loadingData = true;

        this.get$ = this.service.getMetrics()
            .subscribe(
                p => {
                    this.appMetrics = p;
                },
                e => {
                    this.sysMessages = [];
                    this.sysMessages.push({severity: 'error', summary: 'Error Loading metrics', detail: <any>e});
                    this.loadingData = false;
                    this.appMetrics = null;
                },
                () => {
                    this.loadingData = false;
                }
            );
    }

    ngOnDestroy(): void {
        if (this.get$) {
            this.get$.unsubscribe();
        }
    }

}
