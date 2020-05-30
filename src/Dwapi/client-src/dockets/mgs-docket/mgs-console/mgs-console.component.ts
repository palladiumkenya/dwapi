import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {EmrSystem} from '../../../settings/model/emr-system';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@aspnet/signalr';
import {ConfirmationService, Message} from 'primeng/api';
import {RegistryConfigService} from '../../../settings/services/registry-config.service';
import {Subscription} from 'rxjs/Subscription';
import {Extract} from '../../../settings/model/extract';
import {DwhExtract} from '../../../settings/model/dwh-extract';
import {ExtractEvent} from '../../../settings/model/extract-event';
import {SendEvent} from '../../../settings/model/send-event';
import {SendPackage} from '../../../settings/model/send-package';
import {ExtractDatabaseProtocol} from '../../../settings/model/extract-protocol';
import {ExtractProfile} from '../../ndwh-docket/model/extract-profile';
import {CentralRegistry} from '../../../settings/model/central-registry';
import {SendResponse} from '../../../settings/model/send-response';
import {EmrConfigService} from '../../../settings/services/emr-config.service';
import {LoadHtsExtracts} from '../../../settings/model/load-hts-extracts';
import {LoadHtsFromEmrCommand} from '../../../settings/model/load-hts-from-emr-command';
import {environment} from '../../../environments/environment';
import {MgsService} from '../../services/mgs.service';
import {MgsSenderService} from '../../services/mgs-sender.service';
import {LoadMgsExtracts} from '../../../settings/model/load-mgs-extracts';
import {LoadMgsFromEmrCommand} from '../../../settings/model/load-mgs-from-emr-command';

@Component({
    selector: 'liveapp-mgs-console',
    templateUrl: './mgs-console.component.html',
    styleUrls: ['./mgs-console.component.scss']
})
export class MgsConsoleComponent implements OnInit, OnDestroy, OnChanges {
    @Input() emr: EmrSystem;
    @Input() emrVer: string;
    private _hubConnection: HubConnection | undefined;
    private _sendhubConnection: HubConnection | undefined;
    public async: any;

    public emrName: string;
    public emrVersion: string;
    public minEMRVersion: string;

    private _confirmationService: ConfirmationService;
    private _mgsService: MgsService;
    private _registryConfigService: RegistryConfigService;
    private _mgsSenderService: MgsSenderService;

    public load$: Subscription;
    public loadRegistry$: Subscription;
    public send$: Subscription;
    public getStatus$: Subscription;
    public sendManifest$: Subscription;

    public loadingData: boolean;
    public extracts: Extract[] = [];
    public currentExtract: Extract;
    private dwhExtract: DwhExtract;
    private dwhExtracts: DwhExtract[] = [];
    private extractEvent: ExtractEvent;
    public sendEvent: SendEvent = {};
    public sendEventPartners: SendEvent = {};
    public sendEventLinkage: SendEvent = {};
    public recordCount: number;

    public canLoadFromEmr: boolean;
    public canSend: boolean;
    public canSendPatients: boolean = false;
    public manifestPackage: SendPackage;
    public patientPackage: SendPackage;
    public sending: boolean = false;
    public sendingManifest: boolean = false;

    public errorMessage: Message[];
    public otherMessage: Message[];
    public notifications: Message[];
    private _extractDbProtocol: ExtractDatabaseProtocol;
    private _extractDbProtocols: ExtractDatabaseProtocol[];
    private extractLoadCommand: LoadMgsFromEmrCommand;
    private loadExtractsCommand: LoadMgsExtracts;
    private extractClient: ExtractProfile;
    private extractClientLinkage: ExtractProfile;
    private extractClientPartner: ExtractProfile;

    private extractClients: ExtractProfile;
    private extractClientTests: ExtractProfile;
    private extractClientsLinkage: ExtractProfile;
    private extractTestKits: ExtractProfile;
    private extractClientTracing: ExtractProfile;
    private extractPartnerTracing: ExtractProfile;
    private extractPartnerNotificationServices: ExtractProfile;

    private extractProfile: ExtractProfile;
    private extractProfiles: ExtractProfile[] = [];
    public centralRegistry: CentralRegistry;
    public sendResponse: SendResponse;
    public getEmr$: Subscription;

    public sendStage = 2;
    extractSent = [];

    public constructor(
        confirmationService: ConfirmationService,
        emrConfigService: MgsService,
        registryConfigService: RegistryConfigService,
        psmartSenderService: MgsSenderService,
        private emrService: EmrConfigService
    ) {
        this._confirmationService = confirmationService;
        this._mgsService = emrConfigService;
        this._registryConfigService = registryConfigService;
        this._mgsSenderService = psmartSenderService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.loadData();
        this.emrVersion = this.emrVer;
    }

    public ngOnInit() {
        this.loadRegisrty();
        this.liveOnInit();
        this.loadData();
    }

    public loadData(): void {
        this.canLoadFromEmr = this.canSend = false;

        if (this.emr) {
            this.canLoadFromEmr = true;
            this.loadingData = true;
            this.recordCount = 0;
            this.extracts = this.emr.extracts.filter(
                x => x.docketId === 'MGS'
            );
            this.updateEvent();
            this.emrName = this.emr.name;
            this.emrVersion = `(Ver. ${this.emr.version})`;

            if (this.emrName === 'KenyaEMR') {
                this.minEMRVersion = '(The minimum version EMR is 17.1.0)';
            } else if (this.emrName === 'IQCare') {
                this.minEMRVersion = '(The minimum version EMR is 2.2.1)';
            } else {
                this.minEMRVersion = '';
            }
        }
        if (this.centralRegistry) {
            this.canSend = true;
        }
    }

