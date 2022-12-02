import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {Extract} from '../../../settings/model/extract';
import {EmrSystem} from '../../../settings/model/emr-system';
import {ConfirmationService, Message} from 'primeng/api';
import {NdwhExtractService} from '../../services/ndwh-extract.service';
import {Subscription} from 'rxjs/Subscription';
import {ExtractDatabaseProtocol} from '../../../settings/model/extract-protocol';
import {RegistryConfigService} from '../../../settings/services/registry-config.service';
import {CentralRegistry} from '../../../settings/model/central-registry';
import {NdwhSenderService} from '../../services/ndwh-sender.service';
import {SendPackage} from '../../../settings/model/send-package';
import {SendResponse} from '../../../settings/model/send-response';
import {LoadFromEmrCommand} from '../../../settings/model/load-from-emr-command';
import {ExtractProfile} from '../model/extract-profile';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@aspnet/signalr';
import {EmrConfigService} from '../../../settings/services/emr-config.service';
import {ExtractEvent} from '../../../settings/model/extract-event';
import {SendEvent} from '../../../settings/model/send-event';
import {LoadExtracts} from '../../../settings/model/load-extracts';
import {CombinedPackage} from '../../../settings/model/combined-package';
import {CbsService} from '../../services/cbs.service';
import {environment} from '../../../environments/environment';
import {ManifestResponse} from "../../models/manifest-response";
import {EmrSetup} from "../../../settings/model/emr-setup";
import {el} from "@angular/platform-browser/testing/src/browser_util";

@Component({
    selector: 'liveapp-ndwh-console',
    templateUrl: './ndwh-console.component.html',
    styleUrls: ['./ndwh-console.component.scss']
})
export class NdwhConsoleComponent implements OnInit, OnChanges, OnDestroy {
    @Input() emr: EmrSystem;
    @Input() emrVer: string;
    private _hubConnection: HubConnection | undefined;
    private _hubConnectionMpi: HubConnection | undefined;
    public async: any;

    public emrName: string;
    public emrVersion: string;
    public minEMRVersion: string;

    private _confirmationService: ConfirmationService;
    private _ndwhExtractService: NdwhExtractService;
    private _registryConfigService: RegistryConfigService;
    private _ndwhSenderService: NdwhSenderService;

    public load$: Subscription;
    public loadMet$: Subscription;
    public loadRegistry$: Subscription;
    public send$: Subscription;
    public getStatus$: Subscription;
    public sendManifest$: Subscription;

    public loadingData: boolean;
    public extracts: Extract[] = [];
    public cbsExtracts: Extract[] = [];
    public currentExtract: Extract;
    public currentCbsExtract: Extract;
    private extractEvent: ExtractEvent;
    public sendEvent: SendEvent = {};
    public recordCount: number;

    public canLoadFromEmr: boolean;
    public canSend: boolean;
    public canSendDiff: boolean = null;
    // public canSendDiff: string;
    // public canSendAll: string;


    public canSendMpi: boolean;
    public canSendPatients: boolean = false;
    public manifestPackage: CombinedPackage;
    public extractPackage: CombinedPackage;
    public dwhManifestPackage: SendPackage = null;
    public dwhExtractPackage: SendPackage = null;
    public cbsManifestPackage: SendPackage = null;
    public cbsExtractPackage: SendPackage = null;
    public sending: boolean = false;
    public sendingManifest: boolean = false;
    public changesLoaded: boolean = false;



