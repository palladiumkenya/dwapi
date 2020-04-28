import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {Extract} from '../../../settings/model/extract';
import {EmrSystem} from '../../../settings/model/emr-system';
import {ConfirmationService, Message} from 'primeng/api';
import {PsmartExtractService} from '../../services/psmart-extract.service';
import {Subscription} from 'rxjs/Subscription';
import {ExtractDatabaseProtocol} from '../../../settings/model/extract-protocol';
import {PsmartSenderService} from '../../services/psmart-sender.service';
import {RegistryConfigService} from '../../../settings/services/registry-config.service';
import {CentralRegistry} from '../../../settings/model/central-registry';
import {SendPackage} from '../../../settings/model/send-package';

@Component({
  selector: 'liveapp-psmart-middleware-console',
  templateUrl: './psmart-middleware-console.component.html',
  styleUrls: ['./psmart-middleware-console.component.scss']
})
export class PsmartMiddlewareConsoleComponent implements OnInit, OnChanges, OnDestroy {

    @Input() middleware: EmrSystem;

    private _confirmationService: ConfirmationService;
    private _psmartExtractService: PsmartExtractService;
    private _registryConfigService: RegistryConfigService;
    private _psmartSenderService: PsmartSenderService;

    public emrName: string;
    public emrVersion: string;

    public loadMiddleware$: Subscription;
    public loadRegistry$: Subscription;
    public send$: Subscription;

    public loadingData: boolean;
    public extracts: Extract[];
    public recordCount: number;

    public canLoadFromMiddleware: boolean;
    public canSend: boolean;

    public errorMessage: Message[];
    public otherMessage: Message[];
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
                }
            );
    }

    public loadData(): void {
        this.canLoadFromMiddleware = this.canSend = false;
        if (this.middleware) {
            this.canLoadFromMiddleware =  this.canSend = true;
            this.loadingData = true;
            this.recordCount = 0;
            this.extracts = this.middleware.extracts.filter(x => x.docketId === 'PSMART');
            this.emrName = this.middleware.name;
            this.emrVersion = `(Ver. ${this.middleware.version})`;
        }
    }

    public loadFromMiddleware(): void {
        this.errorMessage = [];
        this.loadMiddleware$ = this._psmartExtractService.load(this.getExtractProtocols(this.middleware))
            .subscribe(
                p => {
                    // this.isVerfied = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error verifying ', detail: <any>e});
                },
                () => {
                    this.errorMessage.push({severity: 'success', summary: 'connection was successful '});
                }
            );
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

    private getSendPackage(docketId: string): SendPackage {
        return {
            destination: this.centralRegistry,
            docket: docketId,
            endpoint: ''
        };
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

    public ngOnDestroy(): void {
        if (this.loadMiddleware$) {
            this.loadMiddleware$.unsubscribe();
        }
        if (this.loadRegistry$) {
            this.loadRegistry$.unsubscribe();
        }
        if (this.send$) {
            this.send$.unsubscribe();
        }
    }

}
