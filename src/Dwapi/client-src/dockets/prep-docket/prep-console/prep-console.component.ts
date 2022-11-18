import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {EmrSystem} from '../../../settings/model/emr-system';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@aspnet/signalr';
import {ConfirmationService, Message} from 'primeng/api';
import {PrepService} from '../../services/prep.service';
import {RegistryConfigService} from '../../../settings/services/registry-config.service';
import {PrepSenderService} from '../../services/prep-sender.service';
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
import {LoadPrepExtracts} from '../../../settings/model/load-prep-extracts';
import {LoadPrepFromEmrCommand} from '../../../settings/model/load-prep-from-emr-command';
import {environment} from '../../../environments/environment';

@Component({
    selector: 'liveapp-prep-console',
    templateUrl: './prep-console.component.html',
    styleUrls: ['./prep-console.component.scss']
})
export class PrepConsoleComponent implements OnInit, OnDestroy, OnChanges {
    @Input() emr: EmrSystem;
    @Input() emrVer: string;
    private _hubConnection: HubConnection | undefined;
    private _sendhubConnection: HubConnection | undefined;
    public async: any;

    public emrName: string;
    public emrVersion: string;
    public minEMRVersion: string;

    private _confirmationService: ConfirmationService;
    private _prepService: PrepService;
    private _registryConfigService: RegistryConfigService;
    private _prepSenderService: PrepSenderService;

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
    private extractLoadCommand: LoadPrepFromEmrCommand;
    private loadExtractsCommand: LoadPrepExtracts;

    private extractProfiles: ExtractProfile[] = [];
    public centralRegistry: CentralRegistry;
    public sendResponse: SendResponse;
    public getEmr$: Subscription;

    public sendStage = 2;
    extractSent = [];