    public loadFromEmr(): void {
        this.errorMessage = [];
        this.load$ = this._mgsService
            .extractAll(this.generateExtractLoadCommand(this.emr))
            .subscribe(
                p => {
                    // this.isVerfied = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({
                        severity: 'error',
                        summary: 'Error verifying ',
                        detail: <any>e
                    });
                },
                () => {
                    this.errorMessage.push({
                        severity: 'success',
                        summary: 'load was successful '
                    });
                    this.updateEvent();
                }
            );
    }

    public loadRegisrty(): void {
        this.errorMessage = [];
        this.loadRegistry$ = this._registryConfigService.get('MGS').subscribe(
            p => {
                this.centralRegistry = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error loading regisrty ',
                    detail: <any>e
                });
            },
            () => {
            }
        );
    }

    public updateEvent(): void {
        this.extracts.forEach(extract => {
            this.getStatus$ = this._mgsService
                .getStatus(extract.id)
                .subscribe(
                    p => {
                        extract.extractEvent = p;
                        if (extract.extractEvent) {
                            this.canSend = extract.extractEvent.queued > 0;
                        }
                    },
                    e => {
                        this.errorMessage = [];
                        this.errorMessage.push({
                            severity: 'error',
                            summary: 'Error loading status ',
                            detail: <any>e
                        });
                    },
                    () => {
                    }
                );
        });
    }


    public send(): void {
        this.sendingManifest = true;
        this.errorMessage = [];
        this.notifications = [];
        this.canSendPatients = false;
        this.manifestPackage = this.getSendManifestPackage();
        this.sendManifest$ = this._mgsSenderService.sendManifest(this.manifestPackage)
            .subscribe(
                p => {
                    this.canSendPatients = true;
                    this.sendingManifest = false;
                    this.updateEvent();
                    this.sendClientsExtract();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                },
                () => {
                    this.notifications.push({severity: 'success', summary: 'Manifest sent'});

                }
            );
    }

    public sendClientsExtract(): void {
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getMigrationExtractPackage();
        this.send$ = this._mgsSenderService.sendMigrationExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending client', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    private getSendManifestPackage(): SendPackage {
         return {
             destination: this.centralRegistry,
             extractId: this.extracts.find(x => x.name === 'MetricMigrationExtract').id,
             emrSetup: this.emr.emrSetup,
             emrId:this.emr.id,
             emrName:this.emr.name
         };
    }

    private getMigrationExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'MetricMigrationExtract').id,
            extractName: 'MetricMigrationExtract'
        };
    }

    private liveOnInit() {
        this._hubConnection = new HubConnectionBuilder()
            .withUrl(
                `${window.location.protocol}//${document.location.hostname}:${environment.port}/MgsActivity`
            )
            .configureLogging(LogLevel.Trace)
            .build();

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('ShowMgsProgress', (extractActivityNotification: any) => {
            this.currentExtract = {};
            this.currentExtract = this.extracts.find(
                x => x.name === extractActivityNotification.extract
            );
            if (this.currentExtract) {
                this.extractEvent = {
                    lastStatus: `${extractActivityNotification.status}`,
                    found: extractActivityNotification.found,
                    loaded: extractActivityNotification.loaded,
                    rejected: extractActivityNotification.rejected,
                    queued: extractActivityNotification.queued,
                    sent: extractActivityNotification.sent
                };
                this.currentExtract.extractEvent = {};
                this.currentExtract.extractEvent = this.extractEvent;
                const newWithoutPatientExtract = this.extracts.filter(
                    x => x.name !== extractActivityNotification.extract
                );
                this.extracts = [
                    ...newWithoutPatientExtract,
                    this.currentExtract
                ];
            }
        });

        this._hubConnection.on('ShowMgsSendProgress', (dwhProgress: any) => {
            this.sendEvent = {
                sentProgress: dwhProgress.progress
            };
            this.canLoadFromEmr = this.canSend = !this.sending;
        });

        this._hubConnection.on('ShowMgsSendProgressDone', (extractName: string) => {
            this.extractSent.push(extractName);
            if (this.extractSent.length === 7) {
                this.errorMessage = [];
                this.errorMessage.push({severity: 'success', summary: 'sent successfully '});
                this.updateEvent();
                this.sending = false;
            } else {
                this.updateEvent();
            }
        });
    }

    private getExtractProtocols(
        currentEmr: EmrSystem
    ): ExtractDatabaseProtocol[] {
        this._extractDbProtocols = [];
        this.extracts.forEach(e => {
            e.emr = currentEmr.name;
            this._extractDbProtocols.push({
                extract: e,
                databaseProtocol: currentEmr.databaseProtocols[0]
            });
        });
        return this._extractDbProtocols;
    }

    private generateExtractLoadCommand(currentEmr: EmrSystem): LoadMgsExtracts {
        this.extractProfiles.push(this.generateExtractMigration(currentEmr));

        this.extractLoadCommand = {
            extracts: this.extractProfiles
        };

        this.loadExtractsCommand = {
            loadMgsFromEmrCommand: this.extractLoadCommand
        };
        return this.loadExtractsCommand;
    }

    private generateExtractMigration(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'MetricMigrationExtract').databaseProtocolId;
        this.extractClients = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'MetricMigrationExtract')
        };
        return this.extractClients;
    }

    private getSendPackage(docketId: string): SendPackage {
        return {
            extractId: this.extracts[0].id,
            destination: this.centralRegistry,
            docket: docketId,
            endpoint: ''
        };
    }

    public ngOnDestroy(): void {
        if (this.load$) {
            this.load$.unsubscribe();
        }
        if (this.loadRegistry$) {
            this.loadRegistry$.unsubscribe();
        }
        if (this.send$) {
            this.send$.unsubscribe();
        }
    }
}
