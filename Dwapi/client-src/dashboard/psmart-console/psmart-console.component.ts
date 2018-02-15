import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {Extract} from '../../settings/model/extract';
import {EmrSystem} from '../../settings/model/emr-system';
import {ConfirmationService, Message} from 'primeng/api';
import {PsmartExtractService} from '../services/psmart-extract.service';
import {Subscription} from 'rxjs/Subscription';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';
import {RegistryConfigService} from '../../settings/services/registry-config.service';
import {CentralRegistry} from '../../settings/model/central-registry';
import {PsmartSenderService} from '../services/psmart-sender.service';
import {SendPackage} from '../../settings/model/send-package';

@Component({
  selector: 'liveapp-psmart-console',
  templateUrl: './psmart-console.component.html',
  styleUrls: ['./psmart-console.component.scss']
})
export class PsmartConsoleComponent implements OnInit, OnChanges, OnDestroy {

    @Input() emr: EmrSystem;

    public emrName: string;
    public emrVersion: string;

    private _confirmationService: ConfirmationService;
    private _psmartExtractService: PsmartExtractService;
    private _registryConfigService: RegistryConfigService;
    private _psmartSenderService: PsmartSenderService;

    public load$: Subscription;
    public loadRegistry$: Subscription;
    public send$: Subscription;
    public getStatus$: Subscription;

    public loadingData: boolean;
    public extracts: Extract[];
    public recordCount: number;

    public canLoadFromEmr: boolean;
    public canSend: boolean;

    public errorMessage: Message[];
    public otherMessage: Message[];
    private _extractDbProtocol: ExtractDatabaseProtocol;
    private _extractDbProtocols: ExtractDatabaseProtocol[];
    public centralRegistry: CentralRegistry;

    public constructor(confirmationService: ConfirmationService, emrConfigService: PsmartExtractService,
                       registryConfigService: RegistryConfigService,
                       psmartSenderService: PsmartSenderService) {
        this._confirmationService = confirmationService;
        this._psmartExtractService = emrConfigService;
        this._registryConfigService = registryConfigService;
        this._psmartSenderService = psmartSenderService;
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
            this.extracts = this.emr.extracts.filter(x => x.docketId === 'PSMART');
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
        this.load$ = this._psmartExtractService.load(this.getExtractProtocols(this.emr))
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
            this.getStatus$ = this._psmartExtractService.getStatus(extract.id)
                .subscribe(
                    p => {
                        extract.extractEvent = p;
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
        this.send$ = this._psmartSenderService.send(this.getSendPackage('PSMART'))
            .subscribe(
                p => {
                    // this.isVerfied = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error sending ', detail: <any>e});
                },
                () => {
                    this.errorMessage.push({severity: 'success', summary: 'sent successful '});
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
        })
        return this._extractDbProtocols;
    }

    private getSendPackage(docketId: string): SendPackage {
        return {
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