    public constructor(
        confirmationService: ConfirmationService,
        emrConfigService: PrepService,
        registryConfigService: RegistryConfigService,
        psmartSenderService: PrepSenderService,
        private emrService: EmrConfigService
    ) {
        this._confirmationService = confirmationService;
        this._prepService = emrConfigService;
        this._registryConfigService = registryConfigService;
        this._prepSenderService = psmartSenderService;
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
                x => x.docketId === 'PREP'
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
            localStorage.setItem('canSendPrep', "true");

        }
    }

    public loadFromEmr(): void {
        this.canLoadFromEmr=false;
        this.clearDocketStore();
        this.errorMessage = [];
        this.load$ = this._prepService
            .extractAll(this.generateExtractLoadCommand(this.emr))
            .subscribe(
                p => {
                    // this.isVerfied = p;
                },
                e => {
                    this.canLoadFromEmr=true;
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
                    this.canLoadFromEmr=true;
                    this.updateEvent();
                }
            );
    }

    public loadRegisrty(): void {
        this.errorMessage = [];
        this.loadRegistry$ = this._registryConfigService.get('PREP').subscribe(
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
            this.getStatus$ = this._prepService
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
        this.canSend=false;
        localStorage.setItem('canSendPrep', "false");

        localStorage.setItem('dwapi.prep.send', '0');
        this.sendEvent = {sentProgress: 0};
        this.sendingManifest = true;
        this.errorMessage = [];
        this.notifications = [];
        this.canSendPatients = false;
        const manifestPackage = this.getSendManifestPackage();
        this.sendManifest$ = this._prepSenderService.sendManifest(manifestPackage)
            .subscribe(
                p => {
                    this.canSendPatients = true;
                    this.sendingManifest = false;
                    this.updateEvent();
                    this.sendPatientPrepExtract();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                    this.canSend = true;
                    localStorage.setItem('canSendPrep', "true");

                },
                () => {
                    this.notifications.push({severity: 'success', summary: 'Manifest sent'});
                }
            );
    }

    public sendPatientPrepExtract(): void {
        //this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getPatientPrepExtractPackage();
        this.send$ = this._prepSenderService.sendPatientPrepExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendPrepAdverseEventExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending client', detail: <any>e});
                    this.canSend=true;
                    localStorage.setItem('canSendPrep', "true");

                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendPrepAdverseEventExtracts(): void {
        this.sendStage = 8;
        //this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getPrepAdverseEventExtractPackage();
        this.send$ = this._prepSenderService.sendPrepAdverseEventExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendPrepBehaviourRiskExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending partner notification service', detail: <any>e});
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }



    public sendPrepBehaviourRiskExtracts(): void {
        this.sendStage = 6;
        //this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getPrepBehaviourRiskExtractPackage();
        this.send$ = this._prepSenderService.sendPrepBehaviourRiskExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendCareTerminationExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending partner notification service', detail: <any>e});
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendCareTerminationExtracts(): void {
        this.sendStage = 10;
        //this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getCareTerminationExtractPackage();
        this.send$ = this._prepSenderService.sendPrepCareTerminationExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendPrepPharmacyExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending partner notification service', detail: <any>e});
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendPrepPharmacyExtracts(): void {
        this.sendStage = 7;
        //this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getPrepPharmacyExtractPackage();
        this.send$ = this._prepSenderService.sendPrepPharmacyExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendPrepLabExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending partner notification service', detail: <any>e});
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }



    public sendPrepLabExtracts(): void {
        this.sendStage = 9;
        //this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getPrepLabExtractPackage();
        this.send$ = this._prepSenderService.sendPrepLabExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendPrepVisitExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending partner notification service', detail: <any>e});
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }



    public sendPrepVisitExtracts(): void {
        this.sendStage = 11;
        //this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        const patientPackage = this.getPrepVisitExtractPackage();
        this.send$ = this._prepSenderService.sendPrepVisitExtracts(patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending client linkage', detail: <any>e});
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendHandshake(): void {
        this.manifestPackage = this.getSendManifestPackage();
        this.send$ = this._prepSenderService.sendHandshake(this.manifestPackage)
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
            extractId: this.extracts.find(x => x.name === 'PatientPrepExtract').id,
            emrSetup: this.emr.emrSetup,
            emrId: this.emr.id,
            emrName: this.emr.name
        };
    }

    private getPatientPrepExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PatientPrepExtract').id,
            extractName: 'PatientPrepExtract'
        };
    }

    private getPrepAdverseEventExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PrepAdverseEventExtract').id,
            extractName: 'PrepAdverseEventExtract'
        };
    }

    private getPrepBehaviourRiskExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PrepBehaviourRiskExtract').id,
            extractName: 'PrepBehaviourRiskExtract'
        };
    }

    private getCareTerminationExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PrepCareTerminationExtract').id,
            extractName: 'PrepCareTerminationExtract'
        };
    }



    private getPrepLabExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PrepLabExtract').id,
            extractName: 'PrepLabExtract'
        };
    }


    private getPrepPharmacyExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PrepPharmacyExtract').id,
            extractName: 'PrepPharmacyExtract'
        };
    }

    private getPrepVisitExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PrepVisitExtract').id,
            extractName: 'PrepVisitExtract'
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

    private clearDocketStore() {
        const search = 'PREP';
        Object.keys(localStorage)
            .filter((key) => key.startsWith(search))
            .map((key) => localStorage.removeItem(key));
    }

    private getCurrrentProgress(extract: string, progress: string) {
        let overallProgress = 0;
        const ecount = this.extracts.length;
        const keys = this.extracts.map(x => `PREP-${x.name}`);
        const key = `PREP-${extract}`;
        localStorage.setItem(key, this.ConvertStringToNumber(progress));
        keys.forEach(k => {
            const data = localStorage.getItem(k);
            if (data) {
                overallProgress = overallProgress + (+data);
            }
        });
        return overallProgress / ecount;
    }

    private liveOnInit() {
        this._hubConnection = new HubConnectionBuilder()
            .withUrl(
                `${window.location.protocol}//${document.location.hostname}:${environment.port}/PrepActivity`
            )
            .configureLogging(LogLevel.Information)
            .build();
        this._hubConnection.serverTimeoutInMilliseconds = 120000;

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('ShowPrepProgress', (extractActivityNotification: any) => {
            this.currentExtract = {};
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

        this._hubConnection.on('ShowPrepSendProgress', (dwhProgress: any) => {
            const progress = this.getCurrrentProgress(dwhProgress.extract, dwhProgress.progress);
            // console.log(`${dwhProgress.extract}:${dwhProgress.progress}, Overall:${progress}`);
            this.sendEvent = {
                sentProgress: progress
            };
            this.updateExractStats(dwhProgress);
            this.canLoadFromEmr = this.canSend = !this.sending;
        });

        this._hubConnection.on('ShowPrepSendProgressDone', (extractName: string) => {
            this.extractSent.push(extractName);
            if (this.extractSent.length === this.extracts.length) {
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

    private generateExtractLoadCommand(currentEmr: EmrSystem): LoadPrepExtracts {
        this.extractProfiles.push(this.generateExtractPatientPrep(currentEmr));
        this.extractProfiles.push(this.generateExtractPrepAdverseEvent(currentEmr));
        this.extractProfiles.push(this.generateExtractPrepBehaviourRisk(currentEmr));
        this.extractProfiles.push(this.generateExtractCareTermination(currentEmr));
        this.extractProfiles.push(this.generateExtractPrepLab(currentEmr));
        this.extractProfiles.push(this.generateExtractPrepPharmacy(currentEmr));
        this.extractProfiles.push(this.generateExtractPrepVisit(currentEmr));

        this.extractLoadCommand = {
            extracts: this.extractProfiles
        };

        this.loadExtractsCommand = {
            loadPrepFromEmrCommand: this.extractLoadCommand
        };
        return this.loadExtractsCommand;
    }

    private generateExtractPatientPrep(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientPrepExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientPrepExtract')
        };
    }

    private generateExtractPrepAdverseEvent(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PrepAdverseEventExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PrepAdverseEventExtract')
        };
    }

    private generateExtractPrepBehaviourRisk(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PrepBehaviourRiskExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PrepBehaviourRiskExtract')
        };
    }

    private generateExtractCareTermination(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PrepCareTerminationExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PrepCareTerminationExtract')
        };
    }

    private generateExtractPrepLab(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PrepLabExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PrepLabExtract')
        };
    }

    private generateExtractPrepPharmacy(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PrepPharmacyExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PrepPharmacyExtract')
        };
    }

    private generateExtractPrepVisit(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PrepVisitExtract').databaseProtocolId;
        return {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PrepVisitExtract')
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

    private ConvertStringToNumber(input: string) {
        let finnum = 0;

        if (!input) {
            finnum = 0;
        }

        if (input.length === 0) {
            finnum = 0;
        }
        const num = Number(input);
        if (num < 0) {
            finnum = 0;
        } else {
            finnum = num;
        }
        return `${finnum}`;
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