    public errorMessage: Message[];
    public otherMessage: Message[];
    public notifications: Message[];
    public warningMessage: Message[];
    private _extractDbProtocol: ExtractDatabaseProtocol;
    private _extractDbProtocols: ExtractDatabaseProtocol[];
    private extractLoadCommand: LoadFromEmrCommand;
    private loadExtractsCommand: LoadExtracts;
    private extractPatient: ExtractProfile;
    private extractPatientArt: ExtractProfile;
    private extractPatientBaseline: ExtractProfile;
    private extractPatientLaboratory: ExtractProfile;
    private extractPatientPharmacy: ExtractProfile;
    private extractPatientStatus: ExtractProfile;
    private extractPatientVisit: ExtractProfile;
    private extractPatientAdverseEvent: ExtractProfile;
    private extractMpi: ExtractProfile;
    private extractProfiles: ExtractProfile[] = [];
    public centralRegistry: CentralRegistry;
    public cbsRegistry: CentralRegistry;
    public sendResponse: SendResponse;
    public getEmr$: Subscription;
    public loadMpi: boolean = false;
    public sendMpi: boolean = false;
    public loading = false;
    extractSent = [];
    isLoadingMet = false;
    manifestResponse:ManifestResponse;
    smartMode = false;
    hideMe = true;
    startedSending=false;

    public constructor(
        confirmationService: ConfirmationService,
        emrConfigService: NdwhExtractService,
        registryConfigService: RegistryConfigService,
        psmartSenderService: NdwhSenderService,
        private emrService: EmrConfigService,
        private cbsService: CbsService
    ) {
        this._confirmationService = confirmationService;
        this._ndwhExtractService = emrConfigService;
        this._registryConfigService = registryConfigService;
        this._ndwhSenderService = psmartSenderService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.loadData();
        this.emrVersion = this.emrVer;
    }

    public ngOnInit() {
        this.loadRegisrty();
        this.liveOnInit();
        this.loadData();
        this.checkWhichToSend();
    }

