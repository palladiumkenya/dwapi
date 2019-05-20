import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {EmrSystem} from '../../../settings/model/emr-system';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@aspnet/signalr';
import {ConfirmationService, Message} from 'primeng/api';
import {HtsService} from '../../services/hts.service';
import {RegistryConfigService} from '../../../settings/services/registry-config.service';
import {HtsSenderService} from '../../services/hts-sender.service';
import {Subscription} from 'rxjs/Subscription';
import {Extract} from '../../../settings/model/extract';
import {DwhExtract} from '../../../settings/model/dwh-extract';
import {ExtractEvent} from '../../../settings/model/extract-event';
import {SendEvent} from '../../../settings/model/send-event';
import {SendPackage} from '../../../settings/model/send-package';
import {ExtractDatabaseProtocol} from '../../../settings/model/extract-protocol';
import {LoadFromEmrCommand} from '../../../settings/model/load-from-emr-command';
import {ExtractProfile} from '../../ndwh-docket/model/extract-profile';
import {CentralRegistry} from '../../../settings/model/central-registry';
import {SendResponse} from '../../../settings/model/send-response';
import {EmrConfigService} from '../../../settings/services/emr-config.service';
import {LoadExtracts} from '../../../settings/model/load-extracts';
import {LoadHtsExtracts} from '../../../settings/model/load-hts-extracts';
import {LoadHtsFromEmrCommand} from '../../../settings/model/load-hts-from-emr-command';
import {environment} from '../../../environments/environment';

@Component({
    selector: 'liveapp-hts-console',
    templateUrl: './hts-console.component.html',
    styleUrls: ['./hts-console.component.scss']
})
export class HtsConsoleComponent implements OnInit, OnDestroy, OnChanges {
    @Input() emr: EmrSystem;
    @Input() emrVer: string;
    private _hubConnection: HubConnection | undefined;
    private _sendhubConnection: HubConnection | undefined;
    public async: any;

    public emrName: string;
    public emrVersion: string;

    private _confirmationService: ConfirmationService;
    private _htsService: HtsService;
    private _registryConfigService: RegistryConfigService;
    private _htsSenderService: HtsSenderService;

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
    private extractLoadCommand: LoadHtsFromEmrCommand;
    private loadExtractsCommand: LoadHtsExtracts;
    private extractClient: ExtractProfile;
    private extractClientLinkage: ExtractProfile;
    private extractClientPartner: ExtractProfile;

    private extractProfile: ExtractProfile;
    private extractProfiles: ExtractProfile[] = [];
    public centralRegistry: CentralRegistry;
    public sendResponse: SendResponse;
    public getEmr$: Subscription;

    public constructor(
        confirmationService: ConfirmationService,
        emrConfigService: HtsService,
        registryConfigService: RegistryConfigService,
        psmartSenderService: HtsSenderService,
        private emrService: EmrConfigService
    ) {
        this._confirmationService = confirmationService;
        this._htsService = emrConfigService;
        this._registryConfigService = registryConfigService;
        this._htsSenderService = psmartSenderService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.loadData();
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
                x => x.docketId === 'HTS'
            );
            this.updateEvent();
            this.emrName = this.emr.name;
            this.emrVersion = `(Ver. ${this.emr.version})`;
        }
        if (this.centralRegistry) {
            this.canSend = true;
        }
    }

    public loadFromEmr(): void {
        this.errorMessage = [];
        this.load$ = this._htsService
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
        this.loadRegistry$ = this._registryConfigService.get('HTS').subscribe(
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
            this.getStatus$ = this._htsService
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
        this.sendManifest$ = this._htsSenderService.sendManifest(this.manifestPackage)
            .subscribe(
                p => {
                    this.canSendPatients = true;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                },
                () => {
                    this.notifications.push({severity: 'success', summary: 'Manifest sent'});
                    this.sendingManifest = false;
                    this.updateEvent();
                    this.sendClientExtract();
                }
            );
    }

    public sendClientExtract(): void {
        this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientExtractPackage();
        this.send$ = this._htsSenderService.sendClientExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending Clients', detail: <any>e});
                },
                () => {
                    this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                    this.updateEvent();
                    this.sendClientLinkageExtract();
                    this.sending = false;
                }
            );
    }

    public sendClientLinkageExtract(): void {
        this.sendEvent = {sentProgress: 60};
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientLinkageExtractPackage();
        this.send$ = this._htsSenderService.sendClientLinkageExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending Linkages', detail: <any>e});
                },
                () => {
                    this.errorMessage.push({severity: 'success', summary: 'sent Linkages successfully '});
                    this.updateEvent();
                    this.sendClientPartnerExtract();
                }
            );
    }

    public sendClientPartnerExtract(): void {
        this.sendEvent = {sentProgress: 80};
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientPartnerExtractPackage();
        this.send$ = this._htsSenderService.sendClientPartnerExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending Partners ', detail: <any>e});
                },
                () => {
                    this.errorMessage.push({severity: 'success', summary: 'sent Partners successfully '});
                    this.updateEvent();
                    this.sending = true;
                }
            );
    }

    private getSendManifestPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HTSClientExtract').id
        };
    }

    private getClientExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HTSClientExtract').id,
            extractName: 'HTSClientExtract'
        };
    }

    private getClientLinkageExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HTSClientLinkageExtract').id,
            extractName: 'HTSClientLinkageExtract'
        };
    }

    private getClientPartnerExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HTSClientPartnerExtract').id,
            extractName: 'HTSClientPartnerExtract'
        };
    }


    private liveOnInit() {
        this._hubConnection = new HubConnectionBuilder()
            .withUrl(
                `http://${document.location.hostname}:${environment.port}/HtsActivity`
            )
            .configureLogging(LogLevel.Trace)
            .build();

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('ShowHtsProgress', (extractActivityNotification: any) => {
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

        this._hubConnection.on('ShowHtsSendProgress', (dwhProgress: any) => {
            this.sendEvent = {
                sentProgress: dwhProgress.progress
            };
            this.sending = true;
            this.canLoadFromEmr = this.canSend = !this.sending;
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

    private generateExtractLoadCommand(currentEmr: EmrSystem): LoadHtsExtracts {
        this.extractProfiles.push(this.generateExtractClient(currentEmr));
        this.extractProfiles.push(this.generateExtractClientLinkage(currentEmr));
        this.extractProfiles.push(this.generateExtractClientPartner(currentEmr));
        this.extractLoadCommand = {
            extracts: this.extractProfiles
        };

        this.loadExtractsCommand = {
            loadHtsFromEmrCommand: this.extractLoadCommand
        };
        return this.loadExtractsCommand;

    }

    private generateExtractClient(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HTSClientExtract').databaseProtocolId;
        this.extractClient = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HTSClientExtract')
        };
        return this.extractClient;
    }

    private generateExtractClientLinkage(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HTSClientLinkageExtract').databaseProtocolId;
        this.extractClientLinkage = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HTSClientLinkageExtract')
        };
        return this.extractClientLinkage;
    }

    private generateExtractClientPartner(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HTSClientPartnerExtract').databaseProtocolId;
        this.extractClientPartner = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HTSClientPartnerExtract')
        };
        return this.extractClientPartner;
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
