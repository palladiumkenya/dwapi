import {Component, OnDestroy, OnInit} from '@angular/core';
import {EmrConfigService} from '../../settings/services/emr-config.service';
import {ConfirmationService, Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {EmrSystem} from '../../settings/model/emr-system';
import {BreadcrumbService} from '../../app/breadcrumb.service';
import {Extract} from '../../settings/model/extract';
import {Docket} from '../../settings/model/docket';
import {CbsService} from '../services/cbs.service';
import {DatabaseProtocol} from '../../settings/model/database-protocol';
import {ExtractPatient} from '../ndwh-docket/model/extract-patient';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@aspnet/signalr';
import {ExtractEvent} from '../../settings/model/extract-event';

@Component({
  selector: 'liveapp-cbs-docket',
  templateUrl: './cbs-docket.component.html',
  styleUrls: ['./cbs-docket.component.scss']
})
export class CbsDocketComponent implements OnInit, OnDestroy {

    private _hubConnection: HubConnection | undefined;
    public async: any;

    private _confirmationService: ConfirmationService;
    private _emrConfigService: EmrConfigService;

    public getEmr$: Subscription;
    public load$: Subscription;
    public getStatus$: Subscription;

    public emrSystem: EmrSystem;
    public extracts: Extract[];
    public dbProtocol: DatabaseProtocol;
    public extract: Extract;
    public extractPatient: ExtractPatient;
    private extractEvent: ExtractEvent;

    public messages: Message[];
    public canLoad: boolean = false;

    public constructor(public breadcrumbService: BreadcrumbService,
                       confirmationService: ConfirmationService, emrConfigService: EmrConfigService, private cbsService: CbsService ) {
        this.breadcrumbService.setItems([
            {label: 'Dockets'},
            {label: 'Case Based Surveillance', routerLink: ['/cbs']}
        ]);
        this._confirmationService = confirmationService;
        this._emrConfigService = emrConfigService;
    }

    public ngOnInit() {
        this.loadData();

        this.liveOnInit();
    }

    private liveOnInit() {
        this._hubConnection = new HubConnectionBuilder()
            .withUrl(`http://${document.location.hostname}:5757/cbsactivity`)
            .configureLogging(LogLevel.Trace)
            .build();
        this._hubConnection.serverTimeoutInMilliseconds = 120000;

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('ShowCbsProgress', (dwhProgress: any) => {
            if (this.extract) {
                this.extractEvent = {
                    lastStatus: `${dwhProgress.status}`, found: dwhProgress.found, loaded: dwhProgress.loaded,
                    rejected: dwhProgress.rejected, queued: dwhProgress.queued, sent: dwhProgress.sent
                };
                this.extract.extractEvent = {};
                this.extract.extractEvent = this.extractEvent;
                const newWithoutPatientExtract = this.extracts.filter(x => x.name !== 'MasterPatientIndex');
                this.extracts = [...newWithoutPatientExtract, this.extract];
            }
         });
    }

    public loadData(): void {

        this.canLoad = false;

        this.getEmr$ = this._emrConfigService.getDefault()
            .subscribe(
                p => {
                    this.emrSystem = p;
                },
                e => {
                    this.messages = [];
                    this.messages.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
                },
                () => {

                    if (this.emrSystem) {
                        if (this.emrSystem.extracts) {
                            this.extracts = this.emrSystem.extracts.filter(x => x.docketId === 'CBS');

                            this.extract = this.extracts[0];
                            this.dbProtocol = this.emrSystem.databaseProtocols.find(x => x.id === this.extract.databaseProtocolId);
                            if (this.extract && this.dbProtocol) {
                                this.canLoad = true;
                                this.updateEvent();
                            }
                        }
                    }
                }
            );
    }

    public loadFromEmr(): void {
        this.messages = [];
        this.extractPatient = {extract: this.extract, databaseProtocol: this.dbProtocol};
        this.load$ = this.cbsService.extract(this.extractPatient)
            .subscribe(
                p => {
                    // this.isVerfied = p;
                },
                e => {
                    this.messages = [];
                    this.messages.push({severity: 'error', summary: 'Error loading ', detail: <any>e});
                },
                () => {
                    this.messages = [];
                    this.messages.push({severity: 'success', summary: 'load was successful '});
                    this.updateEvent();
                }
            );


    }

    public updateEvent(): void {

        console.log(this.extract);

        if (!this.extract) {
            return;
        }
        this.getStatus$ = this.cbsService.getStatus(this.extract.id)
            .subscribe(
                p => {
                    this.extract.extractEvent = p;
                    if (this.extract.extractEvent) {
                        this.canLoad = this.extract.extractEvent.queued > 0;
                    }
                },
                e => {
                    this.messages = [];
                    this.messages.push({severity: 'error', summary: 'Error loading status ', detail: <any>e});
                },
                () => {
                    // console.log(extract);
                }
            );

    }

    public ngOnDestroy(): void {
        if (this.getEmr$) {
            this.getEmr$.unsubscribe();
        }
        if (this.getStatus$) {
            this.getStatus$.unsubscribe();
        }
        if (this.load$) {
            this.load$.unsubscribe();
        }
    }
}
