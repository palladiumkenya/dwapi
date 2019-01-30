import {
    Component,
    OnInit,
    OnChanges,
    OnDestroy,
    SimpleChange,
    Input
} from '@angular/core';
import { Extract } from '../../../settings/model/extract';
import { EmrSystem } from '../../../settings/model/emr-system';
import { ConfirmationService, Message } from 'primeng/api';
import { NdwhExtractService } from '../../services/ndwh-extract.service';
import { Subscription } from 'rxjs/Subscription';
import { ExtractDatabaseProtocol } from '../../../settings/model/extract-protocol';
import { RegistryConfigService } from '../../../settings/services/registry-config.service';
import { CentralRegistry } from '../../../settings/model/central-registry';
import { NdwhSenderService } from '../../services/ndwh-sender.service';
import { SendPackage } from '../../../settings/model/send-package';
import { SendResponse } from '../../../settings/model/send-response';
import { LoadFromEmrCommand } from '../../../settings/model/load-from-emr-command';
import { DwhExtract } from '../../../settings/model/dwh-extract';
import { ExtractPatient } from '../model/extract-patient';
import { ExtractProfile } from '../model/extract-profile';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import { EmrConfigService } from '../../../settings/services/emr-config.service';
import { ExtractEvent } from '../../../settings/model/extract-event';
import {SendEvent} from '../../../settings/model/send-event';
import { LoadExtracts } from '../../../settings/model/load-extracts';
import { CombinedPackage } from '../../../settings/model/combined-package';
import { CbsService } from '../../services/cbs.service';

@Component({
    selector: 'liveapp-ndwh-console',
    templateUrl: './ndwh-console.component.html',
    styleUrls: ['./ndwh-console.component.scss']
})
export class NdwhConsoleComponent implements OnInit, OnChanges, OnDestroy {
    @Input() emr: EmrSystem;
    private _hubConnection: HubConnection | undefined;
    private _hubConnectionMpi: HubConnection | undefined;
    private _sendhubConnection: HubConnection | undefined;
    public async: any;

    public emrName: string;
    public emrVersion: string;

    private _confirmationService: ConfirmationService;
    private _ndwhExtractService: NdwhExtractService;
    private _registryConfigService: RegistryConfigService;
    private _ndwhSenderService: NdwhSenderService;

    public load$: Subscription;
    public loadRegistry$: Subscription;
    public send$: Subscription;
    public getStatus$: Subscription;
    public sendManifest$: Subscription;

    public loadingData: boolean;
    public extracts: Extract[] = [];
    public cbsExtracts: Extract[] = [];
    public currentExtract: Extract;
    public mpiExtract: Extract;
    private dwhExtract: DwhExtract;
    private dwhExtracts: DwhExtract[] = [];
    private extractEvent: ExtractEvent;
    public sendEvent: SendEvent = {};
    public recordCount: number;

    public canLoadFromEmr: boolean;
    public canSend: boolean;
    public canSendMpi: boolean;
    public canSendPatients: boolean = false;
    public manifestPackage: CombinedPackage;
    public extractPackage: CombinedPackage;
    public dwhManifestPackage: SendPackage;
    public dwhExtractPackage: SendPackage;
    public cbsManifestPackage: SendPackage;
    public cbsExtractPackage: SendPackage;
    public sending: boolean = false;
    public sendingManifest: boolean = false;