    public loadData(): void {
        this.loadingData = true;
        this.canLoadFromEmr = this.canSend = false;

        if (this.emr) {
            this.canLoadFromEmr = true;
            this.loadingData = true;
            this.recordCount = 0;
            this.extracts = this.emr.extracts.filter(
                x => x.docketId === 'NDWH'
            );
            this.cbsExtracts = this.emr.extracts.filter(
                x => x.docketId === 'CBS'
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
        this.loadingData = false;
    }

    public loadFromEmr(loadChangesOnly): void {
        this.changesLoaded = loadChangesOnly;
        // sessionStorage.setItem("canSendDiff",loadChangesOnly.toString());
        // this.canSendDiff = (sessionStorage.getItem("canSendDiff")) =="true";

        this.canSend = this.canLoadFromEmr = false;
        localStorage.clear();
        this.errorMessage = [];
        this.notifications = [];
        this.load$ = this._ndwhExtractService
            .extractAll(this.generateExtractsLoadCommand(this.emr,loadChangesOnly))
            .subscribe(
                p => {
                },
                e => {
                    this.canSend = this.canLoadFromEmr = true;
                    this.errorMessage = [];
                    this.errorMessage.push({
                        severity: 'error',
                        summary: 'Error loading from EMR',
                        detail: <any>e
                    });
                    this.notifications = [];
                    this.notifications.push({
                        severity: 'error',
                        summary: 'Error loading from EMR',
                        detail: <any>e
                    });

                },
                () => {
                    this.canSend = this.canLoadFromEmr = true;
                    this.errorMessage.push({
                        severity: 'success',
                        summary: 'load was successful '
                    });

                    this.notifications.push({
                        severity: 'success',
                        summary: 'load was successful '
                    });

                    this.checkWhichToSend();

                    this.updateEvent();
                    this.loadMet();
                }
            );


    }

    public loadRegisrty(): void {
        this.errorMessage = [];
        this.loadRegistry$ = this._registryConfigService.get('NDWH').subscribe(
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

    public loadCbsRegisrty(): void {
        this.errorMessage = [];
        this.loadRegistry$ = this._registryConfigService.get('CBS').subscribe(
            p => {
                this.cbsRegistry = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error loading CBS regisrty ',
                    detail: <any>e
                });
            },
            () => {
            }
        );
    }

    public updateEvent(): void {
        this.extracts.forEach(extract => {
            this.getStatus$ = this._ndwhExtractService
                .getStatus(extract.id)
                .subscribe(
                    p => {
                        extract.extractEvent = p;
                        if (extract.extractEvent && extract.name === 'PatientExtract') {
                            this.canSend = (extract.extractEvent.queued > 0);
                            if (this.startedSending) {
                                this.canSend = false;
                            }
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

    public loadMet(): void {
        this.isLoadingMet=true;
        this.loadMet$ = this._ndwhExtractService
            .loadMet()
            .subscribe(
                p => {

                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({
                        severity: 'error',
                        summary: 'Error loading stats ',
                        detail: <any>e
                    });
                    this.isLoadingMet=false;
                    if(this.canSend) {
                        this.canSend = true;
                    }
                },
                () => {
                    this.isLoadingMet=false;
                    if(this.canSend) {
                        this.canSend = true;
                    }
                }
            );

    }

    public send(): void {
        this.startedSending=true;
        localStorage.clear();
        this.sendingManifest = true;
        this.errorMessage = [];
        this.notifications = [];
        this.canSendPatients = false;
        this.manifestPackage = this.getSendManifestPackage();
        this.sendManifest$ = this._ndwhSenderService.sendManifest(this.manifestPackage)
            .subscribe(
                p => {
                    this.canSendPatients = true;
                    this.manifestResponse = p;
                },
                e => {
                    this.startedSending=false;
                    console.error('SEND ERROR', e);
                    if (e && e.ProgressEvent) {

                    } else {
                        this.errorMessage = [];
                        this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                    }
                },
                () => {
                    this.startedSending=false;
                    this.notifications.push({severity: 'success', summary: 'Manifest sent'});
                    this.sendExtracts();
                    this.sendingManifest = false;
                    this.updateEvent();
                }
            );
    }

    public checkWhichToSend(): boolean {

        this.sendManifest$ = this._ndwhSenderService.checkWhichToSend()
            .subscribe(
                p => {
                    console.log('value here is',p)
                    if (p=="SendAll"){
                        this.canSendDiff = false;
                    }else if(p=="SendChanges"){
                        this.canSendDiff = true;
                    }
                    console.log('value send',this.canSendDiff)

                },
                e => {

                },
                () => {
                }
            );
        return this.canSendDiff;
    }


    public sendSmart(): void {
        this.startedSending=true;
        this.canSend=false;
        if(this.emr.emrSetup==EmrSetup.MultiFacility) {
            this.smartMode = false;
            this.send();
            return;
        }
        this.smartMode=true;
        localStorage.clear();
        this.sendingManifest = true;
        this.errorMessage = [];
        this.notifications = [];
        this.canSendPatients = false;
        this.manifestPackage = this.getSendManifestPackage();
        this.sendManifest$ = this._ndwhSenderService.sendSmartManifest(this.manifestPackage)
            .subscribe(
                p => {
                    this.canSendPatients = true;
                    this.manifestResponse = p;
                },
                e => {
                    this.canSend=true;

                    if (e && e.ProgressEvent) {

                    } else {
                        this.errorMessage = [];
                        this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                        this.notifications = [];
                        this.notifications.push({severity: 'error',summary: 'Error loading from EMR',detail: <any>e
                        });
                    }
                    this.startedSending=false;
                },
                () => {
                    this.notifications.push({severity: 'success', summary: 'Manifest sent'});
                    this.sendSmartExtracts();
                    this.sendingManifest = false;
                    this.updateEvent();
                    this.canSend=true;
                    this.startedSending=false;
                }
            );
    }

    public sendDiff(): void {
        this.startedSending=true;
        this.canSend=false;
        localStorage.clear();
        this.sendingManifest = true;
        this.errorMessage = [];
        this.notifications = [];
        this.canSendPatients = false;
        this.manifestPackage = this.getSendManifestPackage();
        this.sendManifest$ = this._ndwhSenderService.sendDiffManifest(this.manifestPackage)
            .subscribe(
                p => {
                    this.canSendPatients = true;
                },
                e => {
                    this.startedSending=false;
                    this.canSend=true;
                    console.error('SEND ERROR', e);
                    if (e && e.ProgressEvent) {

                    } else {
                        this.errorMessage = [];
                        this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                    }
                },
                () => {
                    this.notifications.push({severity: 'success', summary: 'Manifest sent'});
                    this.sendDiffExtracts();
                    this.sendingManifest = false;
                    this.updateEvent();
                    this.canSend=true;
                    this.startedSending=false;
                }
            );
    }

    public sendExtracts(): void {
        this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        this.extractPackage = this.getExtractsPackage();
        if(this.manifestResponse) {
            this.extractPackage.jobId = this.manifestResponse.jobId;
        }
        this.send$ = this._ndwhSenderService.sendPatientExtracts(this.extractPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                },
                e => {
                    this.notifications = [];
                    console.error('SEND ERROR', e);
                    if (e && e.ProgressEvent) {

                    } else {
                        this.errorMessage = [];
                        this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                    }
                },
                () => {
                    this.notifications = [];
                    this.errorMessage.push({severity: 'success', summary: 'Sending Extracts Completed '});
                    this.updateEvent();
                }
            );
    }

    public sendSmartExtracts(): void {
        this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        this.extractPackage = this.getExtractsPackage();
        if(this.manifestResponse) {
            this.extractPackage.jobId = this.manifestResponse.jobId;
        }
        this.send$ = this._ndwhSenderService.sendSmartPatientExtracts(this.extractPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                },
                e => {
                    this.notifications = [];
                    console.error('SEND ERROR', e);
                    if (e && e.ProgressEvent) {

                    } else {
                        this.errorMessage = [];
                        this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                    }
                },
                () => {
                    this.notifications = [];
                    this.errorMessage.push({severity: 'success', summary: 'Sending Extracts Completed '});
                    this.updateEvent();
                }
            );
    }

    public sendDiffExtracts(): void {
        this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        this.extractPackage = this.getExtractsPackage();
        this.send$ = this._ndwhSenderService.sendDiffPatientExtracts(this.extractPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                },
                e => {
                    console.error('SEND ERROR', e);
                    if (e && e.ProgressEvent) {

                    } else {
                        this.errorMessage = [];
                        this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                    }
                },
                () => {
                    this.errorMessage.push({severity: 'success', summary: 'Sending Extracts '});
                    this.updateEvent();
                }
            );
    }

    private getSendManifestPackage(): CombinedPackage {
        return this.manifestPackage = this.manifestPackage = {
            dwhPackage: this.getSendDwhManifestPackage(),
            mpiPackage: null,
            sendMpi: false
        };
    }

    private getExtractsPackage(): CombinedPackage {
        this.getMpiPackage();
        this.getDwhExtractsPackage();
        if (this.cbsExtractPackage !== null) {
            return this.extractPackage = {
                dwhPackage: this.dwhExtractPackage,
                mpiPackage: this.cbsExtractPackage,
                sendMpi: this.sendMpi
            };
        }
        return this.extractPackage = this.extractPackage = {
            dwhPackage: this.dwhExtractPackage,
            mpiPackage: null,
            sendMpi: false
        };
    }

    private getSendDwhManifestPackage(): SendPackage {
        const dwhMan = this.extracts.find(x => x.name === 'PatientExtract');
        if (dwhMan !== null) {
            return this.dwhManifestPackage = {
                destination: this.centralRegistry,
                extractId: dwhMan.id,
                emrSetup: this.emr.emrSetup,
                emrId: this.emr.id,
                emrName: this.emr.name,
                extracts: this.extracts
            };
        }
        return this.dwhManifestPackage;
    }

    private getDwhExtractsPackage(): SendPackage {
        const dwhExt = this.extracts.find(x => x.name === 'PatientExtract');
        if (dwhExt !== null) {
            return this.dwhExtractPackage = {
                destination: this.centralRegistry,
                extractId: dwhExt.id
            };
        }
        return this.dwhExtractPackage;
    }

    private getMpiSendManifestPackage(): SendPackage {
        const mpiMan = this.cbsExtracts.find(x => x.name === 'MasterPatientIndex');
        if (mpiMan && mpiMan !== null) {
            return this.cbsManifestPackage = {
                extractId: mpiMan.id,
                destination: this.cbsRegistry
            };
        }
        return this.cbsManifestPackage;
    }

    private getMpiPackage(): SendPackage {
        const mpiExt = this.cbsExtracts.find(x => x.name === 'MasterPatientIndex');
        if (mpiExt && mpiExt !== null) {
            return this.cbsExtractPackage = {
                destination: this.cbsRegistry,
                extractId: mpiExt.id,
            };
        }
        return this.cbsExtractPackage;
    }


    private getCurrrentProgress(extract: string, progress: string) {
        let overallProgress = 0;
        const ecount = this.extracts.length;
        const keys = this.extracts.map(x => `CT-${x.name}`);
        const key = `CT-${extract}`;
        localStorage.setItem(key, progress);
        keys.forEach(k => {
            const data = localStorage.getItem(k);
            if (data) {
                overallProgress = overallProgress + (+data);
            }
        });
        if (this.smartMode) {
            return overallProgress / ecount;
        }
        return overallProgress;
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
                `${window.location.protocol}//${document.location.hostname}:${environment.port}/ExtractActivity`
            )
            .configureLogging(LogLevel.Error)
            .build();
        this._hubConnection.serverTimeoutInMilliseconds = 120000;

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('ShowProgress', (extractActivityNotification: any) => {
            this.currentExtract = this.extracts.find(
                x => x.id === extractActivityNotification.extractId
            );
            if (this.currentExtract) {
                this.extractEvent = {
                    lastStatus: `${extractActivityNotification.progress.status}`,
                    found: extractActivityNotification.progress.found,
                    loaded: extractActivityNotification.progress.loaded,
                    rejected: extractActivityNotification.progress.rejected,
                    queued: extractActivityNotification.progress.queued,
                    sent: extractActivityNotification.progress.sent
                };
                this.currentExtract.extractEvent = {};
                this.currentExtract.extractEvent = this.extractEvent;
                const newWithoutPatientExtract = this.extracts.filter(
                    x => x.id !== extractActivityNotification.extractId
                );
                this.extracts = [
                    ...newWithoutPatientExtract,
                    this.currentExtract
                ];
            }
        });

        this._hubConnection.on('ShowDwhSendProgress', (dwhProgress: any) => {
            const progress = this.getCurrrentProgress(dwhProgress.extract, dwhProgress.progress);
            this.sendEvent = {
                sentProgress: progress
            };
            this.updateExractStats(dwhProgress);
            if (progress !== 100) {
                this.sending = true;
            } else {
                this.sending = false;
                this.updateEvent();
            }
            this.canLoadFromEmr = this.canSend = !this.sending;
        });

        this._hubConnection.on('ShowDwhSendMessage', (message: any) => {
            if (message === 'Sending started...') {
                localStorage.clear();
            }
            if (message.error) {
                this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: message.message});
            } else {
                this.errorMessage.push({severity: 'success', summary: message.message});
            }
        });
    }


    private liveOnInitMpi() {
        this._hubConnectionMpi = new HubConnectionBuilder()
            .withUrl(`${window.location.protocol}//${document.location.hostname}:${environment.port}/cbsactivity`)
            .configureLogging(LogLevel.Error)
            .build();
        this._hubConnectionMpi.serverTimeoutInMilliseconds = 120000;

        this._hubConnectionMpi.start().catch(err => console.error(err.toString()));

        this._hubConnectionMpi.on('ShowCbsProgress', (dwhProgress: any) => {

            if (this.currentCbsExtract) {
                this.extractEvent = {
                    lastStatus: `${dwhProgress.status}`, found: dwhProgress.found, loaded: dwhProgress.loaded,
                    rejected: dwhProgress.rejected, queued: dwhProgress.queued, sent: dwhProgress.sent
                };
                this.currentCbsExtract.extractEvent = {};
                this.currentCbsExtract.extractEvent = this.extractEvent;
                const newWithoutPatientExtract = this.extracts.filter(x => x.name !== 'MasterPatientIndex');
                this.extracts = [...newWithoutPatientExtract, this.currentCbsExtract];
            }
        });

        this._hubConnectionMpi.on('ShowCbsSendProgress', (dwhProgress: any) => {
            if (this.currentCbsExtract) {
                this.sendEvent = {
                    sentProgress: dwhProgress.progress
                };
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

    private generateExtractLoadCommand(currentEmr: EmrSystem): LoadExtracts {
        this.extractProfiles.push(this.generateExtractPatient(currentEmr));
        this.extractProfiles.push(this.generateExtractPatientArt(currentEmr));
        this.extractProfiles.push(this.generateExtractPatientBaseline(currentEmr));
        this.extractProfiles.push(this.generateExtractPatientLaboratory(currentEmr));
        this.extractProfiles.push(this.generateExtractPatientPharmacy(currentEmr));
        this.extractProfiles.push(this.generateExtractPatientStatus(currentEmr));
        this.extractProfiles.push(this.generateExtractPatientVisit(currentEmr));
        this.extractProfiles.push(this.generateExtractPatientAdverseEvent(currentEmr));

        this.extractLoadCommand = {
            extracts: this.extractProfiles
        };

        this.loadExtractsCommand = {
            loadFromEmrCommand: this.extractLoadCommand,
            extractMpi: null,
            loadMpi: false
        };

        return this.loadExtractsCommand;
    }

    private generateExtractsLoadCommand(currentEmr: EmrSystem,load: boolean): LoadExtracts {

        this.extractLoadCommand = {
            loadChangesOnly:load,
            extracts: this.generateExtractProfiles(currentEmr)
        };

        this.loadExtractsCommand = {
            loadFromEmrCommand: this.extractLoadCommand,
            extractMpi: null,
            loadMpi: false,
            emrSetup: this.emr.emrSetup
        };

        return this.loadExtractsCommand;
    }

    private generateExtractPatient(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientExtract').databaseProtocolId;
        this.extractPatient = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientExtract')
        };
        return this.extractPatient;
    }

    private generateExtractPatientArt(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientArtExtract').databaseProtocolId;
        this.extractPatientArt = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientArtExtract')
        };
        return this.extractPatientArt;
    }

    private generateExtractPatientBaseline(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientBaselineExtract').databaseProtocolId;
        this.extractPatientBaseline = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientBaselineExtract')
        };
        return this.extractPatientBaseline;
    }

    private generateExtractPatientLaboratory(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientLabExtract').databaseProtocolId;
        this.extractPatientLaboratory = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientLabExtract')
        };
        return this.extractPatientLaboratory;
    }

    private generateExtractPatientPharmacy(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientPharmacyExtract').databaseProtocolId;
        this.extractPatientPharmacy = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientPharmacyExtract')
        };
        return this.extractPatientPharmacy;
    }

    private generateExtractPatientStatus(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientStatusExtract').databaseProtocolId;
        this.extractPatientStatus = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientStatusExtract')
        };
        return this.extractPatientStatus;
    }

