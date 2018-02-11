import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChange} from '@angular/core';
import {Extract} from '../../settings/model/extract';
import {EmrSystem} from '../../settings/model/emr-system';
import {ConfirmationService, Message} from 'primeng/api';
import {PsmartExtractService} from '../services/psmart-extract.service';
import {Subscription} from 'rxjs/Subscription';
import {ExtractDatabaseProtocol} from '../../settings/model/extract-protocol';

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

    public load$: Subscription;

    public loadingData: boolean;
    public extracts: Extract[];
    public recordCount: number;

    public canLoadFromEmr: boolean;
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
        this.canLoadFromEmr = this.canSend = false;
        if (this.emr) {
            this.canLoadFromEmr = this.canSend = true;
            this.loadingData = true;
            this.recordCount = 0;
            this.extracts = this.emr.extracts.filter(x => x.docketId === 'PSMART');
            this.emrName = this.emr.name;
            this.emrVersion = `(Ver. ${this.emr.version})`;
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
        if (this.load$) {
            this.load$.unsubscribe();
        }
    }
}
