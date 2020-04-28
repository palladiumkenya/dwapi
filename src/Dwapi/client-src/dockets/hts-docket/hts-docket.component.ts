import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@aspnet/signalr';
import {ConfirmationService, Message} from 'primeng/api';
import {EmrConfigService} from '../../settings/services/emr-config.service';
import {Subscription} from 'rxjs/Subscription';
import {EmrSystem} from '../../settings/model/emr-system';
import {EmrMetrics} from '../../settings/model/emr-metrics';
import {Extract} from '../../settings/model/extract';
import {DatabaseProtocol} from '../../settings/model/database-protocol';
import {ExtractPatient} from '../ndwh-docket/model/extract-patient';
import {ExtractEvent} from '../../settings/model/extract-event';
import {SendEvent} from '../../settings/model/send-event';
import {MasterPatientIndex} from '../models/master-patient-index';
import {SendResponse} from '../../settings/model/send-response';
import {SendPackage} from '../../settings/model/send-package';
import {CentralRegistry} from '../../settings/model/central-registry';
import {BreadcrumbService} from '../../app/breadcrumb.service';
import {CbsService} from '../services/cbs.service';
import {RegistryConfigService} from '../../settings/services/registry-config.service';
import {environment} from '../../environments/environment';
import {HtsService} from '../services/hts.service';
import {NdwhExtractService} from '../services/ndwh-extract.service';
import {NdwhSenderService} from '../services/ndwh-sender.service';
import {CombinedPackage} from '../../settings/model/combined-package';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';
import {LoadFromEmrCommand} from '../../settings/model/load-from-emr-command';
import {LoadExtracts} from '../../settings/model/load-extracts';
import {ExtractProfile} from '../ndwh-docket/model/extract-profile';
import {DwhExtract} from '../../settings/model/dwh-extract';
import {HtsSenderService} from '../services/hts-sender.service';

@Component({
  selector: 'liveapp-hts-docket',
  templateUrl: './hts-docket.component.html',
  styleUrls: ['./hts-docket.component.scss']
})
export class HtsDocketComponent implements OnInit, OnDestroy {
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
                    // console.log(this.emrSystem);
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
                    //this.metricMessages.push({severity: 'warn', summary: 'Could not load EMR metrics', detail: <any>e});
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
