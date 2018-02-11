import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {Extract} from '../../settings/model/extract';
import {EmrSystem} from '../../settings/model/emr-system';
import {ConfirmationService, Message} from 'primeng/api';
import {PsmartExtractService} from '../services/psmart-extract.service';
import {Subscription} from 'rxjs/Subscription';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';

@Component({
  selector: 'liveapp-psmart-middleware-console',
  templateUrl: './psmart-middleware-console.component.html',
  styleUrls: ['./psmart-middleware-console.component.scss']
})
export class PsmartMiddlewareConsoleComponent implements OnInit, OnChanges, OnDestroy {

    @Input() middleware: EmrSystem;

    private _confirmationService: ConfirmationService;
    private _psmartExtractService: PsmartExtractService;

    public emrName: string;
    public emrVersion: string;

    public loadMiddleware$: Subscription;

    public loadingData: boolean;
    public extracts: Extract[];
    public recordCount: number;

    public canLoadFromMiddleware: boolean;
    public canSend: boolean;

    public errorMessage: Message[];
    public otherMessage: Message[];
    private _extractDbProtocols: ExtractDatabaseProtocol[];

    public constructor(confirmationService: ConfirmationService, emrConfigService: PsmartExtractService) {
        this._confirmationService = confirmationService;
        this._psmartExtractService = emrConfigService;
    }

    public ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.loadData();
    }

    public ngOnInit() {
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
    }

}
