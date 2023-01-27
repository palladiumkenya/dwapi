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
import { SendEvent } from '../../../settings/model/send-event';
import { ExportEvent } from '../../../settings/model/export-event';
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
import {el} from '@angular/platform-browser/testing/src/browser_util';

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
    public minEMRVersion: string;

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
    public exportEvent: ExportEvent = {};
    public sendEventPartners: SendEvent = {};
    public sendEventLinkage: SendEvent = {};
    public recordCount: number;

    public canLoadFromEmr: boolean;
    public canSend: boolean = true;
    public canExport: boolean;
    public canSendPatients: boolean = false;
    public manifestPackage: SendPackage;
    public patientPackage: SendPackage;
    public sending: boolean = false;
    public sendingManifest: boolean = false;
    public exporting: boolean = false;
    public exportingManifest: boolean = false;

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

    private extractClients: ExtractProfile;
    private extractClientTests: ExtractProfile;
    private extractClientsLinkage: ExtractProfile;
    private extractTestKits: ExtractProfile;
    private extractClientTracing: ExtractProfile;
    private extractPartnerTracing: ExtractProfile;
    private extractPartnerNotificationServices: ExtractProfile;
    private extractEligibilityScreening: ExtractProfile;

    private extractProfile: ExtractProfile;
    private extractProfiles: ExtractProfile[] = [];
    public centralRegistry: CentralRegistry;
    public sendResponse: SendResponse;
    public getEmr$: Subscription;

    public sendStage = 2;
    extractSent = [];

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
                x => x.docketId === 'HTS'
            );
            this.updateEvent();
            this.emrName = this.emr.name;
            this.emrVersion = `(Ver. ${this.emr.version})`;
            const em = environment.emrs.filter(x => x.name === this.emrName)[0];

            if (this.emrName == 'KenyaEMR') {
                this.minEMRVersion = `(This version of DWAPI works best with ${this.emrName} version ${em.version}) or higher`;
            } else if (this.emrName === 'IQCare') {
                this.minEMRVersion = `(This version of DWAPI works best with ${this.emrName} version ${em.version}) or higher`;
            } else {
                this.minEMRVersion = '';
            }
        }
        if (this.centralRegistry) {
            this.canSend = true;
            this.canExport = true;
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
    public export(): void {
        this.exportingManifest = true;

        this.errorMessage = [];
        this.notifications = [];
        this.canSendPatients = false;
        this.manifestPackage = this.getSendManifestPackage();
        this.sendManifest$ = this._htsSenderService.exportManifest(this.manifestPackage)
            .subscribe(
                p => {
                    this.canSendPatients = true;
                    this.exportingManifest = false;
                    this.updateEvent();
                    this.exportClientsExtract();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error exporting ', detail: <any>e });
                    this.canExport = true;
                },
                () => {
                    //  this.notifications.push({severity: 'success', summary: 'Manifest sent'});
                    this.notifications.push({ severity: 'success', summary: 'Manifest exported' });
                }
            );
    }

    public sendClientsExtract(): void {
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientsExtractPackage();
        this.send$ = this._htsSenderService.sendClientsExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendClientTestsExtracts();
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
    public exportClientsExtract(): void {
        this.exportEvent = { exportProgress: 0 };
        this.exporting = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientsExtractPackage();
        this.send$ = this._htsSenderService.exportClientExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.exportClientTestsExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error exporting client', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendClientTestsExtracts(): void {
        this.sendStage = 2;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientTestsExtractPackage();
        this.send$ = this._htsSenderService.sendClientTestsExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendTestKitsExtracts();
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
    public exportClientTestsExtracts(): void {
        this.sendStage = 2;
        //this.exportEvent = { exportProgress: 0 };
        this.exporting = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientTestsExtractPackage();
        this.send$ = this._htsSenderService.exportClientTestsExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.exportTestKitsExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error exporting client tests', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendTestKitsExtracts(): void {
        this.sendStage = 3;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getTestKitsExtractPackage();
        this.send$ = this._htsSenderService.sendTestKitsExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendClientTracingExtracts();
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
    public exportTestKitsExtracts(): void {
        this.sendStage = 3;
        // this.exportEvent = { exportProgress: 0 };
        this.exporting = true;
        this.errorMessage = [];
        this.patientPackage = this.getTestKitsExtractPackage();
        this.send$ = this._htsSenderService.exportTestKitsExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.exportClientTracingExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error exporting Client Tracing Extracts', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendClientTracingExtracts(): void {
        this.sendStage = 4;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientTracingExtractPackage();
        this.send$ = this._htsSenderService.sendClientTracingExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendPartnerTracingExtracts();
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

    public exportClientTracingExtracts(): void {
        this.sendStage = 4;
        //this.exportEvent = { exportProgress: 0 };
        this.exporting = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientTracingExtractPackage();
        this.send$ = this._htsSenderService.exportClientTracingExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.exportPartnerTracingExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error exporting client tracing', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendPartnerTracingExtracts(): void {
        this.sendStage = 5;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getPartnerTracingExtractPackage();
        this.send$ = this._htsSenderService.sendPartnerTracingExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendPartnerNotificationServicesExtracts();
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
    public exportPartnerTracingExtracts(): void {
        this.sendStage = 5;
        //this.exportEvent = { exportProgress: 0 };
        this.exporting = true;
        this.errorMessage = [];
        this.patientPackage = this.getPartnerTracingExtractPackage();
        this.send$ = this._htsSenderService.exportPartnerTracingExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.exportPartnerNotificationServicesExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error exporting partner tracing', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendPartnerNotificationServicesExtracts(): void {
        this.sendStage = 6;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getPartnerNotificationServicesExtractPackage();
        this.send$ = this._htsSenderService.sendPartnerNotificationServicesExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendClientLinkageExtracts();
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
    public exportPartnerNotificationServicesExtracts(): void {
        this.sendStage = 6;
        //this.exportEvent = { exportProgress: 0 };
        this.exporting = true;
        this.errorMessage = [];
        this.patientPackage = this.getPartnerNotificationServicesExtractPackage();
        this.send$ = this._htsSenderService.exportPartnerNotificationServicesExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.exportClientLinkageExtracts();
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

    public sendClientLinkageExtracts(): void {
        this.sendStage = 7;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientsLinkageExtractPackage();
        this.send$ = this._htsSenderService.sendClientsLinkageExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendHtsEligibilityExtracts();

                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending eligibility', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }
    public exportClientLinkageExtracts(): void {
        this.sendStage = 7;
        //this.exportEvent = { exportProgress: 0 };
        this.exporting = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientsLinkageExtractPackage();
        this.send$ = this._htsSenderService.exportClientsLinkageExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.exportHtsEligibilityExtracts();

                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error exporting client Linkage', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendHtsEligibilityExtracts(): void {
        this.sendStage = 8;
        this.sendEvent = { sentProgress: 0 };
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getHtsEligibilityExtractPackage();
        this.send$ = this._htsSenderService.sendHtsEligibilityExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending hts eligibility screening', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }
    public exportHtsEligibilityExtracts(): void {
        this.sendStage = 8;
        //this.exportEvent = { exportProgress: 0 };
        this.exporting = true;
        this.errorMessage = [];
        this.patientPackage = this.getHtsEligibilityExtractPackage();
        this.send$ = this._htsSenderService.exportHtsEligibilityExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({ severity: 'error', summary: 'Error sending hts eligibility screening', detail: <any>e });
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendHandshake(): void {
        this.manifestPackage = this.getSendManifestPackage();
        this.send$ = this._htsSenderService.sendHandshake(this.manifestPackage)
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

    public ZipFiles(): void {
        this.manifestPackage = this.getSendManifestPackage();
        this.send$ = this._htsSenderService.zipHtsFiles(this.manifestPackage)
            .subscribe(
                p => {

                    this.updateEvent();
                },
                e => {
                    this.errorMessage = [];

                },
                () => {

                }
            );
    }

    /*public sendClientExtract(): void {
        this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientExtractPackage();
        this.send$ = this._htsSenderService.sendClientExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendClientTestsExtracts();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending Clients', detail: <any>e});
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Clients successfully '});
                }
            );
    }

    public sendClientLinkageExtract(): void {
        this.sendStage = 3;
        this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientsLinkageExtractPackage();
        this.send$ = this._htsSenderService.sendClientLinkageExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    this.sendClientPartnerExtract();
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending Linkages', detail: <any>e});
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Linkages successfully '});

                }
            );
    }*/

    /*public sendClientPartnerExtract(): void {
        this.sendStage = 4;
        this.sendEvent = {sentProgress: 0};
        this.sending = true;
        this.errorMessage = [];
        this.patientPackage = this.getClientPartnerExtractPackage();
        this.send$ = this._htsSenderService.sendClientPartnerExtracts(this.patientPackage)
            .subscribe(
                p => {
                    // this.sendResponse = p;
                    this.updateEvent();
                    // this.sending = false;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending Partners ', detail: <any>e});
                },
                () => {
                    // this.errorMessage.push({severity: 'success', summary: 'sent Partners successfully '});


                }
            );
    }*/

    private getSendManifestPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsClient').id,
            emrSetup: this.emr.emrSetup,
            emrId: this.emr.id,
            emrName: this.emr.name
        };
    }

    private getClientExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsClient').id,
            extractName: 'HtsClient'
        };
    }

    /*private getClientLinkageExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsClientLinkage').id,
            extractName: 'HtsClientLinkage'
        };
    }

    private getClientPartnerExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsPartnerTracing').id,
            extractName: 'HtsPartnerTracing'
        };
    }*/

    private getClientsExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsClient').id,
            extractName: 'HtsClient'
        };
    }

    private getPartnerNotificationServicesExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsPartnerNotificationServices').id,
            extractName: 'HtsPartnerNotificationServices'
        };
    }

    private getPartnerTracingExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsPartnerTracing').id,
            extractName: 'HtsPartnerTracing'
        };
    }

    private getClientTracingExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsClientTracing').id,
            extractName: 'HtsClientTracing'
        };
    }

    private getTestKitsExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsTestKits').id,
            extractName: 'HtsTestKits'
        };
    }

    private getClientsLinkageExtractPackage(): SendPackage {
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsClientLinkage').id,
            extractName: 'HtsClientLinkage'
        };
    }

    private getClientTestsExtractPackage(): SendPackage {
        //console.log(this.extracts.find(x => x.name === 'HtsClientTests'));
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsClientTests').id,
            extractName: 'HtsClientTests'
        };
    }

    private getHtsEligibilityExtractPackage(): SendPackage {
        //console.log(this.extracts.find(x => x.name === 'HtsClientTests'));
        return {
            destination: this.centralRegistry,
            extractId: this.extracts.find(x => x.name === 'HtsEligibilityExtract').id,
            extractName: 'HtsEligibilityExtract'
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
                `${window.location.protocol}//${document.location.hostname}:${environment.port}/HtsActivity`
            )
            .configureLogging(LogLevel.Error)
            .build();

        this._hubConnection.start().catch(err => console.error(err.toString()));

        this._hubConnection.on('ShowHtsProgress', (extractActivityNotification: any) => {
            this.currentExtract = {};
            const otheNamer = extractActivityNotification.extract.replace('Extracts', '');
            this.currentExtract = this.extracts.find(
                x => x.name === otheNamer
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
                    x => x.name !== otheNamer
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
            this.updateExractStats(dwhProgress);
            this.canLoadFromEmr = this.canSend = !this.sending;
        });

        this._hubConnection.on('ShowHtsSendProgressDone', (extractName: string) => {
            this.extractSent.push(extractName);
            if (this.extractSent.length === 8) {
                this.errorMessage = [];
                this.errorMessage.push({severity: 'success', summary: 'sent successfully '});
                this.updateEvent();
                this.sendHandshake();
                this.sending = false;
            } else {
                this.updateEvent();
            }
        });

        this._hubConnection.on('ShowHtsExportProgress', (dwhProgress: any) => {
            this.exportEvent = {
                exportProgress: dwhProgress.progress
            };
            this.updateExractStats(dwhProgress);
            this.canLoadFromEmr = this.canExport = !this.exporting;
        });

        this._hubConnection.on('ShowHtsExportProgressDone', (extractName: string) => {
            this.extractSent.push(extractName);
            if (this.extractSent.length === 8) {
                this.errorMessage = [];
                this.errorMessage.push({ severity: 'success', summary: 'success exporting  ' });
                this.updateEvent();
                this.ZipFiles();
                this.exporting = false;
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

    private generateExtractLoadCommand(currentEmr: EmrSystem): LoadHtsExtracts {
        this.extractProfiles.push(this.generateExtractClients(currentEmr));
        this.extractProfiles.push(this.generateExtractClientTests(currentEmr));
        this.extractProfiles.push(this.generateExtractPartnerNotificationServices(currentEmr));
        this.extractProfiles.push(this.generateExtractTestKits(currentEmr));
        this.extractProfiles.push(this.generateExtractClientsLinkage(currentEmr));
        this.extractProfiles.push(this.generateExtractPartnerTracing(currentEmr));
        this.extractProfiles.push(this.generateExtractClientTracing(currentEmr));
        this.extractProfiles.push(this.generateExtractHtsEligibilityExtract(currentEmr));


        this.extractLoadCommand = {
            extracts: this.extractProfiles
        };

        this.loadExtractsCommand = {
            loadHtsFromEmrCommand: this.extractLoadCommand
        };
        return this.loadExtractsCommand;
    }

    private generateExtractClients(currentEmr: EmrSystem): ExtractProfile {
        //console.log(this.extracts);
        const selectedProtocal = this.extracts.find(x => x.name === 'HtsClient').databaseProtocolId;
        this.extractClients = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HtsClient')
        };
        return this.extractClients;
    }

    private generateExtractClientTests(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HtsClientTests').databaseProtocolId;
        this.extractClientTests = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HtsClientTests')
        };
        return this.extractClientTests;
    }

    private generateExtractPartnerNotificationServices(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HtsPartnerNotificationServices').databaseProtocolId;
        this.extractPartnerNotificationServices = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HtsPartnerNotificationServices')
        };
        return this.extractPartnerNotificationServices;
    }

    private generateExtractTestKits(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HtsTestKits').databaseProtocolId;
        this.extractTestKits = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HtsTestKits')
        };
        return this.extractTestKits;
    }

    private generateExtractClientsLinkage(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HtsClientLinkage').databaseProtocolId;
        this.extractClientsLinkage = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HtsClientLinkage')
        };
        return this.extractClientsLinkage;
    }

    private generateExtractPartnerTracing(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HtsPartnerTracing').databaseProtocolId;
        this.extractPartnerTracing = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HtsPartnerTracing')
        };
        return this.extractPartnerTracing;
    }

    private generateExtractClientTracing(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HtsClientTracing').databaseProtocolId;
        this.extractClientTracing = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HtsClientTracing')
        };
        return this.extractClientTracing;
    }
    private generateExtractHtsEligibilityExtract(currentEmr: EmrSystem): ExtractProfile {
        const selectedProtocal = this.extracts.find(x => x.name === 'HtsEligibilityExtract').databaseProtocolId;
        this.extractEligibilityScreening = {
            databaseProtocol: currentEmr.databaseProtocols.filter(x => x.id === selectedProtocal)[0],
            extract: this.extracts.find(x => x.name === 'HtsEligibilityExtract')
        };
        return this.extractEligibilityScreening;
    }


    /*private generateExtractClient(currentEmr: EmrSystem): ExtractProfile {
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
    }*/

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
