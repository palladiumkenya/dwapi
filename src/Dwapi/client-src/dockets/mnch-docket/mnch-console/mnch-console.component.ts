import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {EmrSystem} from '../../../settings/model/emr-system';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@aspnet/signalr';
import {ConfirmationService, Message} from 'primeng/api';
import {MnchService} from '../../services/mnch.service';
import {RegistryConfigService} from '../../../settings/services/registry-config.service';
import {MnchSenderService} from '../../services/mnch-sender.service';
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
import {LoadMnchExtracts} from '../../../settings/model/load-mnch-extracts';
import {LoadMnchFromEmrCommand} from '../../../settings/model/load-mnch-from-emr-command';
import {environment} from '../../../environments/environment';

@Component({
    selector: 'liveapp-mnch-console',
    templateUrl: './mnch-console.component.html',
    styleUrls: ['./mnch-console.component.scss']
})
export class MnchConsoleComponent implements OnInit, OnDestroy, OnChanges {
    @Input() emr: EmrSystem;
    @Input() emrVer: string;
    private _hubConnection: HubConnection | undefined;
    private _sendhubConnection: HubConnection | undefined;
    public async: any;

    public emrName: string;
    public emrVersion: string;
    public minEMRVersion: string;

    private _confirmationService: ConfirmationService;
    private _mnchService: MnchService;
    private _registryConfigService: RegistryConfigService;
    private _mnchSenderService: MnchSenderService;

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
    private extractLoadCommand: LoadMnchFromEmrCommand;
    private loadExtractsCommand: LoadMnchExtracts;

    private extractProfiles: ExtractProfile[] = [];
    public centralRegistry: CentralRegistry;
    public sendResponse: SendResponse;
    public getEmr$: Subscription;

    public sendStage = 2;
    extractSent = [];

    public constructor(
        confirmationService: ConfirmationService,
        emrConfigService: MnchService,
        registryConfigService: RegistryConfigService,
        psmartSenderService: MnchSenderService,
        private emrService: EmrConfigService
    ) {
        this._confirmationService = confirmationService;
        this._mnchService = emrConfigService;
        this._registryConfigService = registryConfigService;
        this._mnchSenderService = psmartSenderService;
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
                x => x.docketId === 'MNCH'
            );
            this.updateEvent();
            this.emrName = this.emr.name;
            this.emrVersion = `(Ver. ${this.emr.version})`;
            const em = environment.emrs.filter(x => x.name === this.emrName)[0];

            if (this.emrName === 'KenyaEMR') {
                this.minEMRVersion = `(This version of DWAPI works best with ${this.emrName} version ${em.version}) or higher`;
            } else if (this.emrName === 'IQCare') {
                this.minEMRVersion = `(This version of DWAPI works best with ${this.emrName} version ${em.version}) or higher`;
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
        this.load$ = this._mnchService
            .extractAll(this.generateExtractLoadCommand(this.emr))
            .subscribe(
                p => {
                    // this.isVerfied = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({
                        severity: 'error',
                        summary: 'Error loading from EMR ',
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
        this.loadRegistry$ = this._registryConfigService.get('MNCH').subscribe(
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
            this.getStatus$ = this._mnchService
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
        const manifestPackage = this.getSendManifestPackage();
        this.sendManifest$ = this._mnchSenderService.sendManifest(manifestPackage)
            .subscribe(
                p => {
                    this.canSendPatients = true;
                    this.sendingManifest = false;
                    this.updateEvent();
                    this.sendPatientMnchExtract();
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

    public sendPatientMnchExtract(): void {
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getPatientMnchExtractPackage();
        this.send$ = this._mnchSenderService.sendPatientMnchExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendAncVisitExtracts();
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

    public sendAncVisitExtracts(): void {
        this.sendStage = 2;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getAncVisitExtractPackage();
        this.send$ = this._mnchSenderService.sendAncVisitExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendCwcEnrolmentExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending client tests', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendCwcEnrolmentExtracts(): void {
        this.sendStage = 3;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getCwcEnrolmentExtractPackage();
        this.send$ = this._mnchSenderService.sendCwcEnrolmentExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendCwcVisitExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending test kits', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendCwcVisitExtracts(): void {
        this.sendStage = 4;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getCwcVisitExtractPackage();
        this.send$ = this._mnchSenderService.sendCwcVisitExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendHeiExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending client tracing', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendHeiExtracts(): void {
        this.sendStage = 5;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getHeiExtractPackage();
        this.send$ = this._mnchSenderService.sendHeiExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendMatVisitExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending partner tracing', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendMatVisitExtracts(): void {
        this.sendStage = 6;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getMatVisitExtractPackage();
        this.send$ = this._mnchSenderService.sendMatVisitExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendMnchArtExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending partner notification service', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }
    public sendMnchArtExtracts(): void {
        this.sendStage = 7;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getMnchArtExtractPackage();
        this.send$ = this._mnchSenderService.sendMnchArtExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendMnchEnrolmentExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending partner notification service', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendMnchEnrolmentExtracts(): void {
        this.sendStage = 8;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getMnchEnrolmentExtractPackage();
        this.send$ = this._mnchSenderService.sendMnchEnrolmentExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendMnchLabExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending partner notification service', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendMnchLabExtracts(): void {
        this.sendStage = 9;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getMnchLabExtractPackage();
        this.send$ = this._mnchSenderService.sendMnchLabExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendMotherBabyPairExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending partner notification service', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendMotherBabyPairExtracts(): void {
        this.sendStage = 10;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getMotherBabyPairExtractPackage();
        this.send$ = this._mnchSenderService.sendMotherBabyPairExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendPncVisitExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending partner notification service', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendPncVisitExtracts(): void {
        this.sendStage = 11;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getPncVisitExtractPackage();
        this.send$ = this._mnchSenderService.sendPncVisitExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending client linkage', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendHandshake(): void {
        this.manifestPackage = this.getSendManifestPackage();
        this.send$ = this._mnchSenderService.sendHandshake(this.manifestPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                },
                e => {
                    this.errorMessage = [];
                    // this.errorMessage.push({ severity: 'error', summary: 'Error sending handshake', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    private getSendManifestPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'MnchClient').id,
            emrSetup: this.emr.emrSetup,
            emrId: this.emr.id,
            emrName: this.emr.name
        };
    }

    private getPatientMnchExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PatientMnchExtract').id,
            extractName: 'PatientMnchExtract'
        };
    }

    private getAncVisitExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'AncVisitExtract').id,
            extractName: 'AncVisitExtract'
        };
    }

    private getCwcEnrolmentExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'CwcEnrolmentExtract').id,
            extractName: 'CwcEnrolmentExtract'
        };
    }