    public errorMessage: Message[];
    public otherMessage: Message[];
    public notifications: Message[];
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
    private extractProfile: ExtractProfile;
    private extractProfiles: ExtractProfile[] = [];
    public centralRegistry: CentralRegistry;
    public cbsRegistry: CentralRegistry;
    public sendResponse: SendResponse;
    public getEmr$: Subscription;
    public loadMpi: boolean = true;
    public sendMpi: boolean = true;

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
    }

    public ngOnInit() {
        this.loadRegisrty();
        this.loadCbsRegisrty();
        this.liveOnInit();
        this.liveOnInitMpi();
        this.loadData();
    }

    public loadData(): void {
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
        }
        if (this.centralRegistry) {
            this.canSend = true;
        }
        this.cbsExtracts.forEach(extract => {
            this.getStatus$ = this.cbsService.getStatus(extract.id)
            .subscribe(
                p => {
                    extract.extractEvent = p;
                    if (extract) {
                        if (extract.extractEvent) {
                            this.canSendMpi = extract.extractEvent.queued > 0;
                        }
                    }
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error loading status ', detail: <any>e});
                },
                () => {
                }
            );
        });
        if (!this.loadMpi && !this.canSendMpi) {
            this.sendMpi = false;
        } else {
            this.sendMpi = true;
        }
    }

    public loadChange() {
        this.loadMpi = !this.loadMpi;
        if (!this.loadMpi || !this.canSendMpi) {
            this.sendMpi = false;
        } else {
            this.sendMpi = true;
        }
    }

    public loadFromEmr(): void {
        this.errorMessage = [];
        this.load$ = this._ndwhExtractService
            .extractAll(this.generateExtractLoadCommand(this.emr))
            .subscribe(
                p => {
                    // this.isVerfied = p;
                    // window.location.reload();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({
                        severity: 'error',
                        summary: 'Error loading from EMR',
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

        this.cbsExtracts.forEach(extract => {
            this.getStatus$ = this.cbsService.getStatus(extract.id)
            .subscribe(
                p => {
                    extract.extractEvent = p;
                    if (extract) {
                        if (extract.extractEvent) {
                            this.canSendMpi = extract.extractEvent.queued > 0;
                        }
                    }
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error loading status ', detail: <any>e});
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
        this.sendManifest$ = this._ndwhSenderService.sendManifest(this.manifestPackage)
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
                    this.sendExtracts();
                    this.sendingManifest = false;
                    this.updateEvent();
                }
            );
    }

    public sendExtracts(): void {
        this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        this.extractPackage = this.getExtractsPackage();
        this.send$ = this._ndwhSenderService.sendPatientExtracts(this.extractPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                },
                () => {
                    this.errorMessage.push({severity: 'success', summary: 'Sending Extracts '});
                    this.sending = false;
                    this.updateEvent();
                }
            );
    }

    private getSendManifestPackage(): CombinedPackage {
        this.manifestPackage = {dwhPackage: this.getSendDwhManifestPackage(),
            mpiPackage: this.getMpiSendManifestPackage(),
            sendMpi: this.sendMpi
        };
        return this.manifestPackage;
    }

    private getExtractsPackage(): CombinedPackage {
        this.extractPackage = {dwhPackage: this.getDwhExtractsPackage(),
            mpiPackage: this.getMpiPackage(),
            sendMpi: this.sendMpi
        };
        return this.extractPackage;
    }

    private getSendDwhManifestPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PatientExtract').id
        };
    }

    private getDwhExtractsPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'PatientExtract').id
        };
    }

    private getMpiSendManifestPackage(): SendPackage {
        return {
            extractId: this.cbsExtracts.find(x => x.name === 'MasterPatientIndex').id,
            destination: this.cbsRegistry
        };
    }

    private getMpiPackage(): SendPackage {
        return {
            destination: this.cbsRegistry,
            extractId: this.cbsExtracts.find(x => x.name === 'MasterPatientIndex').id,
        };
    }


    private liveOnInit() {
        this._hubConnection = new HubConnectionBuilder()
            .withUrl(
                `http://${document.location.hostname}:5757/ExtractActivity`
            )
            .configureLogging(LogLevel.Error)
            .build();

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
            // console.log(dwhProgress);

            this.sendEvent = {
                sentProgress: dwhProgress.progress
            };
            this.sending = true;
            this.canLoadFromEmr = this.canSend = ! this.sending;
        });
    }

    private liveOnInitMpi() {
        this._hubConnectionMpi = new HubConnectionBuilder()
            .withUrl(`http://${document.location.hostname}:5757/cbsactivity`)
            .configureLogging(LogLevel.Trace)
            .build();
        this._hubConnectionMpi.serverTimeoutInMilliseconds = 120000;

        this._hubConnectionMpi.start().catch(err => console.error(err.toString()));

        this._hubConnectionMpi.on('ShowCbsProgress', (dwhProgress: any) => {

            if (this.mpiExtract) {
                this.extractEvent = {
                    lastStatus: `${dwhProgress.status}`, found: dwhProgress.found, loaded: dwhProgress.loaded,
                    rejected: dwhProgress.rejected, queued: dwhProgress.queued, sent: dwhProgress.sent
                };
                this.mpiExtract.extractEvent = {};
                this.mpiExtract.extractEvent = this.extractEvent;
                const newWithoutPatientExtract = this.extracts.filter(x => x.name !== 'MasterPatientIndex');
                this.extracts = [...newWithoutPatientExtract, this.mpiExtract];
            }
         });

        this._hubConnectionMpi.on('ShowCbsSendProgress', (dwhProgress: any) => {
            if (this.mpiExtract) {
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
        this.extractMpi = this.generateExtractMpi(currentEmr);
        if (this.extractMpi !== []) {
            this.loadExtractsCommand = {
                loadFromEmrCommand: this.extractLoadCommand,
                extractMpi: this.extractMpi,
                loadMpi: this.loadMpi
            };
        }
        this.loadExtractsCommand = {
            loadFromEmrCommand: this.extractLoadCommand,
            extractMpi: null,
            loadMpi: false
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
        this.extractPatientArt = {databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientArtExtract')
        };
        return this.extractPatientArt;
    }

    private generateExtractPatientBaseline(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientBaselineExtract').databaseProtocolId;
        this.extractPatientBaseline = {databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientBaselineExtract')
        };
        return this.extractPatientBaseline;
    }

    private generateExtractPatientLaboratory(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientLabExtract').databaseProtocolId;
        this.extractPatientLaboratory = {databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientLabExtract')
        };
        return this.extractPatientLaboratory;
    }

    private generateExtractPatientPharmacy(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientPharmacyExtract').databaseProtocolId;
        this.extractPatientPharmacy = {databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientPharmacyExtract')
        };
        return this.extractPatientPharmacy;
    }

    private generateExtractPatientStatus(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientStatusExtract').databaseProtocolId;
        this.extractPatientStatus = {databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientStatusExtract')
        };
        return this.extractPatientStatus;
    }

    private generateExtractPatientVisit(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientVisitExtract').databaseProtocolId;
        this.extractPatientVisit = {databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientVisitExtract')
        };
        return this.extractPatientVisit;
    }

    private generateExtractPatientAdverseEvent(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'PatientAdverseEventExtract').databaseProtocolId;
        this.extractPatientAdverseEvent = {databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'PatientAdverseEventExtract')
        };
        return this.extractPatientAdverseEvent;
    }

    private generateExtractMpi(currentEmr: EmrSystem): ExtractProfile {
        const mpi = this.cbsExtracts.find(x => x.name === 'MasterPatientIndex');
        if (mpi != null) {
            const selectedProtocal = mpi.databaseProtocolId;
            this.extractMpi = {databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
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
    }
}