    private generateExtractPatientVisit(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientVisitExtract').databaseProtocolId;
        this.extractPatientVisit = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientVisitExtract')
        };
        return this.extractPatientVisit;
    }

    private generateExtractPatientAdverseEvent(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientAdverseEventExtract').databaseProtocolId;
        this.extractPatientAdverseEvent = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientAdverseEventExtract')
        };
        return this.extractPatientAdverseEvent;
    }

    private generateExtractProfiles(currentEmr: EmrSystem): ExtractProfile[] {
        const profiles: ExtractProfile[] = [];
        this.extracts.forEach(e => {
            const profile = {
                databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === e.databaseProtocolId)[0],
                extract: e
            };
            profiles.push(profile);
        });

        return profiles;
    }

    private generateExtractMpi(currentEmr: EmrSystem): ExtractProfile {
        const mpi = this.cbsExtracts.find(x => x.name === 'MasterPatientIndex');
        if (mpi != null) {
            const selectedProtocal = mpi.databaseProtocolId;
            this.extractMpi = {
                databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
                extract: this.cbsExtracts.find(x => x.name === 'MasterPatientIndex')
            };
        }
        return this.extractMpi;
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
        if (this.loadMet$) {
            this.loadMet$.unsubscribe();
        }
    }


}