    private getCwcVisitExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'CwcVisitExtract').id,
            extractName: 'CwcVisitExtract'
        };
    }

    private getHeiExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HeiExtract').id,
            extractName: 'HeiExtract'
        };
    }

    private getMatVisitExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'MatVisitExtract').id,
            extractName: 'MatVisitExtract'
        };
    }

    private getMnchArtExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'MnchArtExtract').id,
            extractName: 'MnchArtExtract'
        };
    }

    private getMnchEnrolmentExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'MnchEnrolmentExtract').id,
            extractName: 'MnchEnrolmentExtract'
        };
    }

    private getMnchLabExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'MnchLabExtract').id,
            extractName: 'MnchLabExtract'
        };
    }

    private getMotherBabyPairExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'MotherBabyPairExtract').id,
            extractName: 'MotherBabyPairExtract'
        };
    }

    private getPncVisitExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PncVisitExtract').id,
            extractName: 'PncVisitExtract'
        };
    }

    private updateExractStats(dwhProgress: any) {
        if (dwhProgress) {
            this.extracts.map(e => {
                    if (e.name === dwhProgress.extract && e.extractEvent) {
                        e.extractEvent.sent = dwhProgress.sent;
                    }
                }
            );
        }
    }

    private liveOnInit() {
        this._hubConnection = new HubConnectionBuilder()
            .withUrl(
                `${window.location.protocol}//${document.location.hostname}:${environment.port}/MnchActivity`
            )
            .configureLogging(LogLevel.Information)
            .build();

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('ShowMnchProgress', (extractActivityNotification: any) => {
            console.log('ShowMnchProgress',extractActivityNotification)
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

        this._hubConnection.on('ShowMnchSendProgress', (dwhProgress: any) => {
            this.sendEvent = {
                sentProgress: dwhProgress.progress
            };
            this.updateExractStats(dwhProgress);
            this.canLoadFromEmr = this.canSend = !this.sending;
        });

        this._hubConnection.on('ShowMnchSendProgressDone', (extractName: string) => {
            this.extractSent.push(extractName);
            if (this.extractSent.length === 11) {
                this.errorMessage = [];
                this.errorMessage.push({severity: 'success', summary: 'sent successfully '});
                this.updateEvent();
                this.sendHandshake();
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

    private generateExtractLoadCommand(currentEmr: EmrSystem): LoadMnchExtracts {
        this.extractProfiles.push(this.generateExtractPatientMnch(currentEmr));
        this.extractProfiles.push(this.generateExtractAncVisit(currentEmr));
        this.extractProfiles.push(this.generateExtractCwcEnrollment(currentEmr));
        this.extractProfiles.push(this.generateExtractCwcVisit(currentEmr));
        this.extractProfiles.push(this.generateExtractHei(currentEmr));
        this.extractProfiles.push(this.generateExtractMatVisit(currentEmr));
        this.extractProfiles.push(this.generateExtractMnchArt(currentEmr));
        this.extractProfiles.push(this.generateExtractMnchEnrolment(currentEmr));
        this.extractProfiles.push(this.generateExtractMnchLab(currentEmr));
        this.extractProfiles.push(this.generateExtractMotherBabyPair(currentEmr));
        this.extractProfiles.push(this.generateExtractPncVisit(currentEmr));

        this.extractLoadCommand = {
            extracts: this.extractProfiles
        };

        this.loadExtractsCommand = {
            loadMnchFromEmrCommand: this.extractLoadCommand
        };
        return this.loadExtractsCommand;
    }

    private generateExtractPatientMnch(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientMnchExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientMnchExtract')
        };
    }

    private generateExtractAncVisit(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'AncVisitExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'AncVisitExtract')
        };
    }

    private generateExtractCwcEnrollment(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'CwcEnrolmentExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'CwcEnrolmentExtract')
        };
    }

    private generateExtractCwcVisit(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'CwcVisitExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'CwcVisitExtract')
        };
    }

    private generateExtractHei(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HeiExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HeiExtract')
        };
    }

    private generateExtractMatVisit(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'MatVisitExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'MatVisitExtract')
        };
    }

    private generateExtractMnchArt(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'MnchArtExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'MnchArtExtract')
        };
    }

    private generateExtractMnchEnrolment(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'MnchEnrolmentExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'MnchEnrolmentExtract')
        };
    }
    private generateExtractMnchLab(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'MnchLabExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'MnchLabExtract')
        };
    }
    private generateExtractMotherBabyPair(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'MotherBabyPairExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'MotherBabyPairExtract')
        };
    }
    private generateExtractPncVisit(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PncVisitExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PncVisitExtract')
        };
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
