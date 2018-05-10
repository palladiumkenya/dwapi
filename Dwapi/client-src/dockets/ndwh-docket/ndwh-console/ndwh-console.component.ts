import { Component, OnInit, OnChanges, OnDestroy, SimpleChange, Input } from '@angular/core';
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

@Component({
  selector: 'liveapp-ndwh-console',
  templateUrl: './ndwh-console.component.html',
  styleUrls: ['./ndwh-console.component.scss']
})
export class NdwhConsoleComponent implements OnInit, OnChanges, OnDestroy {

  @Input() emr: EmrSystem;

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

    public loadingData: boolean;
    public extracts: Extract[] = [];
    public recordCount: number;

    public canLoadFromEmr: boolean;
    public canSend: boolean;

    public errorMessage: Message[];
    public otherMessage: Message[];
    private _extractDbProtocol: ExtractDatabaseProtocol;
    private _extractDbProtocols: ExtractDatabaseProtocol[];
    public centralRegistry: CentralRegistry;
    public sendResponse: SendResponse;

    public constructor(confirmationService: ConfirmationService, emrConfigService: NdwhExtractService,
                       registryConfigService: RegistryConfigService,
                       psmartSenderService: NdwhSenderService) {
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
    }

    public loadData(): void {
        this.canLoadFromEmr = this.canSend = false;

        if (this.emr) {
            this.canLoadFromEmr = true;
            this.loadingData = true;
            this.recordCount = 0;
            this.extracts = this.emr.extracts.filter(x => x.docketId === 'NDWH');
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
        console.log(this.emr);
        this.load$ = this._ndwhExtractService.load(this.getExtractProtocols(this.emr))
            .subscribe(
                p => {
                    // this.isVerfied = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error verifying ', detail: <any>e});
                },
                () => {
                    this.errorMessage.push({severity: 'success', summary: 'load was successful '});
                    this.updateEvent();
                }
            );
    }

    public loadRegisrty(): void {
        this.errorMessage = [];
        this.loadRegistry$ = this._registryConfigService.getDefault()
            .subscribe(
                p => {
                    this.centralRegistry = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error loading regisrty ', detail: <any>e});
                },
                () => {
                    console.log(this.centralRegistry.name);
                }
            );
    }

    public updateEvent(): void {

        this.extracts.forEach((extract) => {
            this.getStatus$ = this._ndwhExtractService.getStatus(extract.id)
                .subscribe(
                    p => {
                        extract.extractEvent = p;
                        if (extract.extractEvent) {
                            this.canSend = extract.extractEvent.queued > 0;
                        }
                    },
                    e => {
                        this.errorMessage = [];
                        this.errorMessage.push({severity: 'error', summary: 'Error loading status ', detail: <any>e});
                    },
                    () => {
                        // console.log(extract);
                    }
                );
        });


    }



    public send(): void {
        this.errorMessage = [];
        this.send$ = this._ndwhSenderService.send(this.getSendPackage('PSMART'))
            .subscribe(
                p => {
                    this.sendResponse = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                },
                () => {
                    console.log(this.sendResponse);
                    this.updateEvent();
                    if (this.sendResponse) {
                        if (this.sendResponse.isSending) {
                            this.errorMessage.push({severity: 'warning', summary: 'sending has already started '});
                        }
                        if (this.sendResponse.isComplete) {
                            this.errorMessage.push({severity: 'success', summary: 'sent successfully '});
                        }
                    }
                    // this.errorMessage.push({severity: 'success', summary: 'sent successful '});
                }
            );
    }

    private getExtractProtocols(currentEmr: EmrSystem): ExtractDatabaseProtocol[] {
        this._extractDbProtocols = [];
        this.extracts.forEach((e) => {
            e.emr = currentEmr.name;
            this._extractDbProtocols.push({
                    extract: e,
                    databaseProtocol: currentEmr.databaseProtocols[0]
                }
            );
        });
        return this._extractDbProtocols;
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
